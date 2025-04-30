using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Базовый класс для всех views
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Событие, генерируемое при изменении свойства
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Метод, устанавливающий новое значение для поля 
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}