//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.Helpers;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class RandomHelperTests
    {
        [TestMethod]
        public void TheDefaultRollIsAPercentage()
        {
            var rolls = Enumerable.Range(0, 10000).Select(_ => RandomHelper.Instance.RandomNumber()).ToList();

            Assert.IsTrue(rolls.All(r => r is >= 0 and < 100));
            Assert.IsTrue(rolls.Distinct().Count() > 90);
        }

        [TestMethod]
        public void ARollStaysWithinItsBounds()
        {
            var rolls = Enumerable.Range(0, 1000).Select(_ => RandomHelper.Instance.RandomNumber(5, 8)).ToList();

            CollectionAssert.AreEquivalent(new[] { 5, 6, 7 }, rolls.Distinct().ToArray());
        }
    }
}
