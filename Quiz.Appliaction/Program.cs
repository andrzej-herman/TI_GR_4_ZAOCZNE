using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Quiz.Appliaction;
using Quiz.Appliaction.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://quizgrupa4api.azurewebsites.net/") });

await builder.Build().RunAsync();
