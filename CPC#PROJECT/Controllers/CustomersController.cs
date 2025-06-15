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
        IBLCustomer customers;
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
        public async Task<IActionResult> LogIn(int id, string name)
        {
            try
            {
                _logger.LogInformation("Login attempt for ID: {Id}, Name: {Name}", id, name);
                
                var result = _bl.Customers.GetById(id, name);
                
                if (result == null)
                {
                    _logger.LogWarning("Customer not found for ID: {Id}, Name: {Name}", id, name);
                    return NotFound(new { message = "Customer not found" });
                }

                _logger.LogInformation("Login successful for ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for ID: {Id}, Name: {Name}", id, name);
                return StatusCode(500, new { 
                    message = "An error occurred during login",
                    error = ex.Message 
                });
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { 
                message = "Customer controller is working", 
                timestamp = DateTime.UtcNow 
            });
        }
        // [HttpGet("logIn/{id}/{name}")]
        // public IActionResult LogIn(int id,string name)
        // {
        //  return Ok( customers.GetById(id,name));
           
        // }
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
