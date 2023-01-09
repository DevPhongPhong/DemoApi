using AutoMapper.Configuration.Conventions;
using Bussiness.DTOs.Admin;
using Bussiness.DTOs.Course;
using Bussiness.DTOs.Student;
using Bussiness.DTOs.Teacher;
using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Bussiness.Services;
using Common.Filters.AuthorizeFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAdminLoginService _adminLoginService;
        private readonly ITeacherService _teacherService;
        private readonly ITeacherLoginService _teacherloginService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IStudentCourseService _studentCourseService;
        private readonly IStudyTimeService _studyTimeService;
        private readonly INotJoinStudyTimeService _notJoinStudyTimeService;
        private readonly ITestService _testService;
        private readonly IStudentTestService _studentTestService;

        public AdminController(IAdminService adminService,
            IAdminLoginService adminLoginService,
            ITeacherService teacherService,
            ITeacherLoginService teacherloginService,
            ICourseService courseService,
            IStudentService studentService,
            IStudentCourseService studentCourseService,
            IStudyTimeService studyTimeService,
            INotJoinStudyTimeService notJoinStudyTimeService,
            ITestService testService,
            IStudentTestService studentTestService)
        {
            _adminService = adminService;
            _adminLoginService = adminLoginService;
            _teacherService = teacherService;
            _teacherloginService = teacherloginService;
            _courseService = courseService;
            _studentService = studentService;
            _studentCourseService = studentCourseService;
            _studyTimeService = studyTimeService;
            _notJoinStudyTimeService = notJoinStudyTimeService;
            _testService = testService;
            _studentTestService = studentTestService;
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult CreateCourse(CourseCreateOrUpdate courseCreate)
        {
            var course = new Course
            {
                BeginDate = courseCreate.BeginDate,
                MaxStudent = courseCreate.MaxStudent,
                Name = courseCreate.Name,
                Price = courseCreate.Price,
                TeacherID = courseCreate.TeacherID
            };
            if (_courseService.Create(course) == 1) return Ok(course);
            else return BadRequest(course);
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
            if (_teacherService.Create(teacher) == 1)
            {
                var teacherLogin = new TeacherLogin
                {
                    Username = "teacher" + teacher.ID,
                    Password = "1",
                    TeacherID = teacher.ID
                };
                if (_teacherloginService.Create(teacherLogin) == 1)
                    return Ok(teacher);
                else
                {
                    return BadRequest(teacher);
                }
            }
            else return BadRequest(teacher);
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult AdminRegister(AdminRegisterOrUpdate adminRegister)
        {
            var admin = new Admin
            {
                ID = adminRegister.ID,
                Address = adminRegister.UserDetail.Address,
                CCCD = adminRegister.UserDetail.CCCD,
                DOB = adminRegister.UserDetail.DOB,
                Email = adminRegister.UserDetail.Email,
                Name = adminRegister.UserDetail.Name,
                PhoneNumber = adminRegister.UserDetail.PhoneNumber
            };
            if (_adminService.Create(admin) == 1)
            {
                var adminLogin = new AdminLogin
                {
                    Username = "admin" + admin.ID,
                    Password = "1",
                    AdminID = admin.ID
                };
                if (_adminLoginService.Create(adminLogin) == 1)
                    return Ok(admin);
                else
                {
                    return BadRequest(admin);
                }
            }
            else return BadRequest(admin);
        }

        [HttpPut]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult UpdateAdminDetail(AdminRegisterOrUpdate adminDetail)
        {
            var id = int.Parse(HttpContext.Request.Headers["id"]);
            var oldAdmin = _adminService.Get(id);
            var admin = new Admin
            {
                ID = id,
                Address = adminDetail.UserDetail.Address,
                CCCD = adminDetail.UserDetail.CCCD,
                DOB = adminDetail.UserDetail.DOB,
                Email = adminDetail.UserDetail.Email,
                Name = adminDetail.UserDetail.Name,
                PhoneNumber = adminDetail.UserDetail.PhoneNumber
            };
            if (_adminService.Update(admin) == 1) return Ok(adminDetail);
            else return BadRequest(adminDetail);
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult ChangeCourse(CourseCreateOrUpdate courseCreateOrUpdate)
        {
            var newCourse = new Course
            {
                BeginDate = courseCreateOrUpdate.BeginDate,
                MaxStudent = courseCreateOrUpdate.MaxStudent,
                ID = courseCreateOrUpdate.Id,
                Name = courseCreateOrUpdate.Name,
                Price = courseCreateOrUpdate.Price,
                TeacherID = courseCreateOrUpdate.TeacherID,
                TotalPeriods = courseCreateOrUpdate.TotalPeriods
            };

            var res = _courseService.Create(newCourse);
            if (res == 1) return Ok(newCourse);
            else return BadRequest(newCourse);
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult ChangeTeacherWorkDetail(TeacherWorkDetail teacherWorkDetail)
        {
            var teacher = new Teacher
            {
                ID = teacherWorkDetail.Id,
                WorkBegin = teacherWorkDetail.WorkBegin,
                WorkEnd = teacherWorkDetail.WorkEnd,
                Salary = teacherWorkDetail.Salary
            };
            var res = _teacherService.Update(teacher);
            if (res != 1) return BadRequest(teacherWorkDetail);
            else return Ok(teacherWorkDetail);    
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult ChangeTeacherStatus(int id)
        {
            try
            {
                var res = _teacherService.ChangeStatus(id);
                if (res == -1) return BadRequest("error");
                else return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult ChangeStudentStatus(int id)
        {
            try
            {
                var res = _studentService.ChangeStatus(id);
                if (res == -1) return BadRequest("error");
                else return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _courseService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _studentService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteTeacher(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _courseService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteStudentCourse(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _studentCourseService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteStudyTime(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _studyTimeService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteNotJoinStudyTime(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _notJoinStudyTimeService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteTest(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _testService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteStudentTest(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _studentTestService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                return Ok(new { Id = id, res = _adminService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
