using ImageCompressApi.Util;
using Microsoft.AspNetCore.Mvc;

namespace ImageCompressApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageCompressController : ControllerBase
{
    private readonly ILogger<ImageCompressController> _logger;

    public ImageCompressController(ILogger<ImageCompressController> logger)
    {
        _logger = logger;
    }

    [HttpPost("[action]")]
    public IActionResult UploadImage([FromForm] FileUploadModel model)
    {
        try
        {
            // this is the directory where file is upload
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string code = Guid.NewGuid().ToString().Substring(0, 6);

            string highQualityFileUrl = Path.Combine(
                uploadFolder,
                $"high_quality{code}{model.file.FileName}"
            );

            string thumbnailFileUrl = Path.Combine(
                uploadFolder,
                $"thumbnail{code}{model.file.FileName}"
            );

            Stream strm = model.file.OpenReadStream();
            CompressImage.Compress(strm, highQualityFileUrl, 1500, 900);
            // CompressImage.Compress(strm, thumbnailFileUrl, 300, 300);
            return Ok(new { message = "Compressed successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public class FileUploadModel
    {
        public IFormFile file { get; set; }
    }
}
