using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthServer.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpGet("ping")]
        public ActionResult Ping() =>
            Ok();            
    }
}
