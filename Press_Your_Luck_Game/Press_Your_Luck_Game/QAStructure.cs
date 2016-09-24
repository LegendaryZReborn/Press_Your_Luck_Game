//QAStructure.cs file
//Class for Question and answer

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

        //getter and setter for questio
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

        //getter and setter for answer
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
