namespace FirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //builder is creating web appli

            // Add services to the container.
            builder.Services.AddControllersWithViews(); //import mcv service or adding

            var app = builder.Build(); //app build up

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
                pattern: "{controller=First}/{action=Index}/{id?}");

            app.Run();
        }
    }
}