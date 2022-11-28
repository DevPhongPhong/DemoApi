using Repository.DTOs.User;
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
        List<TaskTime> GetListTaskTime(int id);
        List<TaskTime> GetListTaskTime(int id,DateTime from,DateTime to);
    }
}
