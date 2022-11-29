using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Test
    {
        [Key]
        [Required]
        public int ID { get; set; }
        
        [Required]
        public int CourseID { get; set; }
        
        [Required]
        public short Time { get; set; }
        
        [Required]
        public DateTime StartAt { get; set; }
        
        [Required]
        public short Percent { get; set; }

    }
}
