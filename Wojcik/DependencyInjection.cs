using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Wojcik.Persistence.Entities;
using Wojcik.Persistence;
using Wojcik.Persistence.Repositories;
using Wojcik.Shared.Interfaces.Repositories;
using Wojcik.Shared.Request;

namespace Wojcik;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
        services.AddControllers().AddNewtonsoftJson();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddSwaggerGen();
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ICommandQuery).Assembly));
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
        services.AddMudServices();

        services.AddScoped<IExampleRepository, ExampleRepository>();

		return services;
	}
}
