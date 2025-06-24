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


            return Ok(orders.addDetails(list, a));

        }
        //update

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









    }
}
