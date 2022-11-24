using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBaseRepository<TEntity,TId>
    {
        TEntity Get(TId id);
        List<TEntity> Get(List<TId> ids);
        int Create(TEntity entity);
        int Update(TEntity newEntity);
        int Delete(TId id);
    }
}
