//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using NosCore.Shared.Enumerations;

namespace NosCore.Shared.Configuration
{
    [Serializable]
    public class WebApiConfiguration : ServerConfiguration
    {
        [Required]
        public string? Password { get; set; }

        public HashingType HashingType { get; set; }

        public string? Salt { get; set; }
    }
}