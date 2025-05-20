using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Devices.Bluetooth.Background;

namespace Blazor_wasm.Models.AuthModels
{
    public class UserBasicDetail
    {
        public string Name { get; set; }
        public string UserID { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpire { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserAvatar { get; set; }

        public void Reset()
        {
            Name = string.Empty;
            UserID = string.Empty;
            AccessToken = string.Empty;
            AccessTokenExpire = DateTime.MinValue;
            RefreshToken = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            UserAvatar = string.Empty;
        }

    }
}
