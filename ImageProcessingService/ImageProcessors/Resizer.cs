using Models;
using Microsoft.Extensions.Logging;

public class Resizer : IImageProcessor
{
    private readonly ILogger<Resizer> _logger;

    public Resizer(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger<Resizer>();
    }

    public async Task Process(byte[] image, ImageOperation operation)
    {
        _logger.LogInformation("Resizing image...");
    }
}