using BL.Api;
using BL.Models;
using Dal.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        IBLCustomer customers;// = new BlPatientService();
        public CustomerController(IBL manager)
        {
            customers = manager.Customers;// כאן קבלנו אוביקט שהוא שרות של פצינטים
        }
        // להחזיר רשימת לקוחות
        [HttpGet("GetAll")]
        public List<BLCustomer> Get()
        {
            return customers.Get(); //new List<string>() { "sara", "shira", "bracha" };  
        }
        //logIn
        [HttpGet("logIn/{id}/{name}")]
        public IActionResult LogIn(int id,string name)
        {
         return Ok( customers.GetById(id,name));
           
        }
        //logOn
        [HttpPost("logOn")]
        public IActionResult Create([FromBody]BLCustomer newCustomer)
        {
          return Ok( customers.Create(newCustomer));
       
        }
        //update
        [HttpPost("update")]
        public IActionResult Update([FromBody] BLCustomer newCustomer)
        {
            return Ok(customers.Update(newCustomer));

        }

    }
}
