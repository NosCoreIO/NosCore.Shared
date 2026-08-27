//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.Runtime.InteropServices;

namespace NosCore.Shared.Helpers
{
    public static class ConsoleHelper
    {
        // Console.WindowWidth reads the screen buffer through stdout, then stderr. Its third
        // try, stdin, cannot answer: a console input handle has no screen buffer, so a process
        // whose output is piped while stdin is still on the console has no width to read.
        public static bool HasWindowSize => !Console.IsOutputRedirected || !Console.IsErrorRedirected;

        public static void SetTitle(string title)
        {
            if (OperatingSystem.IsWindows() && CanCarryTitle)
            {
                Console.Title = title;
            }
        }

        public static void AppendTitle(string suffix)
        {
            if (OperatingSystem.IsWindows() && CanCarryTitle)
            {
                Console.Title += suffix;
            }
        }

        public static bool CanCarryTitle => !Console.IsOutputRedirected && GetConsoleWindow() != IntPtr.Zero;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
    }
}
