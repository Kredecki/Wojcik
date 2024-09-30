using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("Wojcik-API", 
	client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(x => x.GetRequiredService<IHttpClientFactory>().CreateClient("Wojcik-API"));

await builder.Build().RunAsync();
