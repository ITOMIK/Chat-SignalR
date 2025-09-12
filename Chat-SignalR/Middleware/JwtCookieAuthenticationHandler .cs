using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class JwtCookieAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _cookieName;

    public JwtCookieAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IConfiguration config)
        : base(options, logger, encoder, clock)
    {
        _cookieName = config.GetValue<string>("CookieName");
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Берём JWT из cookie
        var token = Request.Cookies[_cookieName];
        if (string.IsNullOrEmpty(token))
            return Task.FromResult(AuthenticateResult.NoResult());

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claims = jwtToken.Claims;
            var identity = new ClaimsIdentity(claims, Scheme.Name);//хранит claims и указывает схему аутентификации (Scheme.Name)
            var principal = new ClaimsPrincipal(identity);//объект, который ASP.NET использует как User в контроллере или Razor Page.

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));//успешно авторизован 
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid JWT token"));
        }
    }
}
