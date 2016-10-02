using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Press_Your_Luck_Game
{
    class Space
    {
        private PictureBox spacePBox;
        private const int numEvents = 4;
        private String[] spaceEvents = new String[numEvents];
        private String sEvent;
        private int eventVal;
        private KeyValuePair<String, int> eveNVal;
        static private Random r = new Random();

        //Parameterized Constructor. Must give it a picture Box.
        public Space(PictureBox p)
        {
            sEvent = "";
            eventVal = 0;

            spaceEvents[0] = "Whammy";
            for (int i = 1; i < numEvents; i++)
                spaceEvents[i] = "Cash";

            spacePBox = p;

            randomizeSpace();

        }

        //Randomizes the spaces event
        public void randomizeSpace()
        {
            String dir = "..\\..\\Images";
            int val = r.Next(0, numEvents);
            sEvent = spaceEvents[val];

            //creates a value between 1 and 20 that are multiples of 5
            //for the Whammy and Cash events
            if(sEvent == "Whammy" || sEvent == "Cash")
                 eventVal = r.Next(1, 5) * 5;

            //Depending on the values for sEvent and eventVal
            //assign the correct image to the spacePBox;
            if(sEvent == "Whammy")
            {
                //Look in the Whammy folder for the right image
                dir += "\\Whammy\\" + ("Whammy" + eventVal + ".png") ;
                spacePBox.Image = Image.FromFile(dir);
            }
            else if(sEvent == "Cash")
            {
                //Look in the Cash folder for the right image
                dir += "\\Cash\\" + ("Cash" + eventVal + ".png");
                spacePBox.Image = Image.FromFile(dir);

            }

            if (sEvent == "Whammy" && eventVal == 20)
                eventVal = 0;

            eveNVal = new KeyValuePair<string, int>(sEvent, eventVal);

        }


        public KeyValuePair<string, int> EventNVal
        {
            get
            {
                return eveNVal;
            }
        }

       
    }
}
