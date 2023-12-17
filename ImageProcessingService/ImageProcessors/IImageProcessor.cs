using Models;

public interface IImageProcessor
{
    Task Process(byte[] image, ImageOperation operation);
}