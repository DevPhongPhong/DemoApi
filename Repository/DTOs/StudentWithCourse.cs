using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs
{
    public class StudentWithCourse
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }
}
