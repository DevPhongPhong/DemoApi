using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Filters.AuthorizeFilters;
using Repository.Entities;
using Bussiness.DTOs.Student;
using System;

namespace DemoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(HyperAuthorizeFilter))]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLoginService _studentloginService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public StudentController(IStudentLoginService studentloginService,
            ICourseService courseService,
            IStudentService studentService)
        {
            _studentloginService = studentloginService;
            _courseService = courseService;
            _studentService = studentService;

        }

        [HttpGet]        
        public IActionResult GetSchedule()
        {
            var id = int.Parse(HttpContext.Request.Headers["id"][0]);
            var schedule = _studentService.GetSchedule(id);
            return Ok(schedule);
        }

        [HttpGet]        
        public IActionResult GetListCourse()
        {
            var id = int.Parse(HttpContext.Request.Headers["id"][0]);
            var listCourse = _studentService.GetListCourse(id);
            return Ok(listCourse);
        }

        [HttpGet]        
        public IActionResult GetScoreBoard(int courseId)
        {
            var id = int.Parse(HttpContext.Request.Headers["id"][0]);
            if (_courseService.HasStudentID(courseId, id))
                return BadRequest("Student doesnt has this course!");

            var scoreBoard = _courseService.GetScoreBoardOfCourse(courseId);
            return Ok(scoreBoard);
        }

        [HttpGet]        
        public IActionResult GetUserDetail()
        {
            try
            {
                return Ok(_studentService.Get(int.Parse(HttpContext.Request.Headers["id"])));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]        
        public IActionResult UpdateUserDetail(UserDetail userDetail)
        {
            var id = int.Parse(HttpContext.Request.Headers["id"]);
            var oldStudent = _studentService.Get(id);
            var newStudent = new Student
            {
                ID = id,
                Address = userDetail.Address,
                CCCD = userDetail.CCCD,
                DOB = userDetail.DOB,
                Email = userDetail.Email,
                Name = userDetail.Name,
                PhoneNumber = userDetail.PhoneNumber,
                Status = oldStudent.Status
            };
            if (_studentService.Update(newStudent) == 1) return Ok(userDetail);
            else return BadRequest(userDetail);
        }

    }
}
