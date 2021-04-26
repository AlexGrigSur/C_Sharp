using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route("Token")]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public ActionResult GetToken() =>
            Ok();

        [HttpPost()]
        public ActionResult RefreshToken() =>
            Ok();

        [HttpPost]
        public ActionResult ChangePassword() =>
            Ok();
    }
}
