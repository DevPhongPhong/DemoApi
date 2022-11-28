using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoTestController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly INotJoinStudyTimeRepository _notJoinStudyTimeRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IStudentLoginRepository _studentLoginRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentTestRepository _studentTestRepository;
        private readonly IStudyTimeRepository _studyTimeRepository;
        private readonly ITeacherLoginRepository _teacherLoginRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITestRepository _testRepository;

        private readonly ICourseService _courseService;
        private readonly INotJoinStudyTimeService _notJoinStudyTimeService;
        private readonly IStudentCourseService _studentCourseService;
        private readonly IStudentLoginService _studentLoginService;
        private readonly IStudentService _studentService;
        private readonly IStudentTestService _studentTestService;
        private readonly IStudyTimeService _studyTimeService;
        private readonly ITeacherLoginService _teacherLoginService;
        private readonly ITeacherService _teacherService;
        private readonly ITestService _testService;

        public DemoTestController(ICourseRepository courseRepository,
        INotJoinStudyTimeRepository notJoinStudyTimeRepository,
        IStudentCourseRepository studentCourseRepository,
        IStudentLoginRepository studentLoginRepository,
        IStudentRepository studentRepository,
        IStudentTestRepository studentTestRepository,
        IStudyTimeRepository studyTimeRepository,
        ITeacherLoginRepository teacherLoginRepository,
        ITeacherRepository teacherRepository,
        ITestRepository testRepository,

        ICourseService courseService,
        INotJoinStudyTimeService notJoinStudyTimeService,
        IStudentCourseService studentCourseService,
        IStudentLoginService studentLoginService,
        IStudentService studentService,
        IStudentTestService studentTestService,
        IStudyTimeService studyTimeService,
        ITeacherLoginService teacherLoginService,
        ITeacherService teacherService,
        ITestService testService)
        {
            _courseRepository = courseRepository;
            _notJoinStudyTimeRepository = notJoinStudyTimeRepository;
            _studentCourseRepository = studentCourseRepository;
            _studentLoginRepository = studentLoginRepository;
            _studentRepository = studentRepository;
            _studentTestRepository = studentTestRepository;
            _studyTimeRepository = studyTimeRepository;
            _teacherLoginRepository = teacherLoginRepository;
            _teacherRepository = teacherRepository;
            _testRepository = testRepository;

            _courseService = courseService;
            _notJoinStudyTimeService = notJoinStudyTimeService;
            _studentCourseService = studentCourseService;
            _studentLoginService = studentLoginService;
            _studentService = studentService;
            _studentTestService = studentTestService;
            _studyTimeService = studyTimeService;
            _teacherLoginService = teacherLoginService;
            _teacherService = teacherService;
            _testService = testService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //return Ok(_studentRepository.GetListTaskTime(1));
            return Ok(_teacherRepository.GetListTaskTime(1));

            //var list = new List<int>();
            //list.Add(1);
            //list.Add(2);
            //return Ok(_teacherRepository.Get(list));
        }
    }
}
