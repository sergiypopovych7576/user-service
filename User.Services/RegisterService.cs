using Services.Shared.Domain.Interfaces;
using Services.Shared.Services;
using Services.Shared.Services.Hash;
using System;
using System.Threading.Tasks;
using User.Domain.Interfaces;

namespace User.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IWriteRepository<Domain.Entities.User> _userRepo;
        private readonly IHashService _hashService;

        public RegisterService(IWriteRepository<Domain.Entities.User> userRepo,
            IHashService hashService)
        {
            _userRepo = userRepo;
            _hashService = hashService;
        }

        public async Task RegisterNewUser(Domain.Entities.User user)
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

            await _userRepo.Create(user);
        }
    }
}
