using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class RectangleCommand : ICanvasCommand
    {
        private int width;
        private int height;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectangleCommand(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Draw a rectangle with width and height
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.Rectangle(width, height);
        }
    }
}