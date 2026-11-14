//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.I18N;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void HeaderWithoutTextIsPrinted()
        {
            Logger.PrintHeader(null!);
        }
    }
}
