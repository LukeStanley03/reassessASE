﻿using System;
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
    }
}
