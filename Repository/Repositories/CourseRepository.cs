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

        public Course GetByID(int id)
        {
            try
            {
                var res = _dbContext.Courses.Find(id);
                return res;
            }
            catch
            {
                return null;
            }
        }


        public int CreateCourse(Course course)
        {
            _dbContext.Courses.Add(course);
            return _dbContext.SaveChanges();
        }

        public int UpdateCourse(Course course)
        {
            var old = _dbContext.Courses.Find(course.ID);
            old.Name = course.Name;
            old.StartTime = course.StartTime;
            old.EndTime = course.EndTime;
            old.BeginDate = course.BeginDate;
            old.EndDate = course.EndDate;
            old.TestTime = course.TestTime;

            return _dbContext.SaveChanges();
        }

        public int DeleteCourse(Course course)
        {
            var listStudentJoinCourses = _dbContext.StudentJoinCourses.Where(x => x.CourseID == course.ID).ToList();
            foreach (var item in listStudentJoinCourses)
            {
                _dbContext.StudentJoinCourses.Remove(item);
            }
            _dbContext.Courses.Remove(course);

            return _dbContext.SaveChanges();
        }
    }
}
