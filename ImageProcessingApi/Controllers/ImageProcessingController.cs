using ImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessingApi;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class ImageProcessingController : ControllerBase
{
    private readonly ILogger<ImageProcessingController> _logger;
    private IImageService _imageService;


    public ImageProcessingController(ILogger<ImageProcessingController> logger, IImageService imageService)
    {
        _logger = logger;
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok();
    }

    [HttpPost(Name = "ProcessImages")]
    public async Task<ProcessResponse> ProcessImages([FromForm] ProcessRequest request)
    {
        var response = new ProcessResponse();

        for (int i = 0; i < request.Files.Count(); i++)
        {
            var file = request.Files[i];
            if (file.Length > 0)
            {
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[file.Length];
                fileStream.Read(bytes, 0, (int)file.Length);

                var processedImage = await _imageService.Process(bytes, request.ImageOperations[i]);
                response.Files.Add(processedImage);
            }
        }

        return response;
    }
}
