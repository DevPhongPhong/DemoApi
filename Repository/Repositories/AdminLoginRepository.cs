using Common.Exceptions;
using Dapper;
using MySql.Data.MySqlClient;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AdminLoginRepository : IAdminLoginRepository
    {
        private readonly DemoDBContext _dbContext;

        public AdminLoginRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AdminLogin Get(int adminID)
        {
            AdminLogin sl = _dbContext.AdminLogins.Find(adminID);
            if (sl == null) throw new NotFoundException<int>(adminID, sl.GetType());
            return sl;
        }

        public List<AdminLogin> Get(List<int> adminIDs)
        {
            string query = @"SELECT * 
                            FROM adminlogins SL
                           WHERE SL.ID in (";
            foreach (var id in adminIDs) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listAdminLogin = connection.Query<AdminLogin>(query).ToList();
            return listAdminLogin;
        }

        public int Create(AdminLogin entity)
        {
            _dbContext.AdminLogins.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(AdminLogin newEntity)
        {
            AdminLogin oldEntity = Get(newEntity.AdminID);
            oldEntity.Password = newEntity.Password;
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            AdminLogin entity = Get(id);
            _dbContext.AdminLogins.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public AdminLogin GetByUsernamePassword(string username, string password)
        {
            try
            {
                var adminLogin = _dbContext.AdminLogins
                    .Where(sl => sl.Username == username && sl.Password == password)
                    .FirstOrDefault();
                return adminLogin;
            }
            catch
            {
                return null;
            }

        }

        public int ChangePassword(int id, string oldPass, string newPass)
        {
            var adminLogin = _dbContext.AdminLogins.Find(id);

            if (adminLogin == null)
            {
                throw new NotFoundException<int>(id, new AdminLogin().GetType());
            }

            if (adminLogin.Password != oldPass)
            {
                throw new WrongPassword();
            }

            adminLogin.Password = newPass;

            return _dbContext.SaveChanges();
        }

        public List<AdminLogin> GetAll()
        {
            return _dbContext.AdminLogins.ToList();
        }
    }
}
