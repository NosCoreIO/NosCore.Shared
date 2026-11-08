//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using NosCore.Shared.I18N;

namespace NosCore.Shared.Configuration
{
    public class ConfiguratorBuilder
    {
        private const string ConfigurationPath = "../../configuration";

        // ${NAME}, ${NAME,fallback} or ${NAME fallback}. The fallback replaces a variable that is
        // unset or empty; a placeholder with neither a value nor a fallback is left as written.
        private static readonly Regex EnvironmentPlaceholder = new(
            @"\$\{(?<variable>[A-Za-z_][A-Za-z0-9_]*)(?:\s*,\s*(?<fallback>[^}]*)|\s+(?<fallback>[^}]+))?\}",
            RegexOptions.Compiled);

        private static void ReplaceEnvironment(IConfiguration configuration)
        {
            foreach (var section in configuration.GetChildren().ToList())
            {
                if (section.Value != null)
                {
                    section.Value = EnvironmentPlaceholder.Replace(section.Value, match =>
                    {
                        var value = Environment.GetEnvironmentVariable(match.Groups["variable"].Value);
                        if (!string.IsNullOrEmpty(value))
                        {
                            return value;
                        }

                        var fallback = match.Groups["fallback"];
                        return fallback.Success ? fallback.Value : match.Value;
                    });
                }
                else
                {
                    ReplaceEnvironment(section);
                }
            }
        }

        public static IConfigurationRoot InitializeConfiguration(string[] args, string[] fileNames)
        {
            var pathIndex = Array.IndexOf(args!, "--config");
            string? path = null;
            if (pathIndex > -1 && args?.Length > pathIndex + 1)
            {
                path = Path.IsPathRooted(args![pathIndex + 1]) ? args[pathIndex + 1] : AppDomain.CurrentDomain.BaseDirectory + args[pathIndex + 1];
            }
            var conf = new ConfigurationBuilder()
                .SetBasePath(path ?? AppDomain.CurrentDomain.BaseDirectory + ConfigurationPath);
            foreach (var fileName in fileNames)
            {
                conf.AddYamlFile(fileName, false);
            }
            var confBuild = conf.Build();
            ReplaceEnvironment(confBuild);
            Logger.Initialize(confBuild);
            return confBuild;
        }
    }
}
