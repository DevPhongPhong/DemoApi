using Repository.DTOs.User;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository<TEntity,TId>
    {
        TEntity GetByEmail(string email);
        TEntity GetByPhoneNumber(string email);
        List<TaskTime> GetListTaskTime(int studentId);
        List<TaskTime> GetListTaskTime(int studentId, DateTime from,DateTime to);
        List<Course> GetListCourse(TId id);
    }
}
