//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace NosCore.Shared.Configuration
{
    [Serializable]
    public class ServerConfiguration : LanguageConfiguration
    {
        public string? Host { get; set; }

        [Range(1, int.MaxValue)]
        public int Port { get; set; }

        public override string ToString()
        {
            return Host + ":" + Port;
        }
    }
}