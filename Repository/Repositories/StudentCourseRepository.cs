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
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly DemoDBContext _dbContext;
        public StudentCourseRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public StudentCourse Get(int id)
        {
            StudentCourse studentCourse = _dbContext.StudentCourses.Find(id);
            if (studentCourse == null) throw new IdNotFoundException<int>(id, studentCourse.GetType());
            return studentCourse;
        }

        public List<StudentCourse> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM studentcourses S
                           WHERE S.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listStudentCourse = connection.Query<StudentCourse>(query).ToList();
            return listStudentCourse;
        }

        public int Create(StudentCourse entity)
        {
            _dbContext.StudentCourses.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(StudentCourse newEntity)
        {
            StudentCourse oldEntity = Get(newEntity.ID);

            oldEntity.CourseID = newEntity.CourseID;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            StudentCourse entity = Get(id);
            _dbContext.StudentCourses.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
