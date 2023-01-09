using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface INotJoinStudyTimeRepository : IBaseRepository<NotJoinStudyTime, int>
    {
        List<NotJoinStudyTime> GetByStudyTime(int studyTimeId);
    }
}
