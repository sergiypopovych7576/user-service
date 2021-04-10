using User.Domain.DTO;

namespace User.Service.Services.Token
{
    public interface ITokenService
    {
        public string GenerateToken(LoginDto login);
    }
}
