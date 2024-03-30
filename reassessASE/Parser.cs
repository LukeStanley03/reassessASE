﻿using System;
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
    }
}
