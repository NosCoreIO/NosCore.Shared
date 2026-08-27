//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.Helpers;
using NosCore.Shared.I18N;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class ConsoleHelperTests
    {
        [TestMethod]
        public void TitleIsSkippedWhenNoConsoleCanCarryIt()
        {
            ConsoleHelper.SetTitle("NosCore");
            ConsoleHelper.AppendTitle(" - Port : 4000");
        }

        [TestMethod]
        public void HeaderPrintsWhateverTheWindowSizeIs()
        {
            Logger.PrintHeader("NosCore");
        }
    }
}
