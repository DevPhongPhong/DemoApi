using Bussiness.DTOs.NotJoinStudyTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.StudyTime
{
    public class CompleteStudyTime
    {
        public int StudyTimeID { get; set; }
        public NotJoinStudent[] NotJoinStudents { get; set; }
    }
}
