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
    public class TestService : ITestService
    {
        readonly ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public int Create(Test entity)
        {
            return _testRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _testRepository.Delete(id);
        }

        public Test Get(int id)
        {
            return _testRepository.Get(id);
        }

        public List<Test> Get(List<int> ids)
        {
            return _testRepository.Get(ids);
        }

        public int Update(Test newEntity)
        {
            return _testRepository.Update(newEntity);
        }

        public List<Test> GetAll()
        {
            return _testRepository.GetAll();
        }
    }
}
