using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.StudyTime
{
    public class StudyTimeCreate
    {
        public int CourseID { get; set; }
        public Time[] ListTime { get; set; }
    }
}
