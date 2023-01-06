using Bussiness.DTOs.User;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IUserService<TEnity,TId>
    {
        DEMOSchedule<TEnity,TId> GetSchedule(TId id);
        List<Course> GetListCourse(TId id);
    }
}
