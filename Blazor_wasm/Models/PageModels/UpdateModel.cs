using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_wasm.Models.PageModels
{
    public class UpdateModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        
        public string OldPassword { get; set; }
         
        public string NewPassword { get;set; }
        
        [Compare(nameof(NewPassword), ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }
        public string UserAvatar { get; set; }
        public string Role { get; set; } = "User";
    }
}
