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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DemoDBContext _dbContext;

        public TeacherRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Teacher Get(int id)
        {
            Teacher teacher = _dbContext.Teachers.Find(id);
            if (teacher == null) throw new IdNotFoundException<int>(id, teacher.GetType());
            return teacher;
        }

        public List<Teacher> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM teachers T
                           WHERE T.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listTeacher = connection.Query<Teacher>(query).ToList();
            return listTeacher;
        }

        public int Create(Teacher entity)
        {
            _dbContext.Teachers.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Teacher newEntity)
        {
            Teacher oldEntity = Get(newEntity.ID);

            oldEntity.Name = newEntity.Name;
            oldEntity.DOB = newEntity.DOB;
            oldEntity.CCCD = newEntity.CCCD;
            oldEntity.Address = newEntity.Address;
            oldEntity.Status = newEntity.Status;
            oldEntity.Email = newEntity.Email;
            oldEntity.PhoneNumber = newEntity.PhoneNumber;
            oldEntity.WorkBegin = newEntity.WorkBegin;
            oldEntity.WorkEnd = newEntity.WorkEnd;
            oldEntity.Salary = newEntity.Salary;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            Teacher entity = Get(id);
            _dbContext.Teachers.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
