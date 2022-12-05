using Repository.DTOs.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Student
{
    public class StudentCourseResult
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int CourseID { get;set; }
        public string CourseName { get; set; }
        public List<TestResult> ListTestResult { get; set; }
    }
}
