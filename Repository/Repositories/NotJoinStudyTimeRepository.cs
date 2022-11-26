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
    public class NotJoinStudyTimeRepository : INotJoinStudyTimeRepository
    {
        private readonly DemoDBContext _dbContext;

        public NotJoinStudyTimeRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public NotJoinStudyTime Get(int id)
        {
            NotJoinStudyTime njst = _dbContext.NotJoinStudyTimes.Find(id);
            if (njst == null) throw new IdNotFoundException<int>(id, njst.GetType());
            return njst;
        }

        public List<NotJoinStudyTime> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM notjoinstudytimes NJST
                           WHERE NJST.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listNotJoinStudyTime = connection.Query<NotJoinStudyTime>(query).ToList();
            return listNotJoinStudyTime;
        }

        public int Create(NotJoinStudyTime entity)
        {
            _dbContext.NotJoinStudyTimes.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(NotJoinStudyTime newEntity)
        {
            NotJoinStudyTime oldEntity = Get(newEntity.ID);

            oldEntity.StudyTimeID = newEntity.StudyTimeID;
            oldEntity.StudentID = newEntity.StudentID;
            oldEntity.Allowed = newEntity.Allowed;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            NotJoinStudyTime entity = Get(id);
            _dbContext.NotJoinStudyTimes.Remove(entity);
            return _dbContext.SaveChanges();
        }

    }
}
