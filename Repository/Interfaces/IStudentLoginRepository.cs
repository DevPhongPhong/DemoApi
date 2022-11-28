using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Repository.Interfaces
{
    public interface IStudentLoginRepository : IBaseRepository<StudentLogin, int>, ILoginRepository<StudentLogin, int>
    {
        
    }
}
