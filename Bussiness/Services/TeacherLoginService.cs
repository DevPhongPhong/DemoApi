using Bussiness.DTOs.User;
using Bussiness.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class TeacherLoginService : ITeacherLoginService
    {
        private readonly ITeacherLoginRepository _teacherLogin;
        private readonly ITeacherRepository _teacher;
        private readonly IConfiguration _configuration;

        public TeacherLoginService(ITeacherLoginRepository teacherLogin, ITeacherRepository teacher,IConfiguration configuration)
        {
            _teacherLogin = teacherLogin;
            _teacher = teacher;
            _configuration = configuration;
        }
        public int ChangePassword(int id, string oldPass, string newPass)
        {
            return _teacherLogin.ChangePassword(id, oldPass, newPass);
        }
        public bool CheckLogin(Login login, out int id)
        {
            var teacher = _teacherLogin.GetByUsernamePassword(login.Username, login.Password);
            if (teacher != null)
            {
                id = teacher.TeacherID;
                return true;
            }
            else
            {
                id = -1;
                return false;
            }
        }

        public int Create(TeacherLogin entity)
        {
            return _teacherLogin.Create(entity);
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
                    new Claim(ClaimTypes.Role, "teacher")
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int Delete(int id)
        {
            return _teacherLogin.Delete(id);
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

        public TeacherLogin Get(int id)
        {
            return _teacherLogin.Get(id);
        }

        public List<TeacherLogin> Get(List<int> ids)
        {
            return _teacherLogin.Get(ids);
        }

        public int Update(TeacherLogin newEntity)
        {
            return _teacherLogin.Update(newEntity);
        }
    }
}
