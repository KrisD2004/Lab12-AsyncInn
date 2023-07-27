using Lab2_AysncInn.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab2_AysncInn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AsyncInnContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            // https://localhost:44391/Home/Privacy/1

            app.Run(); ;
        }
    }
}