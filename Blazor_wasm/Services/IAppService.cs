using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazor_wasm.Models;
using Blazor_wasm.Models.AuthModels;
using Blazor_wasm.Models.PageModels;
using Blazor_wasm.Pages;

namespace Blazor_wasm.Services
{
    public interface IAppService
    {
        public string accessToken { get; set; }
        public JwtSecurityToken jwtAccessToken { get; set; }

        public ForgotPasswordModel forgotPasswordModel { get; set; }
        public ResetPasswordModel resetPasswordModel { get; set; }
        Task<MainResponse<object>> RefreshToken();
        public Task<MainResponse<object>> AuthenticateUser(LoginModel loginModel);
        Task<MainResponse<object>> RegisterUser(RegistrationModel registerUser);
        Task<MainResponse<object>> ForgotPassword(string email);
        Task<MainResponse<object>> ResetPassword(ResetPasswordModel resetPasswordModel);
        Task<MainResponse<object>> UpdateUser(UpdateModel updateModel);
        Task<MainResponse<object>> DeleteUsers(List<UsersEmail> usersEmail);
        Task<MainResponse<UpdateModel>> GetUser();
        Task<MainResponse<List<UsersEmail>>> GetAllUsers();
    }
}
