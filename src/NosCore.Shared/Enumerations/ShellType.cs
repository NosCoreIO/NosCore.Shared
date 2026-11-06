//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

namespace NosCore.Shared.Enumerations
{
    // The kind a shell item names in its data file; it decides which option groups the shell can roll.
    public enum ShellType : byte
    {
        Half = 0,
        Full = 1,
        Special = 2,
        Pvp = 3,
        Perfect = 4
    }
}
