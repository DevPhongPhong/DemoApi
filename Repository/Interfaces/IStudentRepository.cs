using Repository.DTOs;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStudentRepository
    {
        Student GetByID(int id);
        int CreateStudent(Student student);
        int UpdateStudent(Student student);
        int DeleteStudent(Student student);
    }
}
