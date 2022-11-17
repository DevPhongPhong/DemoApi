using Bussiness.DTOs;
using Bussiness.Interfaces;
using Bussiness.Services;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Get(int ID)
        {
            try
            {
                var res = _studentService.GetByID(ID);
                if (res == null || res.ID == 0) return NotFound("Not found by id="+ID);
                return Ok(res);
            }
            catch (Exception){
                return StatusCode(500,"Has error!");
            }
        }

        [HttpGet("getlistcourse")]
        public IActionResult GetListCourse(int ID)
        {
            try
            {
                var stu = _studentService.GetByID(ID);
                var res = _studentService.GetListCourseJoined(stu);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Has error!");
            }
        }


        [HttpPost]
        public IActionResult Create(CreateOrUpdateStudent model)
        {
            if (!ModelState.IsValid)
                return BadRequest("ModelState is not valid!");

            var res = _studentService.CreateStudent(model);
            if (res == 0) return StatusCode(500, "Has error!");

            return Ok("success");
        }

        [HttpPut]
        public IActionResult Update(CreateOrUpdateStudent model)
        {
            var res = _studentService.UpdateStudent(model);
            if (res == 0) return BadRequest("Has error!");
            return Ok("success");
        }

        [HttpDelete]
        public IActionResult Delete(int ID)
        {
            var res = _studentService.DeteleStudentByID(ID);
            if (res == 0) return BadRequest("Has error!");
            return Ok("deleted student, id=" + ID);
        }

    }
}
