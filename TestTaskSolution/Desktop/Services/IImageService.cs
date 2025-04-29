using Desktop.Models;

namespace Desktop.Services
{
    public interface IImageService
    {
        Task<IEnumerable<ImageModel>> GetAll();
        //Task Add(string path);
        //Task Delete(int id);
    }
}
