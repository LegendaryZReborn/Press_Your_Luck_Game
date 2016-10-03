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
        private string player1_name;
        private string player2_name;

        public PlayersNamesForm()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        public void getPlayersNames(ref String name1, ref String name2)
        {
            name1 = player1_name;
            name2 = player2_name;
        }

        private void playersNames_done_button_Click(object sender, EventArgs e)
        {
            if (player1Name_textBox.Text == "")
            {
                MessageBox.Show("Must enter a name for player 1", "Invalid Player Name!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (player2Name_textBox.Text == "")
            {
                MessageBox.Show("Must enter a name for player 2", "Invalid Player Name!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                player1_name = player1Name_textBox.Text;
                player2_name = player2Name_textBox.Text;

                this.Close();
            }
        }
    }
}
