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

namespace OpenCollar.Extensions.Logging
{
    /// <summary>
    ///     A disposable object that allows the values in the logging context to be temporarily changed and then
    ///     reverted to their original values when disposed of.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ITransientContextualInformationScope : IDisposable
    {
        /// <summary>
        ///     Gets the current context.
        /// </summary>
        /// <value>
        ///     The current context.
        /// </value>
        [NotNull]
        LoggingContext Context { get; }
    }
}