

using reassessASE;

namespace reassessTest
{

    [TestClass]
    public class CanvasTests
    {
        /// <summary>
        /// Tests the Canvas Xpos property with the correct value
        /// </summary>
        [TestMethod]
        public void XposPropertyTest()
        {
            //Arrange
            var canvas = new Canvas();
            int expected = 0;
            //Act
            int result = canvas.Xpos;

            //Assert
            Assert.AreEqual(expected, result, "Xpos property return the expected value");

        }
    }
}