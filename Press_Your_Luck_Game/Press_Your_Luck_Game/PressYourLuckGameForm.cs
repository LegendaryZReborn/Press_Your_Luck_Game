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
        Space[] boardSpaces = new Space[NUM_SPACES];
        private String boxname = "pictureBox";
        private String boxnames = "";
        private int borderCounter = 0;
        private int roundNum = 1;
        private int maxRounds = 2;
        private System.Windows.Forms.Timer reassignTimer;
        private System.Windows.Forms.Timer easeTimer;
        private QuestionAnswerForm qAForm;
        private Player player1 = new Player(), player2 = new Player(), whoseTurn;
        private PlayersNamesForm playersNamesForm = new PlayersNamesForm();
        private bool endRound = false, found = false;
       

        public PressYourLuckGameForm()
        {
            InitializeComponent();

            qAForm = new QuestionAnswerForm();

            //Initialize a picturebox that holds the border for the spinning 
            //lights that go around the gameboard
            BorderBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BorderBox.BorderStyle = BorderStyle.Fixed3D;
            BorderBox.BackColor = Color.Transparent;
            BorderBox.Parent = pictureBox1;
            BorderBox.Location = new Point(0, 0);
            BorderBox.Image = Image.FromFile("..\\..\\Images\\Misc\\Border.gif");
            BorderBox.Visible = false;
          
            
            //Assigns each picture box excluding the BorderBox and PressYourLuckSpin
            //to an array. Basically this is the array of picture boxes that will 
            //become game spaces.
            for (int i = 0; i < NUM_SPACES; i++)
            {
                boxnames = boxname + (i + 1);
                pictureBoxes[i] = this.Controls.Find(boxnames, true).FirstOrDefault() as PictureBox;
                pictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
            }

            //create an array of space objects and assign to each picture box
            //around the board a space
            for (int i = 0; i < NUM_SPACES; i++)
            {
                boardSpaces[i] = new Space(pictureBoxes[i]);
            }

            initTimers();
            initPlayers();
            startRound();
        }

       

        //Purpose: Initializes the two timers required to spin and stop the BorderBox
        //Requires: nothing
        //Returns: nothing
        private void initTimers()
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
        //and starts the next round or next players turn when easeTimer
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
                reapSpin(whoseTurn);
                whoseTurn.Spins -= 1;
                stopButton.Enabled = false;

                if (whoseTurn == player1)
                    player1_spins_textBox.Text = player1.Spins.ToString();
                else
                    player2_spins_textBox.Text = player2.Spins.ToString();

                if (whoseTurn.Spins == 0 && whoseTurn == player2)
                {
                    endRound = true;
                   
                }

                if (whoseTurn.Spins == 0 && !endRound)
                {
                    whoseTurn = player2;
                    currentStatusL.Text = player2.Name + "'s Spin";
                }

               

                if (endRound)
                {
                    roundNum++;
                    startRound();   
                }
            }
        }

        //Purpose: Starts spinning the BorderBox around the board,
        //if reassignTimer.Interval >= 500
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void PressYourLuckSpin_Click(object sender, EventArgs e)
        {
            if (reassignTimer.Interval >= 500)
            {
                BorderBox.Visible = true;
                reassignTimer.Interval = 10; // in reassignTimer = new System.Windows.Forms.Timer();
                reassignTimer.Start();
                stopButton.Enabled = true;
            }
        }

        //Purpose: Starts stopping the BorderBox from spinning around the spaces
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void Stop_Click(object sender, EventArgs e)
        {
            easeTimer.Start();
        

        }


        private void questionPlayers()
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
            player.Spins += qAForm.CorrectAnswers;
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
            player1_cash_textBox.Text = "$" + player1.Cash.ToString();
            player2_cash_textBox.Text = "$" + player2.Cash.ToString();
            player1_spins_textBox.Text = player1.Spins.ToString();
            player2_spins_textBox.Text = player2.Spins.ToString();
        }

        //Purpose: Allows a player reap the benefits or
        //drawbacks of his or her spin
        //Requires:Player playerN
        //Returns: nothing
        private void reapSpin(Player playerN)
        {
            KeyValuePair<string, int> sEvent;
            int spaceIndex = 0 ;

            //search for the space that was landed 
            found = false;
            for (int p = 0; p < 18 && !found; p++)
            {
                if (BorderBox.Parent == pictureBoxes[p])
                {
                    found = true;
                    spaceIndex = p;
                }    
            }
           
            sEvent = boardSpaces[spaceIndex].EventNVal;

            if(sEvent.Key == "Whammy")
            {
                if (sEvent.Value != 0 && playerN.Cash != 0)
                {
                    playerN.Cash -= sEvent.Value;
                    if (playerN.Cash < 0)
                        playerN.Cash = 0;
                }
                else
                    playerN.Cash = 0;
            }
            else if(sEvent.Key == "Cash")
            {
                playerN.Cash += sEvent.Value;
            }

            player1_cash_textBox.Text = "$" + player1.Cash.ToString();
            player2_cash_textBox.Text = "$" + player2.Cash.ToString();


        }

        //Purpose: Starts a round in the game itself
        //Requires: nothing
        //Returns: nothing
        private void startRound()
        {
            if (!this.Visible)
                this.Show();

            if (roundNum <= maxRounds)
            {
                
                questionPlayers();

                //randomize each space
                for (int r = 0; r < 18; r++)
                {
                    boardSpaces[r].randomizeSpace();
                }

                stopButton.Enabled = false;


                //give them one spin each if they both still have none
                if (player1.Spins == 0 && player2.Spins == 0)
                {
                    player1.Spins = 1;
                    player2.Spins = 1;
                    player1_spins_textBox.Text = player1.Spins.ToString();
                    player2_spins_textBox.Text = player2.Spins.ToString();
                }

                //set who's turn to spin
                if (player1.Spins != 0)
                {
                    currentStatusL.Text = player1.Name + "'s Spin";
                    whoseTurn = player1;
                  
                }
                else
                {
                    currentStatusL.Text = player2.Name + "'s Spin";
                    whoseTurn = player2;
                }
                endRound = false;

            }
            else
            {
                if (player1.Cash != player2.Cash)
                {
                    currentStatusL.Text = player1.Cash > player2.Cash ? player1.Name : player2.Name;
                    currentStatusL.Text += " Wins";
                }
                else
                {
                    currentStatusL.Text = "Draw";
                }

                PressYourLuckSpin.Enabled = false;
                stopButton.Enabled = false;
            }
        }
    }

}
