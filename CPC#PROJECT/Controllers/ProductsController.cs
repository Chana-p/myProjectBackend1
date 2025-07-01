using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IBLProducts products;
        public ProductsController(IBL manager)
        {
            products = manager.Products;
        }
        [HttpGet("GetAll")]
        public List<BLProduct> Get()
        {
            return products.Get();
        }
        [HttpGet("GetByID/{id}")]
        public BLProduct GetById(int id)
        {
            return products.GetByID(id);
        }
            [HttpPost("Add")]
            public async Task<List<BLProduct>> Add(BLProduct p)
            {
                try 
                {
                    var result =  products.Add(p);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding product: {ex.Message}");
                    throw;
                }
            }


        [HttpPut("Update")]
        public List<BLProduct> Update(BLProduct p)
        {
            return products.Update(p);
        }
        [HttpPut("UpdateAmount")]
        public List<BLProduct> UpdateAmount(int p,int amount)
        {
            return products.UpdateAmount(p,amount);
        }
        [HttpDelete("Delete")]
        public List<BLProduct> Delete(int p)
        {
            return products.DeleteFromList(p);
        }


    }
}
