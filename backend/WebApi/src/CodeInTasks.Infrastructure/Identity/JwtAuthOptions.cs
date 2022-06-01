using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CodeInTasks.Infrastructure.Identity
{
    internal class JwtAuthOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(Key));
    }
}
