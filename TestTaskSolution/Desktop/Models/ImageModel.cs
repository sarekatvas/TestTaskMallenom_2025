namespace Desktop.Models
{
    public class ImageModel
    {
        public int Id { get; set; } // Идентификатор 
        public string FileName { get; set; } // Имя файла 
        public byte[] Data { get; set; } // Бинарные данные изображения
        public string ContentType { get; set; } // image/jpeg
        public DateTime UploadDate { get; set; } = DateTime.UtcNow; // Время загрузки 
    }
}
