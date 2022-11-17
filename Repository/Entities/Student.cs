using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Student
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public int YearOfTrain { get; set; }
        [Required]
        public int SectorID { get; set; }
        [Required]
        public int TypeOfTrain { get; set; }
    }
}
