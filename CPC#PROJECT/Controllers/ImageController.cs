using Microsoft.AspNetCore.Mvc;

namespace CPC_PROJECT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("Invalid file type");

            // Generate unique filename
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            
            // Create uploads directory if it doesn't exist
            var uploadsPath = Path.Combine(_environment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the URL to access the image
            var imageUrl = $"/uploads/{fileName}";
            
            // return Ok(new { 
            //     message = "Image uploaded successfully", 
            //     imageUrl = imageUrl,
            //     fileName = fileName 
            // });
            return Ok(new { imageUrl });
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "uploads", fileName);
            
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            var contentType = fileExtension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType);
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "uploads", fileName);
            
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            System.IO.File.Delete(filePath);
            return Ok(new { message = "Image deleted successfully" });
        }
    }
}