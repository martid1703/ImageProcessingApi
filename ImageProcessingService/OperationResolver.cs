using Microsoft.Extensions.Logging;
using Models;

namespace ImageService;

// todo: move to separate project
public class OperationResolver
{
    private readonly ILoggerFactory _loggerFactory;

    public OperationResolver(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public IImageProcessor Resolve(ImageOperation operation)
    {
        if (operation.Id == 1)
        {
            return new Blurer(_loggerFactory);
        }

        if (operation.Id == 2)
        {
            return new Resizer(_loggerFactory);
        }

        if (operation.Id == 3)
        {
            return new Converter(_loggerFactory);
        }

        throw new NotSupportedException(nameof(operation));
    }
}