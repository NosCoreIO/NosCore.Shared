//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NosCore.Shared.I18N;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class LogLanguageLocalizerTests
    {
        public enum TestLanguageKey
        {
            Present,
            Missing
        }

        public class TestResources
        {
        }

        private static LogLanguageLocalizer<TestLanguageKey, TestResources> CreateLocalizer()
        {
            // IStringLocalizer answers a missing key with the key itself and ResourceNotFound set.
            var stringLocalizer = new Mock<IStringLocalizer<TestResources>>();
            stringLocalizer.Setup(s => s[nameof(TestLanguageKey.Present)])
                .Returns(new LocalizedString(nameof(TestLanguageKey.Present), "Hello"));
            stringLocalizer.Setup(s => s[nameof(TestLanguageKey.Present), It.IsAny<object[]>()])
                .Returns(new LocalizedString(nameof(TestLanguageKey.Present), "Hello 1"));
            stringLocalizer.Setup(s => s[nameof(TestLanguageKey.Missing)])
                .Returns(new LocalizedString(nameof(TestLanguageKey.Missing), nameof(TestLanguageKey.Missing), true));
            stringLocalizer.Setup(s => s[nameof(TestLanguageKey.Missing), It.IsAny<object[]>()])
                .Returns(new LocalizedString(nameof(TestLanguageKey.Missing), nameof(TestLanguageKey.Missing), true));
            return new LogLanguageLocalizer<TestLanguageKey, TestResources>(stringLocalizer.Object);
        }

        [TestMethod]
        public void PresentKeyIsTranslated()
        {
            var localizer = CreateLocalizer();

            Assert.AreEqual("Hello", localizer[TestLanguageKey.Present].Value);
            Assert.AreEqual("Hello 1", localizer[TestLanguageKey.Present, 1].Value);
        }

        [TestMethod]
        public void MissingKeyFallsBackToTheMarker()
        {
            var localizer = CreateLocalizer();

            var value = localizer[TestLanguageKey.Missing];

            Assert.IsTrue(value.ResourceNotFound);
            Assert.AreEqual("#<Missing>", value.Value);
        }

        [TestMethod]
        public void MissingKeyWithArgumentsFallsBackToTheMarker()
        {
            var localizer = CreateLocalizer();

            var value = localizer[TestLanguageKey.Missing, 1];

            Assert.IsTrue(value.ResourceNotFound);
            Assert.AreEqual("#<Missing>", value.Value);
        }
    }
}
