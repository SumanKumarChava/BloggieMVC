using Bloggie.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageRepository _imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var result = await _imageRepository.UploadImageAsync(file);
        if (!string.IsNullOrEmpty(result))
        {
            return new JsonResult(new { link = result});
        }
        return Problem("Something went wrong");
    }
}