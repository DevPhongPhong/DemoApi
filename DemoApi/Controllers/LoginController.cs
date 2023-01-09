using Bussiness.DTOs.Student;
using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;

namespace DemoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAdminLoginService _adminLoginService;
        private readonly ITeacherLoginService _teacherloginService;
        private readonly IStudentService _studentService;
        private readonly IStudentLoginService _studentLoginService;

        public LoginController(
            IAdminLoginService adminLoginService,
            ITeacherLoginService teacherloginService,
            IStudentService studentService,
            IStudentLoginService studentLoginService)
        {
            _adminLoginService = adminLoginService;
            _teacherloginService = teacherloginService;
            _studentService = studentService;
            _studentLoginService = studentLoginService;
        }

        [HttpPost]
        public IActionResult AdminLogin(Login login)
        {
            int id = -1;
            _adminLoginService.CheckLogin(login, out id);
            if (id != -1)
            {
                var token = _adminLoginService.CreateToken(login.Username, id);
                return Ok(token);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult StudentLogin(Login login)
        {
            int id = -1;
            _studentLoginService.CheckLogin(login, out id);
            if (id != -1)
            {
                var token = _studentLoginService.CreateToken(login.Username, id);
                return Ok(token);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult StudentRegister(StudentRegister studentRegister)
        {
            var student = new Student
            {
                ID = studentRegister.ID,
                Address = studentRegister.UserDetail.Address,
                CCCD = studentRegister.UserDetail.CCCD,
                DOB = studentRegister.UserDetail.DOB,
                Email = studentRegister.UserDetail.Email,
                Name = studentRegister.UserDetail.Name,
                PhoneNumber = studentRegister.UserDetail.PhoneNumber
            };
            if (_studentService.Create(student) == 1) return Ok(student);
            else return BadRequest(student);
        }

        [HttpPost]
        public IActionResult TeacherLogin(Login login)
        {
            _teacherloginService.CheckLogin(login, out int id);
            if (id != -1)
            {
                var token = _teacherloginService.CreateToken(login.Username, id);
                return Ok(token);
            }
            else return NotFound();
        }

    }
}
