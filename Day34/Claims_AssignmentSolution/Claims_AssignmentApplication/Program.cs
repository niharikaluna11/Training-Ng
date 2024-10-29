using Claims_AssignmentApplication.Context;
using Claims_AssignmentApplication.Interfaces;
using Claims_AssignmentApplication.Models;
using Claims_AssignmentApplication.Repositories;
using Claims_AssignmentApplication.Services;
using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Mappers;
using System.Security.Claims;

namespace Claims_AssignmentApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<ClaimContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            // Add services to the container.



            builder.Services.AddAutoMapper(typeof(ClaimProfile));
            #region Repositories
            builder.Services.AddScoped<IRepository<int, Claims>, ClaimRepository>();
            builder.Services.AddScoped<IRepository<int, Document>, DocumentRepository>();
      
            #endregion

            #region Services
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            
            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
