using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PortfolioApp
{
    public class Program
    {
/*        1) Cerate a beautiful and responsive profile page for yourself.
        Ensure the page has all your professional details also pics
        responsive using bootstrap
        Hosting is optional*/
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}