using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs
{
    public class CreateOrUpdateStudent
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Dob { get; set; }

        [Required]
        public int YearOfTrain { get; set; }
        [Required]
        public int SectorID { get; set; }
        [Required]
        public int TypeOfTrain { get; set; }
    }
}
