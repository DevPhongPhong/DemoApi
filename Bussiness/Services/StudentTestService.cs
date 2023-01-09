using Bussiness.DTOs.Test;
using Bussiness.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class StudentTestService : IStudentTestService
    {
        readonly IStudentTestRepository _studentTestRepository;
        public StudentTestService(IStudentTestRepository studentTestRepository)
        {
            _studentTestRepository = studentTestRepository;
        }
        public int Create(StudentTest entity)
        {
            return _studentTestRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _studentTestRepository.Delete(id);
        }

        public StudentTest Get(int id)
        {
            return _studentTestRepository.Get(id);
        }

        public List<StudentTest> Get(List<int> ids)
        {
            return _studentTestRepository.Get(ids);
        }

        public int Update(StudentTest newEntity)
        {
            return _studentTestRepository.Update(newEntity);
        }

        public List<StudentTest> GetAll()
        {
            return _studentTestRepository.GetAll();
        }

        public int UpdateScore(ChangeTestScore changeTestScore)
        {
            return _studentTestRepository.ChangeScore(changeTestScore.StudentTestID, changeTestScore.Score);
        }
    }
}
