using System.Threading.Tasks;
using User.Domain.Enitites;

namespace User.Domain.Interfaces
{
    public interface IRegisterService
    {
        Task RegisterNewUser(Entities.User user);
        Task<Entities.User> Login(Login login);
    }
}
