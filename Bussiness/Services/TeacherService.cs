using Bussiness.DTOs.User;
using Bussiness.Interfaces;
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
    public class TeacherService : ITeacherService
    {
        readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public int Create(Teacher entity)
        {
            return _teacherRepository.Create(entity);
        }
        public int Delete(int id)
        {
            return _teacherRepository.Delete(id);
        }

        public Teacher Get(int id)
        {
            return _teacherRepository.Get(id);
        }

        public List<Teacher> Get(List<int> ids)
        {
            return _teacherRepository.Get(ids);
        }

        public DEMOSchedule<Teacher, int> GetSchedule(int teacherId)
        {
            var teacher = _teacherRepository.Get(teacherId);
            var listTaskTime = _teacherRepository.GetListTaskTime(teacherId);
            var res = new DEMOSchedule<Teacher, int>();

            foreach (var item in listTaskTime)
            {
                res.Data[item.Date.DayOfWeek].Add(item);
            }
            res.User = teacher;
            return res;
        }

        public int Update(Teacher newEntity)
        {
            return _teacherRepository.Update(newEntity);
        }
    }
}
