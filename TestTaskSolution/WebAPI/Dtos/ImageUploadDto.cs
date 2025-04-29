using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class ImageUploadDto
    {
        [Required(ErrorMessage = "Файл обязателен")]
        public IFormFile File { get; set; }
    }
}
