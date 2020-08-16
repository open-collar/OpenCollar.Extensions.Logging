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

using OpenCollar.Extensions.Logging.TESTS.Mocks;

using Xunit;

namespace OpenCollar.Extensions.Logging.TESTS
{
    public sealed class OperationScopeTest
    {
        [Fact]
        public void TestBasics()
        {
            var logger = new LoggerMock();
            logger.LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace;

            var x = new OperationScope(logger, Microsoft.Extensions.Logging.LogLevel.Critical, Microsoft.Extensions.Logging.LogLevel.Critical, "MESSAGE", null);

            Assert.NotNull(x);
            Assert.NotEmpty(logger.LastMessage);

            logger.LastMessage = null;
            x.Dispose();

            Assert.NotEmpty(logger.LastMessage);
        }
    }
}