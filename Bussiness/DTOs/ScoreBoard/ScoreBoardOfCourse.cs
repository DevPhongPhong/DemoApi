using Bussiness.DTOs.Student;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.ScoreBoard
{
    public class ScoreBoardOfCourse
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public List<StudentCourseResult> ListStudentCourseResult { get; set; }
    }
}
