using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountService;
        public AccountController(IAccountServices accountService)
        {

            _accountService = accountService;

        }

        [Route("register")]
        [HttpPost]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
