

// //using BL;
// //using BL.Api;
// //using Microsoft.Extensions.FileProviders;
// //using System.Runtime.CompilerServices;
// //using Dal.newModels;
// //using Microsoft.EntityFrameworkCore;
// //using Npgsql.EntityFrameworkCore.PostgreSQL;
// //using Dal.Api;
// //using Dal;
// //namespace CPC_PROJECT
// //{
// //    public class Program
// //    {
// //        public static void Main(string[] args)
// //        {
// //            var builder = WebApplication.CreateBuilder(args);

// //builder.Services.AddDbContext<dbcontext>(options =>
// //{
// //    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// //    options.UseNpgsql(connectionString);
// //});

// //// שאר השירותים שלך
// //            //cors
// //            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
// //            option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


// //            // Add services to the container.
// //builder.Services.AddEndpointsApiExplorer();
// //builder.Services.AddSwaggerGen();
// //            builder.Services.AddControllers();
// //            //add picture

// //            var settings = builder.Configuration.GetSection("filesPath").Value;

// //           // builder.Services.AddSingleton<IBL>(x => new BL.BLManager());// "settings") );

// //// קריאת מחרוזת החיבור ממשתני הסביבה
// //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
// //    ?? Environment.GetEnvironmentVariable("DATABASE_URL"); // גיבוי למקרה של Render
// //builder.Services.AddDbContext<dbcontext>(options =>
// //    options.UseNpgsql(connectionString));
// //            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
// //            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");


// //            var app = builder.Build();
// //                app.UseCors("AllowAll");
// //               app.UseStaticFiles();
// //            // Configure the HTTP request pipeline.
// //            if (app.Environment.IsDevelopment())
// //            {
// //                app.UseDeveloperExceptionPage();
// //            }

// //            app.UseHttpsRedirection();

// //            app.UseAuthorization();
// //           //for render?
// //          // הרצת migrations אוטומטית
// //using (var scope = app.Services.CreateScope())
// //{
// //    try
// //    {
// //        var context = scope.ServiceProvider.GetRequiredService<dbcontext>();
// //         context.Database.Migrate();
// //          builder.Services.AddScoped<IDal, DalManager>();
// //          builder.Services.AddScoped<IBL, BLManager>();

// //                }
// //                catch (Exception ex)
// //    {
// //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
// //        logger.LogError(ex, "An error occurred while migrating the database.");
// //    }
// //}

// //if (app.Environment.IsDevelopment())
// //{
// //    app.UseSwagger();
// //    app.UseSwaggerUI();
// //}

// //app.UseHttpsRedirection();
// //app.UseRouting();
// //app.MapControllers();

// //app.Run();
// //        }
// //    }
// //}
// using Microsoft.EntityFrameworkCore;
// using Npgsql.EntityFrameworkCore.PostgreSQL;
// using Dal.newModels;
// using BL;
// using BL.Api;
// using Microsoft.Extensions.FileProviders;
// using System.Runtime.CompilerServices;
// using Dal;
// using Dal.Api;
// using Dal.Services;

// namespace CPC_PROJECT
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             var builder = WebApplication.CreateBuilder(args);

//             // קריאת מחרוזת החיבור - עדיפות ל-DATABASE_URL של Render
//             var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
//                 ?? builder.Configuration.GetConnectionString("DefaultConnection");

//             // אם יש DATABASE_URL מ-Render, צריך להמיר אותו לפורמט של Npgsql
//             if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATABASE_URL")))
//             {
//                 var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
//                 var uri = new Uri(databaseUrl);
//                 connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.LocalPath.Substring(1)};Username={uri.UserInfo.Split(':')[0]};Password={uri.UserInfo.Split(':')[1]};SSL Mode=Require;Trust Server Certificate=true";
//             }

//             if (string.IsNullOrEmpty(connectionString))
//             {
//                 throw new InvalidOperationException("Connection string not found. Please set DefaultConnection in appsettings.json or DATABASE_URL environment variable.");
//             }

//             Console.WriteLine($"Connecting to database...");
//             // הוסף את זה לפני builder.Services.AddDbContext
//             builder.Services.AddEntityFrameworkNpgsql();

//             builder.Services.AddDbContext<dbcontext>(options =>
//             {
//                 options.UseNpgsql(connectionString);
//                 options.EnableSensitiveDataLogging(); // רק לפיתוח
//                 options.EnableDetailedErrors(); // רק לפיתוח
//             });

//             // רישום DbContext
//             builder.Services.AddDbContext<dbcontext>(options =>
//                 options.UseNpgsql(connectionString));

//             // רישום DAL Services
//             builder.Services.AddScoped<IDal, DalManager>();

//             // רישום BL Services  
//             builder.Services.AddScoped<IBL, BLManager>();

//             builder.Services.AddControllers();
//             builder.Services.AddEndpointsApiExplorer();
//             builder.Services.AddSwaggerGen();

//             // CORS - חשוב ל-Render
//             builder.Services.AddCors(options =>
//             {
//                 options.AddPolicy("AllowAll", policy =>
//                 {
//                     policy.AllowAnyOrigin()
//                           .AllowAnyMethod()
//                           .AllowAnyHeader();
//                 });
//             });

//             var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//             builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

//             var app = builder.Build();

//             app.UseCors("AllowAll");
//             app.UseStaticFiles();

//             if (app.Environment.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//                 app.UseSwagger();
//                 app.UseSwaggerUI();
//             }

//             // הסר HTTPS redirect ל-Render
//             // app.UseHttpsRedirection();

//             app.UseAuthorization();
//             app.MapControllers();

//             // הרצת migrations אוטומטית
//             using (var scope = app.Services.CreateScope())
//             {
//                 try
//                 {
//                     var context = scope.ServiceProvider.GetRequiredService<dbcontext>();
//                     var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//                     logger.LogInformation("Starting database migration...");
//                     context.Database.Migrate();
//                     logger.LogInformation("Database migration completed successfully.");
//                 }
//                 catch (Exception ex)
//                 {
//                     var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//                     logger.LogError(ex, "An error occurred while migrating the database: {Error}", ex.Message);
//                     // אל תזרוק שגיאה ב-production
//                     if (app.Environment.IsDevelopment())
//                     {
//                         throw;
//                     }
//                 }
//             }

//             app.Run();
//         }
//     }
// }
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Dal.newModels;
using BL;
using BL.Api;
using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;
using Dal;
using Dal.Api;
using Dal.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;

namespace CPC_PROJECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // קריאת מחרוזת החיבור
            var connectionString = GetConnectionString(builder.Configuration);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found.");
            }

            Console.WriteLine($"Connecting to database... Environment: {builder.Environment.EnvironmentName}");

            // רישום DbContext עם תמיכה ב-SQL Server ו-PostgreSQL
            ConfigureDatabase(builder.Services, connectionString, builder.Environment);

            // רישום Services
            builder.Services.AddScoped<IDal, DalManager>();
            builder.Services.AddScoped<IBL, BLManager>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

           // Configure URLs based on environment
if (builder.Environment.IsDevelopment())
{
    // For local development, use localhost with standard ports
    builder.WebHost.UseUrls("https://localhost:7064", "http://localhost:5298");
}
else
{
    // For production/deployment, use the PORT environment variable
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}
            var app = builder.Build();

            app.UseCors("AllowAll");
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; // זה יגרום ל-Swagger להיות בכתובת הבסיסית
                });
            }

                app.UseHttpsRedirection();
            }
   
            app.UseAuthorization();
            app.MapControllers();

            // הרצת migrations
            RunMigrations(app);

            app.Run();
        }

        private static void ConfigureDatabase(IServiceCollection services, string connectionString, IWebHostEnvironment environment)
        {
            // בדיקה אם זה PostgreSQL (production) או SQL Server (development)
            // bool isPostgreSQL = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATABASE_URL")) || 
            //                    connectionString.Contains("Host=");

            services.AddDbContext<dbcontext>(options =>
            {
                    Console.WriteLine("trying database");

                // if (isPostgreSQL)
                // {
                    // PostgreSQL עבור production (Render)
                    options.UseNpgsql(connectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.CommandTimeout(30);
                        npgsqlOptions.EnableRetryOnFailure(3);
                    });
                    Console.WriteLine("Using PostgreSQL database");
                // }
                // else
                // {
                //     // SQL Server עבור development
                //     options.UseSqlServer(connectionString, sqlOptions =>
                //     {
                //         sqlOptions.CommandTimeout(30);
                //         sqlOptions.EnableRetryOnFailure(3);
                //     });
                //     Console.WriteLine("Using SQL Server database");
                // }

                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            // בדיקה אם יש DATABASE_URL (Render/Heroku) - PostgreSQL
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                return ConvertPostgreSQLUrl(databaseUrl);
            }

            // אחרת, קח מ-appsettings.json (SQL Server מקומי)
            return configuration.GetConnectionString("DefaultConnection");
        }

        private static string ConvertPostgreSQLUrl(string databaseUrl)
        {
            try
            {
                var uri = new Uri(databaseUrl);
                var userInfo = uri.UserInfo.Split(':');
                
                return $"Host={uri.Host};Port={uri.Port};Database={uri.LocalPath.Substring(1)};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Command Timeout=30";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to parse DATABASE_URL: {ex.Message}");
            }
        }

        private static void RunMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<dbcontext>();
                
                logger.LogInformation("Testing database connection...");
                
                if (context.Database.CanConnect())
                {
                    logger.LogInformation("Database connection successful. Starting migration...");
                    context.Database.Migrate();
                    logger.LogInformation("Database migration completed successfully.");
                }
                else
                {
                    logger.LogWarning("Cannot connect to database. Skipping migration.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database migration failed: {Error}", ex.Message);
                
                if (app.Environment.IsDevelopment())
                {
                    throw;
                }
            }
        }
    }
}

