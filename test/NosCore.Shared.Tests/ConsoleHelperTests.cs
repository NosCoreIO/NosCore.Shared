//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.Helpers;
using NosCore.Shared.I18N;

namespace NosCore.Shared.Tests
{
    // A test process cannot choose whether it owns a console, so each case asserts the
    // half of the contract its own state can prove and stands aside for the other.
    [TestClass]
    public class ConsoleHelperTests
    {
        private const int HeadlessSeparatorWidth = 20;

        [TestMethod]
        public void TitleIsLeftAloneWhenNothingCanCarryIt()
        {
            if (ConsoleHelper.CanCarryTitle)
            {
                Assert.Inconclusive("This process owns a console, so the title is expected to change.");
            }

            var before = ReadTitle();

            ConsoleHelper.SetTitle("NosCore");
            ConsoleHelper.AppendTitle(" - Port : 4000");

            Assert.AreEqual(before, ReadTitle());
        }

        [TestMethod]
        public void TitleIsSetWhenAConsoleCanCarryIt()
        {
            if (!ConsoleHelper.CanCarryTitle)
            {
                Assert.Inconclusive("This process owns no console to carry a title.");
            }

            ConsoleHelper.SetTitle("NosCore");
            ConsoleHelper.AppendTitle(" - Port : 4000");

            Assert.AreEqual("NosCore - Port : 4000", ReadTitle());
        }

        [TestMethod]
        public void HeaderFallsBackToAFixedWidthWhenTheSizeIsUnreadable()
        {
            if (ConsoleHelper.HasWindowSize)
            {
                Assert.Inconclusive("This process can read a console width, so the fallback does not apply.");
            }

            Assert.AreEqual(new string('=', HeadlessSeparatorWidth), PrintHeaderAndReadSeparator());
        }

        [TestMethod]
        public void HeaderUsesTheConsoleWidthWhenThereIsOne()
        {
            if (!ConsoleHelper.HasWindowSize || Console.WindowHeight <= 0)
            {
                Assert.Inconclusive("This process cannot read a console width.");
            }

            Assert.AreEqual(new string('=', Console.WindowWidth - 1), PrintHeaderAndReadSeparator());
        }

        private static string PrintHeaderAndReadSeparator()
        {
            var captured = new StringWriter();
            var original = Console.Out;
            try
            {
                Console.SetOut(captured);
                Logger.PrintHeader("NosCore");
            }
            finally
            {
                Console.SetOut(original);
            }

            return captured.ToString().Split('\n')[0].TrimEnd('\r');
        }

        private static string? ReadTitle()
        {
            if (!OperatingSystem.IsWindows())
            {
                return null;
            }

            try
            {
                return Console.Title;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
