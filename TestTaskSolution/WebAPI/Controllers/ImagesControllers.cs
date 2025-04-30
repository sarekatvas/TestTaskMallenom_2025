using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Models;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с изображениями.
    /// </summary>
    /// 
    [ApiController]
    [Route("api/images")] //Базовый путь для методов 
    public class ImagesControllers : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        // Зависимость репозитория 
        public ImagesControllers(IImageRepository imageRepository) => _imageRepository = imageRepository;

        // Получить все изображения 
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _imageRepository.GetAllAsync());
        }

        // Получить изображения по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null) return NotFound();
            return Ok(image);
        }

        // Добавить новое 
        [HttpPost("add")]
        public async Task<IActionResult> Add ([FromForm] ImageUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is required"); // 400

            // Чтение файла в массив байтов 
            using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);
            // Сохранение данных 
            var image = new Image
            {
                FileName = dto.File.FileName,
                ContentType = dto.File.ContentType,
                Data = ms.ToArray()
            };

            var createdImage = await _imageRepository.AddAsync(image);
            return CreatedAtAction(nameof(GetById), new { id = createdImage.Id }, createdImage); //201
        }


        // Обновить существующее изображение
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ImageUploadDto dto)
        {
            var existingImage = await _imageRepository.GetByIdAsync(id);
            if (existingImage == null) return NotFound();

            using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);

            existingImage.FileName = dto.File.FileName;
            existingImage.ContentType = dto.File.ContentType;
            existingImage.Data = ms.ToArray();

            await _imageRepository.UpdateAsync(existingImage);
            return NoContent();
        }


        // Удалить изображение по ID 
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id) {

            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null) return NotFound();

            await _imageRepository.DeleteAsync(id);
            return NoContent();

        }
    }
}
