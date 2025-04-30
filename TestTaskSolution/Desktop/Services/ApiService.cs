using System.Net.Http;
using System.Net.Http.Json;
using Desktop.Models;

namespace Desktop.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string Url = "https://localhost:5001/api/images";

        public ApiService(HttpClient httpClient) { 
            _httpClient = httpClient;
        }

        public async Task<List<ImageModel>> GetAllImagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ImageModel>>($"{Url}/all");
        }

        public async Task AddImageAsync(byte[] data, string fileName, string contentType)
        {
            var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(data), "File", fileName }
        };
            await _httpClient.PostAsync($"{Url}/add", content);
        }

        public async Task DeleteImageAsync(int id)
        {
            await _httpClient.DeleteAsync($"{Url}/delete/{id}");
        }

        public async Task UpdateImageAsync(byte[] data,int id, string fileName, string contentType)
        {
            var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(data), "file", fileName }
        };
            await _httpClient.PutAsync($"{Url}/update/{id}",content);
        }
    }
}
