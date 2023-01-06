using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;
using Common.Filters.AuthorizeFilters;
using Bussiness.DTOs.Teacher;
using Repository.Entities;

namespace DemoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherLoginService _teacherloginService;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;

        public TeacherController(ITeacherLoginService teacherloginService,
            ITeacherService teacherService,
            ICourseService courseService)
        {
            _teacherloginService = teacherloginService;
            _teacherService = teacherService;
            _courseService = courseService;

        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            _teacherloginService.CheckLogin(login, out int id);
            if (id != -1)
            {
                var token = _teacherloginService.CreateToken(login.Username, id);
                return Ok(token);
            }
            else return NotFound();
        }

        [HttpGet]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult GetSchedule()
        {
            var id = int.Parse(HttpContext.Request.Headers["id"][0]);
            var schedule = _teacherService.GetSchedule(id);
            return Ok(schedule);
        }

        [HttpGet]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult GetListCourse()
        {
            var id = int.Parse(HttpContext.Request.Headers["id"][0]);
            var listCourse = _teacherService.GetListCourse(id);
            return Ok(listCourse);
        }

        [HttpGet]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult GetScoreBoard(int courseId)
        {
            var id = int.Parse(HttpContext.Request.Headers["id"][0]);
            if (_courseService.HasTeacherID(courseId, id))
                return BadRequest("Teacher doesnt has this course!");

            var scoreBoard = _courseService.GetScoreBoardOfCourse(courseId);
            return Ok(scoreBoard);
        }
        [HttpPut]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult UpdateUserDetail(UserDetail userDetail)
        {
            var id = int.Parse(HttpContext.Request.Headers["id"]);
            var teacher = new Teacher
            {
                ID = id,
                Address = userDetail.Address,
                CCCD = userDetail.CCCD,
                DOB = userDetail.DOB,
                Email = userDetail.Email,
                Name = userDetail.Name,
                PhoneNumber = userDetail.PhoneNumber
            };
            if (_teacherService.Update(teacher) == 1) return Ok(userDetail);
            else return BadRequest(userDetail);
        }
        [HttpPut]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult UpdateWorkDetail(TeacherWorkDetail teacherWorkDetail)
        {
            var id = int.Parse(HttpContext.Request.Headers["id"]);
            var teacher = new Teacher
            {
                ID = id,
                Salary = teacherWorkDetail.Salary,
                WorkBegin = teacherWorkDetail.WorkBegin,
                WorkEnd = teacherWorkDetail.WorkEnd
            };
            if (_teacherService.Update(teacher) == 1) return Ok(teacherWorkDetail);
            else return BadRequest(teacherWorkDetail);
        }
    }
}
