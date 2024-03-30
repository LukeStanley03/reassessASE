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
    }
}
