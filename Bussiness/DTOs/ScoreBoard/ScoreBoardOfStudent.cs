using Bussiness.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.ScoreBoard
{
    public class ScoreBoardOfStudent
    {
        public int StudentID { get; set; } 
        public string StudentName { get; set; }   
        public List<StudentCourseResult> ListStudentCourseResult { get; set; }
    }
}
