using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.Domain.Interfaces;
using User.Service.Models.User;
using User.Service.Services.UserSign;

namespace User.Service.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserSignService _userSignService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IUserSignService userSignService,
            IMapper mapper)
        {
            _userService = userService;
            _userSignService = userSignService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task Register(RegisterModel model)
        {
            var user = _mapper.Map<Domain.Entities.User>(model);

            await _userService.RegisterNew(user);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public Task<string> Login(LoginModel model)
        {
            return _userSignService.GenerateToken(model);
        }
    }
}
