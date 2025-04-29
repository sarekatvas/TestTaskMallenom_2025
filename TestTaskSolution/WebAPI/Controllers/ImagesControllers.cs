using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Models;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesControllers : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesControllers(IImageRepository imageRepository) => _imageRepository = imageRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _imageRepository.GetAllAsync());
        }

        [HttpPost("Добавить")]
        public async Task<IActionResult> Add ([FromForm] ImageUploadDto dto)
        {
            using var ms = new MemoryStream ();
            await dto.File.CopyToAsync(ms);

            var image = new Image
            {
                FileName = dto.File.FileName,
                ContentType = dto.File.ContentType,
                Data = ms.ToArray()
            };
            await _imageRepository.AddAsync(image);
            return Ok();
        }

        [HttpDelete("Удалить")]
        public async Task Delete(int id) { 
        
           await _imageRepository.DeleteAsync(id);
           
        }
    }
}
