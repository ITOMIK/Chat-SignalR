using Chat_SignalR.Models;
using Chat_SignalR.Models.ApiModels;
using Chat_SignalR.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Chat_SignalR.Services
{
    public class AuthService
    {
        private readonly UserReposytory userReposytory;
        private readonly PasswordService passwordService;
        private readonly JwtService jwtService;
        private readonly string cookieName;

        public AuthService(UserReposytory _userReposytory, PasswordService _passwordService, JwtService _jwtService, IConfiguration _config) {
            userReposytory = _userReposytory;
            passwordService = _passwordService;
            jwtService = _jwtService;
            cookieName = _config.GetValue<string>("CookieName");
        }

        public async Task<String> RegisterAsync(RegisterModel model)
        {
            if (await userReposytory.GetByName(model.name)!=null)
                return String.Empty;

            await userReposytory.Add(new User(model.name, passwordService.Generate(model.password)));

            return  await LoginAsync(new LoginModel(model));
        }

        public async Task<String> LoginAsync(LoginModel model)
        {
            var u = await userReposytory.GetByName(model.name);
            if (u== null)
                return String.Empty;
            if (passwordService.Verify(model.password, u.hashPassword))
            {
                return jwtService.GenerateToken(u);
            }
            return String.Empty;
        }

        public void setTokenToCookie(HttpContext HttpContext, String token)
        {
            HttpContext.Response.Cookies.Append(cookieName, token);
        }

        public  void removeTokenFromCookie(HttpContext HttpContext)
        {
            HttpContext.Response.Cookies.Delete(cookieName);
        }
    }
}
