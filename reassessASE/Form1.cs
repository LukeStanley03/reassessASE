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

        Color background_colour = Color.Gray;


        /// <summary>
        /// Initialises a new instance of <see cref="Form1"/>
        /// Initialises the output bitmap and the canvas for drawing
        /// </summary>

        public Form1()
        {
            InitializeComponent();
            g = Graphics.FromImage(OutputBitmap);
            //Class for handling the drawing, pass the drawing surface to it
            MyCanvas = new Canvas(this, Graphics.FromImage(OutputBitmap), Graphics.FromImage(CursorBitmap));
            MyParser = new Parser(MyCanvas);
            MyCanvas.updateCursor();
            g.Clear(background_colour);
        }

        private void runbutton_Click(object sender, EventArgs e)
        {

        }

        private void syntaxbutton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Displays the graphics
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">instance containing the graphics context</param>
        private void outputWindow_Paint(object sender, PaintEventArgs e)
        {
            //get graphics context of the form
            var graphics = e.Graphics;

            //put the off screen bitmaps on the form
            graphics.DrawImageUnscaled(OutputBitmap, x: 0, y: 0);
            graphics.DrawImageUnscaled(CursorBitmap, x: 0, y: 0);

        }

        /// <summary>
        /// handles the click event for the open button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open GPL File",
                Filter = "GPL File (*.gpl)|*.gpl"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileHandler fileHandler = new FileHandler();
                    string fileContent = fileHandler.ReadFromFile(openFileDialog.FileName);
                    programWindow.Text = fileContent;
                }
                catch (GPLexception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the saveButton control
        /// <para>The save button is used to open a save dialogue to save the current program in the program window</para>
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">instance containing the event data</param>
        /// <exception cref="GPLexception">Handles the exceptions</exception>
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save GPL File",
                Filter = "GPL File (*.gpl)|*.gpl"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileHandler fileHandler = new FileHandler();
                    fileHandler.WriteToFile(saveFileDialog.FileName, programWindow.Text);
                }
                catch (GPLexception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
