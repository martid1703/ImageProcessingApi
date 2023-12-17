using Microsoft.Extensions.Logging;
using Models;

public class Blurer : IImageProcessor
{
    private readonly ILogger<Blurer> _logger;

    public Blurer(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger<Blurer>();
    }
    
    public async Task Process(byte[] image, ImageOperation operation)
    {
        _logger.LogInformation("Bluring image...");
        // process
    }
}