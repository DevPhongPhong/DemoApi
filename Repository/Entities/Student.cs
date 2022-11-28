using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Student : User
    {
        public StudentLogin StudentLogin { get; set; }

        public List<NotJoinStudyTime> ListNotJoinStudyTime { get; set; }

        public List<Course> ListCourse { get; set; }

        public List<StudentTest> ListStudentTest { get; set; }
    }
}
