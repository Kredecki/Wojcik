using MudBlazor.Services;
using Wojcik.Persistence.Repositories;
using Wojcik.Shared.Interfaces.Repositories;
using Wojcik.Shared.Request;

namespace Wojcik;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ICommandQuery).Assembly));
        services.AddMudServices();

        services.AddScoped<IExampleRepository, ExampleRepository>();

		return services;
	}
}
