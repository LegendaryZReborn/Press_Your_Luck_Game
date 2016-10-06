/*
PlayersNamesForm.cs
By Anthony Enem and Cavaughn Browne
CMPS 4143: Contemporary programming Languages
10/06/2016

This form implements enables 2 players to enter their names and hit a start
game button to validate the names, and close the form.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Press_Your_Luck_Game
{
    public partial class PlayersNamesForm : Form
    {
        //private data
        private string player1_name;
        private string player2_name;

        //constructor
        public PlayersNamesForm()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        //Purpose: to get names of players by reference
        //Requires: 2 strings passed by reference
        //Returns: none
        public void getPlayersNames(ref String name1, ref String name2)
        {
            name1 = player1_name;
            name2 = player2_name;
        }

        //Purpose: handles event for clicking playersNames_done_buttton
        //Requires: object sender, EventArgs
        //Returns: none
        private void playersNames_done_button_Click(object sender, EventArgs e)
        {
            //If either of the textboxes for players' names are empty, display error message box
            if (player1Name_textBox.Text == "")
            {
                MessageBox.Show("Must enter a name for player 1", "Invalid Player Name!", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }

            else if (player2Name_textBox.Text == "")
            {
                MessageBox.Show("Must enter a name for player 2", "Invalid Player Name!", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            else
            {
                //else assign data in text boxes and close form
                player1_name = player1Name_textBox.Text;
                player2_name = player2Name_textBox.Text;

                this.Close();
            }
        }
    }
}
