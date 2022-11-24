using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TestRepository : ITestRepository
    {
        private DemoDBContext _dbContext;
        public TestRepository(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course GetCourse()
        {
            var res = _dbContext.Courses.Find(1);
            return res;
        }
    }
}
