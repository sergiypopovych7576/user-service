using System.Threading.Tasks;
using User.Service.Models.User;

namespace User.Service.Services.UserSign
{
    public interface IUserSignService
    {
        public Task<string> GenerateToken(LoginModel loginModel);
    }
}
