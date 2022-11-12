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

        private static IConfiguration ReplaceEnvironment<T>(IConfiguration configuration, T strongTypedConfiguration)
        {
            var ses = configuration.GetChildren().ToList();
            for (var index = ses.Count; index > 0; index--)
            {
                if (ses[index - 1].Value != null)
                {
                    var regexp = new Regex(@"\${(?<variable>[a-zA-Z_]+)\s*,?\s*(?<fallback>[^}]+)}");
                    var matches = regexp.Matches(ses[index - 1].Value ?? "");
                    foreach (var match in matches.ToList())
                    {
                        var value = Environment.GetEnvironmentVariable(match.Groups[1].Value);
                        if (string.IsNullOrEmpty(value) && (match.Groups.Count > 2) && (match.Groups[2].Value != null))
                        {
                            value ??= match.Groups[2].Value;
                        }
                        ses[index - 1].Value = regexp.Replace(ses[index - 1].Value ?? "", value!, 1);
                    }
                }
                else if (ses[index - 1].GetChildren().Any())
                {
                    ReplaceEnvironment(configuration.GetSection(ses[index - 1].Path), configuration).Bind(strongTypedConfiguration);
                }
            }

            return configuration;
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
            Logger.Initialize(confBuild);
            ReplaceEnvironment(confBuild, confBuild);
            return confBuild;
        }
    }
}
