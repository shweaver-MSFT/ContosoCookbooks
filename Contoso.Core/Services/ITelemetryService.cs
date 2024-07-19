using System;

namespace Contoso.Core.Services
{
    public interface ITelemetryService
    {
        void Log(string message);
        void LogException(string message, Exception exception);
    }
}
