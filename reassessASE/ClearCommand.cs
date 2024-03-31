using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class ClearCommand : ICanvasCommand
    {
        /// <summary>
        /// Clear the drawing area
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.Clear();
        }
    }

}