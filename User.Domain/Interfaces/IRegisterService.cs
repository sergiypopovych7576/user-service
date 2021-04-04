using System.Threading.Tasks;

namespace User.Domain.Interfaces
{
    public interface IRegisterService
    {
        Task RegisterNewUser(Entities.User user);
    }
}
