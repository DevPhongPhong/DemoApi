using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Repositories;

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

        public DemoTestController(ICourseRepository courseRepository,
        INotJoinStudyTimeRepository notJoinStudyTimeRepository,
        IStudentCourseRepository studentCourseRepository,
        IStudentLoginRepository studentLoginRepository,
        IStudentRepository studentRepository,
        IStudentTestRepository studentTestRepository,
        IStudyTimeRepository studyTimeRepository,
        ITeacherLoginRepository teacherLoginRepository,
        ITeacherRepository teacherRepository,
        ITestRepository testRepository)
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
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_teacherRepository.Get(2));
        }
    }
}
