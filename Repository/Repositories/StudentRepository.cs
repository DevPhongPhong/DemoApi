using Common.Exceptions;
using Dapper;
using MySqlConnector;
using Repository.DTOs.User;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DemoDBContext _dbContext;
        public StudentRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Student Get(int id)
        {
            Student student = _dbContext.Students.Find(id);
            if (student == null) throw new NotFoundException<int>(id, student.GetType());
            return student;
        }

        public List<Student> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM students S
                           WHERE S.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listStudent = connection.Query<Student>(query).ToList();
            return listStudent;
        }

        public int Create(Student entity)
        {
            _dbContext.Students.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Student newEntity)
        {
            Student oldEntity = Get(newEntity.ID);

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
            Student entity = Get(id);
            _dbContext.Students.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Student GetByEmail(string email)
        {
            try
            {
                var student = _dbContext.Students.Where(s => s.Email == email).FirstOrDefault();
                return student;
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException<string>(email, new Student().GetType());
            }
        }

        public Student GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var student = _dbContext.Students.Where(s => s.PhoneNumber == phoneNumber).FirstOrDefault();
                return student;
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException<string>(phoneNumber, new Student().GetType());
            }
        }

        public List<TaskTime> GetListTaskTime(int id)
        {
            try
            {
                using var conn = new MySqlConnection(Global.Global.ConnectionString);
                var param = new DynamicParameters();
                param.Add("@studentID", id);
                var res = conn.Query<TaskTime>("GetStudentTaskTime",param,commandType:CommandType.StoredProcedure).ToList();
                return res;
            }
            catch
            {
                return null;
            }
        }

        public List<TaskTime> GetListTaskTime(int id, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
