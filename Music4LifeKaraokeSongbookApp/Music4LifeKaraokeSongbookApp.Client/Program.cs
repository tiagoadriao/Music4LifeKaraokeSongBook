using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Music4LifeKaraokeSongbookApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IMusic4LifeSongbookService, Music4LifeSongbookProxyService>();

builder.Services.AddTransient<Songbook>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
