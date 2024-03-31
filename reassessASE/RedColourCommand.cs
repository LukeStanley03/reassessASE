using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class RedColourCommand : ICanvasCommand
    {
        /// <summary>
        /// Set the colour to red
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.RedColour();
        }
    }
}
