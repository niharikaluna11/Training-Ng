using FoodOrderApplication.Interfaces;
using FoodOrderApplication.Models;
using FoodOrderApplication.Repository;
using FoodOrderApplication.Services;

namespace FoodOrderApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Service will be used in controller
            builder.Services.AddScoped<IPizzaService,PizzaService>();
            //Repo will be used in service
            builder.Services.AddScoped<IRepository<int, PizzaImages>, PizzaImageRepository>();
            //Repo will be used in service
            builder.Services.AddScoped<IRepository<int,Pizza>,PizzaRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pizza}/{action=Index}/{id?}");

            app.Run();
        }
    }
}