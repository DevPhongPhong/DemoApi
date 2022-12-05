using Bussiness.DTOs;
using Bussiness.DTOs.ScoreBoard;
using Bussiness.DTOs.Student;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IStudentService : IBaseService<Student, int>, IUserService<Student, int>
    {
        ScoreBoardOfStudent GetScoreBoardOfStudent(int studentID);
    }
}
