using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.AuthModels
{
    public class MainResponse<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T? Content { get; set; }
        public string ExMessage { get; set; }
    }
}
