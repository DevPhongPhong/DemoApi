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
using Bussiness.DTOs.StudyTime;

namespace DemoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherLoginService _teacherloginService;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        private readonly IStudyTimeService _studyTimeService;
        private readonly INotJoinStudyTimeService _notJoinStudyTimeService;

        public TeacherController(ITeacherLoginService teacherloginService,
            ITeacherService teacherService,
            ICourseService courseService,
            IStudyTimeService studyTimeService,
            INotJoinStudyTimeService notJoinStudyTimeService)
        {
            _teacherloginService = teacherloginService;
            _teacherService = teacherService;
            _courseService = courseService;
            _studyTimeService = studyTimeService;
            _notJoinStudyTimeService = notJoinStudyTimeService;
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
            var oldTeacher = _teacherService.Get(id);
            var teacher = new Teacher
            {
                ID = id,
                Address = userDetail.Address,
                CCCD = userDetail.CCCD,
                DOB = userDetail.DOB,
                Email = userDetail.Email,
                Name = userDetail.Name,
                PhoneNumber = userDetail.PhoneNumber,
                Salary = oldTeacher.Salary,
                Status = oldTeacher.Status,
                WorkBegin = oldTeacher.WorkBegin,
                WorkEnd = oldTeacher.WorkEnd
            };
            if (_teacherService.Update(teacher) == 1) return Ok(userDetail);
            else return BadRequest(userDetail);
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult CreateStudyTime(StudyTimeCreate studyTimeCreate)
        {
            try
            {
                var teacherID = int.Parse(HttpContext.Request.Headers["id"]);
                if (!_courseService.HasTeacherID(studyTimeCreate.CourseID, teacherID)) return BadRequest("This teacher do not has courseID " + studyTimeCreate.CourseID);

                var course = _courseService.Get(studyTimeCreate.CourseID);
                foreach (var item in studyTimeCreate.ListTime)
                {
                    var studyTime = new StudyTime
                    {
                        CourseID = studyTimeCreate.CourseID,
                        Date = item.Date,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        Status = true
                    };
                    _studyTimeService.Create(studyTime);
                }
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(HyperAuthorizeFilter))]
        public IActionResult CompleteStudyTime(CompleteStudyTime completeStudyTime)
        {
            try
            {
                var teacherID = int.Parse(HttpContext.Request.Headers["id"]);
                var studyTime = _studyTimeService.Get(completeStudyTime.StudyTimeID);
                var course = _courseService.Get(studyTime.CourseID);
                if (course.TeacherID != teacherID) return BadRequest("This teacher do not has courseID " + studyTime.CourseID);

                if (_studyTimeService.ChangeStatus(completeStudyTime.StudyTimeID) != 1) return BadRequest();

                try
                {
                    foreach (var item in completeStudyTime.NotJoinStudents)
                    {
                        var notJoinStudyTime = new NotJoinStudyTime
                        {
                            StudentID = item.StudentID,
                            Allowed = item.Allowed,
                            StudyTimeID = completeStudyTime.StudyTimeID
                        };
                        _notJoinStudyTimeService.Create(notJoinStudyTime);
                    }
                }
                catch
                {

                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
