using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentJoinCourseRepository : IStudentJoinCourseRepository
    {
        private DemoDBContext _dbContext;

        public StudentJoinCourseRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Course> GetListCourseFromStudentID(int id, int status = -1)
        {
            var list = _dbContext.StudentJoinCourses.Where(x => x.StudentID == id).ToList();
            if (status != -1)
            {
                list = list.Where(x => x.Status == (status == 1 ? true : false)).ToList();
            }

            var res = new List<Course>();
            foreach (var item in list)
            {
                res.Add(_dbContext.Courses.Find(item.CourseID));
            }

            return res;
        }
    }
}
