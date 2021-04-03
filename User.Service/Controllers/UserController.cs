using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Service.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "UP";
        }
    }
}
