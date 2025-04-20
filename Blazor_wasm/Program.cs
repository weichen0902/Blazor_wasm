using System.Globalization;
using Blazor_wasm.Authentication;
using Blazor_wasm.Controller;
using Blazor_wasm.Models.ModbusModels;
using Blazor_wasm.Pages.Components.Csharp;
using Blazor_wasm.Services;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazor_wasm.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Blazored.LocalStorage;

namespace Blazor_wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<DevicesDataModel>();
            builder.Services.AddScoped<GetAllCalData>();
            builder.Services.AddScoped<GetSchedule>();
            builder.Services.AddSingleton<AlarmCollection>();
            builder.Services.AddScoped<TouchKeyboard>();
            builder.Services.AddScoped<PageTitleService>();
            builder.Services.AddTransient<ExportFile>();
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazorise(options => { options.Immediate = true; options.ProductToken = "<CjxRBHF7NAs9UARyfzE1BlEAc3o1Dj1QAXB6Nww6bjoNJ2ZdYhBVCCo/CTRaB0xERldhE1EvN0xcNm46FD1gSkUHCkxESVFvBl4yK1FBfAYKFTxsWWBuOh4RQXlYIncTB0FnUy5xGRFaakM0Yx4RP3ZDPHwIA0xsX246HhFEbVgscw4DVXRJN3UeEUh5VDxvEwFSa1M8Cg8BWnRFLnkVHQgyUzxzCQ9XbF88bwwPXWdTMX8WHVpnNi1/HgJMdUU3Y0xEWmdAKmMVGEx9WzxvDA9dZ1MxfxYdWmc2LX8eAkx1RTdjTERaZ1gxdQQYTH1bPG8MD11nUzF/Fh1aZzYtfx4CTHVFN2NMRGB+TyBnbgt3bWsZf3EdVEtDEEIAFk0ORFtkEyhAUVUifBcUTQxtIXJweTddShRTeTxyfl5IRQYhQ3lbKFMHeVRVJy99GQs0dkUXAjA3NnR6KwcSOENCZAgDLgt2aVYIUhEWQ0F4EEoGOVxRSwplKSVVa2cVeQsoVnRZBX0oHjZ7bzZ6cwc3eTgLBXQ9aUt/NnkXCT0JODIIE2Fjf3o0WxMfQ39BIQgXekNJa14=>"; })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
            builder.Services.AddSyncfusionBlazor();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM2MjgzNEAzMjM2MmUzMDJlMzBFWUpiYU1GSmFpL0tLaVdEaCtRV0cxSnNxTmZZNUVha0JzQXpXZG5SclowPQ==");

            builder.Services.AddLocalization();
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;


            builder.Services.AddScoped<IAppService, AppService>();
            builder.Services.AddScoped<IDataService, DataService>();

            await builder.Build().RunAsync();
        }
    }
}
