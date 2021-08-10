using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDQUAN.Auth.API.IService;
using NDQUAN.Core.DL.IServices;
using NDQUAN.Core.Models.AuthModels;

namespace NDQUAN.Auth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        protected readonly IDatabaseService _db;
        //private readonly AppSettings _appSettings;

        public RegisterController(
            IUserService userService,
            IDatabaseService db
        )
        {
            _userService = userService;
            _db = db;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterModel user)
        {
            bool success = _userService.Register(user);
            return Ok(success);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public string Test()
        {
            return "OK";
        }
    }
}
