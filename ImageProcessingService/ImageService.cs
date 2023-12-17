using Models;

namespace ImageService;

// todo: move to separate project
public class ImageService : IImageService
{
    public async Task<byte[]> Process(byte[] image, IEnumerable<ImageOperation> operations)
    {
        var processedImage = new byte[image.Length];
        Array.Copy(image, processedImage, image.Length);

        var tasks = new List<Task>();

        var operationResolver = new OperationResolver();
        foreach (var operation in operations)
        {
            // run cpu intensive task on tread pool
            var processor = operationResolver.Resolve(operation);

            tasks.Add(Task.Run(() => processor.Process(processedImage, operation)));
        }

        Task.WaitAll(tasks.ToArray());

        // process image
        return processedImage;
    }
}