//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using NosCore.Shared.Enumerations;

namespace NosCore.Shared.Configuration
{
    [Serializable]
    public class LanguageConfiguration
    {
        public RegionType Language { get; set; }
    }
}