using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.PageModels
{
    public class ResetPasswordModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }

       
        public string? Email { get; set; }

        public string? Token { get; set; }
    }
}
