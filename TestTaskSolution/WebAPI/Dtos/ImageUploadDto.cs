using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    /// <summary>
    /// Модель данных для загрузки изображения через API.
    /// </summary>
    public class ImageUploadDto
    {
        [Required(ErrorMessage = "Файл обязателен")]
        public IFormFile File { get; set; }
    }
}
