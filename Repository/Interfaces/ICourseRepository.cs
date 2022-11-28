using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course,int>
    {
        List<int> GetListStudentID(int courseID);
        List<Student> GetListStudent(int courseID);
        List<StudyTime> GetListStudyTime(int courseID);
    }
}
