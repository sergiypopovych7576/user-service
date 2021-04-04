using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Services.Shared.Services.Hash
{
    public class HashService : IHashService
    {
        public string HashText(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var textHash = Encoding.ASCII.GetBytes(text).Select(c => c.ToString("x2"));

                return string.Join("", textHash);
            }
        }
    }
}
