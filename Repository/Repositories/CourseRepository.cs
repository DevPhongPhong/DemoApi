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
        private readonly DemoDBContext _dbContext;

        public CourseRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course Get(int id)
        {
            Course course = _dbContext.Courses.Find(id);
            if (course == null) throw new NotFoundException<int>(id, course.GetType());
            return course;
        }

        public List<Course> Get(List<int> ids)
        {
            string query = @"SELECT * 
                            FROM courses C
                           WHERE C.ID in (";
            foreach (var id in ids) query += (id + ",");
            query = query.Substring(0, query.Length - 1) + ")";

            using var connection = new MySqlConnection(Global.Global.ConnectionString);
            var listCourse = connection.Query<Course>(query).ToList();
            return listCourse;
        }

        public int Create(Course entity)
        {
            _dbContext.Courses.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Course newEntity)
        {
            Course oldEntity = Get(newEntity.ID);

            oldEntity.Name = newEntity.Name;
            oldEntity.BeginDate = newEntity.BeginDate;
            oldEntity.MaxStudent = newEntity.MaxStudent;
            oldEntity.TeacherID = newEntity.TeacherID;

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            Course entity = Get(id);
            _dbContext.Courses.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public List<int> GetListStudentID(int courseID)
        {
            try
            {
                var list = _dbContext.StudentCourses.Where(sc => sc.CourseID == courseID).Select(sc => sc.ID).ToList();
                return list;
            }
            catch
            {
                return null;
            }
        }

        public List<Student> GetListStudent(int courseID)
        {
            var list = from sc in _dbContext.StudentCourses
                       join s in _dbContext.Students
                       on sc.StudentID equals s.ID
                       where sc.CourseID == courseID
                       select s;
            return list.ToList();
        }

        public List<StudyTime> GetListStudyTime(int courseID)
        {
            try
            {
                var list = _dbContext.StudyTimes.Where(st => st.CourseID == courseID).ToList();
                return list;
            }
            catch
            {
                return null;
            }
        }

        public List<Course> GetAll()
        {
            return _dbContext.Courses.ToList();
        }
    }
}
