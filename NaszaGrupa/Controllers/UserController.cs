using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaszaGrupa.Models;
using NaszaGrupa.Services;


namespace ProductsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IdUser _userService;

        public UsersController(IdUser userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Nazwka użytkownika lub hasło jest błędne" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}