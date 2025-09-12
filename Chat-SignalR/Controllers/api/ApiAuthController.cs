using Chat_SignalR.Models.ApiModels;
using Chat_SignalR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_SignalR.Controllers.api
{
    [Route("api/[controller]")]
    public class ApiAuthController : Controller
    {
        private readonly AuthService authService;
        public ApiAuthController(AuthService _authService) {
            authService = _authService;
        }
        [HttpPost("reg")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
               
                    var token = await authService.RegisterAsync(model);
                    if (!String.IsNullOrEmpty(token))
                    {
                    authService.setTokenToCookie(HttpContext, token);
                    return Ok();
                    }
                        

                Response.StatusCode = 301;
                return BadRequest("Такой пользователь уже существует");
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                authService.removeTokenFromCookie(HttpContext);
                var token = await authService.LoginAsync(model);
                if (token == String.Empty)
                {
                    Response.StatusCode = 401;
                    return BadRequest("Inccorect data");
                }

                authService.setTokenToCookie(HttpContext,token);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            authService.removeTokenFromCookie(HttpContext);
            return Ok();
        }
    }
}
