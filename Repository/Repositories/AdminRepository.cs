using Common.Exceptions;
using Dapper;
using MySql.Data.MySqlClient;
using Repository.DTOs.User;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DemoDBContext _dbContext;

        public AdminRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Admin Get(int id)
        {
            Admin admin = _dbContext.Admins.Find(id);
            if (admin == null) throw new NotFoundException<int>(id, admin.GetType());
            return admin;
        }

        public List<Admin> Get(List<int> ids)
        {
            string query = @"SELECT * 
                               FROM admins T
                              WHERE T.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query[..^1] + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listAdmin = connection.Query<Admin>(query).ToList();
            return listAdmin;
        }

        public int Create(Admin entity)
        {
            _dbContext.Admins.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Admin newEntity)
        {
            Admin oldEntity = Get(newEntity.ID);

            oldEntity.Name = newEntity.Name;
            oldEntity.DOB = newEntity.DOB;
            oldEntity.CCCD = newEntity.CCCD;
            oldEntity.Address = newEntity.Address;
            oldEntity.Status = newEntity.Status;
            oldEntity.Email = newEntity.Email;
            oldEntity.PhoneNumber = newEntity.PhoneNumber;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            Admin entity = Get(id);
            _dbContext.Admins.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Admin GetByEmail(string email)
        {
            try
            {
                var admin = _dbContext.Admins.Where(s => s.Email == email).FirstOrDefault();
                return admin;
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException<string>(email, new Admin().GetType());
            }
        }

        public Admin GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var admin = _dbContext.Admins.Where(s => s.PhoneNumber == phoneNumber).FirstOrDefault();
                return admin;
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException<string>(phoneNumber, new Admin().GetType());
            }
        }
    }
}
