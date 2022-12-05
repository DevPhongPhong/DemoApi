using Bussiness.DTOs.ScoreBoard;
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

        public int Create(Course entity)
        {
            return _courseRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _courseRepository.Delete(id);
        }

        public Course Get(int id)
        {
            return _courseRepository.Get(id);
        }

        public List<Course> Get(List<int> ids)
        {
            return _courseRepository.Get(ids);
        }

        public List<Student> GetListStudent(int courseID)
        {
            var list = _courseRepository.GetListStudent(courseID);
            return list;
        }

        public List<StudyTime> GetListStudyTime(int courseID)
        {
            return _courseRepository.GetListStudyTime(courseID);
        }

        public ScoreBoardOfCourse GetScoreBoardOfCourse(int courseID)
        {
            var course = _courseRepository.Get(courseID);
            var listStudent = _courseRepository.GetListStudent(courseID);
            var teacher = _teacherRepository.Get(course.TeacherID);
            List<StudentCourseResult> listStudentCourseResult = new List<StudentCourseResult>();
            foreach (var item in listStudent)
            {
                var obj = new StudentCourseResult
                {
                    StudentID = item.ID,
                    StudentName = item.Name,
                    ListTestResult = _studentCourseRepository.GetListTestResult(item.ID, courseID),
                    CourseID = courseID,
                    CourseName = course.Name
                };
                listStudentCourseResult.Add(obj);
            }
            return new ScoreBoardOfCourse
            {
                CourseID = courseID,
                ListStudentCourseResult = listStudentCourseResult,
                CourseName = course.Name,
                TeacherID = course.TeacherID,
                TeacherName = teacher.Name
            };
        }

        public Teacher GetTeacher(int courseID)
        {
            return _teacherRepository.Get(_courseRepository.Get(courseID).TeacherID);
        }

        public int Update(Course newEntity)
        {
            return _courseRepository.Update(newEntity);
        }
    }
}