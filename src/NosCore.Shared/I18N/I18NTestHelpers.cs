using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NosCore.Shared.I18N
{
    public static class I18NTestHelpers
    {
        public static IEnumerable<T> GetKeysWithMissingTranslations<T>(ILogLanguageLocalizer<T> localizer) where T : struct, Enum
        {
            foreach (var val in Enum.GetValues(typeof(T)))
            {
                var value = localizer[(T)val!];
                if (value.ResourceNotFound || value == $"#<{val}>")
                {
                    yield return (T)val;
                }
            }
        }

        public static IEnumerable<string> GetUselessTranslations(ILogLanguageLocalizer localizer, List<string> keys)
        {
            return localizer.GetAllStrings(false)
                .Where(resourceKey => !keys.Contains(resourceKey.Name))
                .Select(translation => translation.Value);
        }

        public static IEnumerable<T> GetUselessLanguageKeys<T>()
        {
            var dict = new Dictionary<string, int>();
            var list = Directory.GetFiles(Environment.CurrentDirectory + @"../../..", "*.cs",
                SearchOption.AllDirectories);
            foreach (var file in list)
            {
                var content = File.ReadAllText(file);
                var regex = new Regex(@$"{typeof(T).Name}\.[0-9A-Za-z_]*");
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

            var enums = Enum.GetValues(typeof(T));
            foreach (var val in enums)
            {
                if (!dict.ContainsKey($"{typeof(T).Name}.{val}"))
                {
                    yield return (T)val;
                }
            }
        }
    }
}
