/*
PressYourLuckForm.cs
By Anthony Enem and Cavaughn Browne
CMPS 4143: Contemporary programming Languages
10/06/2016

This form implements a PressYourLuck game for 2 players. They have to answer 3
questions correctly and get spins to get cash prizes or get their money stolen
by whammies. The program uses different music for the general game, the spin
phase and the win phase. 18 spaces are created and randomly assigned cash or
whammy values in the form. A display board is made for each user on the same
form for each user with their number of spins and current cash value. A general
display board is at the top right corner to display whose turn it is to play.
Players enter their names at the start of the game and the names are used to
direct the game flow for the players. A QuestionAnswerForm takes care of 
questioning the players and validating their answers. The player with the most
cash at the end of 2 rounds win.

*/

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
using System.Media;

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
        private int roundNum;
        private const int MAX_ROUNDS = 2;
        private System.Windows.Forms.Timer reassignTimer;
        private System.Windows.Forms.Timer easeTimer;
        private QuestionAnswerForm qAForm;
        private Player player1 = new Player(), player2 = new Player(), whoseTurn;
        private PlayersNamesForm playersNamesForm = new PlayersNamesForm();
        private bool endRound = false, found = false;
        private const string SPIN_MUSIC = "..\\..\\Music\\naruto_5ths_fight.wav";
        private const string GAME_MUSIC = "..\\..\\Music\\one_punch.wav";
        private const string WIN_MUSIC = "..\\..\\Music\\Dragon Ball" + 
            " Super OST - Turning the Tables.wav";
        private SoundPlayer spin_sound_player = new SoundPlayer(SPIN_MUSIC);
        private SoundPlayer game_sound_player = new SoundPlayer(GAME_MUSIC);
        private SoundPlayer win_sound_player = new SoundPlayer(WIN_MUSIC);
        private const string BORDER_FILE_NAME = "..\\..\\Images\\Misc\\Border.gif";
        private bool game_soundRunning = false;
        private string intructions = "The Press Your Luck Game brought to you"
       + " by Cavaughn Browne and Anthony Enem. The game is simple. You answer 3"
       + " questions each (2 players) and gain one spin chance for each correct."
       + " You use the spins to take turns spinning the wheel and gain fabulous"
       + " prizes or loose some(or all) by landing on Whammy's(the little man)."
       + " You can pass your spins to the other player if you dont want to spin."
       + " Have fun!!";

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
            BorderBox.Image = Image.FromFile(BORDER_FILE_NAME);

            //Assigns each picture box excluding the BorderBox and 
            //PressYourLuckSpin to an array. Basically this is the array
            // of picture boxes that will become game spaces.
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                boxnames = boxname + (i + 1);
                pictureBoxes[i] = this.Controls
                    .Find(boxnames, true).FirstOrDefault() as PictureBox;

                pictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
            }

            //create an array of space objects and assign to each picture box
            //around the board a space
            for (int i = 0; i < boardSpaces.Length; i++)
            {
                boardSpaces[i] = new Space(pictureBoxes[i]);
            }

            playOrQuitButton.Text = "Play";
            BorderBox.Visible = false;
            stopButton.Enabled = false;
            PressYourLuckSpin.Enabled = false;
            passSpinsButton.Enabled = false;
            initTimers();
            game_sound_player.PlayLooping(); //start music
            game_soundRunning = true;

                //Show instructions on a message box;
                MessageBox.Show(intructions, "Intructions", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

        }

        //Purpose: Initializes the all the variables needed to be 
        //initialized at the start 
        //Requires: nothing
        //Returns: nothing
        private void initGame()
        {
            roundNum = 1;
            PressYourLuckSpin.Enabled = false;
            stopButton.Enabled = false;
            passSpinsButton.Enabled = false;
            currentStatusL.Text = "";
            player1.Cash = 0;
            player2.Cash = 0;
            player1.Spins = 0;
            player2.Spins = 0;
            updatePlayersCashInfo();
            updatePlayersSpinInfo();
            playOrQuitButton.Text = "Play";
            BorderBox.Visible = false;
           
        }


        //Purpose: Initializes the two timers required to spin and stop 
        //the BorderBox
        //Requires: nothing
        //Returns: nothing
        private void initTimers()
        {
            reassignTimer = new System.Windows.Forms.Timer();
            reassignTimer.Tick += new EventHandler(reassignTimer_Tick);
            reassignTimer.Interval = 500; 

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

        //Purpose: Calls the reassignBorder function every 
        //reassignTimer.Interval
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

                //decrement current player's spins
                whoseTurn.Spins -= 1;
                passSpinsButton.Enabled = true;
                stopButton.Enabled = false;

                //reset textboxes for players' spins
                if (whoseTurn == player1)
                    player1_spins_textBox.Text = player1.Spins.ToString();
                else
                    player2_spins_textBox.Text = player2.Spins.ToString();

                if (player1.Spins == 0 && player2.Spins == 0)
                {
                    endRound = true;
                }
                else if(whoseTurn.Spins == 0)
                {
                    //change to the next player
                    whoseTurn = whoseTurn == player1 ? player2 : player1;
                    currentStatusL.Text = whoseTurn.Name + "'s Spin";

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
                reassignTimer.Interval = 10; 
                reassignTimer.Start();
                stopButton.Enabled = true;
                passSpinsButton.Enabled = false;
            }
        }

        //Purpose: Starts stopping the BorderBox from spinning around the spaces
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void Stop_Click(object sender, EventArgs e)
        {
            easeTimer.Start();
        }

        //Purpose: Starts or quits the game
        //Requires: object sender, EventArgs e
        //Returns: nothing
        private void playOrQuitButton_Click(object sender, EventArgs e)
        {
            if (playOrQuitButton.Text == "Play")
            {
                initGame();
                initPlayers();
                startRound();
                playOrQuitButton.Text = "Quit Game";
                PressYourLuckSpin.Enabled = true;
            }
            else
            {
                this.Close();
            }
        }

        //Purpose: Updates all players cash information on the form
        //Requires: nothing
        //Returns: nothing
        private void updatePlayersCashInfo()
        {
            player1_cash_textBox.Text = "$" + player1.Cash.ToString();
            player2_cash_textBox.Text = "$" + player2.Cash.ToString();
        }

        //Purpose: Updates all players spins information on the form
        //Requires: nothing
        //Returns: nothing
        private void updatePlayersSpinInfo()
        {
            player1_spins_textBox.Text = player1.Spins.ToString();
            player2_spins_textBox.Text = player2.Spins.ToString();
        }

        //Purpose: Call a function to ask all players questions
        // and record their spins 
        //Requires: nothing
        //Returns: nothing
        private void questionPlayers()
        {
            getPlayerSpins(player1);
            getPlayerSpins(player2);

            updatePlayersSpinInfo();
        }

        //Purpose: Ask a player some questions and award him/her spins
        //for each correct answer
        //Requires: Player player
        //Returns: nothing
        private void getPlayerSpins(Player player)
        {
            qAForm.startQuestioning(player.Name);
            qAForm.ShowDialog(this);
            player.Spins += qAForm.CorrectAnswers;
        }
        
        //Purpose: initializes players' names, cash and spins
        //Requires: none
        //Returns: none
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
            updatePlayersCashInfo();
            updatePlayersSpinInfo();
        }

        //Purpose: pass spins from p1 to p2
        //Requires: Player objects p1 and p2
        //Returns: none
        void passSpins(Player p1, Player p2)
        {
            p2.Spins += p1.Spins;
            p1.Spins = 0;
            whoseTurn = p2;
            currentStatusL.Text = p2.Name + "'s Spin";
        }

        //Purpose: passes spins between players when button is clicked
        //Requires: object sender and EventArgs
        //Returns: none
        private void passSpinsButton_Click(object sender, EventArgs e)
        {
            if (whoseTurn == player1)
            {
                passSpins(player1, player2);
            }
            else
            {
                passSpins(player2, player1);
            }

            updatePlayersSpinInfo();
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

            updatePlayersCashInfo();
        }

        //Purpose: Ask the players if they want to play again
        //Requires: nothing
        //Returns: nothing
        private void playAgain()
        {
            DialogResult dialogResult = MessageBox.Show
                ("Do you want to play again?", "Play Again", 
                MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                initGame();
                if (!game_soundRunning)
                    game_sound_player.PlayLooping();
                game_soundRunning = true;

            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }

        //Purpose: Starts a round in the game itself
        //Requires: nothing
        //Returns: nothing
        private void startRound()
        {
           
            if (roundNum <= MAX_ROUNDS)
            {
                passSpinsButton.Enabled = true;
                currentStatusL.Text = "Round " + roundNum;
                
                //start general game music
                if(!game_soundRunning)
                     game_sound_player.PlayLooping();

                questionPlayers();

                //start spin music
                spin_sound_player.PlayLooping();
                game_soundRunning = false;

                //randomize each space
                for (int r = 0; r < boardSpaces.Length; r++)
                {
                    boardSpaces[r].randomizeSpace();
                }

                stopButton.Enabled = false;


                //give them one spin each if they both still have none
                if (player1.Spins == 0 && player2.Spins == 0)
                {
                    player1.Spins = 1;
                    player2.Spins = 1;
                    updatePlayersSpinInfo();
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
                    currentStatusL.Text = player1.Cash > player2.Cash ? 
                        player1.Name : player2.Name;

                    currentStatusL.Text += " Wins";
                    win_sound_player.PlayLooping();
                    game_soundRunning = false;
                }
                else
                {
                    currentStatusL.Text = "Draw";
                }

                PressYourLuckSpin.Enabled = false;
                stopButton.Enabled = false;
                playAgain();
                
            }
        }
    }

}
