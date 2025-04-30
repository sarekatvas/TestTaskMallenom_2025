namespace Core.Models
{
    /// <summary>
    /// Модель изображения для хранения в базе данных.
    /// </summary>
    public class Image
    {
        public int Id { get; set; } // Уникальный идентификатор изображения
        public string FileName { get; set; } // Имя файла изображения 
        public byte[] Data { get; set; } // Бинарные данные изображения
        public string ContentType { get; set; } // image/jpeg
        public DateTime UploadDate { get; set; } = DateTime.UtcNow; // Время загрузки изображения
    }
}
