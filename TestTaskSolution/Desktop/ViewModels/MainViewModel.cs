using System.Collections.ObjectModel;
using Desktop.Models;
using Desktop.Services;


namespace Desktop.ViewModels
{
    internal class MainViewModel
    {
        private readonly IImageService _imageService;
        private ImageModel _selectedImage;

        public ObservableCollection<ImageModel> Images { get; } = new();
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ImageModel SelectedImage
        {
            get => _selectedImage;
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;

                }
            }
        }
        public MainViewModel(IImageService imageService) {
            _imageService = imageService;
            AddCommand = new RelayCommand(async _ => await AddImage());
        
        }
       

    }
}
