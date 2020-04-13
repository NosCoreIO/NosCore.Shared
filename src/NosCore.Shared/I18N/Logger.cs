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
        private const string ConfigurationPath = "../../../configuration";

        private static IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + ConfigurationPath)
            .AddYamlFile("logger.yml", false)
            .Build();

        private static readonly string[] AsciiTitle =
        {
            @" __  _  __    __   ___ __  ___ ___ ",
            @"|  \| |/__\ /' _/ / _//__\| _ \ __|",
            @"| | ' | \/ |`._`.| \_| \/ | v / _| ",
            @"|_|\__|\__/ |___/ \__/\__/|_|_\___|",
            @"-----------------------------------"
        };

        public static void SetLoggerConfiguration(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public static LoggerConfiguration GetLoggerConfiguration()
        {
            return new LoggerConfiguration().ReadFrom.Configuration(_configuration);
        }

        public static void PrintHeader(string text)
        {
            Log.Logger = GetLoggerConfiguration().CreateLogger();

            var titleLogger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                .CreateLogger();
            var offset = Console.WindowWidth / 2 + text?.Length / 2;
            var separator = new string('=', Console.WindowHeight > 0 ? Console.WindowWidth - 1 : 20);
            titleLogger.Information(separator);
            foreach (var s in AsciiTitle)
            {
                titleLogger.Information(string.Format(CultureInfo.CurrentCulture, "{0," + (Console.WindowWidth / 2 + s.Length / 2) + "}", s));
            }

            titleLogger.Information(string.Format(CultureInfo.CurrentCulture, "{0," + offset + "}", text));
            titleLogger.Information(separator);
        }
    }
}