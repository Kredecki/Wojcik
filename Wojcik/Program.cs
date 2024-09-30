using Microsoft.EntityFrameworkCore;
using Wojcik;
using Wojcik.Components;
using Wojcik.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var configurtaion = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
	.Build();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddApplication();

#region Database Connection
var connectionString = configurtaion.GetConnectionString("DefaultConnection");

if (String.IsNullOrEmpty(connectionString))
	throw new NullReferenceException("DefaultConnection cannot be null");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(connectionString));
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Wojcik API V1");
    x.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Wojcik.Client._Imports).Assembly);

app.MapControllers();

app.Run();