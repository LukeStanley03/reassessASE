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

        public Canvas()
        {
            XCanvasSize = XSIZE;
            YCanvasSize = YSIZE;
        }

        /// <summary>
        /// Constructor initialises canvas to black pen at 0,0
        /// </summary>
        /// <param name="g">Graphics context of place to draw on</param>
        public Canvas(Form CallingForm, Graphics gin, Graphics cursorG)
        {
            this.g = gin;
            this.cursorG = cursorG;
            XCanvasSize = XSIZE;
            YCanvasSize = YSIZE;
            xPos = yPos = 0;
            pen = new Pen(Color.Black, 2);

            this.CallingForm = CallingForm;
        }
        /// <summary>
        /// read only property for xpos of cursor
        /// </summary>
        public int Xpos
        {
            get
            {
                return xPos;
            }
        }

        /// <summary>
        /// read only property for ypos of cursor
        /// </summary>
        public int Ypos
        {
            get
            {
                return yPos;
            }
        }

        /// <summary>
        /// move the cursor display to the current drawing position
        /// </summary>
        public void updateCursor()
        {
            cursorG.Clear(Color.Transparent);
            Pen p = new Pen(Color.Red, 1);
            cursorG.DrawRectangle(p, xPos, yPos, 4, 4);
            CallingForm.Refresh();
        }

        /// <summary>
        /// sets the RGB colours
        /// </summary>
        /// <param name="red">integer value of red</param>
        /// <param name="green">integer value of green</param>
        /// <param name="blue">integer value of blue</param>
        public void SetColour(int red, int green, int blue)
        {
            if (red > 255 || green > 255 || blue > 255)
                throw new GPLexception("Invalid colour");
            penColour = Color.FromArgb(red, green, blue);
            pen = new Pen(penColour, 1);
        }

        /// <summary>
        /// Moves pen position
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        public void MoveTo(int x, int y)
        {
            penPosition = new Point(x, y);

            if (x < 0 || x > XCanvasSize || y < 0 || y > XCanvasSize)
                throw new GPLexception("invalid screen position Canvas.MoveTo");
            //update the pen position as it has moved to the end of the line
            xPos = x;
            yPos = y;
        }

        /// <summary>
        /// draw a line from current pen position (xPos,yPos)
        /// </summary>
        /// <param name="toX">x position to draw to</param>
        /// <param name="toY">y position to draw to</param>
        public void DrawTo(int toX, int toY)
        {
            if (toX < 0 || toX > XCanvasSize || toY < 0 || toX > XCanvasSize)
                throw new GPLexception("invalid screen position Canvas.DrawTo");
            if (g != null) //if from a unit test then g will be null
                //draw the line
                g.DrawLine(pen, xPos, yPos, toX, toY);

            //update the pen position as it has moved to the end of the line
            xPos = toX;
            yPos = toY;
        }


        /// <summary>
        /// draw a square 
        /// </summary>
        /// <param name="width">size of the square length</param>
        public void Square(int width)
        {
            if (width < 0)
                throw new GPLexception("Invalid square width");

            if (g != null)
            {
                if (fill)
                {
                    //fill the square
                    g.FillRectangle(pen.Brush, xPos - width / 2, yPos - width / 2, width, width);
                }
                else
                {
                    //draw the square
                    g.DrawRectangle(pen, xPos, yPos, width, width);
                }
            }
        }

        /// <summary>
        /// draws rectangle
        /// </summary>
        /// <param name="width">width of the rectangle</param>
        /// <param name="height">height of the rectangle</param>
        public void Rectangle(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new GPLexception("Invalid rectangle width and height");

            if (g != null)
            {
                if (fill)
                {
                    //fill the rectangle
                    g.FillRectangle(pen.Brush, xPos - width / 2, yPos - width / 2, width, height);
                }
                else
                {
                    //draw the rectangle
                    g.DrawRectangle(pen, xPos - width / 2, yPos - width / 2, width, height);
                }
            }
        }

        /// <summary>
        /// draws triangle
        /// </summary>
        /// <param name="width">width of the triangle</param>
        /// <param name="height">height of the triangle</param>
        public void Triangle(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new GPLexception("Invalid rectangle width and height");

            //Points used to draw the triangle
            Point[] points = { new Point(xPos, yPos - height / 2), new Point(xPos - width / 2, yPos + height / 2), new Point(xPos + width / 2, yPos + height / 2) };

            if (g != null)
            {
                if (fill)
                {
                    //fill the triangle
                    g.FillPolygon(pen.Brush, points);
                }
                else
                {
                    //draw the triangle
                    g.DrawPolygon(pen, points);
                }
            }
        }

        /// <summary>
        /// draws a circle
        /// </summary>
        /// <param name="radius">radius of the circle</param>
        public virtual void Circle(int radius)
        {
            if (radius < 0)
                throw new GPLexception("invalid circle radius");

            if (g != null)
            {
                if (fill)
                {
                    //fill the circle
                    g.FillEllipse(pen.Brush, xPos - radius, yPos - radius, radius * 2, radius * 2);
                }
                else
                {
                    //draw the circle
                    g.DrawEllipse(pen, xPos - radius, yPos - radius, radius * 2, radius * 2);
                }
            }

        }
    }
}
