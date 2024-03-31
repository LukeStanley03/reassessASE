using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class BlueColourCommand : ICanvasCommand
    {
        /// <summary>
        /// Set the colour to blue
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.BlueColour();
        }
    }
}
