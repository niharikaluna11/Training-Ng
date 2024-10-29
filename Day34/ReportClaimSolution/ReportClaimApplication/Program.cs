using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Context;
using ReportClaimApplication.Interfaces;
using ReportClaimApplication.Mappers;
using ReportClaimApplication.Models;
using ReportClaimApplication.Repositories;
using ReportClaimApplication.Services;
using System.Security.Claims;

namespace ReportClaimApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(ClaimProfile));

            // Register the repository
            builder.Services.AddScoped<IRepository<Claims>, ClaimRepository>();

            // Register the service
            builder.Services.AddScoped<IClaimService, ClaimService>();

            // Add DbContext
            builder.Services.AddDbContext<ClaimDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
