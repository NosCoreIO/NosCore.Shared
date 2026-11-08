//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;
using NosCore.Shared.Enumerations;

namespace NosCore.Shared.I18N
{
    public class LogLanguageLocalizer<T, T2> : ILogLanguageLocalizer<T> where T : struct, Enum
    {
        private readonly IStringLocalizer<T2> _stringLocalizer;

        public LogLanguageLocalizer(IStringLocalizer<T2> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public LocalizedString this[T key]
        {
            get
            {
                var localString = _stringLocalizer[key.ToString()];
                return Translated(key, localString);
            }
        }

        public LocalizedString this[T key, params object[] arguments]
        {
            get
            {
                var localString = _stringLocalizer[key.ToString(), arguments];
                return Translated(key, localString);
            }
        }

        // IStringLocalizer answers a missing key with the key itself, so ResourceNotFound is what tells it apart.
        private static LocalizedString Translated(T key, LocalizedString localString) =>
            !localString.ResourceNotFound && !string.IsNullOrEmpty(localString.Value)
                ? localString
                : new LocalizedString(key.ToString(), $"#<{key}>", true);

        public IEnumerable<LocalizedString> GetAllStrings() =>
            _stringLocalizer.GetAllStrings(CultureInfo.CurrentUICulture.Equals(new CultureInfo(RegionType.EN.ToString())));
    }
}
