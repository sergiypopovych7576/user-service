using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Service.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
