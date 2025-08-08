using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Chat_SignalR.Data;
using System.Security.Claims;

public class JwtBearerOptionsConfigurator : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IOptionsMonitor<AuthOptions> _authOptions;
    private readonly string cookieName;

    public JwtBearerOptionsConfigurator(IOptionsMonitor<AuthOptions> authOptions, IConfiguration _config)
    {
        _authOptions = authOptions;
        cookieName = _config.GetValue<string>("CookieName");
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        var auth = _authOptions.CurrentValue;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = auth.Issuer,
            ValidAudience = auth.Audience,
            IssuerSigningKey = auth.GetSymmetricSecurityKey() 
        };
        // Читаем токен из cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Название куки должно совпадать с тем, что ты ставишь в AuthService
                var token = context.Request.Cookies[cookieName];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    }

    public void Configure(JwtBearerOptions options) => Configure(Options.DefaultName, options);
}
