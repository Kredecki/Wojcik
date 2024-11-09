using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Wojcik.Client.Interfaces;
using Wojcik.Client.Providers;
using Wojcik.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("Wojcik", client =>
{
    client.BaseAddress = new Uri("https://localhost:7000/");
});

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
