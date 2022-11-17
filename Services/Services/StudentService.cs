using Entities;
using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class StudentService : UserService, IStudentService
    {
        private readonly DemoDBContext _dbContext;
        public StudentService(DemoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Student GetByID(int ID)
        {
            var res = _dbContext.Students.FirstOrDefault(x => x.ID == ID);
            return res;
        }
    }
}
