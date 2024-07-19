namespace Contoso.Core.Services
{
    public interface ISettingsService
    {
        T Get<T>(string key);
        bool TryGet<T>(string key, out T value);
        void Set<T>(string key, T value);
        bool IsSet(string key);
    }
}
