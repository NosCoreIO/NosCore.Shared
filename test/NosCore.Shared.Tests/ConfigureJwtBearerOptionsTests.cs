//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NosCore.Shared.Authentication;
using NosCore.Shared.Configuration;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class ConfigureJwtBearerOptionsTests
    {
        private const string HashedPassword = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";

        private static ConfigureJwtBearerOptions Create()
        {
            var hasher = new Mock<IHasher>();
            hasher.Setup(h => h.Hash("password", "salt")).Returns(HashedPassword);
            var configuration = Options.Create(new WebApiConfiguration { Password = "password", Salt = "salt" });
            return new ConfigureJwtBearerOptions(configuration, hasher.Object);
        }

        [TestMethod]
        public void TokensAreSignedWithTheHashedApiPassword()
        {
            var options = new JwtBearerOptions();

            Create().Configure(options);

            var key = options.TokenValidationParameters.IssuerSigningKey as SymmetricSecurityKey;
            CollectionAssert.AreEqual(Encoding.Default.GetBytes(HashedPassword), key?.Key);
            Assert.IsTrue(options.TokenValidationParameters.ValidateIssuerSigningKey);
            Assert.IsTrue(options.TokenValidationParameters.ValidateLifetime);
            Assert.AreEqual("Issuer", options.TokenValidationParameters.ValidIssuer);
            Assert.AreEqual("Audience", options.TokenValidationParameters.ValidAudience);
            Assert.IsTrue(options.SaveToken);
            Assert.IsFalse(options.RequireHttpsMetadata);
        }

        [TestMethod]
        public void AnotherSchemeIsLeftAlone()
        {
            var options = new JwtBearerOptions();

            Create().Configure("Other", options);

            Assert.IsNull(options.TokenValidationParameters.IssuerSigningKey);
            Assert.IsTrue(options.RequireHttpsMetadata);
        }

        [TestMethod]
        public void MissingOptionsAreRefused()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => Create().Configure(JwtBearerDefaults.AuthenticationScheme, null!));
        }
    }
}
