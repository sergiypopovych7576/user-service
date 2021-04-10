using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.Controllers;

namespace User.Service.API.Controllers
{
    [AllowAnonymous]
    [Route("alive")]
    public class AliveController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "Alive";
        }
    }
}
