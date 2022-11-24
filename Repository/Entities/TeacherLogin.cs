using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class TeacherLogin
    {
        [Key]
        public int TeacherID { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        public Teacher Teacher { get; set; }
    }
}
