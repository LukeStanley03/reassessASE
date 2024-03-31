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

        /// <summary>
        /// Tests the Canvas Xpos property with the correct value
        /// </summary>
        [TestMethod]
        public void YposPropertyTest()
        {
            //Arrange
            var canvas = new Canvas();
            int expected = 0;
            //Act
            int result = canvas.Ypos;

            //Assert
            Assert.AreEqual(expected, result, "Ypos property return the expected value");

        }

        /// <summary>
        /// Tests Canvas MoveTo method for valid positions
        /// </summary>
        [TestMethod]
        public void MoveTo_withValidCoordinates_updatesPosition()
        {
            // Arrange
            Canvas canvas = new Canvas();
            int validX = 50;
            int validY = 50;

            // Act
            canvas.MoveTo(validX, validY);

            // Assert
            Assert.AreEqual(validX, canvas.Xpos, "X position should be updated correctly.");
            Assert.AreEqual(validY, canvas.Ypos, "Y position should be updated correctly.");
        }

        /// <summary>
        /// Tests Canvas MoveTo method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void MoveTo_withInvalidCoordinates_throwsException()
        {
            // Arrange
            var canvas = new Canvas();
            int invalidX = -10; // Less than 0, hence invalid
            int invalidY = 500; // Greater than XCanvasSize, hence invalid

            // Act
            canvas.MoveTo(invalidX, invalidY);

            // Assert is handled by ExpectedException
        }


        /// <summary>
        /// Tests Canvas DrawTo method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void DrawTo_withInvalidCoordinates_throwsException()
        {
            // Arrange
            var canvas = new Canvas();
            int invalidX = 700;
            int invalidY = 500;

            // Act
            canvas.DrawTo(invalidX, invalidY);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests Canvas Square method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception), "The expected exception was not thrown")]
        public void Square_withNegativeWidth_throwsException()
        {
            // Arrange
            Canvas canvas = new Canvas();
            int negativeWidth = -10;

            // Act
            canvas.Square(negativeWidth);

            // Assert is handled by ExpectedException
        }


        /// <summary>
        /// Tests Canvas Rectangle method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void Rectangle_withInvaliWidthAndHeight_throwsException()
        {
            // Arrange
            var canvas = new Canvas();
            int invalidWidth = -10;
            int invalidHeight = -20;

            // Act
            canvas.Rectangle(invalidWidth, invalidHeight);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests Canvas Triangle method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void Triangle_withInvaliWidthAndHeight_throwsException()
        {
            // Arrange
            var canvas = new Canvas();
            int invalidWidth = -10;
            int invalidHeight = -20;

            // Act
            canvas.Triangle(invalidWidth, invalidHeight);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests Canvas SeColour method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception), "The expected exception was not thrown")]
        public void SetColour_withInvaliColourValues_throwsException()
        {
            // Arrange
            var canvas = new Canvas();
            int invalidRed = 300;
            int invalidGreen = 400;
            int invalidBlue = 400;

            // Act
            canvas.SetColour(invalidRed, invalidGreen, invalidBlue);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests Canvas Circle method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception), "The expected exception was not thrown")]
        public void Circle_withNegativeRadius_throwsException()
        {
            // Arrange
            Canvas canvas = new Canvas();
            int negativeRadius = -10;

            // Act
            canvas.Square(negativeRadius);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests Canvas SetFill method for fill on state
        /// </summary>
        [TestMethod]
        public void SetFill_On_setsFillToTrue()
        {
            // Arrange
            var canvas = new Canvas();
            string input = "on";

            // Act
            canvas.SetFill(input);

            // Assert
            Assert.IsTrue(canvas.fill, "Fill should be set to true when 'on' is passed.");
        }

        /// <summary>
        /// Tests Canvas SetFill method for fill off state
        /// </summary>
        [TestMethod]
        public void SetFill_Off_setsFillToFalse()
        {
            // Arrange
            var canvas = new Canvas();
            string input = "off";

            // Act
            canvas.SetFill(input);

            // Assert
            Assert.IsFalse(canvas.fill, "Fill should be set to false when 'off' is passed.");
        }

        /// <summary>
        /// Tests Canvas SetFill method for exception handling
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void SetFill_withInvalidInput_throwsException()
        {
            // Arrange
            var canvas = new Canvas();
            string invalidInput = "invalid";

            // Act
            canvas.SetFill(invalidInput);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests Canvas Reset method for resetting xPos and yPos to 0,0
        /// </summary>
        [TestMethod]
        public void Reset_setsXposAndYposToZero()
        {
            // Arrange
            var canvas = new Canvas();

            // Act
            canvas.Reset();

            // Assert
            Assert.AreEqual(0, canvas.xPos, "xPos should be reset to 0.");
            Assert.AreEqual(0, canvas.yPos, "yPos should be reset to 0.");
        }
    }

    /// <summary>
    /// Unit tests for FileHandler class
    /// </summary>
    [TestClass]
    public class FileHandlerTests
    {
        //Setup variable declarations
        private string tempFilePath;
        private FileHandler fileHandler;

        /// <summary>
        /// Sets up the required resources
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            fileHandler = new FileHandler();
            tempFilePath = Path.GetTempFileName(); // Creates a temporary file
        }
        /// <summary>
        /// Runs after each test to clean up resources
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath); // Cleanup after test
            }
        }
        /// <summary>
        /// Ensures that content is successfully read from a file
        /// </summary>
        [TestMethod]
        public void ReadFromFile_validFile_returnsContent()
        {
            // Arrange
            var fileHandler = new FileHandler();
            string testFilePath = "testfile.gpl";
            File.WriteAllText(testFilePath, "Test Content");

            // Act
            string content = fileHandler.ReadFromFile(testFilePath);

            // Assert
            Assert.AreEqual("Test Content", content);
        }
        /// <summary>
        /// ReadFromFile throws an exception if file does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void ReadFromFile_invalidFile_throwsException()
        {
            // Arrange
            var fileHandler = new FileHandler();

            // Act
            fileHandler.ReadFromFile("nonexistentfile.gpl");

            // Assert is handled by ExpectedException
        }
        /// <summary>
        /// Ensures that content is successfully written to a file
        /// </summary>
        [TestMethod]
        public void WriteToFile_shouldCreateFileWithContent()
        {
            // Arrange
            string expectedContent = "Hello, World!";

            // Act
            fileHandler.WriteToFile(tempFilePath, expectedContent);
            string actualContent = File.ReadAllText(tempFilePath);

            // Assert
            Assert.AreEqual(expectedContent, actualContent, "The file content is not as expected.");
        }
        /// <summary>
        /// If invalid file path WriteToFile throws an exception 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLexception))]
        public void WriteToFile_shouldThrowExceptionForInvalidPath()
        {
            // Arrange
            string invalidPath = "/invalid/path/file.txt";

            // Act
            fileHandler.WriteToFile(invalidPath, "Some content");

            // Assert is handled by the ExpectedException attribute
        }

    }


}