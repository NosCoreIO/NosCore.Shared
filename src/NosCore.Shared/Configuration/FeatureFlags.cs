//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System.Collections.Generic;

namespace NosCore.Shared.Configuration
{
    public class FeatureFlags : Dictionary<FeatureFlag, bool>
    {
        public new bool this[FeatureFlag key] => TryGetValue(key, out var value) && value;
    }
}