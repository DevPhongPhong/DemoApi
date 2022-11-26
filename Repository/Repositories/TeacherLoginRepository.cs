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
    public class TeacherLoginRepository : ITeacherLoginRepository
    {
        private readonly DemoDBContext _dbContext;

        public TeacherLoginRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TeacherLogin Get(int studentID)
        {
            TeacherLogin sl = _dbContext.TeacherLogins.Find(studentID);
            if (sl == null) throw new IdNotFoundException<int>(studentID, sl.GetType());
            return sl;
        }

        public List<TeacherLogin> Get(List<int> studentIDs)
        {
            string query = @"SELECT * 
                            FROM teachers SL
                           WHERE SL.ID in (";
            foreach (var id in studentIDs) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listTeacherLogin = connection.Query<TeacherLogin>(query).ToList();
            return listTeacherLogin;
        }

        public int Create(TeacherLogin entity)
        {
            _dbContext.TeacherLogins.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(TeacherLogin newEntity)
        {
            TeacherLogin oldEntity = Get(newEntity.TeacherID);
            oldEntity.Password = newEntity.Password;
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            TeacherLogin entity = Get(id);
            _dbContext.TeacherLogins.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
