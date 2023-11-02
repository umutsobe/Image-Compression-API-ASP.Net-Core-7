using ImageCompressApi.Util;
using Microsoft.AspNetCore.Mvc;

namespace ImageCompressApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageCompressController : ControllerBase
{
    [HttpPost("[action]")]
    public IActionResult UploadImage([FromForm] FileUploadModel model)
    {
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
        CompressImage.Compress(strm, highQualityFileUrl, 70, 900, 900);
        CompressImage.Compress(strm, thumbnailFileUrl, 70, 300, 300);
        return Ok(new { message = "Compressed successfully" });
    }

    public class FileUploadModel
    {
        public IFormFile file { get; set; }
    }
}
