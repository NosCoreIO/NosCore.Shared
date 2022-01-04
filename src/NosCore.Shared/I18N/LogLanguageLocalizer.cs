using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using NosCore.Data.Resource;

namespace NosCore.Core.I18N
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
                return !string.IsNullOrEmpty(localString.Value) ? localString : new LocalizedString(key.ToString(), $"#<{key}>", true);
            }
        }

        public LocalizedString this[T key, params object[] arguments]
        {
            get
            {
                var localString = _stringLocalizer[key.ToString(), arguments];
                return !string.IsNullOrEmpty(localString.Value) ? localString : new LocalizedString(key.ToString(), $"#<{key}>", true);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            _stringLocalizer.GetAllStrings(includeParentCultures);
    }
}
