﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Teacher
{
    public class TeacherWorkDetail
    {
        [Required]
        public DateTime WorkBegin { get; set; }

        [Required]
        public DateTime WorkEnd { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
