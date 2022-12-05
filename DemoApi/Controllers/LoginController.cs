using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IStudentLoginService _studentloginService;
        private readonly IAuthorizeService _authorizeService;
        public LoginController(IStudentLoginService studentloginService, IAuthorizeService authorizeService)
        {
            _studentloginService = studentloginService;
            _authorizeService = authorizeService;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (_studentloginService.CheckLogin(login))
            {
                var token = _authorizeService.CreateToken(login);
                return Ok(token);
            }
            else return NotFound();
        }
    }
}
