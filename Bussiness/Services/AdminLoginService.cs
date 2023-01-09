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
    public class AdminLoginService : IAdminLoginService
    {
        private readonly IAdminLoginRepository _adminLogin;
        private readonly IAdminRepository _admin;
        private readonly IConfiguration _configuration;

        public AdminLoginService(IAdminLoginRepository adminLogin, IAdminRepository admin, IConfiguration configuration)
        {
            _adminLogin = adminLogin;
            _admin = admin;
            _configuration = configuration;
        }
        public int ChangePassword(int id, string oldPass, string newPass)
        {
            return _adminLogin.ChangePassword(id, oldPass, newPass);
        }
        public bool CheckLogin(Login login, out int id)
        {
            var admin = _adminLogin.GetByUsernamePassword(login.Username, login.Password);
            if (admin != null)
            {
                id = admin.AdminID;
                return true;
            }
            else
            {
                id = -1;
                return false;
            }
        }

        public int Create(AdminLogin entity)
        {
            return _adminLogin.Create(entity);
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
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int Delete(int id)
        {
            return _adminLogin.Delete(id);
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
            Admin sl = new Admin();
            if (check)
            {
                sl = _admin.GetByPhoneNumber(emailOrPhoneNumber);
            }
            else
            {
                sl = _admin.GetByEmail(emailOrPhoneNumber);
            }
            return 0;
        }

        public AdminLogin Get(int id)
        {
            return _adminLogin.Get(id);
        }

        public List<AdminLogin> Get(List<int> ids)
        {
            return _adminLogin.Get(ids);
        }

        public List<AdminLogin> GetAll()
        {
            return _adminLogin.GetAll();
        }

        public int Update(AdminLogin newEntity)
        {
            return _adminLogin.Update(newEntity);
        }
    }
}
