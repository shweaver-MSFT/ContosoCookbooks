using Contoso.Core.Services;
using Windows.Foundation.Collections;

namespace Contoso.WinUI.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IPropertySet _settings;

        public SettingsService(IPropertySet? cache = null) 
        {
            _settings = cache ?? new PropertySet();
        }

        public T Get<T>(string key)
        {
            return (T)_settings[key];
        }

        public bool IsSet(string key)
        {
            return _settings.ContainsKey(key);
        }

        public void Set<T>(string key, T value)
        {
            _settings[key] = value;
        }

        public bool TryGet<T>(string key, out T? value)
        {
            if (_settings.TryGetValue(key, out object? valueObj))
            {
                value = (T)valueObj;
                return true;
            }
            else
            {
                value = default;
                return false; 
            }
        }
    }
}
