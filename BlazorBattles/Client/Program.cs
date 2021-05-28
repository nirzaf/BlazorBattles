using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorBattles.Client.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace BlazorBattles.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IBananaService, BananaService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
            builder.Services.AddScoped<IBattleService, BattleService>();

            await builder.Build().RunAsync();
        }
    }
}
