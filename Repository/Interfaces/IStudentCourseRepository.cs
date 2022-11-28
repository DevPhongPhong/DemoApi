using Repository.DTOs.Test;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStudentCourseRepository : IBaseRepository<StudentCourse, int>
    {
        List<TestResult> GetListTestResult(int studentID, int courseID);
    }
}
