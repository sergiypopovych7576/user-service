using System.Threading.Tasks;
using User.Domain.Enitites;

namespace User.Domain.Interfaces
{
    public interface IUserService
    {
        Task RegisterNew(Entities.User user);
        Task<Entities.User> Login(Login login);
    }
}
