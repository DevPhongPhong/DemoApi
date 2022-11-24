using Bussiness.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class TestService : ITestService
    {
        private ITestRepository _tr;

        public TestService(ITestRepository tr)
        {
            _tr = tr;
        }

        public Course GetCourse()
        {
            return _tr.GetCourse();
        }
    }
}
