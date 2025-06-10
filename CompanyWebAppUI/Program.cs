using DataAccess;
using CompanyWebAppUI.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;

namespace CompanyWebAppUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMudServices();

            builder.Services.AddScoped<ProtectedLocalStorage>();
            builder.Services.AddScoped<UserSessionService>();

            string? connectionString = builder.Configuration.GetConnectionString("OracleDb");
            if (connectionString != null)
            {
                builder.Services.AddScoped(sp => new DatabaseAccess(connectionString));
            }

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
