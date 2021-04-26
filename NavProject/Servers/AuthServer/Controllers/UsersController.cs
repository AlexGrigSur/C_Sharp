using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AuthServer.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddUser() =>
            Ok();

        [HttpPost]
        public ActionResult EditUser() =>
            Ok();

        [HttpPost]
        public ActionResult ChangePassword() =>
            Ok();
    }
}
