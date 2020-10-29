using System;

using Xunit;

namespace OpenCollar.Extensions.Logging.TESTS
{
    public sealed class LoggerExtensionsTest
    {
        [Fact]
        public void Log_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, (Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, (Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void Log_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLog(Microsoft.Extensions.Logging.LogLevel.Error, context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogCritical("TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical("TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogCritical(() => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(() => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogCritical(exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogCritical((Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogCritical(exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogCritical((Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogCritical(context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogCritical(context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogCritical(context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogCritical(context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogCritical_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogCritical(context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogCritical(context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Critical, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogCritical(context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogCritical(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogDebug("TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug("TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogDebug(() => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(() => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogDebug(exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogDebug((Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogDebug(exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogDebug((Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogDebug(context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogDebug(context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogDebug(context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogDebug(context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogDebug_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogDebug(context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogDebug(context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Debug, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogDebug(context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogDebug(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogError("TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError("TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogError(() => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(() => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogError(exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogError((Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogError(exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogError((Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogError(context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogError(context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogError(context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogError(context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogError_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogError(context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogError(context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogError(context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogError(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogInformation("TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation("TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogInformation(() => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(() => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogInformation(exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogInformation((Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogInformation(exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogInformation((Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogInformation(context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogInformation(context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogInformation(context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogInformation(context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogInformation_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogInformation(context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogInformation(context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Information, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogInformation(context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogInformation(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogTrace("TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace("TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogTrace(() => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(() => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogTrace(exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogTrace((Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogTrace(exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogTrace((Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogTrace(context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogTrace(context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogTrace(context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogTrace(context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogTrace_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogTrace(context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogTrace(context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Trace, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogTrace(context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogTrace(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_1()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogWarning("TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning("TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_2()
        {
            var mock = new Mocks.LoggerMock();

            var fluent = mock.SafeLogWarning(() => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(() => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_3()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogWarning(exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogWarning((Exception?)null, "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning((string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_4()
        {
            var mock = new Mocks.LoggerMock();

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogWarning(exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogWarning((Exception?)null, () => "TEST");

            Assert.Equal("TEST", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning((Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_5()
        {
            var mock = new Mocks.LoggerMock();
            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogWarning(context, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(context, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_6()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var fluent = mock.SafeLogWarning(context, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(context, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_7()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogWarning(context, exception, "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogWarning(context, (Exception?)null, "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(context, exception, "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning(context, (string?)null);

            Assert.Equal(mock, fluent);
        }

        [Fact]
        public void LogWarning_8()
        {
            var mock = new Mocks.LoggerMock();

            var context = LoggingContext.Current();
            context.AppendInfo("KEY", "VALUE");

            var exception = new Exception("EXCEPTION");

            var fluent = mock.SafeLogWarning(context, exception, () => "TEST");

            Assert.Equal(mock, fluent);

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Equal(exception, mock.LastException);

            fluent = mock.SafeLogWarning(context, (Exception?)null, () => "TEST");

            Assert.StartsWith("TEST\r\n\r\n[", mock.LastMessage);
            Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Warning, mock.LastLogLevel);
            Assert.Null(mock.LastException);

            fluent = ((Microsoft.Extensions.Logging.ILogger?)null).SafeLogWarning(context, exception, () => "TEST");

            Assert.Null(fluent);

            fluent = mock.SafeLogWarning(context, (Func<string?>?)null);

            Assert.Equal(mock, fluent);
        }
    }
}