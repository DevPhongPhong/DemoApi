using Bussiness.DTOs.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.Teacher
{
    public class TeacherRegister : UserRegister
    {
        public TeacherWorkDetail TeacherWorkDetail { get;set; } 
    }
}
