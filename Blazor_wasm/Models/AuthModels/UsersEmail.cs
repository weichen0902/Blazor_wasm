using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.AuthModels
{
    public class UsersEmail
    {
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
