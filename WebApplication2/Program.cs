
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using static System.Net.WebRequestMethods;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<PractikaContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));

            // Add services to the container.
<<<<<<< HEAD

            builder.Services.AddControllers();
=======
            var app = builder.Build();
            builder.Services.AddControllers();  
          
>>>>>>> 97c98d8069af15162de17ee2026d9bf2671ddf9d
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PractikaContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
<<<<<<< HEAD

            app.UseCors(builder => builder.WithOrigins(new[] { "https://localhost:7157" })
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
=======
              app.UseCors(builder => builder.WithOrigins(new[] { "https://localhost:7157", "https://apirep-1.onrender.com" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
         
>>>>>>> 97c98d8069af15162de17ee2026d9bf2671ddf9d




           app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

<<<<<<< HEAD
            app.Run();
=======
            app.Run();
        }
    }
}
>>>>>>> 97c98d8069af15162de17ee2026d9bf2671ddf9d
