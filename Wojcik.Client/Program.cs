using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Wojcik.Client.Interfaces;
using Wojcik.Client.Providers;
using Wojcik.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient("Wojcik-API", 
	client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(x => x.GetRequiredService<IHttpClientFactory>().CreateClient("Wojcik-API"));

await builder.Build().RunAsync();
