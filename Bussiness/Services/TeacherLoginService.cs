using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class TeacherLoginService : ITeacherLoginService
    {
        private readonly ITeacherLoginRepository _teacherLogin;
        private readonly ITeacherRepository _teacher;
        public TeacherLoginService(ITeacherLoginRepository teacherLogin, ITeacherRepository teacher)
        {
            _teacherLogin = teacherLogin;
            _teacher = teacher;
        }
        public int ChangePassword(int id, string oldPass, string newPass)
        {
            return _teacherLogin.ChangePassword(id, oldPass, newPass);
        }
        public bool CheckLogin(Login login)
        {
            return _teacherLogin.GetByUsernamePassword(login.Username, login.Password) == null;
        }
        public int ForgotPassword(string emailOrPhoneNumber)
        {
            bool check = true;
            foreach (char c in emailOrPhoneNumber)
            {
                if (c < '0' || c > '9')
                {
                    check = false;
                    break;
                }
            }
            Teacher sl = new Teacher();
            if (check)
            {
                sl = _teacher.GetByPhoneNumber(emailOrPhoneNumber);
            }
            else
            {
                sl = _teacher.GetByEmail(emailOrPhoneNumber);
            }
            return 0;
        }
    }
}
