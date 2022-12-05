using Bussiness.DTOs;
using Bussiness.DTOs.ScoreBoard;
using Bussiness.DTOs.Student;
using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Repository;
using Repository.DTOs.User;
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
    public class StudentService : IStudentService
    {
        readonly IStudentRepository _studentRepository;
        readonly IStudentCourseRepository _studentCourseRepository;
        readonly ICourseRepository _courseRepository;
        readonly IStudyTimeRepository _studyTimeRepository;

        public StudentService(IStudentRepository studentRepository
            , IStudentCourseRepository studentCourseRepository
            , ICourseRepository courseRepository
            , IStudyTimeRepository studyTimeRepository)
        {
            _studentRepository = studentRepository;
            _studentCourseRepository = studentCourseRepository;
            _courseRepository = courseRepository;
            _studyTimeRepository = studyTimeRepository;
        }
        public int Create(Student entity)
        {
            return _studentRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _studentRepository.Delete(id);
        }

        public Student Get(int id)
        {
            return _studentRepository.Get(id);
        }

        public List<Student> Get(List<int> ids)
        {
            return _studentRepository.Get(ids);
        }

        public DEMOSchedule<Student, int> GetSchedule(int studentId)
        {
            var student = _studentRepository.Get(studentId);
            var listTaskTime = _studentRepository.GetListTaskTime(studentId);
            var res = new DEMOSchedule<Student, int>();

            foreach(var item in listTaskTime)
            {
                res.Data[item.Date.DayOfWeek].Add(item);
            }
            res.User = student;

            return res;
        }

        public ScoreBoardOfStudent GetScoreBoardOfStudent(int studentID)
        {
            var student = _studentRepository.Get(studentID);
            var listCourseId = _studentCourseRepository.GetListCourseId(studentID);
            var listCourseResult = new List<StudentCourseResult>();
            foreach (var courseId in listCourseId)
            {
                var course = _courseRepository.Get(courseId);
                var listTestResult = _studentCourseRepository
                    .GetListTestResult(studentID, courseId);
                var obj = new StudentCourseResult
                {
                    CourseID = courseId,
                    CourseName = course.Name,
                    ListTestResult = listTestResult
                };
                listCourseResult.Add(obj);
            }
            return new ScoreBoardOfStudent
            {
                ListStudentCourseResult = listCourseResult,
                StudentID = studentID,
                StudentName = student.Name
            };
        }

        public int Update(Student newEntity)
        {
            return _studentRepository.Update(newEntity);
        }
    }
}
