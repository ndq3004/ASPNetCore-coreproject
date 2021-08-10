using Microsoft.AspNetCore.Mvc;
using NDQUAN.Auth.API.Helpers;
using NDQUAN.Auth.API.IService;
using NDQUAN.Core.DL.IServices;
using NDQUAN.Core.DL.Services;
//using NDQUAN.Auth.API.IService;

namespace NDQUAN.Auth.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        protected readonly IDatabaseService _db;
        //private readonly AppSettings _appSettings;

        public UserController(
            IUserService userService,
            IDatabaseService db
        )
        {
            _userService = userService;
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            //return Ok(users);
            return Ok(users);
        }

        

    }
}
