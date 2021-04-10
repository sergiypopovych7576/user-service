using System.Threading.Tasks;
using User.Domain.DTO;

namespace User.Service.Services.Token
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(LoginDto login);
    }
}
