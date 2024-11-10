using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Wojcik.Persistence.Entities;
using Wojcik.Persistence;
using Wojcik.Shared.Request;
using Wojcik.Client.Providers;
using Wojcik.Client.Interfaces;
using Wojcik.Client.Services;
using Radzen;
using Wojcik.Shared.Interfaces.Repositories;
using Wojcik.Persistence.Repositories.Administration;

namespace Wojcik;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = false;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
        });
        services.AddControllers().AddNewtonsoftJson();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddSwaggerGen();
        services.AddHttpClient("Wojcik", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7000/");
        });
        services.AddRadzenComponents();
        services.AddMudServices();
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ICommandQuery).Assembly));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthStateProvider>();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();

        return services;
    }
}
