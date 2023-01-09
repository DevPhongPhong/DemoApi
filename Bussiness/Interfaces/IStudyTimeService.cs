using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IStudyTimeService:IBaseService<StudyTime,int>
    {
        int ChangeStatus(int studyTimeID);
        List<NotJoinStudyTime> GetNotJoin(int studyTimeId);
    }
}
