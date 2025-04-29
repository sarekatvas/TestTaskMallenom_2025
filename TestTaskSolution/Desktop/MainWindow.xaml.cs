using System.Net.Http;
using System.Windows;
namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new();
        private string _apiUrl = "https://localhost:5001/api/images"; // URL API
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}