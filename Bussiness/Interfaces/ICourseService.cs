using Bussiness.DTOs.Course;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface ICourseService
    {
        List<Student> GetListStudent(int courseID);
        List<StudyTime> GetSchedule(int courseID);
        Teacher GetTeacher(int courseID);
        ScoreBoardOfCourse GetScoreBoard(int courseID);
    }
}
