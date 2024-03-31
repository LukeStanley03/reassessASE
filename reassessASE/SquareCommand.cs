using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class SquareCommand : ICanvasCommand
    {
        private int width;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width"></param>
        public SquareCommand(int width)
        {
            this.width = width;
        }

        /// <summary>
        /// Draw a square with width
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.Square(width);
        }
    }
}
