using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private bool _isLoaded = false;

        public bool IsLoaded
        {
            get => _isLoaded;
            set => OnPropertyChanged(ref _isLoaded, value);
        }

        public virtual Task LoadAsync(object parameter = null)
        {
            IsLoaded = true;
            return Task.CompletedTask;
        }

        public virtual void Unload()
        {
            IsLoaded = false;
        }
    }
}
