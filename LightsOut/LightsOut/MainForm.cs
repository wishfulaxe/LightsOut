using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            

            lightsOutGame.NewGame();
        }

        private LightsOutGame lightsOutGame;
        private const int GridOffset = 25; // Distance from upper-left side of window
        private const int GridLength = 200; // Size in pixels of grid
        
     

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int r = 0; r < lightsOutGame.GridSize; r++)
            {
                for (int c = 0; c < lightsOutGame.GridSize; c++)
                {
                    // Get proper pen and brush for on/off
                    // grid section
                    Brush brush;
                    Pen pen;
                    if (lightsOutGame.GetGridValue(r,c))
                    {
                        pen = Pens.Black;
                        brush = Brushes.White; // On
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black; // Off
                    }
                    // Determine (x,y) coord of row and col to draw rectangle
                    int x = c * lightsOutGame.GridSize + GridOffset;
                    int y = r * lightsOutGame.GridSize + GridOffset;
                    // Draw outline and inner rectangle
                    g.DrawRectangle(pen, x, y, lightsOutGame.GridSize, lightsOutGame.GridSize);
                    g.FillRectangle(brush, x + 1, y + 1, lightsOutGame.GridSize - 1, lightsOutGame.GridSize - 1);
                }
            }
        }

        private bool PlayerWon()
        {
            bool temp = true;
            // Write the function code here
            for(int r=0; r< lightsOutGame.GridSize; r++)
            {
                for(int c=0; c< lightsOutGame.GridSize; c++)
                {
                    if(lightsOutGame.GetGridValue(r, c)==true)
                    {
                        temp = false;
                    }
                }
            }
            return temp;
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            // Fill grid with either white or black
            for (int r = 0; r < lightsOutGame.GridSize; r++)
                for (int c = 0; c < lightsOutGame.GridSize; c++)
                    lightsOutGame.NewGame();
            // Redraw grid
            this.Invalidate();
        } 

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Make sure click was inside the grid
            if (e.X < GridOffset || e.X > lightsOutGame.GridSize * lightsOutGame.GridSize + GridOffset ||
            e.Y < GridOffset || e.Y > lightsOutGame.GridSize * lightsOutGame.GridSize + GridOffset)
                return;
            // Find row, col of mouse press
            int r = (e.Y - GridOffset) / lightsOutGame.GridSize;
            int c = (e.X - GridOffset) / lightsOutGame.GridSize;
            
            // Invert selected box and all surrounding boxes
             for (int i = r - 1; i <= r + 1; i++)
                for (int j = c - 1; j <= c + 1; j++)
                    if (i >= 0 && i < lightsOutGame.GridSize && j >= 0 && j < lightsOutGame.GridSize)
                        lightsOutGame.Move(r,c);
            // Redraw grid
            this.Invalidate();
            // Check to see if puzzle has been solved
            if (lightsOutGame.IsGameOver())
            {
                // Display winner dialog box
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newGameButton_Click_1(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForms aboutBox = new AboutForms();
            aboutBox.ShowDialog(this);
        }
    }
}
