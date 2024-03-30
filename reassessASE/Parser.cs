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

    }
}
