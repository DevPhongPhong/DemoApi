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
    public class StudentLoginRepository:IStudentLoginRepository
    {
        private readonly DemoDBContext _dbContext;

        public StudentLoginRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StudentLogin Get(int studentID)
        {
            StudentLogin sl = _dbContext.StudentLogins.Find(studentID);
            if (sl == null) throw new IdNotFoundException<int>(studentID, sl.GetType());
            return sl;
        }

        public List<StudentLogin> Get(List<int> studentIDs)
        {
            string query = @"SELECT * 
                            FROM studentlogins SL
                           WHERE SL.ID in (";
            foreach (var id in studentIDs) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listStudentLogin = connection.Query<StudentLogin>(query).ToList();
            return listStudentLogin;
        }

        public int Create(StudentLogin entity)
        {
            _dbContext.StudentLogins.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(StudentLogin newEntity)
        {
            StudentLogin oldEntity = Get(newEntity.StudentID);
            oldEntity.Password = newEntity.Password;
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            StudentLogin entity = Get(id);
            _dbContext.StudentLogins.Remove(entity);
            return _dbContext.SaveChanges();
        }

    }
}
