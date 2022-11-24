using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class NotJoinStudyTime
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int StudyTimeID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public bool Allowed { get; set; }

        public Student Student { get; set; }

        public StudyTime StudyTime { get; set; }
    }
}
