using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class StudentJoinCourse
    {
        [Key]
        public int ID { get; set; }
        [Required] 
        public int CourseID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public float Score1 { get; set; }
        [Required]
        public float Score2 { get; set; }
        [Required]
        public float Score3 { get; set; }
    }
}
