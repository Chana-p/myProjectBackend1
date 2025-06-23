
using Microsoft.AspNetCore.Mvc;

namespace CPC_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded."); // לא שונה

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

            // שינוי: הוספת בדיקה אם התיקייה הראשית wwwroot קיימת
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads); // יצירת תיקיית wwwroot אם אינה קיימת
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // לא שונה
            }

            var filePath = Path.Combine(folderPath, uniqueFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); // לא שונה
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // שינוי: טיפול בשגיאות בעת כתיבת הקובץ
            }

            return Ok(new { imageUrl = $"{uniqueFileName}" }); // לא שונה
        }
    }
}


