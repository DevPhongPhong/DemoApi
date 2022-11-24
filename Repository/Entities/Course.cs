﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public short MaxStudent { get; set; }

        [Required]
        public int TeacherID { get; set; }

        public Teacher Teacher { get; set; }

        public List<StudyTime> ListStudyTime { get; set; }

        public List<Test> ListTest { get; set; }

        public List<Student> ListStudent { get; set; }

        
    }
}
