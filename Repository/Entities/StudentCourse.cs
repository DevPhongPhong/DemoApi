using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class StudentCourse
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }
    }
}
