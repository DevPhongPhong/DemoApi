using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.NotJoinStudyTime
{
    public class NotJoinStudent
    {
        public int StudentID { get; set; }
        public bool Allowed { get; set; }
    }
}
