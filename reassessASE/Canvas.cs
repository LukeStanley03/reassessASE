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

        protected Color background_colour = Color.Gray;

        public bool fill = false;

        //Graphics g;
        //Pen pen = new Pen(Color.Black, 1);        
        //int xPos = 0, yPos = 0; // pen position when drawing

        Point penPosition = new Point(10, 10);
        Color penColour;

        //Variables to handle animated star

    }
}
