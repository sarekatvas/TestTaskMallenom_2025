using Core.Models;

namespace Core.Interfaces
{
    public interface IImageRepository
    {
        Task <List<Image>> GetAllAsync();
        //Task<Image> GetByIdAsync(int id);
        Task<Image> AddAsync(Image image);
        //Task UpdateAsync(Image image);
        Task DeleteAsync(int id);

    }        
}
