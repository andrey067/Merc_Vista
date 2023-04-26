using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Merc_Vista_Blazor;
using MudBlazor.Services;
using Merc_Vista_Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ServicesUrlOptions:ApiBaseUrl"]!) });
builder.Services.AddMudServices();

builder.Services.AddScoped<IServiceCaller, ServiceCaller>();
builder.Services.AddSingleton<ISpinnerService, SpinnerService>();

await builder.Build().RunAsync();
