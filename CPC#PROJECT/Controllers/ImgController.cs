              using Microsoft.AspNetCore.Mvc;
              using CloudinaryDotNet;
              using CloudinaryDotNet.Actions;

              namespace CPC_PROJECT.Controllers
              {
   
                  [Route("api/[controller]")]
                  [ApiController]
                  public class ImgController : ControllerBase
                  {
                      private readonly Cloudinary _cloudinary;

                      public ImgController(IConfiguration configuration)
                      {
                          var cloudName = configuration["Cloudinary:CloudName"];
                          var apiKey = configuration["Cloudinary:ApiKey"];
                          var apiSecret = configuration["Cloudinary:ApiSecret"];

                          var account = new Account(cloudName, apiKey, apiSecret);
                          _cloudinary = new Cloudinary(account);
                      }

                      [HttpPost("upload")]
                      public async Task<IActionResult> UploadFile(IFormFile file)
                      {
                          try
                          {
                              if (file == null || file.Length == 0)
                                  return BadRequest(new { message = "לא נבחר קובץ" });

                              // בדיקת סוג קובץ
                              var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                              var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                              
                              if (!allowedExtensions.Contains(fileExtension))
                                  return BadRequest(new { message = "סוג קובץ לא תקין" });

                              // העלאה ל-Cloudinary
                              using var stream = file.OpenReadStream();
                              var uploadParams = new ImageUploadParams()
                              {
                                  File = new FileDescription(file.FileName, stream),
                                  PublicId = $"products/{Guid.NewGuid()}",
                                  Transformation = new Transformation()
                                      .Width(800)
                                      .Height(600)
                                      .Crop("limit")
                                      .Quality("auto")
                                      .FetchFormat("auto")
                              };

                              var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                              if (uploadResult.Error != null)
                              {
                                  return StatusCode(500, new { message = "שגיאה בהעלאת התמונה", error = uploadResult.Error.Message });
                              }

                              // החזרת URL של התמונה
                              return Ok(new { imageUrl = uploadResult.SecureUrl.ToString() });
                          }
                          catch (Exception ex)
                          {
                              return StatusCode(500, new { message = "שגיאה בהעלאת התמונה", error = ex.Message });
                          }
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
                          return Ok(new { message = "Image controller is working!", timestamp = DateTime.Now });
                      }
                  }
              }
