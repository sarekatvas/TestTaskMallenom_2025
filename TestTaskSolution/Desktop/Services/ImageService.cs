using System.Net.Http;
using System.Net.Http.Json;
using Desktop.Models;

namespace Desktop.Services
{
    public class ImageService:IImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");

        }   

        public async Task<IEnumerable<ImageModel>> GetAll()
        {
            var responce = await _httpClient.GetAsync("api/image/all");
            return await responce.Content.ReadFromJsonAsync<List<ImageModel>>();
        }
    }
}
