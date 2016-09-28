using System;
using System.IO;
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
        private PictureBox[] pictureBoxes = new PictureBox[18];
        private String boxname = "pictureBox";
        private String boxnames = "";
        private int borderCounter = 0;
        private System.Windows.Forms.Timer reassignTimer;
        private System.Windows.Forms.Timer easeTimer;
        QuestionAnswerForm qAForm;


        public PressYourLuckGameForm()
        {
            InitializeComponent();
            BorderBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BorderBox.BorderStyle = BorderStyle.Fixed3D;
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
                pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
            }

            Space[] boardSpaces = new Space[18];
            for (int i = 0; i < 18; i++)
            {
                boardSpaces[i] = new Space(pictureBoxes[i]);
            }

            InitTimer();
        }

        //Purpose: Initializes the two timers required to spin and stop the BorderBox
        //Requires: nothing
        //Returns: nothing
        private void InitTimer()
        {
            reassignTimer = new System.Windows.Forms.Timer();
            reassignTimer.Tick += new EventHandler(reassignTimer_Tick);
            reassignTimer.Interval = 500; // in reassignTimer = new System.Windows.Forms.Timer();

            easeTimer = new System.Windows.Forms.Timer();
            easeTimer.Tick += new EventHandler(easeTimer_Tick);
            easeTimer.Interval = 500; // in miliseconds

        }

        //Purpose: Reassigns the border's parent from one picture box to 
        //another
        //Requires: nothing
        //Returns: nothing
        private void reassignBorder()
        {
            if (borderCounter == 18)
                borderCounter = 0;

            BorderBox.Parent = pictureBoxes[borderCounter];
            BorderBox.Location = new Point(0, 0);
            borderCounter++;
        }

        //Purpose: Calls the reassignBorder function every reassignTimer.Interval
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void reassignTimer_Tick(object sender, EventArgs e)
        {
            reassignBorder();
        }


        //Purpose: Decreases the reassignTimer.Interval every easeTimer.Interval
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void easeTimer_Tick(object sender, EventArgs e)
        {
            //decrease reassignTimer inverval
            reassignTimer.Interval += 50;

            if (reassignTimer.Interval >= 500)
            {
                reassignTimer.Stop();
                easeTimer.Stop();
            }
        }

        //Purpose: Starts stopping the BorderBox from spinning around the spaces
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void Stop_Click(object sender, EventArgs e)
        {
            easeTimer.Start();
        }


        //Purpose: Starts spinning the BorderBox around the board,
        //if reassignTimer.Interval >= 500
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void Spin_Click(object sender, EventArgs e)
        {
            if (reassignTimer.Interval >= 500)
            {
                BorderBox.Visible = true;
                reassignTimer.Interval = 10; // in reassignTimer = new System.Windows.Forms.Timer();
                reassignTimer.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qAForm = new QuestionAnswerForm();
            qAForm.Show();

        }
    }

}
