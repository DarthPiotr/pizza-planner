using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Rewrite;
using pizza_planner.Models;
using System.Globalization;

namespace pizza_planner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			builder.Services.AddServerSideBlazor();

			builder.Services.AddDbContext<DataContext>();

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

            var defaultCulture = new CultureInfo("pl-PL");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);

            app.MapBlazorHub();
            var rewriteOptions = new RewriteOptions();
            rewriteOptions.AddRewrite("_blazor(.*)", "/_blazor$1", skipRemainingRules: true);
            app.UseRewriter(rewriteOptions);

            app.Run();
        }
    }
}
