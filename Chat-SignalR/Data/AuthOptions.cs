using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chat_SignalR.Data
{
    public class AuthOptions
    {
        public const string SectionName = "Auth";
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Key { get; set; } = default!;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
        public int TokenLifetimeHours { get; set; }

    }

}
