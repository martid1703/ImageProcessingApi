using Models;

namespace ImageService;
public interface IImageService
{
    Task<byte[]> Process(byte[] image, IEnumerable<ImageOperation> operations);
}