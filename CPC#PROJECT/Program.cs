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

            // רישום DbContext - רק PostgreSQL
            builder.Services.AddDbContext<dbcontext>(options =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(30);
                    npgsqlOptions.EnableRetryOnFailure(3);
                });

                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });

            // רישום Services
            builder.Services.AddScoped<IDal, DalManager>();
            builder.Services.AddScoped<IBL, BLManager>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS - Enhanced configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("*");
                });

                // Alternative specific policy for production
                options.AddPolicy("Production", policy =>
                {
                    policy.WithOrigins(
                        "https://myprojectfrontend1.onrender.com",
                        "http://localhost:3000",
                        "http://localhost:5173",
                        "http://localhost:4200"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // Configure URLs based on environment
            if (builder.Environment.IsDevelopment())
            {
                builder.WebHost.UseUrls("https://localhost:7064", "http://localhost:5298");
            }
            else
            {
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
            }

            var app = builder.Build();

            // CORS must be before other middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("AllowAll");
            }
            else
            {
                // Use both policies in production for maximum compatibility
                app.UseCors("AllowAll");
            }

            // Only use static files if wwwroot exists
            if (Directory.Exists(Path.Combine(app.Environment.ContentRootPath, "wwwroot")))
            {
                app.UseStaticFiles();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
                app.UseHttpsRedirection();
            }

            // Important: UseRouting must come before UseAuthorization
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            // הרצת migrations
            RunMigrations(app);

            app.Run();
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                return ConvertPostgreSQLUrl(databaseUrl);
            }

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
