using Bussiness.DTOs;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IStudentService
    {
        Student GetByID(int id);
        int CreateStudent(CreateOrUpdateStudent model);
        int UpdateStudent(CreateOrUpdateStudent model);
        int DeteleStudentByID(int id);
        string GetStudentCode(Student student);
        List<Course> GetListCourseJoined(Student student,int status = -1);
    }
}
