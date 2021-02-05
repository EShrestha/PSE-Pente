using NUnit.Framework;
using Pente.GameLogic;

namespace Pente.NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCheckNumber()
        {
            BoardLogic bl = new BoardLogic();
            Assert.AreEqual(313, bl.BoardLogicMethod());
        }
    }
}