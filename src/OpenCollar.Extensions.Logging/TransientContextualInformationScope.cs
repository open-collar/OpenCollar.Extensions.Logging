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

using System.Collections.Generic;

using JetBrains.Annotations;

namespace OpenCollar.Extensions.Logging
{
    /// <summary>
    ///     A class that allows values to be reverted after a temporary section of code.
    /// </summary>
    /// <seealso cref="OpenCollar.Extensions.Disposable" />
    internal sealed class TransientContextualInformationScope : Disposable, ITransientContextualInformationScope
    {
        /// <summary>
        ///     The logging context from which to clear values.
        /// </summary>
        [NotNull]
        private readonly LoggingContext _context;

        /// <summary>
        ///     The snapshot of the information held in the logging context at the start of the scope.
        /// </summary>
        [CanBeNull]
        private readonly KeyValuePair<string, string>[] _snapshot;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransientContextualInformationScope" /> class.
        /// </summary>
        /// <param name="context">
        ///     The logging context from which to clear values.
        /// </param>
        internal TransientContextualInformationScope([NotNull] LoggingContext context)
        {
            _context = context;
            _snapshot = _context.GetSnapshot();
        }

        /// <summary>
        ///     Gets the current context.
        /// </summary>
        /// <value>
        ///     The current context.
        /// </value>
        public LoggingContext Context => _context;

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to
        ///     release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _context.RevertToSnapshot(_snapshot);
            }

            base.Dispose(disposing);
        }
    }
}