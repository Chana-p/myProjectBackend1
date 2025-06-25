  using Microsoft.AspNetCore.Mvc;

namespace CPC_PROJECT.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImgController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new { message = "לא נבחר קובץ" });

                // בדיקת סוג קובץ
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest(new { message = "סוג קובץ לא תקין" });

                // יצירת שם קובץ ייחודי
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                
                // שנה את הנתיב ל-wwwroot/images
                var uploadsPath = Path.Combine(_environment.WebRootPath ?? 
                    Path.Combine(_environment.ContentRootPath, "wwwroot"), "images");
                
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var filePath = Path.Combine(uploadsPath, fileName);

                // שמירת הקובץ
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // שנה את ה-URL
                var imageUrl = $"/images/{fileName}";
                
                // return Ok(new { 
                //     message = "התמונה הועלתה בהצלחה", 
                //     imageUrl = imageUrl,
                //     fileName = fileName 
                // });
                return Ok(new { imageUrl }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "שגיאה בהעלאת התמונה", error = ex.Message });
            }
        }
    }
}
