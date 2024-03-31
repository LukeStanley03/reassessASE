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

    /// <summary>
    /// Unit tests for Parser class
    /// </summary>
    [TestClass]
    public class ParserTests
    {
        //Setup variable declarations
        private Parser parser;

        /// <summary>
        /// Sets up the required resources
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            parser = new Parser(new Canvas());
        }
        /// <summary>
        /// Tests if invalid command ProcessProgram returns appropriate error message
        /// </summary>
        [TestMethod]
        public void ProcessProgram_shouldReturnErrorForInvalidCommand()
        {
            // Arrange
            string invalidCommand = "crcle 50";
            string expectedErrorMessage = "Unknown command 'crcle'";

            // Act
            string result = parser.ProcessProgram(invalidCommand);

            // Assert
            Assert.IsTrue(result.Contains(expectedErrorMessage), "The error message for an invalid command was not as expected.");
        }

        /// <summary>
        /// Tests if command with invalid parameters, ProcessProgram returns appropriate error message
        /// </summary>
        [TestMethod]
        public void ProcessProgram_shouldReturnErrorForInvalidParameters()
        {
            // Arrange
            string commandWithInvalidParams = "circle x";
            string expectedErrorMessage = "Invalid parameter";

            // Act
            string result = parser.ProcessProgram(commandWithInvalidParams);

            // Assert
            Assert.IsTrue(result.Contains(expectedErrorMessage), "The error message for invalid parameters was not as expected.");
        }

        /// <summary>
        /// Tests if while loop works correctly
        /// </summary>
        [TestMethod]
        public void ProcessProgram_shouldExecuteWhileLoopCorrectly()
        {
            // Arrange
            string programWithWhileLoop = @"
            count = 0
            while count < 3
                drawto count * 10, count * 10
                count = count + 1
            endwhile";

            string expectedOutcome = ""; // This should be the expected outcome or error message

            // Act
            string result = parser.ProcessProgram(programWithWhileLoop);

            // Assert
            Assert.AreEqual(expectedOutcome, result, "The while loop did not execute as expected.");
        }

        /// <summary>
        /// Tests whether ProcessProgram method handles variables correctly
        /// </summary>
        [TestMethod]
        public void ProcessProgram_shouldHandleVariableCorrectly()
        {
            // Arrange
            string programWithVariable = @"
            size = 10
            circle size";

            string expectedOutcome = ""; // Expected outcome or error message

            // Act
            string result = parser.ProcessProgram(programWithVariable);

            // Assert
            Assert.AreEqual(expectedOutcome, result, "The parser did not handle the variable correctly.");
        }

        /// <summary>
        /// Tests whether ProcessProgram method handles expressions correctly
        /// </summary>
        [TestMethod]
        public void ProcessProgram_shouldHandleExpressionCorrectly()
        {
            // Arrange
            string programWithExpression = @"
            count = 5
            size = count * 10
            circle size";

            string expectedOutcome = ""; // Expected outcome or error message
            //int expectedSize = 50; // As count is 5 and size is count * 10

            // Act
            string result = parser.ProcessProgram(programWithExpression);

            //Assert
            Assert.AreEqual(expectedOutcome, result, "There were errors processing the program with an expression.");
        }

        /// <summary>
        /// Tests whether while loop with varibalesProcessProgram method handles expressions correctly
        /// </summary>
        [TestMethod]
        public void LoopWithVariable_shouldExecuteCorrectly()
        {
            // Arrange
            string loopProgram = @"
            count = 1
            while count < 5
                circle 10
                count = count + 1
            endwhile";

            string expectedOutcome = ""; // Expected outcome or error message
            //int expectedRadius = 60; // Final expected radius value
            //int expectedCount = 5; // Final expected count value

            // Act
            string result = parser.ProcessProgram(loopProgram);

            // Assert
            Assert.AreEqual(expectedOutcome, result, "There were errors processing the program with a loop.");
        }

        /// <summary>
        /// Tests whether if statement execute the inner command correctly
        /// </summary>
        [TestMethod]
        public void IfStatement_shouldExecuteInnerCommand()
        {
            // Arrange
            string program = @"
            if 100 < 200
                circle 100
            endif";

            string expectedOutcome = ""; // Expected outcome or error message

            // Act
            string result = parser.ProcessProgram(program);

            // Assert
            Assert.AreEqual(expectedOutcome, result, "There were errors processing the program with an if statement.");
        }

        /// <summary>
        /// Tests whether syntax checking returns error if command is invalid
        /// </summary>
        [TestMethod]
        public void CheckSyntax_invalidCommand_shouldReturnError()
        {
            // Arrange
            string invalidProgram = "crcle 50"; // Example of an invalid command

            // Act
            string result = parser.CheckSyntax(invalidProgram);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(result), "Expected an error for invalid command, but got none.");
            // Assert.AreEqual("Expected error message", result.Trim());
        }

        /// <summary>
        /// Tests whether syntax checking returns no error if command is valid
        /// </summary>
        [TestMethod]
        public void CheckSyntax_validCommand_shouldNotReturnError()
        {
            // Arrange
            string validProgram = "circle 50"; // Example of a valid command

            // Act
            string result = parser.CheckSyntax(validProgram);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(result), "Expected no error for valid command, but found one.");
        }

        /// <summary>
        /// Tests whether method with no parameters execute without error
        /// </summary>
        [TestMethod]
        public void Method_noParameters_shouldExecuteWithoutError()
        {
            // Arrange
            string programWithMethod = @"
            method myCircle()
                circle 50
            endmethod
            myCircle()";

            // Act
            string result = parser.ProcessProgram(programWithMethod);

            // Assert
            Assert.AreEqual(string.Empty, result, "Method with no parameters should execute without error.");
        }
    }

}