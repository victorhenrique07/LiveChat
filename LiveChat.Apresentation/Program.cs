using Blazored.LocalStorage;
using LiveChat.Api.Integration.Services;
using LiveChat.Apresentation;
using LiveChat.Apresentation.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped(sp =>
{
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    var tokenHandler = new TokenHandler(localStorage)
    {
        InnerHandler = new HttpClientHandler()
    };

    return new HttpClient(tokenHandler)
    {
        BaseAddress = new Uri("https://livechat-api.up.railway.app/api/")
    };
});

builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<UserService>();

await builder.Build().RunAsync();
