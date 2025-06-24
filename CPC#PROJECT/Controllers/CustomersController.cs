// using BL.Api;
// using BL.Models;
// using Dal.Api;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using System.Collections.Generic;

// namespace CPC_PROJECT.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class CustomerController:ControllerBase
//     {
//         IBLCustomer customers;
//         private readonly ILogger<CustomersController> _logger;
//         public CustomerController(IBL manager)
//         {
//             customers = manager.Customers;// כאן קבלנו אוביקט שהוא שרות של פצינטים
//         }
//         // להחזיר רשימת לקוחות
//         [HttpGet("GetAll")]
//         public List<BLCustomer> Get()
//         {
//             return customers.Get(); //new List<string>() { "sara", "shira", "bracha" };  
//         }
//         //logIn
//         [HttpGet("logIn/{id}/{name}")]
//         public async Task<IActionResult> LogIn(int id, string name)
//         {
//             try
//             {
//                 _logger.LogInformation("Login attempt for ID: {Id}, Name: {Name}", id, name);
                
//                 var result = _bl.Customers.GetById(id, name);
                
//                 if (result == null)
//                 {
//                     _logger.LogWarning("Customer not found for ID: {Id}, Name: {Name}", id, name);
//                     return NotFound(new { message = "Customer not found" });
//                 }

//                 _logger.LogInformation("Login successful for ID: {Id}", id);
//                 return Ok(result);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error during login for ID: {Id}, Name: {Name}", id, name);
//                 return StatusCode(500, new { 
//                     message = "An error occurred during login",
//                     error = ex.Message 
//                 });
//             }
//         }

//         [HttpGet("test")]
//         public IActionResult Test()
//         {
//             return Ok(new { 
//                 message = "Customer controller is working", 
//                 timestamp = DateTime.UtcNow 
//             });
//         }
//         // [HttpGet("logIn/{id}/{name}")]
//         // public IActionResult LogIn(int id,string name)
//         // {
//         //  return Ok( customers.GetById(id,name));
           
//         // }
//         //logOn
//         [HttpPost("logOn")]
//         public IActionResult Create([FromBody]BLCustomer newCustomer)
//         {
//           return Ok( customers.Create(newCustomer));
       
//         }
//         //update
//         [HttpPost("update")]
//         public IActionResult Update([FromBody] BLCustomer newCustomer)
//         {
//             return Ok(customers.Update(newCustomer));

//         }

//     }
// }
using BL.Api;
using BL.Models;
using Dal.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly IBLCustomer customers;
        private readonly IBL _bl;
        private readonly ILogger<CustomerController> _logger; // תוקן השם להתאים לשם הקלאס

        public CustomerController(IBL manager, ILogger<CustomerController> logger)
        {
            _bl = manager;
            customers = manager.Customers; // כאן קבלנו אוביקט שהוא שרות של לקוחות
            _logger = logger;
        }


        // להחזיר רשימת לקוחות
        [HttpGet("GetAll")]
        public List<BLCustomer> Get()
        {

            try
            {
                _logger.LogInformation("Getting all customers");
                return customers.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all customers");
                throw;
            }
        }

        // logIn
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
                return StatusCode(500, new 
                {
                    message = "An error occurred during login",
                    error = ex.Message
                });
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new 
            {
                message = "Customer controller is working",
                timestamp = DateTime.UtcNow
            });
        }

        // logOn
        [HttpPost("logOn")]
        public IActionResult Create([FromBody] BLCustomer newCustomer)
        {
            try
            {
                _logger.LogInformation("Creating new customer");
                var result = customers.Create(newCustomer);
                _logger.LogInformation("Customer created successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(500, new 
                {
                    message = "An error occurred while creating customer",
                    error = ex.Message
                });
            }
        }

        // update
        [HttpPost("update")]
        public IActionResult Update([FromBody] BLCustomer newCustomer)
        {
            try
            {
                _logger.LogInformation("Updating customer with ID: {Id}", newCustomer?.CustId);
                var result = customers.Update(newCustomer);
                _logger.LogInformation("Customer updated successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer");
                return StatusCode(500, new 
                {
                    message = "An error occurred while updating customer",
                    error = ex.Message
                });
            }
        }

    }
}
