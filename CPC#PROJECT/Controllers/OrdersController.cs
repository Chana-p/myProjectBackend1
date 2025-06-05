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
<<<<<<< HEAD
      
=======
        // להחזיר רשימת כל ההזמנות
        [HttpDelete("DeleteAll")]
        public void Delete()
        {
             orders.DeleteAll(); //new List<string>() { "sara", "shira", "bracha" };  
        }
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
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
<<<<<<< HEAD
        [HttpGet("GetCompletedByEmployee/{id}")]
        public IActionResult GetCompletedByemp(int id)
        {
            return Ok(orders.GetCompletedForEmployee(id));
        }
        [HttpGet("GetNews")]
        public IActionResult GetNews()
        {
            return Ok(orders.GetNews());
        }
        //add
        [HttpPost("addToCustomer/{id}/{empId?}")]
        public IActionResult Add(int id,int? empId, [FromBody] List<BLOrderDetail> list)
        {
            int a = orders.Add(id,empId);
=======
        //add
        [HttpPost("addToCustomer/{id}")]
        public IActionResult Add(int id, [FromBody] List<BLOrderDetail> list)
        {
            int a = orders.Add(id);
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            return Ok(orders.addDetails(list, a));

        }
        //update
<<<<<<< HEAD
        [HttpPut("updateSending/{orderId}/{empId}")]
        public void updateSending(int orderId, int empId)
        {
            
             orders.UpdateSending(orderId,empId);

        }
        [HttpPut("AssignOrder/{empId}")]
        public void AssignOrder(int empId, [FromBody]List<BLOrder> orderList)
        {

            orders.AssignOrders(empId, orderList);

        }

=======
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
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550






    }
}
