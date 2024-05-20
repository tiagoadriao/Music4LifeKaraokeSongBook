using MudBlazor.Services;
using Music4LifeKaraokeSongbookApp.Client.Services;
using Music4LifeKaraokeSongbookApp.Components;
using Music4LifeKaraokeSongbookApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddTransient<IMusic4LifeSongbookService, Music4LifeSongbookService>();

builder.Services.AddMemoryCache();

builder.Services.AddMudServices();

var app = builder.Build();

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
    .AddAdditionalAssemblies(typeof(Music4LifeKaraokeSongbookApp.Client._Imports).Assembly);

app.MapGet("/api/songbook", async (IMusic4LifeSongbookService service) =>
{
    return TypedResults.Ok(await service.GetSongbookAsync());
});

app.Run();
