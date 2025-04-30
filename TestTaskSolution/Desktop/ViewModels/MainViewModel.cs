using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Desktop.Commands;
using Desktop.Models;
using Desktop.Services;
using Microsoft.Win32;


namespace Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // Сервис для работы с api
        private readonly ApiService _apiService;
        // Выбранное изображение 
        private ImageModel _selectedImage;

        // Коллекция отображаемых изображений 
        public ObservableCollection<ImageModel> Images { get; } = new();

        public ImageModel SelectedImage
        {
            get => _selectedImage;
            set => SetField(ref _selectedImage, value);
        }

        // Команды добавления, редактирования и удаления 
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService;

            AddCommand = new RelayCommand(_ => AddImage());
            EditCommand = new RelayCommand(_ => EditImage(), _ => SelectedImage != null);
            DeleteCommand = new RelayCommand(_ => DeleteImage(), _ => SelectedImage != null);

            LoadImages();
        }

        // Асинхронный метод для загрузки всех изображений 
        private async void LoadImages()
        {
            try
            {
                var images = await _apiService.GetAllImagesAsync();
                Images.Clear();
                foreach (var image in images)
                {
                    Images.Add(image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображений: {ex.Message}");
            }
        }

        // Метод для добавления нового изображения через диалог
        private async void AddImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Добавить изображение";
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
           

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var fileBytes = System.IO.File.ReadAllBytes(dialog.FileName);
                    await _apiService.AddImageAsync(fileBytes, dialog.SafeFileName, "image/jpeg");
                    LoadImages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении изображения: {ex.Message}");
                }
            }
        }

        // Метод для удаления выбранного изображения
        private async void DeleteImage()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить изображение?",
                "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _apiService.DeleteImageAsync(SelectedImage.Id);
                    MessageBox.Show("Изображение удалено");
                    LoadImages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении изображения: {ex.Message}");
                }
            }
        }

        // Метод для редактирования
        private async void EditImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Добавить изображение";
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";


            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var filePath = dialog.FileName;
                    var fileBytes = File.ReadAllBytes(filePath);
                    var fileName = Path.GetFileName(filePath);

                    // Определяем ContentType
                    string contentType = filePath switch
                    {
                        var f when f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) => "image/jpeg",
                        var f when f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) => "image/png",
                        _ => "application/octet-stream"
                    };
                    
                     await _apiService.UpdateImageAsync(
                            fileBytes,
                            SelectedImage.Id,
                            fileName,
                            contentType
                        );
                    LoadImages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении изображения: {ex.Message}");
                } 
            } 

        }
    }
}

