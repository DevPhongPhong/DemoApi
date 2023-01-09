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
    public class StudyTimeRepository : IStudyTimeRepository
    {
        private readonly DemoDBContext _dbContext;
        public StudyTimeRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public StudyTime Get(int id)
        {
            StudyTime studentTest = _dbContext.StudyTimes.Find(id);
            if (studentTest == null) throw new NotFoundException<int>(id, studentTest.GetType());
            return studentTest;
        }

        public List<StudyTime> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM studytimes S
                           WHERE S.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listStudyTime = connection.Query<StudyTime>(query).ToList();
            return listStudyTime;
        }

        public int Create(StudyTime entity)
        {
            _dbContext.StudyTimes.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(StudyTime newEntity)
        {
            StudyTime oldEntity = Get(newEntity.ID);

            oldEntity.Status = newEntity.Status;
            oldEntity.StartTime = newEntity.StartTime;
            oldEntity.EndTime = newEntity.EndTime;
            oldEntity.Date = newEntity.Date;
    
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            StudyTime entity = Get(id);
            _dbContext.StudyTimes.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int ChangeStatus(int studyTimeID)
        {
            var studyTime = Get(studyTimeID);
            studyTime.Status = !studyTime.Status;
            return _dbContext.SaveChanges();
        }
    }
}
