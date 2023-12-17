using Models;

namespace ImageService;
public interface IImageService
{
    Task<byte[]> Process(byte[] image, List<ImageOperation> operations);
}