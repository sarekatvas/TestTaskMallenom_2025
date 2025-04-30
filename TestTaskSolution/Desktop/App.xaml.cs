using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views;
using Desktop.Commands;

namespace Desktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Конфигурация сервисов
            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            // Создание главного окна
            var mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = provider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // HTTP-клиент и сервисы
            services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://ваш-api-адрес.com/");
            });

            // ViewModels и окна
            services.AddSingleton<MainViewModel>();
            services.AddTransient<MainWindow>(); 

        }
    }
}