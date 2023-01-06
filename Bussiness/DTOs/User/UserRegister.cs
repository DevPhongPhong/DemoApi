using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.User
{
    public class UserRegister
    {
        public int ID { get; set; }
        public UserDetail UserDetail { get; set; }
    }
}
