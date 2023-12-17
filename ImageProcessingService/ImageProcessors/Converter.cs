using Microsoft.Extensions.Logging;
using Models;

public class Converter : IImageProcessor
{
    private readonly ILogger<Converter> _logger;

    public Converter(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger<Converter>();
    }
    public async Task Process(byte[] image, ImageOperation operation)
    {
        _logger.LogInformation("Converting image...");
        // process
    }
}