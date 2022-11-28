using Repository.DTOs.User;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student, int>, IUserRepository<Student, int>
    {
        
    }
}
