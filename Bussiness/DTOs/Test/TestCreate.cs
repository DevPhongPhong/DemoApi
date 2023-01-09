using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Test
{
    public class TestCreate
    {
        [Required]
        public int CourseID { get; set; }

        [Required]
        public short Time { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public short Percent { get; set; }

        public List<int> StudentIDs { get; set; }
    }
}
