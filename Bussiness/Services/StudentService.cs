using Bussiness.DTOs;
using Bussiness.Interfaces;
using Repository;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class StudentService : IStudentService
    {
        readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public int Create(Student entity)
        {
           return _studentRepository.Create(entity);    
        }

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

        public int Delete(int id)
        {
            return _studentRepository.Delete(id);
        }
            
        public Student Get(int id)
        {
            return _studentRepository.Get(id);
        }

        public List<Student> Get(List<int> ids)
        {
            return _studentRepository.Get(ids);
        }

        public int Update(Student newEntity)
        {
            return _studentRepository.Update(newEntity);
        }
    }
}
