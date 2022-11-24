﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(12)]
        public string CCCD { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public StudentLogin StudentLogin { get; set; }

        public List<NotJoinStudyTime> ListNotJoinStudyTime { get; set; }

        public List<Course> ListCourse { get; set; }

        public List<StudentTest> ListStudentTest { get; set; }
    }
}
