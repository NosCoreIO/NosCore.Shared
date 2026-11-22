//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.Configuration;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void AServerIsShownAsHostAndPort()
        {
            var server = new ServerConfiguration { Host = "127.0.0.1", Port = 4000 };

            Assert.AreEqual("127.0.0.1:4000", server.ToString());
        }

        [TestMethod]
        public void TheConnectionStringCarriesEveryField()
        {
            var sql = new SqlConnectionConfiguration
            {
                Host = "db",
                Port = 5432,
                Database = "noscore",
                Username = "user",
                Password = "secret",
            };

            Assert.AreEqual("Host=db;Port=5432;Database=noscore;Username=user;Password=secret;", sql.ConnectionString);
        }

        [TestMethod]
        public void AnUnlistedFeatureIsOff()
        {
            Assert.IsFalse(new FeatureFlags()[(FeatureFlag)1]);
        }

        [TestMethod]
        public void AListedFeatureReadsItsValue()
        {
            var flags = new FeatureFlags { { (FeatureFlag)1, true }, { (FeatureFlag)2, false } };

            Assert.IsTrue(flags[(FeatureFlag)1]);
            Assert.IsFalse(flags[(FeatureFlag)2]);
        }
    }
}
