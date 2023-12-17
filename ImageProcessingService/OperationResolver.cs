using Models;

namespace ImageService;

// todo: move to separate project
public class OperationResolver
{
    public IImageProcessor Resolve(ImageOperation operation)
    {
        if (operation.Id == 1)
        {
            return new Blurer();
        }

        if (operation.Id == 2)
        {
            return new Resizer();
        }

        if (operation.Id == 3)
        {
            return new Converter();
        }

        throw new NotSupportedException(nameof(operation));
    }
}