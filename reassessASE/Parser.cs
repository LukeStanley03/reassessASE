using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class Parser
    {
        private Canvas myCanvas;

        //collection set of valid commands 
        private HashSet<string> validCommands = new HashSet<string>
        {
            "moveto",
            "drawto",
            "circle",
            "triangle",
            "rectangle",
            "colour",
            "fill",
            "clear",
            "reset",
            "square",
            "run",
            "red",
            "green",
            "blue",
            "black",
            "pattern",
            "complex",
            "shapes",
            "randomlines",
            "star"
        };

        //Dictionary to store variable names and values
        private Dictionary<string, int> variables = new Dictionary<string, int>();

        // A stack to keep track of nested if statements.
        private Stack<bool> ifStack = new Stack<bool>();

        //A stack to keep track of the start line numbers of the nested while loops.
        //private Stack<int> whileStack = new Stack<int>();

        //Dictinary to store loop conditions with their line numbers
        private Dictionary<int, string> loopConditions = new Dictionary<int, string>();

        //Dictionary to map method names to their definitions
        private Dictionary<string, MethodDefinition> methodDefinitions = new Dictionary<string, MethodDefinition>();

        /// <summary>
        /// Contsructor to set up the canvas
        /// </summary>
        /// <param name="canvas">name of canvas</param>
        public Parser(Canvas canvas)
        {
            MyCanvas = canvas;
        }

        /// <summary>
        /// Parses the commands from the user input
        /// </summary>
        /// <param name="line">command string entered by the user</param>
        public string ParseCommand(string[] lines, int currentLineIndex, ref int newLineIndex, out bool skipExecution)
        {
            skipExecution = false;
            newLineIndex = currentLineIndex; // Initialize newLineIndex with the current line index

            // Get the current line using the currentLineIndex
            string line = lines[currentLineIndex].Trim();


            //Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
            {
                return $"Empty command at line {currentLineIndex + 1}";
            }

            // Handling if statements
            if (line.StartsWith("if"))
            {
                // Extract the condition from the line
                string condition = line.Substring(2).Trim(); // Get the condition expression

                // Evaluate the condition
                bool conditionResult = EvaluateCondition(condition);

                // Push the result onto the stack
                ifStack.Push(conditionResult);

                return string.Empty;
            }
            else if (line.StartsWith("endif"))
            {
                if (ifStack.Count > 0)
                    ifStack.Pop(); // Pop the result for the current block
                else
                    return $"Mismatched endif at line {currentLineIndex + 1}";
                return string.Empty;
            }
            else if (ifStack.Count > 0 && !ifStack.Peek())
            {
                // Inside an if block and the condition is false, skip the command
                return string.Empty;
            }

            // Handling variable assignment
            if (line.Contains("="))
            {
                var parts = line.Split('=');
                if (parts.Length != 2)
                    return $"Syntax error in variable assignment at line {currentLineIndex + 1}";

                var varName = parts[0].Trim();

                // Evaluate the expression on the right side of the '='
                var expression = parts[1].Trim();
                if (!EvaluateExpression(expression, out int result))
                    return $"Invalid expression '{expression}' at line {currentLineIndex + 1}";

                variables[varName] = result; // Store the updated variable value
                return string.Empty;
            }

            // Handling "while" command
            // Check for the start of a while loop
            if (line.StartsWith("while"))
            {
                // Extract and store the while loop condition
                string condition = ExtractConditionFromWhile(line);
                loopConditions[currentLineIndex] = condition; // Store the condition

                // Evaluate the condition
                if (!EvaluateCondition(condition))
                {
                    // If the condition is false, find the index of the corresponding "endwhile"
                    newLineIndex = FindIndexOfEndWhile(lines, currentLineIndex) + 1;
                    skipExecution = true;
                }
                else
                {
                    // If the condition is true, delegate the execution to ExecuteWhileLoop
                    ExecuteWhileLoop(lines, ref currentLineIndex);
                    // After ExecuteWhileLoop, newLineIndex should be the line after endwhile
                    newLineIndex = currentLineIndex + 1;
                }
            }
            // Handling "endwhile" command
            else if (line.StartsWith("endwhile"))
            {
                // Handling of endwhile should be done inside ExecuteWhileLoop, so this is just a safety check
                return $"Unexpected 'endwhile' at line {currentLineIndex + 1}, without a matching 'while'.";
            }

            // Handling Canvas commands using the factory pattern
            if (IsCanvasCommand(line))
            {
                // Extract the command type and parameters, replacing variables with their values
                string[] parts = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string commandType = parts[0].ToLower();
                List<string> parameters = new List<string>();

                for (int i = 1; i < parts.Length; i++)
                {
                    if (variables.TryGetValue(parts[i], out int value))
                    {
                        parameters.Add(value.ToString());
                    }
                    else if (commandType == "fill" && (parts[i] == "on" || parts[i] == "off"))
                    {
                        parameters.Add(parts[i]); // Allow specific string parameters for the fill command
                    }
                    else if (!int.TryParse(parts[i], out _))
                    {
                        return $"Error: Invalid parameter '{parts[i]}' at line {currentLineIndex + 1}.";
                    }
                    else
                    {
                        parameters.Add(parts[i]);
                    }
                }

                try
                {
                    ICanvasCommand command = CommandFactory.CreateCommand(commandType, parameters.ToArray());

                    command.Execute(MyCanvas);
                }
                catch (GPLexception ex)
                {
                    return $"Error at line {currentLineIndex + 1}: {ex.Message}";
                }
            }
            else
            {
                // Check if the command is a control structure or variable-related operation.
                // If it's neither, then it's an unrecognized command.
                if (!IsControlStructure(line) && !IsVariableRelatedOperation(line))
                {
                    return $"Error: Unrecognized command at line {currentLineIndex + 1}.";
                }
            }

            return string.Empty; // No error

        }
        /// <summary>
        /// Checks if the command needs canvas
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>

        private bool IsCanvasCommand(string line)
        {
            string command = line.Split(' ')[0].ToLower();
            return validCommands.Contains(command);
        }

        /// <summary>
        /// Checks whether the command is the control structure or not
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsControlStructure(string line)
        {
            // This method should return true if the line is a control structure such as if, while, etc.
            // For now, let's assume it's just checking for 'if' and 'while'
            string command = line.Split(new[] { ' ', '(' }, StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
            return command == "if" || command == "while";
        }
        /// <summary>
        /// Checks whether the variable is operation related
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsVariableRelatedOperation(string line)
        {
            // This method should return true if the line is related to variable assignment or manipulation
            return line.Contains("=");
        }

        /// <summary>
        /// Takes a while line and extract condition it
        /// </summary>
        /// <param name="whileLine"></param>
        /// <returns></returns>
        /// <exception cref="GPLexception"></exception>
        private string ExtractConditionFromWhile(string whileLine)
        {
            int indexOfWhile = whileLine.IndexOf("while");
            if (indexOfWhile == -1) throw new GPLexception("Not a while line");

            // Assumes condition starts after "while "
            return whileLine.Substring(indexOfWhile + "while".Length).Trim();
        }


        /// <summary>
        /// Executes a while loop
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="currentLineIndex"></param>
        /// <exception cref="GPLexception"></exception>
        /// 
        private void ExecuteWhileLoop(string[] lines, ref int currentLineIndex)
        {
            int loopStartIndex = currentLineIndex;
            string condition = ExtractConditionFromWhile(lines[currentLineIndex]);

            // Loop until the condition is false
            while (EvaluateCondition(condition))
            {
                // Increment the line index to move to the first command inside the loop
                currentLineIndex++;

                // Execute each line in the loop until endwhile is reached
                while (!lines[currentLineIndex].Trim().StartsWith("endwhile"))
                {
                    bool skipExecution; // Used to indicate if the execution should skip to endwhile
                    int newLineIndex = currentLineIndex;
                    string commandResult = ParseCommand(lines, currentLineIndex, ref newLineIndex, out skipExecution);

                    // Handle any errors that might have occurred in command execution
                    if (!string.IsNullOrEmpty(commandResult))
                    {
                        throw new GPLexception($"Error at line {currentLineIndex + 1}: {commandResult}");
                    }

                    if (skipExecution)
                    {
                        // Find the index of endwhile that matches the current while loop
                        currentLineIndex = FindIndexOfEndWhile(lines, loopStartIndex);
                        break; // Exit the inner loop to evaluate the while condition again
                    }
                    else
                    {
                        currentLineIndex++; // Move to the next command inside the loop
                    }
                }

                // Update the condition to evaluate if the loop should continue
                condition = GetLoopCondition(loopStartIndex);

                // Loop back to the start of the while loop if the condition is still true
                if (EvaluateCondition(condition))
                {
                    currentLineIndex = loopStartIndex;
                }
                else
                {
                    // Move past the endwhile if the loop is done
                    currentLineIndex++;
                }
            }
        }
            /// <summary>
            /// Finds the index of the endwhile that corresponds to the while
            /// </summary>
            /// <param name="lines"></param>
            /// <param name="startLineIndex"></param>
            /// <returns></returns>
            /// <exception cref="GPLexception"></exception>
            private int FindIndexOfEndWhile(string[] lines, int startLineIndex)
            {
                int openLoops = 1;
                for (int i = startLineIndex + 1; i < lines.Length; i++)
                {
                    if (lines[i].Trim().StartsWith("while")) openLoops++;
                    if (lines[i].Trim().StartsWith("endwhile"))
                    {
                        openLoops--;
                        if (openLoops == 0) return i;
                    }
                }
                throw new GPLexception("No matching endwhile found");
            }


            /// <summary>
            /// Gets the loop condition, look it up in the dictionary using the line number
            /// </summary>
            /// <param name="lineNumber">line number</param>
            /// <returns>condition</returns>
            /// <exception cref="GPLexception">exception</exception>
            private string GetLoopCondition(int lineNumber)
            {
                if (loopConditions.TryGetValue(lineNumber, out string condition))
                {
                    return condition;
                }
                else
                {
                    throw new GPLexception($"No loop condition found for line number: {lineNumber + 1}");
                }
            }


            /// <summary>
            /// Takes condition string, breaks it into parts to parse the operands
            /// </summary>
            /// <param name="condition">condition string in the if statement</param>
            /// <returns></returns>
            /// <exception cref="GPLexception"></exception>
            private bool EvaluateCondition(string condition)
            {
                // Example condition format: "num1 < num2"
                string[] tokens = condition.Split(' ');

                if (tokens.Length != 3)
                {
                    throw new GPLexception("Invalid condition format");
                }

                // Extract operands and operator
                string leftOperand = tokens[0];
                string operatorToken = tokens[1];
                string rightOperand = tokens[2];

                // Try to get values for variables, if they are variables
                int leftValue;
                int rightValue;

                if (!int.TryParse(leftOperand, out leftValue))
                {
                    if (!variables.TryGetValue(leftOperand, out leftValue))
                    {
                        throw new GPLexception($"Undefined variable '{leftOperand}' in condition");
                    }
                }

                if (!int.TryParse(rightOperand, out rightValue))
                {
                    if (!variables.TryGetValue(rightOperand, out rightValue))
                    {
                        throw new GPLexception($"Undefined variable '{rightOperand}' in condition");
                    }
                }

                // Perform the comparison based on the operator
                switch (operatorToken)
                {
                    case "<":
                        return leftValue < rightValue;
                    case ">":
                        return leftValue > rightValue;
                    case "==":
                        return leftValue == rightValue;
                    case "!=":
                        return leftValue != rightValue;
                    case "<=":
                        return leftValue <= rightValue;
                    case ">=":
                        return leftValue >= rightValue;
                    // Add more operators as needed
                    default:
                        throw new GPLexception($"Invalid operator '{operatorToken}' in condition");
                }
            }
        /// <summary>
        /// Handles arithmetic operations
        /// </summary>
        /// <param name="expression">string operation</param>
        /// <param name="result">input result</param>
        /// <returns>true if supported expession or false if unsupported expression</returns>
        private bool EvaluateExpression(string expression, out int result)
        {
            result = 0;

            // Split the expression into parts (e.g., 'radius + 10' -> ['radius', '+', '10'])
            var tokens = expression.Split(' ');
            if (tokens.Length == 3) // Simple binary expressions like 'a + b'
            {
                int leftOperand, rightOperand;
                string operatorToken = tokens[1];

                // Retrieve or parse the left operand
                if (!int.TryParse(tokens[0], out leftOperand))
                {
                    if (!variables.TryGetValue(tokens[0], out leftOperand))
                        return false; // Left operand is not a valid number or variable
                }

                // Retrieve or parse the right operand
                if (!int.TryParse(tokens[2], out rightOperand))
                {
                    if (!variables.TryGetValue(tokens[2], out rightOperand))
                        return false; // Right operand is not a valid number or variable
                }

                // Perform the arithmetic operation
                switch (operatorToken)
                {
                    case "+":
                        result = leftOperand + rightOperand;
                        return true;
                    // Add cases for '-', '*', '/' as needed
                    case "-":
                        result = leftOperand - rightOperand;
                        return true;
                    case "*":
                        result = leftOperand * rightOperand;
                        return true;
                    case "/":
                        result = leftOperand / rightOperand;
                        return true;
                    default:
                        return false; // Unsupported operator
                }
            }
            else if (tokens.Length == 1) // Single value (variable or number)
            {
                return int.TryParse(expression, out result) || variables.TryGetValue(expression, out result);
            }

            return false; // Unsupported expression format
        }
        /// <summary>
        /// Process the program, either running it or just syntax check it
        /// </summary>
        /// <param name="program">program string</param>
        public string ProcessProgram(string program)
        {
            string[] lines = program.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder errors = new StringBuilder();

            int i = 0;
            while (i < lines.Length)
            {
                bool skipExecution = false;
                int newLineIndex = i;
                string result = ParseCommand(lines, i, ref newLineIndex, out skipExecution);

                if (!string.IsNullOrEmpty(result))
                {
                    errors.AppendLine(result);
                }

                if (skipExecution)
                {
                    i = newLineIndex; // Use the new line index if skipping
                }
                else
                {
                    i++; // Otherwise, go to the next line
                }
            }

            return errors.ToString();
        }

        /// <summary>
        /// processes all lines and calls ParseCommand, collecting any errors
        /// </summary>
        /// <param name="program">big input string in the program window</param>
        /// <returns>Appended error message</returns>
        public string CheckSyntax(string program)
        {
            var lines = program.Split('\n');
            var errorMessages = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                bool skipExecution = false;

                // Declare newLineIndex and initialize it with the current index
                int newLineIndex = i;

                // Pass the entire lines array and the current index
                string error = ParseCommand(lines, i, ref newLineIndex, out skipExecution);
                if (!string.IsNullOrEmpty(error))
                {
                    errorMessages.AppendLine(error);
                }
            }

            return errorMessages.ToString();
        }
        /// <summary>
        /// Parses and stores a method definition
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="currentLineIndex"></param>
        private void ParseMethodDefinition(string[] lines, ref int currentLineIndex)
        {
            // Assume currentLineIndex is the index of the "method" line
            string methodLine = lines[currentLineIndex].Trim();
            string[] parts = methodLine.Substring("method".Length).Split(new[] { '(', ')', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            string methodName = parts[0];
            List<string> parameters = parts.Skip(1).ToList(); // Skip method name to get parameters
            List<string> body = new List<string>();

            // Increment currentLineIndex to start reading the body
            currentLineIndex++;
            while (currentLineIndex < lines.Length && !lines[currentLineIndex].Trim().StartsWith("endmethod"))
            {
                body.Add(lines[currentLineIndex].Trim());
                currentLineIndex++;
            }


            // Pass the parameters directly to the constructor
            MethodDefinition methodDefinition = new MethodDefinition(methodName, parameters, body);


            methodDefinitions[methodName] = methodDefinition;

            // Increment past the "endmethod" line
            currentLineIndex++;
        }
        /// <summary>
        /// Handles the method invocation to execute the method body
        /// </summary>
        /// <param name="line"></param>
        /// <param name="globalVariables"></param>
        /// <param name="methodStartLineNumber"></param>
        /// <returns></returns>
        public string InvokeMethod(string line, Dictionary<string, int> globalVariables, int methodStartLineNumber)
        {
            // Extract method name and arguments
            string[] parts = line.Split(new char[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string methodName = parts[0].Trim();
            List<int> arguments = new List<int>();

            try
            {
                arguments = parts.Skip(1).Select(arg => int.Parse(arg.Trim())).ToList();
            }
            catch (FormatException)
            {
                return $"Error: Invalid format for parameters in method call '{line}'.";
            }

            if (methodDefinitions.TryGetValue(methodName, out MethodDefinition method))
            {
                if (arguments.Count != method.Parameters.Count)
                {
                    return $"Error: Method '{methodName}' expects {method.Parameters.Count} parameters, got {arguments.Count}.";
                }

                var localVariables = new Dictionary<string, int>(globalVariables);

                for (int i = 0; i < method.Parameters.Count; i++)
                {
                    localVariables[method.Parameters[i]] = arguments[i];
                }

                foreach (string command in method.Body)
                {
                    string result = ExecuteCommand(command, methodStartLineNumber, localVariables);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }
            }
            else
            {
                return $"Error: Method '{methodName}' is not defined.";
            }

            return string.Empty;
        }
        /// <summary>
        /// Handles method definitions and invocations
        /// </summary>
        /// <param name="lines"></param>
        public void ProcessCommands(string[] lines)
        {
            int currentLineIndex = 0;
            while (currentLineIndex < lines.Length)
            {
                string line = lines[currentLineIndex].Trim();
                if (line.StartsWith("method"))
                {
                    ParseMethodDefinition(lines, ref currentLineIndex);
                }
                else if (line.Contains("(") && line.Contains(")")) // Simple check for method invocation
                {
                    // Pass the current line index as the method invocation line number
                    InvokeMethod(line, variables, currentLineIndex + 1); // Assuming line numbers are 1-based
                    currentLineIndex++;
                }
                else
                {
                    // Handle other types of commands
                    currentLineIndex++;
                }
            }
        }
        /// <summary>
        /// Parses the string parameters into integers and checks for validity.
        /// </summary>
        /// <param name="parameters">string array of parameters</param>
        /// <param name="command">string command</param>
        /// <returns>integer array of parameters</returns>
        /// <exception cref="GPLexception">exception thrown</exception>
        private int[] ParseIntegerParameters(string[] parameters, string command, int lineNumber, Dictionary<string, int> variables)
        {
            int[] paramsInt = new int[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                // Check if the parameter is a variable
                if (variables.TryGetValue(parameters[i], out int value))
                {
                    paramsInt[i] = value;
                }
                else if (!int.TryParse(parameters[i], out paramsInt[i]))
                {
                    throw new GPLexception($"Invalid parameter or undefined variable '{parameters[i]}' for {command} at line {lineNumber + 1}");
                }
            }
            return paramsInt;
        }

    }
}
