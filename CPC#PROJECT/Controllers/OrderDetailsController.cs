using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController:ControllerBase
    {
        IBLOrderDetails orderDetails;// = new BlPatientService();
        public OrderDetailsController(IBL manager)
        {
            orderDetails = manager.OrderDetails;// כאן קבלנו אוביקט שהוא שרות של פצינטים
        }
        // להחזיר רשימת כל ההזמנות
        [HttpGet("GetAll")]
        public List<BLOrderDetail> Get()
        {
            return orderDetails.GetAll(); //new List<string>() { "sara", "shira", "bracha" };  
        }

        [HttpGet("GetDetailsForOrder/{id}")]
        public IActionResult GetByCustomer(int id)
        {
            return Ok(orderDetails.GetForOrderId(id));
        }
        ////add
        //[HttpPost("addToCustomer/{id}")]
        //public IActionResult add(int id, [FromBody] BLOrder newOrder)
        //{
        //    orders.Add(newOrder);
        //    return Ok(orders.GetForCustomer(id));

        //}







    }
}
