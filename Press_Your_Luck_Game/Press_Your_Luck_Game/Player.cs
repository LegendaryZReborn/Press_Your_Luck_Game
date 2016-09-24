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
            get
            {
                return spins;
            }

            set
            {
                this.spins = value;
            }
        }

        public int Cash
        {
            get
            {
                return cash;
            }
            set
            {
                this.cash = value;
            }
        }
    }
}
