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
    public class TestRepository : ITestRepository
    {
        private readonly DemoDBContext _dbContext;
        public TestRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Test Get(int id)
        {
            Test test = _dbContext.Tests.Find(id);
            if (test == null) throw new NotFoundException<int>(id, test.GetType());
            return test;
        }

        public List<Test> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM tests S
                           WHERE S.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listTest = connection.Query<Test>(query).ToList();
            return listTest;
        }

        public int Create(Test entity)
        {
            _dbContext.Tests.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Test newEntity)
        {
            Test oldEntity = Get(newEntity.ID);

            oldEntity.StartAt = newEntity.StartAt;
            oldEntity.Time = newEntity.Time;
            oldEntity.Percent = newEntity.Percent;

            return _dbContext.SaveChanges();
        }
        
        public int Delete(int id)
        {
            Test entity = Get(id);
            _dbContext.Tests.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
