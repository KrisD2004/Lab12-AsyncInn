using Lab2_AysncInn.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Lab2_AysncInn.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Lab2_AysncInn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container
            builder.Services.AddControllersWithViews().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AsyncInnContext>();


            builder.Services.AddSwaggerGen(options =>
            {
                // Make sure get the "using Statement"
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "School Demo",
                    Version = "v1",
                });
            });

            builder.Services.AddDbContext<AsyncInnContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"))); 
            
           /* builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    // Tell the authenticaion scheme "how/where" to validate the token + secret
                    options.TokenValidationParameters = JwtTokenService.GetValidationParameters(builder.Configuration);
                });*/
            
            builder.Services.AddAuthorization(options =>
            {
                // Add "Name of Policy", and the Lambda returns a definition
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
            });

            //builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<AsyncInnContext>();


            var app = builder.Build();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Student Demo");
                options.RoutePrefix = "docs";
            });

            //printing message to see if it works
            app.MapGet("/", () => "Hello World!");


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