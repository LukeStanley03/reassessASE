using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reassessASE
{
    public interface ICanvas
    {
        void Circle(int radius);
    }

    public class Canvas : ICanvas
    {
        //Standard size of canvas
        const int XSIZE = 640;
        const int YSIZE = 480;


        //instance data for pen and x, y positions and graphics context
        //graphics context is the drawing area to draw on
        Form CallingForm;
        Graphics g, cursorG;
        int XCanvasSize, YCanvasSize;
        Pen pen;
        public int xPos, yPos; // pen position when drawing
    }
}
