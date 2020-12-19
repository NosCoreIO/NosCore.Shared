
using System;
using System.Text;
#if NET5_0 || NET5
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
#endif
using Microsoft.Extensions.Options;
using NosCore.Shared.Configuration;

namespace NosCore.Shared.Authentication
{

#if NET5_0 || NET5
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IOptions<WebApiConfiguration> _webApiConfiguration;
        private readonly IEncryption _encryption;

        public ConfigureJwtBearerOptions(IOptions<WebApiConfiguration> webApiConfiguration, IEncryption encryption)
        {
            _webApiConfiguration = webApiConfiguration;
            _encryption = encryption;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var password = _encryption.Encrypt(_webApiConfiguration.Value.Password!, _webApiConfiguration.Value.Salt);
            if (name != JwtBearerDefaults.AuthenticationScheme)
            {
                return;
            }
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.Default.GetBytes(password)),
                ValidAudience = "Audience",
                ValidIssuer = "Issuer",
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(JwtBearerDefaults.AuthenticationScheme, options);
        }
    }
#endif
}
