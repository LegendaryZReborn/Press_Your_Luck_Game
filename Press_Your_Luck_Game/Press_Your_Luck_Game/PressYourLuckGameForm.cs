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
<<<<<<< HEAD
        const int MAX_QUESTIONS = 100;

        QAStructure[] qaStructure = new QAStructure[MAX_QUESTIONS];
        PictureBox[] pictureBoxes = new PictureBox[18];
        String boxname = "pictureBox";
        String boxnames = "";
        int i = 0;
        System.Windows.Forms.Timer timer1;
=======
        private PictureBox[] pictureBoxes = new PictureBox[18];
        private String boxname = "pictureBox";
        private String boxnames = "";
        private int borderCounter = 0;
        private System.Windows.Forms.Timer reassignTimer;
        private System.Windows.Forms.Timer easeTimer;

>>>>>>> eb4b6bb4b80003f782be2f6cb687f59cf5f3015e

        public PressYourLuckGameForm()
        {
            InitializeComponent();

            readQuestions("..\\..\\luckfile.txt");

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


        private void InitTimer()
        {
            reassignTimer = new System.Windows.Forms.Timer();
            reassignTimer.Tick += new EventHandler(reassignTimer_Tick);
            reassignTimer.Interval = 500; // in reassignTimer = new System.Windows.Forms.Timer();

            easeTimer = new System.Windows.Forms.Timer();
            easeTimer.Tick += new EventHandler(easeTimer_Tick);
            easeTimer.Interval = 500; // in miliseconds

        }

       private void reassignBorder()
        {
            if (borderCounter == 18)
                borderCounter = 0;

            // code here will run every second
            BorderBox.Parent = pictureBoxes[borderCounter];
            BorderBox.Location = new Point(0, 0);
            borderCounter++;
        }

<<<<<<< HEAD
        //Purpose: reads all questions and answers from file and returns the number of pairs read
        //Requires: none
        //Returns: number of pars of questions and answers in the file
        public int readQuestions(string file)
        {
            StreamReader streamReader;

            //try reading from file first
            try
            {
                streamReader = new StreamReader(file);
                int count;
                //read all questions and answers and keep track of number read
                for(count = 0; !streamReader.EndOfStream && count < MAX_QUESTIONS; ++count)
                {
                    qaStructure[count] = new QAStructure();
                    qaStructure[count].Question = streamReader.ReadLine();
                    qaStructure[count].Answer = streamReader.ReadLine();
                }

                //return count of pairs of questions and answers
                return count;
            }
            catch (Exception exception)
            {
                //notify user that file could not be read
                
                return -1; //exit from program
            }

        }
=======
>>>>>>> eb4b6bb4b80003f782be2f6cb687f59cf5f3015e


        private void reassignTimer_Tick(object sender, EventArgs e)
        {
            reassignBorder();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            easeTimer.Start();
        }

        private void Spin_Click(object sender, EventArgs e)
        {
            if (reassignTimer.Interval >= 500)
            {
                BorderBox.Visible = true;
                reassignTimer.Interval = 10; // in reassignTimer = new System.Windows.Forms.Timer();
                reassignTimer.Start();
            }
        }

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

    }

}
