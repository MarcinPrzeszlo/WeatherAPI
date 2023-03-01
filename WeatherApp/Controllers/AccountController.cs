using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;
using WeatherApp.Models;


namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountControler : Controller
    {
        private readonly IAccountService _accountService;
        public AccountControler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Login([FromBody] RegisterDto dto)
        {
            var isLogged = _accountService.Register(dto);
            if (!isLogged)
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login() 
        {
            var dto = new LoginDto();
            return View("Login");
        }
        
        [HttpPost]
        public IActionResult Logged([FromBody] LoginDto dto)
        {
            //odp na token
            var jwtToken = _accountService.GenerateJwt(dto);
            return View(jwtToken);
        }
    }
}
