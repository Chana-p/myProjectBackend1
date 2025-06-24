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
        public async Task<List<BLProduct>> Add(BLProduct p)//,IFormFile productPicture
        {
           // await UploadFile(productPicture);
            return products.Add(p);
        }
<<<<<<< HEAD
        [HttpPost("AddFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uniqueFileName = Guid.NewGuid().ToString().Substring(0, 8) + Path.GetExtension(file.FileName);

            string folderPath;

            if (file.ContentType.StartsWith("image/"))
            {
                folderPath = Path.Combine(uploads, "IMG");
            }
            else
            {
                folderPath = Path.Combine(uploads, "FILES");
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath = $"/{(file.ContentType.StartsWith("image/") ? "IMG" : "FILES")}/{uniqueFileName}" });
        }
=======
>>>>>>> origin/main

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
