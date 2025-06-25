using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Dal.newModels;
using BL;
using BL.Api;
using Dal;
using Dal.Api;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;


namespace CPC_PROJECT
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            try
            {
                // קריאת מחרוזת החיבור
                var connectionString = GetConnectionString(builder.Configuration);

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string not found.");
                }

                Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
                Console.WriteLine($"Connection string configured: {!string.IsNullOrEmpty(connectionString)}");

                // רישום DbContext - רק PostgreSQL
                builder.Services.AddDbContext<dbcontext>(options =>
                {
                    options.UseNpgsql(connectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.CommandTimeout(60);
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorCodesToAdd: null);
                    });

                    // Only enable detailed logging in development
                    if (builder.Environment.IsDevelopment())
                    {
                        options.EnableSensitiveDataLogging();
                        options.EnableDetailedErrors();
                    }
                }, ServiceLifetime.Scoped);

                // רישום Services
                builder.Services.AddScoped<IDal, DalManager>();
                builder.Services.AddScoped<IBL, BLManager>();

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // הוספת CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowFrontend", policy =>
                    {
                        policy.WithOrigins("https://myprojectfrontend1.onrender.com")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
                });

                // Configure URLs - חשוב מאוד ל-Render
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                Console.WriteLine($"Configuring to listen on port: {port}");

                // ב-Render תמיד צריך להאזין על 0.0.0.0
                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

                builder.Services.Configure<FormOptions>(options =>
                {
                    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB limit
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                // הפעלת CORS
                app.UseCors("AllowFrontend");

                // Enable static files
                app.UseStaticFiles();

                // הגדרת נתיב תיקיית התמונות בהתאם לסביבה
                var imgPath = app.Environment.IsDevelopment() 
                    ? Path.Combine(app.Environment.WebRootPath ?? app.Environment.ContentRootPath, "IMG")
                    : "/app/IMG";

                // וידוא שהתיקייה קיימת
                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(imgPath),
                    RequestPath = "/IMG"
                });

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                // Add a simple root endpoint
                app.MapGet("/", () => "API is running!");

                // Improve health check
                app.MapGet("/health", () => Results.Ok(new
                {
                    status = "healthy",
                    timestamp = DateTime.UtcNow,
                    environment = app.Environment.EnvironmentName,
                    port = Environment.GetEnvironmentVariable("PORT")
                }));

                // Test database connection before starting
                await TestDatabaseConnection(app);

                // Run migrations
                try 
                {

                    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        await RunMigrationsAsync(app);
                    }
                    else
                    {
                        Console.WriteLine("Skipping database migrations - no connection string");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipping database migrations: {ex.Message}");
                    // ממשיכים להריץ את האפליקציה בלי מסד נתונים
                }
                // Seed database with initial data
                await SeedDatabaseAsync(app);
                Console.WriteLine($"Application starting on port {port}");
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application failed to start: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (!string.IsNullOrEmpty(databaseUrl))
            {
                Console.WriteLine("Using DATABASE_URL from environment");
                Console.WriteLine($"Raw DATABASE_URL: {databaseUrl.Substring(0, Math.Min(50, databaseUrl.Length))}...");
                return ConvertPostgreSQLUrl(databaseUrl);
            }

            var connString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine("Using DefaultConnection from appsettings");
            return connString;
        }

        private static string ConvertPostgreSQLUrl(string databaseUrl)
        {
            try
            {
                Console.WriteLine($"Parsing DATABASE_URL: {databaseUrl.Substring(0, Math.Min(30, databaseUrl.Length))}...");

                var uri = new Uri(databaseUrl);
                var userInfo = uri.UserInfo.Split(':');

                // Validate required components
                if (string.IsNullOrEmpty(uri.Host))
                    throw new InvalidOperationException("Host is missing from DATABASE_URL");

                if (userInfo.Length != 2 || string.IsNullOrEmpty(userInfo[0]) || string.IsNullOrEmpty(userInfo[1]))
                    throw new InvalidOperationException("Username or password is missing from DATABASE_URL");

                if (string.IsNullOrEmpty(uri.LocalPath) || uri.LocalPath.Length <= 1)
                    throw new InvalidOperationException("Database name is missing from DATABASE_URL");

                // Handle default PostgreSQL port if not specified
                var port = uri.Port == -1 ? 5432 : uri.Port;
                var database = uri.LocalPath.Substring(1);

                Console.WriteLine($"Parsed connection - Host: {uri.Host}, Port: {port}, Database: {database}, Username: {userInfo[0]}");

                return $"Host={uri.Host};Port={port};Database={database};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;Timeout=60;Command Timeout=60;Pooling=true;MinPoolSize=1;MaxPoolSize=20;";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing DATABASE_URL: {ex.Message}");
                throw new InvalidOperationException($"Failed to parse DATABASE_URL: {ex.Message}", ex);
            }
        }

        private static async Task TestDatabaseConnection(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<dbcontext>();
                logger.LogInformation("Testing database connection...");

                var canConnect = await context.Database.CanConnectAsync();
                if (canConnect)
                {
                    logger.LogInformation("Database connection successful");
                }
                else
                {
                    logger.LogWarning("Cannot connect to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database connection test failed: {Error}", ex.Message);
            }
        }

        private static async Task RunMigrationsAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var migrationLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<dbcontext>();

                migrationLogger.LogInformation("Starting database migration...");
                await context.Database.MigrateAsync();
                migrationLogger.LogInformation("Database migration completed successfully");
            }
            catch (Exception ex)
            {
                migrationLogger.LogError(ex, "Database migration failed: {Error}", ex.Message);

                // Don't throw in production - let the app start even if migrations fail
                if (app.Environment.IsDevelopment())
                {
                    throw;
                }
            }
        }

        static async Task SeedDatabaseAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<dbcontext>();

                logger.LogInformation("Starting database seeding...");

                // בדיקה אם כבר יש נתונים
                if (!context.Customers.Any())
                {
                    var customers = new List<Customer>
            {

                new Customer
                {
                    CustId = 12345,
                    CustNum = 1,
                    CustName = "jonatan",
                    CustAddress = "ירמיהו 3",
                    CustEmail = "jjjjjjjjjjjj",
                    CustPhone = "0556750905"
                }
            };

                    context.Customers.AddRange(customers);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Customers seeded successfully");
                }



                if (!context.Employees.Any())
                {
                    var employees = new List<Employee>
            {
                new Employee
                {
                    EmpId = 1,
                    EmpNum = 1001,



                    Ename = "Manager", // 7 תווים - צריך להיות בסדר
                    Egmail = "mgr@co.com", // קצר יותר
                    Ephone = "0501111111" // 10 תווים
                },
                new Employee
                {
                    EmpId = 4545,
                    EmpNum = 1002,




                    Ename = "moshe", // 5 תווים
                    Egmail = "m@co.com", // קצר יותר
                    Ephone = "0502222222" // 10 תווים
                }
            };

                    context.Employees.AddRange(employees);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Employees seeded successfully");
                }

                else
                {
                    logger.LogInformation("Database already contains data, skipping seeding");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database seeding failed: {Error}", ex.Message);
                // אל תזרוק שגיאה - תן לאפליקציה להמשיך לרוץ
            }
        }

    }

}

