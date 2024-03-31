using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public static class CommandFactory
    {
        public static ICanvasCommand CreateCommand(string commandType, params string[] parameters)
        {
            switch (commandType.ToLower())
            {
                case "moveto":
                    // Ensure parameters length and parse to integers
                    if (parameters.Length != 2)
                        throw new GPLexception("moveto expects 2 parameters");
                    int x = int.Parse(parameters[0]);
                    int y = int.Parse(parameters[1]);
                    return new MoveToCommand(x, y);

                case "drawto":
                    if (parameters.Length != 2)
                        throw new GPLexception("drawto expects 2 parameters");
                    int toX = int.Parse(parameters[0]);
                    int toY = int.Parse(parameters[1]);
                    return new DrawToCommand(toX, toY);

                case "circle":
                    if (parameters.Length != 1)
                        throw new GPLexception("moveto expects 1 parameter");
                    int radius = int.Parse(parameters[0]);
                    return new CircleCommand(radius);

                case "square":
                    if (parameters.Length != 1)
                        throw new GPLexception("square expects 1 parameter");
                    int squareWidth = int.Parse(parameters[0]);
                    return new SquareCommand(squareWidth);

                case "rectangle":
                    if (parameters.Length != 2)
                        throw new GPLexception("rectangle expects 2 parameters");
                    int rectangleWidth = int.Parse(parameters[0]);
                    int rectangleHeight = int.Parse(parameters[1]);
                    return new RectangleCommand(rectangleWidth, rectangleHeight);

                case "triangle":
                    if (parameters.Length != 2)
                        throw new GPLexception("triangle expects 2 parameters");
                    int triangleWidth = int.Parse(parameters[0]);
                    int triangleHeight = int.Parse(parameters[1]);
                    return new TriangleCommand(triangleWidth, triangleHeight);

                case "colour":
                    if (parameters.Length != 3)
                        throw new GPLexception("setcolour expects 3 parameters");

                    if (!int.TryParse(parameters[0], out int red) ||
                        !int.TryParse(parameters[1], out int green) ||
                        !int.TryParse(parameters[2], out int blue))
                    {
                        throw new GPLexception("Invalid parameters for SetColour command.");
                    }

                    return new SetColourCommand(red, green, blue);

                case "fill":
                    if (parameters.Length != 1)
                        throw new GPLexception("SetFill command expects 1 parameter.");
                    return new SetFillCommand(parameters[0]);

                case "updatecursor":
                    return new UpdateCursorCommand();

                case "red":
                    return new RedColourCommand();

                case "green":
                    return new GreenColourCommand();

                case "blue":
                    return new BlueColourCommand();

                case "black":
                    return new BlackColourCommand();

                case "reset":
                    return new ResetCommand();

                case "clear":
                    return new ClearCommand();

               
                default:
                    throw new GPLexception($"Unknown command type: {commandType}");
            }
        }
    }
}
    

