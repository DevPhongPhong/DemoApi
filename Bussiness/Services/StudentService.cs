using Bussiness.DTOs;
using Bussiness.Interfaces;
using Repository;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class StudentService : IStudentService, IUserService<Student, int>
    {
    
        public Dictionary<DayOfWeek, List<StudyTime>> CreateSchedule(int id)
        {
            Dictionary<DayOfWeek, List<StudyTime>> res = new Dictionary<DayOfWeek, List<StudyTime>>();
            res.Add(DayOfWeek.Monday, new List<StudyTime>());
            res.Add(DayOfWeek.Tuesday, new List<StudyTime>());
            res.Add(DayOfWeek.Wednesday, new List<StudyTime>());
            res.Add(DayOfWeek.Thursday, new List<StudyTime>());
            res.Add(DayOfWeek.Friday, new List<StudyTime>());
            res.Add(DayOfWeek.Saturday, new List<StudyTime>());
            res.Add(DayOfWeek.Sunday, new List<StudyTime>());

            return res;
        }
    }
}
