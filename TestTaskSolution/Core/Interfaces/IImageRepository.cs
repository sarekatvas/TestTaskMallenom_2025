using Core.Models;

namespace Core.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с изображениями в базе данных.
    /// </summary>
    public interface IImageRepository
    {
        // Получить все изображения 
        Task <List<Image>> GetAllAsync();

        // Получить изображения по ID
        Task<Image> GetByIdAsync(int id);

        // Добавить новое изображение 
        Task<Image> AddAsync(Image image);

        // Изменить существующее изображение
        Task UpdateAsync(Image image);

        // Удалить изображение по ID 
        Task DeleteAsync(int id);

    }        
}
