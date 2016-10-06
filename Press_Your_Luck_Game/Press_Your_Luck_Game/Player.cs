/*
Player.cs
By Anthony Enem and Cavaughn Browne
CMPS 4143: Contemporary programming Languages
10/06/2016

This class maintains data for each player such as number of spins, name
and cash money.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Press_Your_Luck_Game
{
    class Player
    {
        //private data
        private int cash;
        private int spins;
        private string name;

        //constructor
        public Player()
        {
            cash = 0;
            spins = 0;
            name = "";
        }

        //Spins property for number of spins
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

        //Cash specifies get and set property for cash
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

        //Name specifies get and set property for name
        public string Name
        {
            get
            {
                //return name value
                return name;
            }
            set
            {
                //set this name to value passed in
                this.name = value;
            }
        }
    }
}
