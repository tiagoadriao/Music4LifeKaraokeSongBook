using M4LKaraokeSongbook;
using M4LKaraokeSongbook.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<Songbook>();
builder.Services.AddSingleton<DbHandler>();

builder.Services.AddMemoryCache();

builder.Services.AddMudServices();

await builder.Build().RunAsync();