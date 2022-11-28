using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ILoginRepository<TEntity, TId>
    {
        TEntity GetByUsernamePassword(string username, string password);
        int ChangePassword(TId id, string oldPass, string newPass);
    }
}
