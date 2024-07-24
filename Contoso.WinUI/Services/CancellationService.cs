using Contoso.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.WinUI.Services
{
    internal class CancellationService : ICancellationService
    {
        private readonly CancellationTokenSource _cts;

        public CancellationService()
        {
            _cts = new CancellationTokenSource();
        }

        public void CancelLinkedTokens()
        {
            _cts.Cancel();
        }

        public Task CancelLinkedTokensAsync()
        {
            return _cts.CancelAsync();
        }

        public CancellationTokenSource GetLinkedTokenSource()
        {
            if (_cts.IsCancellationRequested)
            {
                _cts.TryReset();
            }

            return CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);
        }
    }
}
