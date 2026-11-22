using System.ComponentModel.DataAnnotations;
using M4LKaraokeSongbook.Services;
using MudBlazor.Services;
using Music4LifeKaraokeSongbookApp.Client.Services;
using Music4LifeKaraokeSongbookApp.Components;
using Music4LifeKaraokeSongbookApp.Data;
using Music4LifeKaraokeSongbookApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddTransient<IMusic4LifeSongbookService, Music4LifeSongbookService>();
builder.Services.AddTransient<IAskDJHandler, Music4LifeKaraokeSongbookApp.Services.AskDJHandler>();
builder.Services.AddHttpClient();
builder.Services.AddValidation();

builder.Services.AddMemoryCache();

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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Music4LifeKaraokeSongbookApp.Client._Imports).Assembly);

app.MapGet("/api/songbook", async (IMusic4LifeSongbookService service) =>
{
    return TypedResults.Ok(await service.GetSongbookAsync());
});

app.MapPost("/api/requestsong", async (IAskDJHandler handler, SongRequestInput request) =>
{
    bool result = await handler.RequestASongAsync(new AskDJRequest
    {
        Name = request.Name,
        Message = request.Message
    });
    var message = result ? "Request sent successfully." : "Failed to send request.";
    var statusCode = result ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError;

    return Results.Json(new { message }, statusCode: statusCode);
});

app.Run();

public sealed class SongRequestInput
{
    [Required(ErrorMessage = "Indica o teu nome.")]
    [StringLength(AskDJRequest.MaxNameLength, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Indica a música pretendida.")]
    [StringLength(AskDJRequest.MaxMessageLength, ErrorMessage = "O resumo da música não pode exceder 500 caracteres.")]
    public string Message { get; set; } = null!;
}
