using Bussiness.DTOs.ScoreBoard;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface ICourseService:IBaseService<Course,int>
    {
        List<Student> GetListStudent(int courseID);
        List<StudyTime> GetListStudyTime(int courseID);
        Teacher GetTeacher(int courseID);
        ScoreBoardOfCourse GetScoreBoardOfCourse(int courseID);
    }
}
