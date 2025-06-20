using Blazor_wasm.Models.AuthModels;
using Blazor_wasm.Models.LanguagesModels;
using Blazor_wasm.Resources.Languages;
using Blazor_wasm.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace Blazor_wasm
{
    public class AlertMessages
    {
        private IJSRuntime _js;
        private IStringLocalizer<MyStrings> _localizer;

        public AlertMessages(IJSRuntime js, IStringLocalizer<MyStrings> localizer)
        {
            _js = js;
            _localizer = localizer;
        }

        public async Task<string> ErrorAlert(string pageTitle, MainResponse<object> result)
        {
            var stringMsg = string.Empty;
            switch (result.StatusCode)
            {
                case 400:
                    if (pageTitle == "forgotPassword")
                        stringMsg = $"{_localizer[StringDescriptionModel.message_email_incorrect]}";
                    else if (pageTitle == "registration")
                        stringMsg = $"{_localizer[StringDescriptionModel.account_alert_account_alreadyexist]}";        
                    break;
                case 401:
                    if (pageTitle == "update")
                        stringMsg = $"{_localizer[StringDescriptionModel.message_oldpassword_incorrect]}";
                    else
                        stringMsg = $"{_localizer[StringDescriptionModel.httpcode_401]}";
                    break;
                case 403:
                    stringMsg = $"{_localizer[StringDescriptionModel.httpcode_403]}";
                    break;
                case 404:
                    stringMsg = $"{_localizer[StringDescriptionModel.httpcode_404]}";
                    break;
                default:
                    stringMsg = $"{_localizer[StringDescriptionModel.httpcode_urlerr]}";
                    break;
            }
            return $"{pageTitle}: {stringMsg}";
        }
    }
}
