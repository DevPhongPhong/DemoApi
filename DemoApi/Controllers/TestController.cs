using Bussiness.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ITestService _ts;
        public TestController(ITestService ts)
        {
            _ts = ts;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var teacher = _ts.GetCourse().Teacher;
            return Ok(teacher);
        }
    }
}
