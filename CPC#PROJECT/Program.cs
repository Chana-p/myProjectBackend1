
<<<<<<< HEAD
using BL;
=======
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
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
<<<<<<< HEAD
            //var settings = builder.Configuration.GetSection("filesPath").Value;
=======
            var settings = builder.Configuration.GetSection("filesPath").Value;
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            builder.Services.AddSingleton<IBL>(x => new BL.BLManager());// "settings") );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
<<<<<<< HEAD
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
=======
            
            var app = builder.Build();
               app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //add picture
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
            //    RequestPath = "/Images"
            //});
            app.UseHttpsRedirection();

            app.UseAuthorization();
          
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550

            app.MapControllers();

            app.Run();
        }
    }
}
<<<<<<< HEAD







=======
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
