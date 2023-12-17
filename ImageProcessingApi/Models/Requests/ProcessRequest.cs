using Models;

namespace ImageProcessingApi;

public class ProcessRequest
{
    public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    public List<List<ImageOperation>> ImageOperations { get; set; } = new  List<List<ImageOperation>>();
}