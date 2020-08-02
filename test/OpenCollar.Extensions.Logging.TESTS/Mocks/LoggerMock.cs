using System;

using Microsoft.Extensions.Logging;

namespace OpenCollar.Extensions.Logging.TESTS.Mocks
{
    internal sealed class LoggerMock : ILogger
    {
        public string LastMessage { get; set; }

        public LogLevel LogLevel { get; set; }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LastMessage = formatter(state, exception);
        }
    }
}