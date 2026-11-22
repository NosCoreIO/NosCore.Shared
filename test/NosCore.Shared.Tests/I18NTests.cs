//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NosCore.Shared.I18N;
using NosCore.Shared.Tests.Registration;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class I18NTests
    {
        [TestMethod]
        public void EachAssemblysLogKeysGetALocalizer()
        {
            var services = new ServiceCollection().AddI18NLogs();

            var registration = services.Single(s => s.ServiceType == typeof(ILogLanguageLocalizer<LogLanguageKey>));
            Assert.AreEqual(typeof(LogLanguageLocalizer<LogLanguageKey, LocalizedResources>), registration.ImplementationType);
            Assert.AreEqual(ServiceLifetime.Transient, registration.Lifetime);
        }

        [TestMethod]
        public void KeysWithoutATranslationAreListed()
        {
            var localizer = new Mock<ILogLanguageLocalizer<LogLanguageKey>>();
            localizer.Setup(l => l[LogLanguageKey.Translated]).Returns(new LocalizedString("Translated", "Hello"));
            localizer.Setup(l => l[LogLanguageKey.Missing]).Returns(new LocalizedString("Missing", "Missing", true));
            localizer.Setup(l => l[LogLanguageKey.Marked]).Returns(new LocalizedString("Marked", "#<Marked>"));

            var missing = I18NTestHelpers.GetKeysWithMissingTranslations(localizer.Object).ToList();

            CollectionAssert.AreEquivalent(new[] { LogLanguageKey.Missing, LogLanguageKey.Marked }, missing);
        }

        [TestMethod]
        public void TranslationsWithoutAKeyAreListed()
        {
            var localizer = new Mock<ILogLanguageLocalizer>();
            localizer.Setup(l => l.GetAllStrings()).Returns([
                new LocalizedString("Translated", "Hello"),
                new LocalizedString("Leftover", "Bye"),
            ]);

            var useless = I18NTestHelpers.GetUselessTranslations(localizer.Object, new List<string> { "Translated" });

            CollectionAssert.AreEqual(new[] { "Leftover" }, useless.ToArray());
        }
    }
}

namespace NosCore.Shared.Tests.Registration
{
    public enum LogLanguageKey
    {
        Translated,
        Missing,
        Marked,
    }

    public class LocalizedResources
    {
    }
}
