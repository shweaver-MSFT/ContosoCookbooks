using Contoso.Core.Services;
using Microsoft.Windows.ApplicationModel.Resources;

namespace Contoso.WinUI.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ResourceLoader _resourceLoader;

        public LocalizationService(ResourceLoader? resourceLoader = null)
        {
            _resourceLoader = resourceLoader ?? new();
        }

        public string GetString(string key)
        {
            return _resourceLoader.GetString(key);
        }
    }
}
