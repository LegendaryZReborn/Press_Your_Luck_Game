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
        private const int NUM_SPACES = 18;
        private PictureBox[] pictureBoxes = new PictureBox[NUM_SPACES];
        private String boxname = "pictureBox";
        private String boxnames = "";
        private int borderCounter = 0;
        private System.Windows.Forms.Timer reassignTimer;
        private System.Windows.Forms.Timer easeTimer;
        private QuestionAnswerForm qAForm;
        private Player player1 = new Player(), player2 = new Player();
        private PlayersNamesForm playersNamesForm = new PlayersNamesForm();

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
            qAForm = new QuestionAnswerForm();

            for (int i = 0; i < NUM_SPACES; i++)
            {
                boxnames = boxname + (i + 1);
                pictureBoxes[i] = this.Controls.Find(boxnames, true).FirstOrDefault() as PictureBox;
                pictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
            }

            Space[] boardSpaces = new Space[NUM_SPACES];
            for (int i = 0; i < NUM_SPACES; i++)
            {
                boardSpaces[i] = new Space(pictureBoxes[i]);
            }

            initPlayers();

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
            BorderBox.Parent = pictureBoxes[borderCounter];
            BorderBox.Location = new Point(0, 0);
            borderCounter = (borderCounter + 1) % NUM_SPACES;
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

        private void startQ_Click(object sender, EventArgs e)
        {
            //here assign number of spins returned from question form to a variable
            getPlayerSpins(player1);
            getPlayerSpins(player2);

            player1_spins_textBox.Text = player1.Spins.ToString();
            player2_spins_textBox.Text = player2.Spins.ToString();
            //maybe we want to deactivate start question button here until users have done used their spins

        }

        private void getPlayerSpins(Player player)
        {
            qAForm.startQuestioning(player.Name);
            qAForm.ShowDialog(this);
            player.Spins = qAForm.CorrectAnswers;
        }

        private void initPlayers()
        {
            //Get players' names
            playersNamesForm.ShowDialog();
            String name1 = "", name2 = "";

            playersNamesForm.getPlayersNames(ref name1, ref name2);
            player1.Name = name1;
            player2.Name = name2;
            player1_groupBox.Text = player1.Name;
            player2_groupBox.Text = player2.Name;
        }
    }

}
