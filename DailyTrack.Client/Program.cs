using DailyTrack.Common.Interface;
using DailyTrack.Common.Service;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUIComponents();

builder.Services.AddHttpClient<IActivityWebService, ActivityWebService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7057"); // Sostituisci con l'URL del tuo server Web
});

await builder.Build().RunAsync();
