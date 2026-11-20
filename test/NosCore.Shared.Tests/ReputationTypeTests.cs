//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NosCore.Shared.Enumerations;

namespace NosCore.Shared.Tests
{
    [TestClass]
    public class ReputationTypeTests
    {
        // The first fd of a session reads "fd 230 0 52 1", before "fd 230 3 52 1".
        [TestMethod]
        public void TheIconBeforeTheRankIsKnownIsDefined()
        {
            Assert.AreEqual(nameof(ReputationType.None), Enum.GetName(typeof(ReputationType), (byte)0));
        }
    }
}
