using Bussiness.DTOs.Course;
using Bussiness.DTOs.Student;
using Bussiness.Interfaces;
using Repository;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
        public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository, IStudentCourseRepository studentCourseRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _studentCourseRepository = studentCourseRepository;
        }
        public List<Student> GetListStudent(int courseID)
        {
            var list = _courseRepository.GetListStudent(courseID);
            return list;
        }

        public List<StudyTime> GetSchedule(int courseID)
        {
            throw new NotImplementedException();
        }

        public ScoreBoardOfCourse GetScoreBoard(int courseID)
        {
            var course = _courseRepository.Get(courseID);
            var listStudent = _courseRepository.GetListStudent(courseID);
            List<StudentCourseResult> listStudentCourseResult = new List<StudentCourseResult>();
            foreach (var item in listStudent)
            {
                var obj = new StudentCourseResult
                {
                    ID = item.ID,
                    Name = item.Name,
                    ListTestResult = _studentCourseRepository.GetListTestResult(item.ID, courseID)
                };
                listStudentCourseResult.Add(obj);
            }
            return new ScoreBoardOfCourse
            {
                ID = courseID,
                ListStudentCourseResult = listStudentCourseResult,
                Name = course.Name
            };
        }

        public Teacher GetTeacher(int courseID)
        {
            return _teacherRepository.Get(_courseRepository.Get(courseID).TeacherID);
        }
    }
}