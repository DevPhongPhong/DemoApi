using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class StudentTest
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int TestID { get; set; }

        [Required]
        public float Score { get; set; }

    }
}
