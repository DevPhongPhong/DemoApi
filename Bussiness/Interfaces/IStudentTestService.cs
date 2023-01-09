using Bussiness.DTOs.Test;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IStudentTestService:IBaseService<StudentTest,int>
    {
        int UpdateScore(ChangeTestScore changeTestScore);
    }
}
