using Microsoft.AspNetCore.Mvc;
using NDQUAN.Core.Models.AuthModels;
using NDQUAN.Core.Common;
using Microsoft.AspNetCore.Authorization;

namespace NDQUAN.Auth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    { 
        public TestController()
        {
        }

        [HttpGet("test-log-es")]
        [AllowAnonymous]
        public bool TestLogES()
        {
            var res = LogManager.LogES();
            return res != null && res.IsValid;
        }
    }
}
