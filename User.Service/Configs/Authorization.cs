using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace User.Service.Configs
{
    public static class Authorization
    {
        public static string ISSUER = "USER-SERVICE";
        public static string AUDIENCE = "USER";

        public static SymmetricSecurityKey CreateKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        public static SigningCredentials GenerateSigningCredentials(string key)
        {
            return new SigningCredentials(CreateKey(key), SecurityAlgorithms.HmacSha256);
        }
    }
}
