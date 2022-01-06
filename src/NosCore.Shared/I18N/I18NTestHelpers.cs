using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NosCore.Shared.I18N
{
    public static class I18NTestHelpers
    {
        public static IEnumerable<T> GetUnusedLanguageKeys<T>(ILogLanguageLocalizer<T> localizer) where T : struct, Enum
        {
            foreach (var val in Enum.GetValues(typeof(T)))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(typeof(T).ToString());
                var value = localizer[(T)val!];
                if (value.ResourceNotFound || value == $"#<{val}>")
                {
                    yield return (T)val;
                }
            }
        }

        public static IEnumerable<string> GetUselessTranslations<T>(ILogLanguageLocalizer<T> localizer, List<string> keys) where T : struct, Enum
        {
            return localizer.GetAllStrings(false)
                .Where(resourceKey => !keys.Contains(resourceKey.Name))
                .Select(translation => translation.Value);
        }

        public static IEnumerable<T> GetUselessLanguageKeys<T>(Type keyType)
        {
            var uselessKeys = new StringBuilder();
            var dict = new Dictionary<string, int>();
            var list = Directory.GetFiles(Environment.CurrentDirectory + @"../../..", "*.cs",
                SearchOption.AllDirectories);
            foreach (var file in list)
            {
                var content = File.ReadAllText(file);
                var regex = new Regex(@$"{keyType.Name}\.[0-9A-Za-z_]*");
                var matches = regex.Matches(content);
                foreach (Match? match in matches)
                {
                    if (match?.Success != true)
                    {
                        continue;
                    }

                    if (dict.ContainsKey(match.Value))
                    {
                        dict[match.Value]++;
                    }
                    else
                    {
                        dict.Add(match.Value, 1);
                    }
                }
            }

            var enums = Enum.GetValues(keyType);
            foreach (var val in enums)
            {
                if (!dict.ContainsKey($"{keyType.Name}.{val}"))
                {
                    yield return (T)val;
                }
            }
        }
    }
}
