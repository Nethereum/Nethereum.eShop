using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    /// <summary>
    /// Minimal ILogger version of the xunit ITestOutputHelper logger
    /// </summary>
    public class TestOutputHelperLogger : ILogger
    {
        private readonly ITestOutputHelper _output;

        public TestOutputHelperLogger(ITestOutputHelper output)
        {
            _output = output;
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return null;
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = string.Empty;
            if (formatter != null)
            {
                message = formatter(state, exception);
            }
            _output.WriteLine(message);
        }
    }
}
