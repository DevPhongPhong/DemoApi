using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : IUserService
    {
        public int CountAge(DateTime Dob)
        {
            return DateTime.Now.Year - Dob.Year; 
        }
    }
}
