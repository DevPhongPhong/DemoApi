using Dapper;
using MySqlConnector;
using Repository.DTOs;
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
        /// <summary>
        /// Get Student By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student</returns>
        /// <exception cref="Exception">Not found student</exception>
        public Student GetByID(int id)
        {
            try
            {
                var res = _dbContext.Students.Find(id);
                return res;
            }
            catch
            {
                return null;
            }
        }

        public int CreateStudent(Student student)
        {
            _dbContext.Students.Add(student);
            return _dbContext.SaveChanges();
        }

        public int UpdateStudent(Student student)
        {
            var old = _dbContext.Students.Find(student.ID);
            old.Name = student.Name;
            old.Dob = student.Dob;
            old.SectorID = student.SectorID;
            old.TypeOfTrain = student.TypeOfTrain;
            old.YearOfTrain = student.YearOfTrain;

            return _dbContext.SaveChanges();
        }

        public int DeleteStudent(Student student)
        {
            _dbContext.Students.Remove(student);
            return _dbContext.SaveChanges();
        }
    }
}
