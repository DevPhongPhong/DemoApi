using Common.Exceptions;
using Dapper;
using MySql.Data.MySqlClient;
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
            if (teacher == null) throw new NotFoundException<int>(id, teacher.GetType());
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

        public Teacher GetByEmail(string email)
        {
            try
            {
                var teacher = _dbContext.Teachers.Where(s => s.Email == email).FirstOrDefault();
                return teacher;
            }
            catch(ArgumentNullException)
            {
                throw new NotFoundException<string>(email, new Teacher().GetType());
            }
        }

        public Teacher GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var teacher = _dbContext.Teachers.Where(s => s.PhoneNumber == phoneNumber).FirstOrDefault();
                return teacher;
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException<string>(phoneNumber, new Teacher().GetType());
            }
        }

        public List<TaskTime> GetListTaskTime(int id)
        {
            try
            {
                using var conn = new MySqlConnection(Global.Global.ConnectionString);
                var param = new DynamicParameters();
                param.Add("@teacherID", id);
                var res = conn.Query<TaskTime>("GetTeacherTaskTime",param, commandType: CommandType.StoredProcedure).ToList();
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

        public List<Course> GetListCourse(int id)
        {
            try
            {
                var res = _dbContext.Courses.Where(c => c.TeacherID == id).ToList();
                return res;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public int ChangeStatus(int id)
        {
            var teacher = _dbContext.Teachers.Find(id);
            if (teacher == null) return -1;
            teacher.Status = !teacher.Status;
            return _dbContext.SaveChanges();
        }

        public List<Teacher> GetAll()
        {
            return _dbContext.Teachers.ToList();
        }
    }
}
