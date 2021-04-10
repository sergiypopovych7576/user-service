using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.Domain.DTO;
using User.Domain.Interfaces;
using User.Service.Services.Token;

namespace User.Service.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ITokenService tokenService,
            IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task Register(RegisterDto register)
        {
            var user = _mapper.Map<Domain.Entities.User>(register);

            await _userService.RegisterNew(user);
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public string Token(LoginDto login)
        {
            return _tokenService.GenerateToken(login);
        }
    }
}
