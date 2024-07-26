using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contoso.WinUI.Views.Controls
{
    public abstract class UserControlViewBase : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged<T>(DependencyProperty property, T value, [CallerMemberName] string? propertyName = null)
        {
            T currentValue = (T)GetValue(property);
            if (!EqualityComparer<T>.Default.Equals(value, currentValue))
            {
                SetValue(property, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
