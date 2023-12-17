using System.Text.Json;
using Microsoft.Extensions.Logging;
using Models;

namespace ImageService;

// todo: move to separate project
public class ImageService : IImageService
{
    private readonly ILoggerFactory _loggerFactory;

    public ImageService(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public async Task<byte[]> Process(byte[] image, List<ImageOperation> operations)
    {
        var processedImage = new byte[image.Length];
        Array.Copy(image, processedImage, image.Length);

        var tasks = new List<Task>();

        var operationResolver = new OperationResolver(_loggerFactory);
        foreach (var operation in operations)
        {
            var op = GetOperation(operation);

            // run cpu intensive task on tread pool
            var processor = operationResolver.Resolve(op);

            tasks.Add(Task.Run(() => processor.Process(processedImage, op)));
        }

        Task.WaitAll(tasks.ToArray());

        // process image
        return processedImage;
    }

    private ImageOperation GetOperation(ImageOperation operation)
    {
        // var dic = operation.Properties
        // .Trim(new char[] { '{', '}' })
        // .Replace("\"", string.Empty)
        // .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        // .Select(part => part.Split(':'))
        // .ToDictionary(split => split[0], split => split[1]);

        var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(operation.Properties);

        if (operation.Id == 1)
        {
            var op = new Blur
            {
                Id = operation.Id
            };

            var props = op.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var prop in props)
            {
                if (dic.TryGetValue(prop.Name, out string val))
                {
                    prop.SetValue(op, int.Parse(val));
                }
            }

            return op;
        }

        if (operation.Id == 2)
        {
            var op = new Resize
            {
                Id = operation.Id
            };

            var props = op.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var prop in props)
            {
                if (dic.TryGetValue(prop.Name, out string val))
                {
                    prop.SetValue(op, int.Parse(val));
                }
            }

            return op;
        }

        if (operation.Id == 3)
        {
            var op = new Models.Convert
            {
                Id = operation.Id
            };

            var props = op.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var prop in props)
            {
                if (dic.TryGetValue(prop.Name, out string val))
                {
                    prop.SetValue(op, int.Parse(val));
                }
            }

            return op;
        }

        throw new NotSupportedException();
    }
}