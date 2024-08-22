namespace Contoso.Core.Services
{
    public interface IFactoryService<T>
    {
        T Create();
    }
}
