using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController:ControllerBase
    {
        IBLOrders orders;// = new BlPatientService();
        public OrdersController(IBL manager)
        {
            orders = manager.Orders;// כאן קבלנו אוביקט שהוא שרות של פצינטים
        }
        // להחזיר רשימת כל ההזמנות
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(orders.Get()); //new List<string>() { "sara", "shira", "bracha" };  
        }
        // להחזיר רשימת כל ההזמנות
        [HttpDelete("DeleteAll")]
        public void Delete()
        {
             orders.DeleteAll(); //new List<string>() { "sara", "shira", "bracha" };  
        }
        // להחזיר רשימת כל ההזמנות
        [HttpGet("GetByCustomer/{id}")]
        public IActionResult GetByCustomer(int id)
        {
            return Ok( orders.GetForCustomer(id)); 
        }
        [HttpGet("GetByEmployee/{id}")]
        public IActionResult GetByemp(int id)
        {
            return Ok(orders.GetForEmployee(id));
        }
        //add
        [HttpPost("addToCustomer/{id}")]
        public IActionResult Add(int id, [FromBody] List<BLOrderDetail> list)
        {
            int a = orders.Add(id);
            return Ok(orders.addDetails(list, a));

        }
        //update
        [HttpPut("updateSending/{orderId}")]
        public void updateSending(int orderId)
        {
            
             orders.UpdateSending(orderId);

        }

        //public IActionResult add(int id, [FromBody]List<BLOrderDetail>list)
        //{
        //  int a=orders.Add(id);
        //  return Ok(orders.addDetails(list,a));

        //}






    }
}
