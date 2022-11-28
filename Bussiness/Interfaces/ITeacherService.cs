using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface ITeacherService: IBaseService<Teacher, int>, IUserService<Teacher, int>
    {
    }
}
