using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Bussiness.Services
{
    public class StudentLoginService : IStudentLoginService
    {
        private readonly IStudentLoginRepository _studentLogin;
        private readonly IStudentRepository _student;
        private readonly IConfiguration _configuration;

        public StudentLoginService(IStudentLoginRepository studentLogin, IStudentRepository student, IConfiguration configuration)
        {
            _studentLogin = studentLogin;
            _student = student;
            _configuration = configuration;
        }
        public int ChangePassword(int id, string oldPass, string newPass)
        {
            return _studentLogin.ChangePassword(id, oldPass, newPass);
        }
        public bool CheckLogin(Login login,out int id)
        {
            var student = _studentLogin.GetByUsernamePassword(login.Username, login.Password);
            if (student != null)
            {
                id = student.StudentID;
                return true;
            }
            else
            {
                id = -1;
                return false;
            }
        }

        public int Create(StudentLogin entity)
        {
            return _studentLogin.Create(entity);
        }

        public string CreateToken(string username, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecretKey = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id",id+""),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "student")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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

        public List<StudentLogin> GetAll()
        {
            return _studentLogin.GetAll();
        }
    }
}
