using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reassessASE
{
    public partial class Form1 : Form
    {
        // Declarations

        const int screenX = 640;
        const int screenY = 480;

        Bitmap OutputBitmap = new Bitmap(screenX, screenY); //Bitmap to draw on which will be displayed in the outputWindow
        Bitmap CursorBitmap = new Bitmap(screenX, screenY); //overlay bitmap for cursor
        Graphics g;
        Canvas MyCanvas;
        Parser MyParser;

        public Form1()
        {
            InitializeComponent();
        }

        private void runbutton_Click(object sender, EventArgs e)
        {

        }

        private void syntaxbutton_Click(object sender, EventArgs e)
        {

        }

        private void outputWindow_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
