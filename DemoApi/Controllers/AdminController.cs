using Bussiness.DTOs.Student;
using Bussiness.DTOs.Teacher;
using Bussiness.Services;
using Common.Filters.AuthorizeFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly TeacherService _teacherService;
        public AdminController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult TeacherRegister(TeacherRegister teacherRegister)
        {
            var teacher = new Teacher
            {
                ID = teacherRegister.ID,
                Address = teacherRegister.UserDetail.Address,
                CCCD = teacherRegister.UserDetail.CCCD,
                DOB = teacherRegister.UserDetail.DOB,
                Email = teacherRegister.UserDetail.Email,
                Name = teacherRegister.UserDetail.Name,
                PhoneNumber = teacherRegister.UserDetail.PhoneNumber,
                Salary = teacherRegister.TeacherWorkDetail.Salary,
                WorkBegin = teacherRegister.TeacherWorkDetail.WorkBegin,
                WorkEnd = teacherRegister.TeacherWorkDetail.WorkEnd
            };
            if (_teacherService.Create(teacher) == 1) return Ok(teacher);
            else return BadRequest(teacher);
        }
    }
}
