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

using JetBrains.Annotations;

namespace OpenCollar.Extensions.Logging
{
    /// <summary>
    ///     A class that allows information to be captured for use in providing contextual information when logging.
    /// </summary>
    public sealed class LoggingContext
    {
        /// <summary>
        ///     A string that represents the role of the application (e.g. Web App, Func App, ..).
        /// </summary>
        [CanBeNull] private static string _hostRole;

        /// <summary>
        ///     Defines the context for the current thread.
        /// </summary>
        [ThreadStatic]
        [CanBeNull]
        private static LoggingContext _threadContext;

        /// <summary>
        ///     A dictionary containing the contextual information set in the current logging context. Keys are case insensitive.
        /// </summary>
        [NotNull]
        private readonly Dictionary<string, string> _contextualInformation = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///     The parent logging context from which to inherit contextual information.
        /// </summary>
        [CanBeNull]
        private readonly LoggingContext _parentContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingContext" /> class.
        /// </summary>
        /// <param name="parentContext">
        ///     The parent logging context from which to inherit contextual information.
        /// </param>
        private LoggingContext([CanBeNull] LoggingContext parentContext)
        {
            _parentContext = parentContext;

            InitializeHostInformation();
        }

        /// <summary>
        ///     Clears the context for the current thread.
        /// </summary>
        public static void ClearContext()
        {
            _threadContext = null;
        }

        /// <summary>
        ///     Gets the context for the current thread.
        /// </summary>
        /// <returns>
        ///     The context for the current thread.
        /// </returns>
        [NotNull]
        public static LoggingContext GetContext() => GetContext(null);

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
        [NotNull]
        public static LoggingContext GetContext([CanBeNull] LoggingContext parentContext)
        {
            if (!ReferenceEquals(_threadContext, null))
            {
                return _threadContext;
            }

            if (!ReferenceEquals(_threadContext, null) && ReferenceEquals(_threadContext._parentContext, parentContext))
            {
                return _threadContext;
            }

            _threadContext = new LoggingContext(parentContext);

            return _threadContext;
        }

        /// <summary>
        ///     Sets the host role to be used when logging to application insights.
        /// </summary>
        /// <param name="hostRole">
        ///     The host role.
        /// </param>
        public static void SetHostRole([CanBeNull] string hostRole)
        {
            _hostRole = hostRole;
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
        public void AddInformation([CanBeNull] string key, [CanBeNull] string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                // We mustn't throw exceptions or do anything disruptive, this is just logging after all.
                return;
            }

            _contextualInformation[key] = value;
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
        public void AddInformation([CanBeNull] string key, int value)
        {
            AddInformation(key, value.ToString("D0", CultureInfo.InvariantCulture));
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
        public void AddInformation([CanBeNull] string key, int? value)
        {
            if (!value.HasValue)
            {
                AddInformation(key, "NULL");
                return;
            }

            AddInformation(key, value.Value);
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
        public void AddInformation([CanBeNull] string key, Guid value)
        {
            if (value == Guid.Empty)
            {
                AddInformation(key, "EMPTY");
                return;
            }

            AddInformation(key, value.ToString("D", CultureInfo.InvariantCulture));
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
        public void AddInformation([CanBeNull] string key, Guid? value)
        {
            if (!value.HasValue)
            {
                AddInformation(key, "NULL");
                return;
            }

            AddInformation(key, value.Value);
        }

        /// <summary>
        ///     Removes any information from the current context associated with the key given.
        /// </summary>
        /// <param name="key">
        ///     The key identifying the item of information. This is case-insensitive. <see langword="null" />,
        ///     zero-length or white-space only keys are ignored.
        /// </param>
        public void ClearInformation([CanBeNull] string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                // We mustn't throw exceptions or do anything disruptive, this is just logging after all.
                return;
            }

            _contextualInformation.Remove(key);
        }

        /// <summary>
        ///     Removes all information from the current context.
        /// </summary>
        /// <returns>
        ///     Returns this logging context, allow fluent-style chaining of commands.
        /// </returns>
        [NotNull]
        public LoggingContext ClearInformation()
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
        [NotNull]
        public KeyValuePair<string, string>[] GetSnapshot() => _contextualInformation.ToArray();

        /// <summary>
        ///     Reload the contextual information from the snapshot given.
        /// </summary>
        /// <param name="snapshot">
        ///     The snapshot from which to reload.
        /// </param>
        public void RevertToSnapshot([CanBeNull] KeyValuePair<string, string>[] snapshot)
        {
            if (ReferenceEquals(snapshot, null))
            {
                return;
            }

            _contextualInformation.Clear();
            foreach (var pair in snapshot)
            {
                if (string.IsNullOrWhiteSpace(pair.Key))
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
        [NotNull]
        public ITransientContextualInformationScope StartScope() => new TransientContextualInformationScope(this);

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            Dictionary<string, string> contextualInformation;
            if (ReferenceEquals(_parentContext, null))
            {
                // Don't create a new instance if we don't have to.
                contextualInformation = _contextualInformation;
            }
            else
            {
                contextualInformation = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                BuildAggregateContext(contextualInformation);
            }

            if (_contextualInformation.Count <= 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (var pair in contextualInformation)
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
        ///     Builds the aggregate context from the current context and any parent contexts.
        /// </summary>
        /// <param name="contextualInformation">
        ///     The dictionary of contextual information to which to add.
        /// </param>
        /// <remarks>
        ///     Contextual information from lower down the tree always takes priority.
        /// </remarks>
        private void BuildAggregateContext([NotNull] Dictionary<string, string> contextualInformation)
        {
            /*
             * It is possible that the parent came from another thread and potentially could be changed while we iterate, so
             * we take a snapshot using ToArray() to prevent an error.
             */
            foreach (var pair in _contextualInformation.ToArray())
            {
                if (!contextualInformation.ContainsKey(pair.Key))
                {
                    contextualInformation.Add(pair.Key, pair.Value);
                }
            }

            if (!ReferenceEquals(_parentContext, null))
            {
                _parentContext.BuildAggregateContext(contextualInformation);
            }
        }

        /// <summary>
        ///     Captures environmental information about the host.
        /// </summary>
        private void InitializeHostInformation()
        {
            if (!ReferenceEquals(_parentContext, null))
            {
                // Only capture machine context if we haven't got a parent that has already done so.
                return;
            }

            // Capture some information about the host environment.
            var value = Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SITE_NAME");
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Host.Website.Name", value);
            }

            value = Environment.GetEnvironmentVariable("COMPUTERNAME");
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("USERNAME");
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Host.Computer.Name", value);
            }

            value = Environment.GetEnvironmentVariable("APP_POOL_ID");
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("WEBSITE_IIS_SITE_NAME");
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Host.AppPool", value);
            }

            value = Environment.GetEnvironmentVariable("WEBSITE_RESOURCE_GROUP");
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Host.ResourceGroup", value);
            }

            value = Environment.GetEnvironmentVariable("HTTP_AUTHORITY");
            if (string.IsNullOrWhiteSpace(value))
            {
                value = Environment.GetEnvironmentVariable("HTTP_HOST");
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Host.Authority", value);
            }

            value = Environment.GetEnvironmentVariable("Meta:Environment");
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Meta.Environment", value);
            }

            value = Environment.GetEnvironmentVariable("Meta:PortalUrl");
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Meta.Portal.Url", value);
            }

            value = Environment.GetEnvironmentVariable("Meta:Region");
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Meta.Region", value);
            }

            value = Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SLOT_NAME");
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddInformation("Host.Slot.Name", value);
            }

            AddInformation("App.Version", GetType().Assembly.GetName().Version.ToString(4));
            AddInformation("App.Role", _hostRole);
        }
    }
}