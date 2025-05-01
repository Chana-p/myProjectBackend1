using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        IBLEmployee Employees;// = new BlPatientService();
        public EmployeeController(IBL manager)
        {
            Employees = manager.Employees;
        }
        // להחזיר רשימת עובדים
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(Employees.Get()); 
        }
        //logIn
        [HttpGet("logIn/{id}/{name}")]
        public BLEmployee LogIn(int id, string name)
        {
            BLEmployee a = Employees.GetById(id);
            if (a.Ename == name)
                return a;
            else return null;
        }
        //add
        [HttpPost("AddEmployee")]
        public IActionResult Create([FromBody] BLEmployee newEmp)
        {
           return Ok(  Employees.Create(newEmp));
        }

    }
}
