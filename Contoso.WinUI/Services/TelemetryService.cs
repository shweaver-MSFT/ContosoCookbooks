using Contoso.Core.Services;
using System;

namespace Contoso.WinUI.Services
{
    public class TelemetryService : ITelemetryService
    {
        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void LogException(string message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine($"Exception: {message}, Message: {exception.Message}");
        }
    }
}
