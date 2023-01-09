using AutoMapper.Configuration.Conventions;
using Bussiness.DTOs.Admin;
using Bussiness.DTOs.Course;
using Bussiness.DTOs.Student;
using Bussiness.DTOs.Teacher;
using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Bussiness.Services;
using Common.Filters.AuthorizeFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System;

namespace DemoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(HyperAuthorizeFilter))]
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
        private readonly IStudentLoginService _studentLoginService;

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
            IStudentTestService studentTestService,
            IStudentLoginService studentLoginService)
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
            _studentLoginService = studentLoginService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(new { Detail = _studentService.GetAll(), LoginDetail = _studentLoginService.GetAll() });
        }

        [HttpGet]
        public IActionResult GetStudent(int studentId)
        {
            return Ok(new { Detail = _studentService.Get(studentId), LoginDetail = _studentLoginService.Get(studentId) });
        }

        [HttpGet]
        public IActionResult GetAdmins()
        {
            return Ok(new { Detail = _adminService.GetAll(), LoginDetail = _adminLoginService.GetAll() });
        }

        [HttpGet]

        public IActionResult GetAdmin(int adminId)
        {
            return Ok(new { Detail = _adminService.Get(adminId), LoginDetail = _adminLoginService.Get(adminId) });
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok(_courseService.GetAll());
        }

        [HttpGet]
        public IActionResult GetCourse(int courseId)
        {
            return Ok(new
            {
                course = _courseService.Get(courseId),
                listStudent = _courseService.GetListStudent(courseId),
                listScore = _courseService.GetScoreBoardOfCourse(courseId)
            });
        }

        [HttpGet]
        public IActionResult GetStudyTimes(int courseId)
        {
            return Ok(_courseService.GetListStudyTime(courseId));
        }

        [HttpGet]
        public IActionResult GetNotJoin(int studyTimeId)
        {
            return Ok(_studyTimeService.GetNotJoin(studyTimeId));
        }

        [HttpGet]
        public IActionResult GetTests()
        {
            return Ok(_testService.GetAll());
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            return Ok(_teacherService.GetAll());
        }

        [HttpPost]
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

        [HttpPut]
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

        [HttpPut]
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

        [HttpPut]
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

        [HttpPut]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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

        [HttpDelete]
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
