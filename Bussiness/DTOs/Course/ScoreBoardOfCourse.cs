﻿using Bussiness.DTOs.Student;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Course
{
    public class ScoreBoardOfCourse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<StudentCourseResult> ListStudentCourseResult { get; set; }
    }
}