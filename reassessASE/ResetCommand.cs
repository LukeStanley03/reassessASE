using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class ResetCommand : ICanvasCommand
    {
        /// <summary>
        /// Reset cursor to its original position
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.Reset();
            canvas.updateCursor(); // This should update the cursor position on the UI
        }
    }
}
