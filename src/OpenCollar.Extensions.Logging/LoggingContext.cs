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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace OpenCollar.Extensions.Logging
{
    /// <summary>
    ///     A class that allows information to be captured for use in providing contextual information when logging.
    /// </summary>
    public sealed class LoggingContext
    {
        /// <summary>
        ///     The value recorded when an empty value is recorded.
        /// </summary>
        private const string _emptyInformation = @"EMPTY";

        /// <summary>
        ///     The value recorded when a <see langword="null" /> value is recorded.
        /// </summary>
        private const string _nullInformation = @"NULL";

        /// <summary>
        ///     Defines the context for the current thread.
        /// </summary>
        /// <remarks>
        ///     We use the <see cref="AsyncLocal{T}" /> class to ensure that the value is local to the current task.
        /// </remarks>
        private static AsyncLocal<LoggingContext?> _threadContext = new AsyncLocal<LoggingContext?>();

        /// <summary>
        ///     A dictionary containing the contextual information set in the current logging context. Keys are case insensitive.
        /// </summary>
        private readonly Dictionary<string, string> _contextualInformation = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingContext" /> class.
        /// </summary>
        /// <param name="parentContext">
        ///     The parent logging context from which to inherit contextual information. This is typically used when the
        ///     context is to be passed to a new thread.
        /// </param>
        private LoggingContext(LoggingContext? parentContext)
        {
            if(!ReferenceEquals(parentContext, null))
            {
                foreach(var pair in parentContext._contextualInformation)
                {
                    _contextualInformation.Add(pair.Key, pair.Value);
                }
            }
            else
            {
                InitializeHostInformation();
            }
        }

        /// <summary>
        ///     Gets the information identified by the key given..
        /// </summary>
        /// <param name="key">
        ///     The key identifing the information requested.
        /// </param>
        /// <returns>
        ///     The information item requested, or <see langword="null" /> if there was no corresponding item.
        /// </returns>
        public string? this[string key]
        {
            get
            {
                if(_contextualInformation.TryGetValue(key, out var value))
                {
                    return value;
                }

                return null;
            }
        }

        /// <summary>
        ///     Clears the context for the current thread.
        /// </summary>
        public static void Clear()
        {
            _threadContext.Value = null;
        }

        /// <summary>
        ///     Gets the context for the current thread.
        /// </summary>
        /// <returns>
        ///     The context for the current thread.
        /// </returns>
        public static LoggingContext Current() => Current(null);

        /// <summary>
        ///     Gets the context for the current thread, initializing it from the state of the an existing context (e.g.
        ///     the parent thread's context when a child thread is created).
        /// </summary>
        /// <param name="parentContext">
        ///     The parent logging context from which to inherit contextual information.
        /// </param>
        /// <returns>
        ///     The context for the current thread.
        /// </returns>
        public static LoggingContext Current(LoggingContext? parentContext)
        {
            if(!ReferenceEquals(_threadContext.Value, null))
            {
                return _threadContext.Value;
            }

            var context = new LoggingContext(parentContext);

            _threadContext.Value = context;

            return context;
        }

        /// <summary>
        ///     Adds the information to the context in the form of a key/value pair.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        /// <param name="value">
        ///     This is the value to record against the key given. This is typically an ID that we may want to find when
        ///     searching logs.
        /// </param>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext AppendInfo(string? key, Guid value)
        {
            if(value == Guid.Empty)
            {
                AppendInfo(key, _emptyInformation);
                return this;
            }

            return AppendInfo(key, value.ToString("D", CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Adds the information to the context in the form of a key/value pair.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        /// <param name="value">
        ///     This is the value to record against the key given. This is typically an ID that we may want to find when
        ///     searching logs.
        /// </param>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext AppendInfo(string? key, string? value)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                // We mustn't throw exceptions or do anything disruptive, this is just logging after all.
                return this;
            }

            _contextualInformation[key] = value;

            return this;
        }

        /// <summary>
        ///     Adds the information to the context in the form of a key/value pair.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        /// <param name="value">
        ///     This is the value to record against the key given. This is typically an ID that we may want to find when
        ///     searching logs.
        /// </param>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext AppendInfo(string? key, int value)
        {
            return AppendInfo(key, value.ToString("D0", CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Adds the information to the context in the form of a key/value pair.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        /// <param name="value">
        ///     This is the value to record against the key given. This is typically an ID that we may want to find when
        ///     searching logs.
        /// </param>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext AppendInfo(string? key, int? value)
        {
            if(!value.HasValue)
            {
                AppendInfo(key, _nullInformation);
                return this;
            }

            return AppendInfo(key, value.Value);
        }

        /// <summary>
        ///     Adds the information to the context in the form of a key/value pair.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        /// <param name="value">
        ///     This is the value to record against the key given. This is typically an ID that we may want to find when
        ///     searching logs.
        /// </param>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext AppendInfo(string? key, Guid? value)
        {
            if(!value.HasValue)
            {
                AppendInfo(key, _nullInformation);
                return this;
            }

            return AppendInfo(key, value.Value);
        }

        /// <summary>
        ///     Removes any information from the current context associated with the key given.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext ClearInfo(string? key)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                // We mustn't throw exceptions or do anything disruptive, this is just logging after all.
                return this;
            }

            _contextualInformation.Remove(key);

            return this;
        }

        /// <summary>
        ///     Removes all information from the current context.
        /// </summary>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        public LoggingContext ClearInfo()
        {
            _contextualInformation.Clear();

            InitializeHostInformation();

            return this;
        }

        /// <summary>
        ///     Gets a snapshot of the current values held in the context.
        /// </summary>
        /// <returns>
        ///     An array containing the key values pairs currently in the context.
        /// </returns>
        public KeyValuePair<string, string>[] GetSnapshot() => _contextualInformation.ToArray();

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            var contextualInformation = _contextualInformation.ToArray();

            if(_contextualInformation.Count <= 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach(var pair in contextualInformation)
            {
                sb.Append("\r\n[");

                sb.Append(pair.Key.ToUpperInvariant());

                sb.Append(':');

                sb.Append(pair.Value);

                sb.Append(']');
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Reload the contextual information from the snapshot given.
        /// </summary>
        /// <param name="snapshot">
        ///     The snapshot from which to reload.
        /// </param>
        internal void RevertToSnapshot(KeyValuePair<string, string>[]? snapshot)
        {
            if(ReferenceEquals(snapshot, null))
            {
                return;
            }

            _contextualInformation.Clear();
            foreach(var pair in snapshot)
            {
                if(string.IsNullOrWhiteSpace(pair.Key))
                {
                    return;
                }

                _contextualInformation[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        ///     Initializes and returns a scope that, when disposed of, will revert the contextual information to the
        ///     values at the time of calling.
        /// </summary>
        /// <returns>
        ///     An object that implements the <see cref="ITransientContextualInformationScope" /> interface that will
        ///     revert the contextual information held in this logging context on disposal.
        /// </returns>
        internal ITransientContextualInformationScope StartScope() => new TransientContextualInformationScope(this);

        /// <summary>
        ///     Captures environmental information about the host, capturing data commonly found in Azure and Windows environments.
        /// </summary>
        private void InitializeHostInformation()
        {
            // Capture some information about the host environment.
            var value = Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SITE_NAME");
            if(string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");
            }

            if(!string.IsNullOrWhiteSpace(value))
            {
                AppendInfo("Host.Website.Name", value);
            }

            value = Environment.GetEnvironmentVariable("COMPUTERNAME");
            if(string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("USERNAME");
            }

            if(!string.IsNullOrWhiteSpace(value))
            {
                AppendInfo("Host.Computer.Name", value);
            }

            value = Environment.GetEnvironmentVariable("APP_POOL_ID");
            if(string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("WEBSITE_IIS_SITE_NAME");
            }

            if(!string.IsNullOrWhiteSpace(value))
            {
                AppendInfo("Host.AppPool", value);
            }

            value = Environment.GetEnvironmentVariable("WEBSITE_RESOURCE_GROUP");
            if(!string.IsNullOrWhiteSpace(value))
            {
                AppendInfo("Host.ResourceGroup", value);
            }

            value = Environment.GetEnvironmentVariable("HTTP_AUTHORITY");
            if(string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("HTTP_HOST");
            }

            if(!string.IsNullOrWhiteSpace(value))
            {
                AppendInfo("Host.Authority", value);
            }

            value = Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SLOT_NAME");
            if(!string.IsNullOrWhiteSpace(value))
            {
                AppendInfo("Host.Slot.Name", value);
            }
        }
    }
}