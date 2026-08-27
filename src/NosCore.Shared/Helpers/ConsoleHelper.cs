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
        public static bool HasWindowSize =>
            !Console.IsOutputRedirected || !Console.IsErrorRedirected || !Console.IsInputRedirected;

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

        private static bool CanCarryTitle => !Console.IsOutputRedirected && GetConsoleWindow() != IntPtr.Zero;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
    }
}
