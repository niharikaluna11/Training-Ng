using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TrialApplication.Context;
using TrialApplication.Interfaces;
using TrialApplication.Models;
using TrialApplication.Repositories;
using TrialApplication.Services;

namespace TrialApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program)); // Registers all profiles in this assembly

            // DbContext configuration
            builder.Services.AddDbContext<EventBookingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, Event>, EventRepository>();
            builder.Services.AddScoped<IRepository<int, Booking>, BookingRepository>();

            // Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

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
