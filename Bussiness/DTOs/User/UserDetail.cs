using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.User
{
    public class UserDetail
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(12)]
        public string CCCD { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
    }
}
