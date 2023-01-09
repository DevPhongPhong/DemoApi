using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IAdminLoginService: ILoginService<int>, IBaseService<AdminLogin, int>
    {
    }
}
