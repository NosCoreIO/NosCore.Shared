//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System.ComponentModel.DataAnnotations;

namespace NosCore.Shared.Configuration
{
    public class SqlConnectionConfiguration
    {
        [Required]
        public string? Host { get; set; }

        [Range(1, int.MaxValue)]
        public int Port { get; set; }

        [Required]

        public string? Database { get; set; }

        [Required]

        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public string ConnectionString =>
            $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};";
    }
}