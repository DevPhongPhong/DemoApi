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
    public class StudentTestRepository : IStudentTestRepository
    {
        private readonly DemoDBContext _dbContext;
        public StudentTestRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public StudentTest Get(int id)
        {
            StudentTest studentTest = _dbContext.StudentTests.Find(id);
            if (studentTest == null) throw new IdNotFoundException<int>(id, studentTest.GetType());
            return studentTest;
        }

        public List<StudentTest> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM studenttests S
                           WHERE S.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listStudentTest = connection.Query<StudentTest>(query).ToList();
            return listStudentTest;
        }

        public int Create(StudentTest entity)
        {
            _dbContext.StudentTests.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(StudentTest newEntity)
        {
            StudentTest oldEntity = Get(newEntity.ID);

            oldEntity.Score = newEntity.Score;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            StudentTest entity = Get(id);
            _dbContext.StudentTests.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
