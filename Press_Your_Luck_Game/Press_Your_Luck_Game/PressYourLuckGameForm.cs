using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;

namespace Press_Your_Luck_Game
{
    public partial class PressYourLuckGameForm : Form
    {
        PictureBox[] pictureBoxes = new PictureBox[18];
        String boxname = "pictureBox";
        String boxnames = "";
        int i = 0;
       System.Windows.Forms.Timer timer1;

        public PressYourLuckGameForm()
        {
            InitializeComponent();
<<<<<<< HEAD
            QAStructure[] qas = new QAStructure[3];
            
=======

            BorderBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BorderBox.BackColor = Color.Transparent;
            BorderBox.Parent = pictureBox1;
            BorderBox.Location = new Point(0, 0);
            BorderBox.Image = Image.FromFile("..\\..\\Images\\Misc\\Border.gif");
            BorderBox.Visible = false;
           
            for (int i = 0; i < 18; i++)
            {

                boxnames = boxname + (i + 1);
                pictureBoxes[i] = this.Controls.Find(boxnames, true).FirstOrDefault() as PictureBox;
                pictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }

            Space[] boardSpaces = new Space[18];
            for (int i = 0; i < 18; i++)
            {
                boardSpaces[i] = new Space(pictureBoxes[i]);
            }

            InitTimer();
        }

        
        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 30; // in miliseconds
        }

        public void reassignBorder()
        {
            if (i == 18)
                i = 0; 
            // code here will run every second
            BorderBox.Parent = pictureBoxes[i];
            BorderBox.Location = new Point(0, 0);
            i++;
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            reassignBorder();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Spin_Click(object sender, EventArgs e)
        {
            BorderBox.Visible = true;
            timer1.Start();
>>>>>>> 895646f61fd88f18b2ca697b32e8d053db6a6d6b
        }
    }

}
