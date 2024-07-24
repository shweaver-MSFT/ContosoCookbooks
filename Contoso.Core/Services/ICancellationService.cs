using System.Threading;
using System.Threading.Tasks;

namespace Contoso.Core.Services
{
    public interface ICancellationService
    {
        CancellationTokenSource GetLinkedTokenSource();

        void CancelLinkedTokens();

        Task CancelLinkedTokensAsync();
    }
}
