
using BL;
using BL.Api;
using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;

namespace CPC_PROJECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //cors
            builder.Services.AddCors(c => c.AddPolicy("AllowAll",
            option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


            // Add services to the container.

            builder.Services.AddControllers();
            //add picture
            //var settings = builder.Configuration.GetSection("filesPath").Value;
            builder.Services.AddSingleton<IBL>(x => new BL.BLManager());// "settings") );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
            var app = builder.Build();
                app.UseCors("AllowAll");
               app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            app.UseHttpsRedirection();

            app.UseAuthorization();
           //for render?
            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}







