using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Repository.Interfaces
{
    public interface IAdminRepository:IBaseRepository<Admin,int>
    {
        Admin GetByEmail(string email);
        Admin GetByPhoneNumber(string email);
    }
}
