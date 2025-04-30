using System.Collections.ObjectModel;
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
        private readonly ApiService _apiService;
        private ImageModel _selectedImage;

        public ObservableCollection<ImageModel> Images { get; } = new();

        public ImageModel SelectedImage
        {
            get => _selectedImage;
            set => SetField(ref _selectedImage, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService;

            AddCommand = new RelayCommand(_ => AddImage());
           // EditCommand = new RelayCommand(_ => EditImage(), _ => SelectedImage != null);
            DeleteCommand = new RelayCommand(_ => DeleteImage(), _ => SelectedImage != null);

            LoadImages();
        }

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

        private async void AddImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Добавить изображение";
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
           

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
    }
}

