using System.Threading.Tasks;
using User.Domain.DTO;

namespace User.Domain.Interfaces
{
    public interface IUserService
    {
        Task RegisterNew(Entities.User user);
        Entities.User Login(LoginDto login);
    }
}
