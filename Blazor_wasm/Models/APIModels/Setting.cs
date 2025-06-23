using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor_wasm.Models.AuthModels;
namespace Blazor_wasm.Models.APIModels
{
    public class Setting
    {
        public static UserBasicDetail UserBasicDetail { get; set; }
        public static string Url { get; set; } 
        //219.91.108.13
        public static string BaseUrl => $"http://{Url}:5256";
    }
}
