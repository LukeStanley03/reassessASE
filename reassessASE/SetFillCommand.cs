using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class SetFillCommand : ICanvasCommand
    {
        private string fillSetting;

        /// <summary>
        /// Set the fill on either ON or OFF
        /// </summary>
        /// <param name="fillSetting"></param>
        public SetFillCommand(string fillSetting)
        {
            this.fillSetting = fillSetting;
        }

        /// <summary>
        /// Set the fill
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.SetFill(fillSetting);
        }
    }
}
