/*
 * This file is part of OpenCollar.Extensions.Logging.
 *
 * OpenCollar.Extensions.Logging is free software: you can redistribute it
 * and/or modify it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or (at your
 * option) any later version.
 *
 * OpenCollar.Extensions.Logging is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public
 * License for more details.
 *
 * You should have received a copy of the GNU General Public License along with
 * OpenCollar.Extensions.Logging.  If not, see <https://www.gnu.org/licenses/>.
 *
 * Copyright © 2020 Jonathan Evans (jevans@open-collar.org.uk).
 */

using System;

using JetBrains.Annotations;

using Microsoft.Extensions.Logging;

using OpenCollar.Extensions.Validation;

namespace OpenCollar.Extensions.Logging
{
    /// <summary>
    ///     Extension methods for the <see cref="ILogger" /> interface.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        ///     Gets or sets a value indicating whether a mechanism for appending contextual information in the logs has
        ///     been initialized (e.g. using telemetry).
        /// </summary>
        /// <value>
        ///     <see langword="false" /> if a mechanism for appending contextual information in the logs has been
        ///     initialized (e.g. using telemetry); otherwise, <see langword="true" /> to append the information
        ///     directly to messages.
        /// </value>
        /// <remarks>
        ///     If set to <see langword="true" /> then contextual information will be appended directly to each message.
        /// </remarks>
        public static bool AppendContextualInformationToLogMessages { get; set; } = true;

        /// <summary>
        ///     Creates a new operation scope.
        /// </summary>
        /// <param name="logger">
        ///     The logger to which messages will be written.
        /// </param>
        /// <param name="logLevel">
        ///     The level at which to write to the log.
        /// </param>
        /// <param name="message">
        ///     The fragment of message to write. Should be formed to fit into a sentence of the form "Starting XXX."
        /// </param>
        /// <returns>
        ///     A disposable object representing the scope of the operations. Call <see cref="IDisposable.Dispose" /> on
        ///     the returned object to complete the operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="message" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> is zero-length.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> contains only white space characters.
        /// </exception>
        [NotNull]
        public static IDisposable BeginOperation([NotNull] this ILogger logger, LogLevel logLevel, [NotNull] string message)
        {
            logger.Validate(nameof(logger), ObjectIs.NotNull);
            message.Validate(nameof(message), StringIs.NotNullEmptyOrWhiteSpace);

            return new OperationScope(logger, logLevel, logLevel, message, null);
        }

        /// <summary>
        ///     Creates a new operation scope.
        /// </summary>
        /// <param name="logger">
        ///     The logger to which messages will be written.
        /// </param>
        /// <param name="logLevel">
        ///     The level at which to write to the log.
        /// </param>
        /// <param name="message">
        ///     The fragment of message to write. Should be formed to fit into a sentence of the form "Starting XXX."
        /// </param>
        /// <param name="getAdditionalEndMessage">
        ///     An action that will be called when the operation has finished to get any additional details of the operation.
        /// </param>
        /// <returns>
        ///     A disposable object representing the scope of the operations. Call <see cref="IDisposable.Dispose" /> on
        ///     the returned object to complete the operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> is zero-length.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> is zero-length.
        /// </exception>
        [NotNull]
        public static IDisposable BeginOperation([NotNull] this ILogger logger, LogLevel logLevel, [NotNull] string message, [CanBeNull] Func<string>? getAdditionalEndMessage)
        {
            logger.Validate(nameof(logger), ObjectIs.NotNull);
            message.Validate(nameof(message), StringIs.NotNullEmptyOrWhiteSpace);

            return new OperationScope(logger, logLevel, logLevel, message, getAdditionalEndMessage);
        }

        /// <summary>
        ///     Creates a new operation scope.
        /// </summary>
        /// <param name="logger">
        ///     The logger to which messages will be written.
        /// </param>
        /// <param name="beginLogLevel">
        ///     The level at which to write the log message at the start of the operation.
        /// </param>
        /// <param name="endLogLevel">
        ///     The log level to use to write the message at the end of the operation.
        /// </param>
        /// <param name="message">
        ///     The fragment of message to write. Should be formed to fit into a sentence of the form "Starting XXX."
        /// </param>
        /// <returns>
        ///     A disposable object representing the scope of the operations. Call <see cref="IDisposable.Dispose" /> on
        ///     the returned object to complete the operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="message" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> is zero-length.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> contains only white space characters.
        /// </exception>
        [NotNull]
        public static IDisposable BeginOperation([NotNull] this ILogger logger, LogLevel beginLogLevel, LogLevel endLogLevel, [NotNull] string message)
        {
            logger.Validate(nameof(logger), ObjectIs.NotNull);
            message.Validate(nameof(message), StringIs.NotNullEmptyOrWhiteSpace);

            return new OperationScope(logger, beginLogLevel, beginLogLevel, message, null);
        }

        /// <summary>
        ///     Creates a new operation scope.
        /// </summary>
        /// <param name="logger">
        ///     The logger to which messages will be written.
        /// </param>
        /// <param name="beginLogLevel">
        ///     The level at which to write the log message at the start of the operation.
        /// </param>
        /// <param name="endLogLevel">
        ///     The log level to use to write the message at the end of the operation.
        /// </param>
        /// <param name="message">
        ///     The fragment of message to write. Should be formed to fit into a sentence of the form "Starting XXX."
        /// </param>
        /// <param name="getAdditionalEndMessage">
        ///     An action that will be called when the operation has finished to get any additional details of the operation.
        /// </param>
        /// <returns>
        ///     A disposable object representing the scope of the operations. Call <see cref="IDisposable.Dispose" /> on
        ///     the returned object to complete the operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="logger" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="message" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> is zero-length.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="message" /> contains only white space characters.
        /// </exception>
        [NotNull]
        public static IDisposable BeginOperation([NotNull] this ILogger logger, LogLevel beginLogLevel, LogLevel endLogLevel, [NotNull] string message, [CanBeNull] Func<string>? getAdditionalEndMessage)
        {
            logger.Validate(nameof(logger), ObjectIs.NotNull);
            message.Validate(nameof(message), StringIs.NotNullEmptyOrWhiteSpace);

            return new OperationScope(logger, beginLogLevel, beginLogLevel, message, getAdditionalEndMessage);
        }

        /// <summary>
        ///     Logs the message given at the specified log level, with additional contextual information taken from the
        ///     context given.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="logLevel">
        ///     The log level at which to log the message.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void Log([CanBeNull] this ILogger logger, LogLevel logLevel, [CanBeNull] LoggingContext context, [CanBeNull] string message)
        {
            Log(logger, logLevel, context, null, message);
        }

        /// <summary>
        ///     Logs the message given at the specified log level, with additional contextual information taken from the
        ///     context given.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="logLevel">
        ///     The log level at which to log the message.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void Log([CanBeNull] this ILogger logger, LogLevel logLevel, [CanBeNull] string message)
        {
            Log(logger, logLevel, null, null, message);
        }

        /// <summary>
        ///     Logs the message given at the specified log level, with additional contextual information taken from the
        ///     context given.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="logLevel">
        ///     The log level at which to log the message.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void Log([CanBeNull] this ILogger logger, LogLevel logLevel, [CanBeNull] Exception exception, [CanBeNull] string message)
        {
            Log(logger, logLevel, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Critical", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogCritical([CanBeNull] this ILogger logger, string message)
        {
            Log(logger, LogLevel.Critical, null, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Critical", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogCritical([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, string message)
        {
            Log(logger, LogLevel.Critical, context, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Critical", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogCritical([CanBeNull] this ILogger logger, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Critical, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Critical", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogCritical([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Critical, context, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Debug", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogDebug([CanBeNull] this ILogger logger, string message)
        {
            Log(logger, LogLevel.Debug, null, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Debug", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogDebug([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, string message)
        {
            Log(logger, LogLevel.Debug, context, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Debug", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogDebug([CanBeNull] this ILogger logger, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Debug, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Debug", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogDebug([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Debug, context, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Error", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogError([CanBeNull] this ILogger logger, string message)
        {
            Log(logger, LogLevel.Error, null, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Error", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogError([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, string message)
        {
            Log(logger, LogLevel.Error, context, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Error", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogError([CanBeNull] this ILogger logger, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Error, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Error", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogError([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Error, context, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Information", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogInformation([CanBeNull] this ILogger logger, string message)
        {
            Log(logger, LogLevel.Information, null, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Information", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogInformation([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, string message)
        {
            Log(logger, LogLevel.Information, context, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Information", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogInformation([CanBeNull] this ILogger logger, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Information, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Information", with additional contextual information
        ///     taken from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogInformation([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Information, context, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Trace", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogTrace([CanBeNull] this ILogger logger, string message)
        {
            Log(logger, LogLevel.Trace, null, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Trace", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogTrace([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, string message)
        {
            Log(logger, LogLevel.Trace, context, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Trace", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogTrace([CanBeNull] this ILogger logger, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Trace, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Trace", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogTrace([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Trace, context, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Warning", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogWarning([CanBeNull] this ILogger logger, string message)
        {
            Log(logger, LogLevel.Warning, null, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Warning", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogWarning([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, string message)
        {
            Log(logger, LogLevel.Warning, context, null, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Warning", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogWarning([CanBeNull] this ILogger logger, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Warning, null, exception, message);
        }

        /// <summary>
        ///     Logs the message given with the log level set to "Warning", with additional contextual information taken
        ///     from the current thread's logging context.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        public static void LogWarning([CanBeNull] this ILogger logger, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception, string message)
        {
            Log(logger, LogLevel.Warning, context, exception, message);
        }

        /// <summary>
        ///     Logs the message given at the specified log level, with additional contextual information taken from the
        ///     context given.
        /// </summary>
        /// <param name="logger">
        ///     The logger into which to log the message given.
        /// </param>
        /// <param name="logLevel">
        ///     The log level at which to log the message.
        /// </param>
        /// <param name="context">
        ///     The context from which to take contextual information. If <see langword="null" /> the current thread's
        ///     logging context is used.
        /// </param>
        /// <param name="exception">
        ///     The exception to record alongside the message (can be left as <see langword="null" /> if there is no
        ///     exception to record).
        /// </param>
        /// <param name="message">
        ///     The message to log.
        /// </param>
        private static void Log([CanBeNull] ILogger logger, LogLevel logLevel, [CanBeNull] LoggingContext context, [CanBeNull] Exception exception,
            [CanBeNull] string message)
        {
            if(ReferenceEquals(logger, null))
            {
                return;
            }

            if(!logger.IsEnabled(logLevel))
            {
                return;
            }

            // If the ApplicationInsightsTelemeteryInitializer has failed to be loaded for some reason, or has never
            // been called to append contextual information, append the contextual data directly onto the message (less
            // readable, but still useful).
            string modifiedMessage;

            if(!AppendContextualInformationToLogMessages || ReferenceEquals(context, null))
            {
                modifiedMessage = message;
            }
            else
            {
                var contextualInformation = context.ToString();
                if(string.IsNullOrWhiteSpace(contextualInformation))
                {
                    modifiedMessage = message;
                }
                else
                {
                    modifiedMessage = string.Concat(message, "\r\n", contextualInformation);
                }
            }

            if(ReferenceEquals(exception, null))
            {
                Microsoft.Extensions.Logging.LoggerExtensions.Log(logger, logLevel, modifiedMessage);
            }
            else
            {
                Microsoft.Extensions.Logging.LoggerExtensions.Log(logger, logLevel, exception, modifiedMessage);
            }
        }
    }
}