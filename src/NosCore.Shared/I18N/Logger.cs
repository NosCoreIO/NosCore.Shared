//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace NosCore.Shared.I18N
{
    public static class Logger
    {
        private const int HeadlessWindowWidth = 21;

        private static IConfigurationRoot? _configuration;
        private static readonly string[] AsciiTitle =
        {
            @" __  _  __    __   ___ __  ___ ___ ",
            @"|  \| |/__\ /' _/ / _//__\| _ \ __|",
            @"| | ' | \/ |`._`.| \_| \/ | v / _| ",
            @"|_|\__|\__/ |___/ \__/\__/|_|_\___|",
            @"-----------------------------------"
        };

        public static void Initialize(IConfigurationRoot configuration)
        {
            _configuration = configuration;
            Log.Logger = GetLoggerConfiguration().CreateLogger();
        }

        public static LoggerConfiguration GetLoggerConfiguration()
        {
            return new LoggerConfiguration().ReadFrom.Configuration(_configuration!);
        }

        public static void PrintHeader(string text)
        {
            var titleLogger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                .CreateLogger();
            var width = GetWindowWidth();
            var offset = width / 2 + text?.Length / 2;
            var separator = new string('=', width - 1);
            titleLogger.Information(separator);
            foreach (var s in AsciiTitle)
            {
                titleLogger.Information(string.Format(CultureInfo.CurrentCulture, "{0," + (width / 2 + s.Length / 2) + "}", s));
            }

            titleLogger.Information(string.Format(CultureInfo.CurrentCulture, "{0," + offset + "}", text));
            titleLogger.Information(separator);
        }

        private static int GetWindowWidth()
        {
            try
            {
                return Console.WindowHeight > 0 ? Console.WindowWidth : HeadlessWindowWidth;
            }
            catch (IOException)
            {
                return HeadlessWindowWidth;
            }
        }
    }
}