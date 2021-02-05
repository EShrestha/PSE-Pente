using NUnit.Framework;

namespace Pente.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Arrange 
            PlayWindow win = new PlayWindow();

            // Act
            int result = win.testMeth();

            // Assert
            Assert.Equals(1, result);
        }
    }
}