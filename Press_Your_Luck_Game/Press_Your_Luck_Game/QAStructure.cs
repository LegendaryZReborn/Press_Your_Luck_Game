/*
QAStructure.cs
By Anthony Enem and Cavaughn Browne
CMPS 4143: Contemporary programming Languages
10/06/2016

This class maintains data for a question and its corresponding answer.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Press_Your_Luck_Game
{
    class QAStructure
    {
        //private data
        private String question;
        private String answer;

        //constructor
        public QAStructure()
        {
            question = "None";
            answer = "None";
        }

        //parameterized constructor
        public QAStructure(String question, String answer)
        {
            this.question = question;
            this.answer = answer;
        }

        //specifies get and get properties for question data
        public String Question
        {
            get
            {
                return question;
            }
            set
            {
                this.question = value;
            }
        }

        //specifies get and get properties for answer data
        public String Answer
        {
            get
            {
                return answer;
            }
            set
            {
                this.answer = value;
            }
        }
    }
}
