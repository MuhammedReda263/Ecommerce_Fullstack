using Ecom.API.Mapping;
using Ecom.API.Middlewares;
using Ecom.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Ecom.Core.Entities;
using Ecom.Infrastructure.Data;

namespace Ecom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowCredentials()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddMemoryCache();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Register Identity (uses AppDbContext registered in infrastructure)
            builder.Services.AddIdentity<AppUser, IdentityRole>(options => { /* options */ })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<GlobalExceptionMiddleware>();

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAngular");
            app.UseMiddleware<GlobalExceptionMiddleware>();
            // Enable authentication middleware
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
