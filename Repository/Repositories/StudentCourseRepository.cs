using Common.Exceptions;
using Dapper;
using MySql.Data.MySqlClient;
using Repository.DTOs.Test;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            if (studentCourse == null) throw new NotFoundException<int>(id, studentCourse.GetType());
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

        public List<TestResult> GetListTestResult(int studentID, int courseID)
        {
            var query = from st in _dbContext.StudentTests
                                    join t in _dbContext.Tests
                                    on st.TestID equals t.ID
                                    where st.StudentID == studentID && t.CourseID == courseID
                                    select new TestResult
                                    {
                                        Score = st.Score,
                                        Percent = t.Percent
                                    };
            return query.ToList();
        }

        public List<int> GetListCourseId(int studentID)
        {
            var query = from sc in _dbContext.StudentCourses
                        where sc.StudentID == studentID
                        select sc.CourseID;
            return query.ToList();
        }
    }
}
