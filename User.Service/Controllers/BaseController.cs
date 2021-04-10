using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Service.Controllers
{

    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
