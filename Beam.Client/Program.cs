using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Beam.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Beam.Animation;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace Beam.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                  .AddBlazorise(options =>
                 {
                     options.ChangeTextOnKeyPress = true;
                 })
                  .AddBootstrapProviders()
                  .AddFontAwesomeIcons();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddTransient<IBeamApiService, BeamApiService>();
            builder.Services.AddSingleton<IDataService, DataService>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<BeamAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>
                (s => s.GetRequiredService<BeamAuthenticationStateProvider>());

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddSingleton<AnimationService>();

            var host = builder.Build();

            host.Services
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
