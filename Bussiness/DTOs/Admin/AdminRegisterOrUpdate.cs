using Bussiness.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Admin
{
    public class AdminRegisterOrUpdate
    {
        public int ID { get; set; }
        public UserDetail UserDetail { get; set; }
    }
}
