using Services.Shared.Services;
using Services.Shared.Services.Hash;
using Services.Shared.Services.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.DTO;
using User.Domain.Exceptions;
using User.Domain.Interfaces;

namespace User.Services
{
    public class UserService : IUserService
    {
        private readonly IReadRepository<Domain.Entities.User> _userReadRepo;
        private readonly IWriteRepository<Domain.Entities.User> _userWriteRepo;
        private readonly IHashService _hashService;

        public UserService(IReadRepository<Domain.Entities.User> userReadRepo,
            IWriteRepository<Domain.Entities.User> userWriteRepo,
            IHashService hashService)
        {
            _userReadRepo = userReadRepo;
            _userWriteRepo = userWriteRepo;
            _hashService = hashService;
        }

        public Domain.Entities.User Login(LoginDto login)
        {
            Guard.AgainstNull(login);
            Guard.AgainstNull(login.Email);
            Guard.AgainstNull(login.Password);

            var hashPassword = _hashService.HashText(login.Password);

            var user = _userReadRepo.Get().FirstOrDefault(user =>
                user.Email == login.Email && user.Password == hashPassword);

            if (user == null)
            {
                throw new UnAuthorizedException("Invalid email or password");
            }

            return user;
        }

        public async Task RegisterNew(Domain.Entities.User user)
        {
            Guard.AgainstNull(user);

            Guard.IsNull(user.Id);

            Guard.AgainstNull(user.Name);
            Guard.MinLength(user.Name, 4);
            Guard.MaxLength(user.Name, 30);

            Guard.AgainstNull(user.Email);
            Guard.IsValidEmail(user.Email);

            Guard.AgainstNull(user.Password);
            Guard.MinLength(user.Password, 6);

            user.Id = Guid.NewGuid();
            user.Password = _hashService.HashText(user.Password);

            await _userWriteRepo.Create(user);
        }
    }
}
