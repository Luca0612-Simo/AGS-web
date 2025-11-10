using AGS_services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CarouselController : ControllerBase
{
    private readonly ICarouselService _carouselService;

    public CarouselController(ICarouselService carouselService)
    {
        _carouselService = carouselService;
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadImage(IFormFile file, string? title, int sortOrder)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No se ha proporcionado un archivo.");
        }
        var createdImage = await _carouselService.AddImageToCarouselAsync(file, title, sortOrder);

        return CreatedAtAction(nameof(GetImages), new { id = createdImage.Id }, createdImage);           
    }

    [HttpGet]
    public async Task<IActionResult> GetImages()
    {
        var images = await _carouselService.GetImages();
        return Ok(images);
    }
}