using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Bussiness.Services
{
    public class StudentLoginService : IStudentLoginService
    {
        private readonly IStudentLoginRepository _studentLogin;
        private readonly IStudentRepository _student;
        public StudentLoginService(IStudentLoginRepository studentLogin, IStudentRepository student)
        {
            _studentLogin = studentLogin;
            _student = student;
        }
        public int ChangePassword(int id, string oldPass, string newPass)
        {
            return _studentLogin.ChangePassword(id, oldPass, newPass);
        }
        public bool CheckLogin(Login login)
        {
            return _studentLogin.GetByUsernamePassword(login.Username, login.Password) == null;
        }

        public int Create(StudentLogin entity)
        {
            return _studentLogin.Create(entity);
        }

        public int Delete(int id)
        {
            return _studentLogin.Delete(id);
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
            Student sl = new Student();
            if (check)
            {
                sl = _student.GetByPhoneNumber(emailOrPhoneNumber);
            }
            else
            {
                sl = _student.GetByEmail(emailOrPhoneNumber);
            }
            return 0;
        }

        public StudentLogin Get(int id)
        {
            return _studentLogin.Get(id);
        }

        public List<StudentLogin> Get(List<int> ids)
        {
            return _studentLogin.Get(ids);
        }

        public int Update(StudentLogin newEntity)
        {
            return _studentLogin.Update(newEntity);
        }
    }
}
