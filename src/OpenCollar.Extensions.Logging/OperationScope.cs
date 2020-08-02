﻿/*
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
using System.Globalization;

using JetBrains.Annotations;

namespace OpenCollar.Extensions.Logging
{
    /// <summary>
    ///     A class used to represent the scope of an operation and automatically write entry and exit messages to the
    ///     log specified.
    /// </summary>
    /// <remarks>
    ///     Use <see cref="IDisposable.Dispose" /> to exit the scope.
    /// </remarks>
    /// <seealso cref="OpenCollar.Extensions.Disposable" />
    internal sealed class OperationScope : Disposable
    {
        /// <summary>
        ///     The logger to which messages will be written.
        /// </summary>
        [NotNull]
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        /// <summary>
        ///     The level at which to write to the log.
        /// </summary>
        private readonly Microsoft.Extensions.Logging.LogLevel _logLevel;

        /// <summary>
        ///     The fragment of message to write. Should be formed to fit into a sentence of the form "Starting XXX."
        /// </summary>
        [NotNull]
        private readonly string _message;

        /// <summary>
        ///     The UTC date/time at which the operation started.
        /// </summary>
        private readonly DateTime _start;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationScope" /> class.
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
        internal OperationScope([NotNull] Microsoft.Extensions.Logging.ILogger logger, Microsoft.Extensions.Logging.LogLevel logLevel, [NotNull] string message)
        {
            _logLevel = logLevel;

            _start = DateTime.UtcNow;
            _logger = logger;
            _message = message;

            if(logLevel != Microsoft.Extensions.Logging.LogLevel.None)
            {
                _logger.Log(logLevel, string.Concat(@"Starting: ", message, @"."));
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to
        ///     release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (_logLevel != Microsoft.Extensions.Logging.LogLevel.None))
            {
                var duration = DateTime.UtcNow - _start;
                _logger.Log(_logLevel, string.Concat(@"Finished: ", _message, @".  Duration: ", duration.TotalMilliseconds.ToString("N0", CultureInfo.InvariantCulture), @"ms."));
            }

            base.Dispose(disposing);
        }
    }
}