using Common.Exceptions;
using Dapper;
using MySqlConnector;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private DemoDBContext _dbContext;

        public CourseRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course Get(int id)
        {
            var course = _dbContext.Courses.Find(id);
            if (course == null) throw new IdNotFoundException<int>(id, course.GetType());
            return course;
        }

        public List<Course> Get(List<int> ids)
        {
            var query = @"SELECT * 
                            FROM courses C
                           WHERE C.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listCourse = connection.Query<Course>(query).ToList();
            return listCourse;
        }

        public int Create(Course course)
        {
            _dbContext.Courses.Add(course);
            return _dbContext.SaveChanges();
        }

        public int Update(Course newCourse)
        {
            var oldCourse = Get(newCourse.ID);

            oldCourse.Name = newCourse.Name;
            oldCourse.BeginDate = newCourse.BeginDate;
            oldCourse.EndDate = newCourse.EndDate;
            oldCourse.MaxStudent= newCourse.MaxStudent;
            oldCourse.TeacherID = newCourse.TeacherID;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var course = Get(id);
            _dbContext.Courses.Remove(course);
            return _dbContext.SaveChanges();
        }

    }
}
