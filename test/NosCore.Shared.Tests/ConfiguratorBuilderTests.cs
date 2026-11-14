//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.Configuration;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class ConfiguratorBuilderTests
    {
        private static IConfigurationRoot Load(string yaml)
        {
            var directory = Path.Combine(Path.GetTempPath(), "noscore-shared-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directory);
            File.WriteAllText(Path.Combine(directory, "test.yml"), yaml);
            return ConfiguratorBuilder.InitializeConfiguration(new[] { "--config", directory }, new[] { "test.yml" });
        }

        // Letters only, so that only the test about digits exercises digits.
        private static string Variable()
        {
            var letters = Guid.NewGuid().ToString("N").ToUpperInvariant().ToCharArray();
            for (var i = 0; i < letters.Length; i++)
            {
                if (char.IsDigit(letters[i]))
                {
                    letters[i] = (char)('G' + (letters[i] - '0'));
                }
            }

            return "NOSCORE_TEST_" + new string(letters);
        }

        [TestMethod]
        public void PlaceholderWithoutFallbackReadsTheWholeVariableName()
        {
            var name = Variable();
            Environment.SetEnvironmentVariable(name, "db.local");

            var configuration = Load("Host: \"${" + name + "}\"");

            Assert.AreEqual("db.local", configuration["Host"]);
        }

        [TestMethod]
        public void VariableNamesMayContainDigits()
        {
            var name = Variable() + "_2";
            Environment.SetEnvironmentVariable(name, "replica");

            var configuration = Load("Host: \"${" + name + ",localhost}\"");

            Assert.AreEqual("replica", configuration["Host"]);
        }

        [TestMethod]
        public void FallbackIsUsedWhenTheVariableIsUnset()
        {
            var configuration = Load("Host: \"${" + Variable() + ",localhost}\"");

            Assert.AreEqual("localhost", configuration["Host"]);
        }

        [TestMethod]
        public void FallbackIsUsedWhenTheVariableIsEmpty()
        {
            if (OperatingSystem.IsWindows())
            {
                Assert.Inconclusive("Windows removes a variable set to an empty value.");
            }

            var name = Variable();
            Environment.SetEnvironmentVariable(name, "");
            var configuration = Load("Host: \"${" + name + ",localhost}\"");

            Assert.AreEqual("localhost", configuration["Host"]);
        }

        [TestMethod]
        public void DollarSignsInTheValueAreKeptLiterally()
        {
            var name = Variable();
            Environment.SetEnvironmentVariable(name, "pa$$w0rd$1$0");

            var configuration = Load("Password: \"${" + name + ",password}\"");

            Assert.AreEqual("pa$$w0rd$1$0", configuration["Password"]);
        }

        [TestMethod]
        public void SeveralPlaceholdersInOneValueAreAllReplaced()
        {
            var host = Variable();
            Environment.SetEnvironmentVariable(host, "db.local");

            var configuration = Load("Url: \"${" + host + ",localhost}:${" + Variable() + ",5432}\"");

            Assert.AreEqual("db.local:5432", configuration["Url"]);
        }

        [TestMethod]
        public void PlaceholdersAreReplacedAtAnyDepth()
        {
            var name = Variable();
            Environment.SetEnvironmentVariable(name, "deep");

            var configuration = Load("A:\n  B:\n    C:\n      Value: \"${" + name + ",fallback}\"\n    List:\n      - \"${" + name + ",fallback}\"");

            Assert.AreEqual("deep", configuration["A:B:C:Value"]);
            Assert.AreEqual("deep", configuration["A:B:List:0"]);
        }

        [TestMethod]
        public void UnsetPlaceholderWithoutFallbackIsLeftAsWritten()
        {
            var placeholder = "${" + Variable() + "}";

            var configuration = Load("Host: \"" + placeholder + "\"");

            Assert.AreEqual(placeholder, configuration["Host"]);
        }

        [TestMethod]
        public void LoggerSettingsAreReadAfterPlaceholdersAreReplaced()
        {
            var configuration = Load("Serilog:\n  MinimumLevel: \"${" + Variable() + ",Warning}\"");

            Assert.AreEqual("Warning", configuration["Serilog:MinimumLevel"]);
        }
    }
}
