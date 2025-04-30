using System.Net.Http;
using System.Net.Http.Json;
using Desktop.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Сервис для взаимодействия с внешним API, работающим с изображениями
    /// </summary>
    public class ApiService
    {
        private readonly HttpClient _httpClient; // Экземпляр HttpClient для отправки HTTP-запросов
        private readonly string Url = "https://localhost:5001/api/images"; // Базовый url

        public ApiService(HttpClient httpClient) { 
            _httpClient = httpClient;
        }

        // Получение всех изображений 
        public async Task<List<ImageModel>> GetAllImagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ImageModel>>($"{Url}/all");
        }

        // Добавление нового изображения
        public async Task AddImageAsync(byte[] data, string fileName, string contentType)
        {
            var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(data), "File", fileName }
        };
            await _httpClient.PostAsync($"{Url}/add", content);
        }

        // Удаление 
        public async Task DeleteImageAsync(int id)
        {
            await _httpClient.DeleteAsync($"{Url}/delete/{id}");
        }

        // Обновление 
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
