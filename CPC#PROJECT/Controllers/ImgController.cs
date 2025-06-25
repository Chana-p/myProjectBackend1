              using Microsoft.AspNetCore.Mvc;
              using CloudinaryDotNet;
              using CloudinaryDotNet.Actions;

              namespace CPC_PROJECT.Controllers
              {
   
                  [Route("api/[controller]")]
                  [ApiController]
                  public class ImgController : ControllerBase
                  {
                      private readonly IConfiguration _configuration;

                      public ImgController(IConfiguration configuration)
                      {
                          _configuration = configuration;
                      }

                      [HttpPost("upload")]
                      public async Task<IActionResult> UploadFile(IFormFile file)
                      {
                          // בינתיים נחזיר הודעה שהשירות לא זמין
                          return Ok(new { 
                              message = "Upload service will be available after Cloudinary configuration",
                              received = file?.FileName ?? "No file"
                          });
                      }

                      [HttpDelete("delete/{publicId}")]
                      public async Task<IActionResult> DeleteImage(string publicId)
                      {
                          try
                          {
                              // החלף / ב-_ כי URL encoding
                              publicId = publicId.Replace("_", "/");
                
                              var deleteParams = new DeletionParams(publicId);
                              var result = await _cloudinary.DestroyAsync(deleteParams);
                
                              if (result.Result == "ok")
                              {
                                  return Ok(new { message = "התמונה נמחקה בהצלחה" });
                              }
                              else
                              {
                                  return BadRequest(new { message = "לא ניתן למחוק את התמונה" });
                              }
                          }
                          catch (Exception ex)
                          {
                              return StatusCode(500, new { 
                                  message = "שגיאה במחיקת התמונה", 
                                  error = ex.Message 
                              });
                          }
                      }

                      [HttpGet("test")]
                      public IActionResult Test()
                      {
                          var cloudName = _configuration["Cloudinary:CloudName"];
                          var apiKey = _configuration["Cloudinary:ApiKey"];
            
                          return Ok(new { 
                              message = "Image controller is working",
                              cloudinaryConfigured = !string.IsNullOrEmpty(cloudName) && !string.IsNullOrEmpty(apiKey),
                              cloudName = cloudName ?? "Not configured"
                          });
                      }
                  }
              }
