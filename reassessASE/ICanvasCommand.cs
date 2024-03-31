using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public interface ICanvasCommand
    {
        /// <summary>
        /// Execute the given Canvas command
        /// </summary>
        /// <param name="canvas"></param>
        void Execute(Canvas canvas);
    }
}
