using Microsoft.EntityFrameworkCore;
using Services.Shared.Domain.Interfaces;
using Services.Shared.Services;
using Services.Shared.Services.Hash;
using System;
using System.Threading.Tasks;
using User.Domain.Enitites;
using User.Domain.Exceptions;
using User.Domain.Interfaces;

namespace User.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IReadRepository<Domain.Entities.User> _userReadRepo;
        private readonly IWriteRepository<Domain.Entities.User> _userWriteRepo;
        private readonly IHashService _hashService;

        public RegisterService(IReadRepository<Domain.Entities.User> userReadRepo,
            IWriteRepository<Domain.Entities.User> userWriteRepo,
            IHashService hashService)
        {
            _userReadRepo = userReadRepo;
            _userWriteRepo = userWriteRepo;
            _hashService = hashService;
        }

        public async Task<Domain.Entities.User> Login(Login login)
        {
            Guard.AgainstNull(login);
            Guard.AgainstNull(login.Email);
            Guard.AgainstNull(login.Password);

            var hashPassword = _hashService.HashText(login.Password);

            var user = await _userReadRepo.Get()
                .FirstOrDefaultAsync(user => user.Email == login.Email && user.Password == hashPassword);

            if (user == null)
            {
                throw new UnAuthorizedException("Invalid email or password");
            }

            return user;
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

            await _userWriteRepo.Create(user);
        }
    }
}
