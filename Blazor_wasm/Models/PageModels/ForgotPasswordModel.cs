using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.PageModels
{
    public class ForgotPasswordModel
    {
        public string VerificationCode { get; set; }
        public string? Token { get; set; }
    }
}
