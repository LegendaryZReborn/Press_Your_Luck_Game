//Player.cs file
//Player class to hold information about player

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Press_Your_Luck_Game
{
    class Player
    {
        private int cash;
        private int spins;

        public Player()
        {
            cash = 0;
            spins = 0;
        }

        public int Spins
        {
            //returns the value of spins
            get
            {
                return spins;
            }

            //Sets spins to value if value is positive, else spins is unchanged
            set
            {
                this.spins = value < 0 ? this.spins : value;
            }
        }

        public int Cash
        {
            //returns the value of cash
            get
            {
                return cash;
            }

            //Sets cash to vlaue if value is positive, else cash is unchanged
            set
            {
                this.cash = value < 0 ? this.cash : value;
            }
        }
    }
}
