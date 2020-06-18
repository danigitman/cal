using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("~/v1/authorization/token")]
        [AllowAnonymous]
     
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
