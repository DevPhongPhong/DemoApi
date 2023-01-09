using Bussiness.Interfaces;
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
    public class StudentCourseService : IStudentCourseService
    {
        readonly IStudentCourseRepository _studentCourseRepository;
        public StudentCourseService(IStudentCourseRepository studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }
        public int Create(StudentCourse entity)
        {
            return _studentCourseRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _studentCourseRepository.Delete(id);
        }

        public StudentCourse Get(int id)
        {
            return _studentCourseRepository.Get(id);
        }

        public List<StudentCourse> Get(List<int> ids)
        {
            return _studentCourseRepository.Get(ids);
        }

        public int Update(StudentCourse newEntity)
        {
            return _studentCourseRepository.Update(newEntity);
        }

        public List<StudentCourse> GetAll()
        {
            return _studentCourseRepository.GetAll();
        }
    }
}
