using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Course
{
    public class CourseCreateOrUpdate
    {
        public int Id { get; set; }
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
        public decimal Price { get; set; }

        [Required]
        public int TeacherID { get; set; }

        [Required]
        public short TotalPeriods { get; set; }

    }
}
