using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.Domain.Enitites;
using User.Domain.Interfaces;
using User.Service.Models.User;
using User.Service.ViewModels.User;

namespace User.Service.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        public UserController(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task Register(RegisterModel model)
        {
            var user = _mapper.Map<Domain.Entities.User>(model);

            await _registerService.RegisterNewUser(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<UserViewModel> Login(LoginModel model)
        {
            var login = _mapper.Map<Login>(model);

            var user = await _registerService.Login(login);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
