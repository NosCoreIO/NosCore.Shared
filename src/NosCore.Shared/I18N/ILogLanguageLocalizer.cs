//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace NosCore.Shared.I18N;

public interface ILogLanguageLocalizer<T> where T : struct, Enum
{
    LocalizedString this[T key, params object[] arguments] { get; }
    LocalizedString this[T key] { get; }
    IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures);
}