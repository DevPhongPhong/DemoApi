using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICourseRepository
    {
        Course GetByID(int id);
        int CreateCourse(Course course);
        int UpdateCourse(Course course);
        int DeleteCourse(Course course);
    }
}
