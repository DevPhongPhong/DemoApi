using Bussiness.DTOs;
using Bussiness.Interfaces;
using Repository;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentJoinCourseRepository _studentJoinCourseRepository;

        public StudentService(IStudentRepository studentRepository,ICourseRepository courseRepository, IStudentJoinCourseRepository studentJoinCourseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentJoinCourseRepository = studentJoinCourseRepository;
        }

        public Student GetByID(int id)
        {
            return _studentRepository.GetByID(id);
        }

        public int CreateStudent(CreateOrUpdateStudent model)
        {
            try
            {
                //var student = new Student
                //{
                //    Name = model.Name,
                //    Dob = model.Dob,

                //};
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateStudent(CreateOrUpdateStudent model)
        {
            try
            {
                //var newStudent = new Student
                //{
                //    ID = model.ID,
                //    Name = model.Name,
                //    Dob = model.Dob,
                //    SectorID = model.SectorID,
                //    TypeOfTrain = model.TypeOfTrain,
                //    YearOfTrain = model.YearOfTrain
                //};
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeteleStudentByID(int id)
        {
            try
            {
                var student = _studentRepository.GetByID(id);
                return _studentRepository.DeleteStudent(student);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public string GetStudentCode(Student student)
        {
            return ""/*+student.YearOfTrain + student.TypeOfTrain + student.SectorID + student.ID*/;
        }

        public List<Course> GetListCourseJoined(Student student, int status = -1)
        {
            var res = _studentJoinCourseRepository.GetListCourseFromStudentID(student.ID);
            return res;
        }

        #region Private Method

        #endregion
    }
}
