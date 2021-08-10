using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDQUAN.Auth.API.IService;
using NDQUAN.Core.DL;
using NDQUAN.Core.Models.AuthModels;

namespace NDQUAN.Auth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            LogDB.LogDuration("get authen");
            var res = _authService.Authenticate(model);
            if(res == null)
            {
                return BadRequest(new { message = "Username or password is uncorrect!" });
            }
            res.SessionId = HttpContext.Session.Id;
            try
            {
                SessionManager.Add(res);
                //SessionManager.Get(res.UserId);
            }
            catch (System.Exception)
            {

            }
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public string Test()
        {
            return "OK";
        }
    }
}
