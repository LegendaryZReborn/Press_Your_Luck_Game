using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Press_Your_Luck_Game
{
    public partial class QuestionAnswerForm : Form
    {
        private string userAns = "";
        private string correctAns = "";
        private const int MAX_QUESTIONS = 100;
        private int num_questions;
        private string fileDir = "..\\..\\luckfile.txt";
        private QAStructure[] qaStructure = new QAStructure[MAX_QUESTIONS];
        private static int questionCount = 0;

        public QuestionAnswerForm()
        {
            InitializeComponent();
            submitButton.Enabled = false;
            nextButton.Enabled = false;
            answerBox.ReadOnly = true;
            num_questions = readQuestions(fileDir);
            shuffleQuestions();
        }


        //Purpose: reads all questions and answers from file and returns the number of pairs read
        //Requires: none
        //Returns: number of paIrs of questions and answers in the file
        private int readQuestions(string file)
        {
            StreamReader streamReader;

            //try reading from file first
            try
            {
                streamReader = new StreamReader(file);
                int count;
                //read all questions and answers and keep track of number read
                for (count = 0; !streamReader.EndOfStream && count < MAX_QUESTIONS; ++count)
                {
                    qaStructure[count] = new QAStructure();
                    qaStructure[count].Question = streamReader.ReadLine();
                    qaStructure[count].Answer = streamReader.ReadLine();
                }

                //return count of pairs of questions and answers
                return count;
            }
            catch (Exception exception)
            {
                //notify user that file could not be read

                return -1; //exit from program
            }

        }


        //Purpose: Disables the start button and calls askQuestions()
        //Requires: object sender, EventArgs
        //Returns: nothing
        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            answerBox.ReadOnly = false;
            answerBox.Clear();
            askQuestion();

        }

        //Purpose: Asks the user a questions 
        //Requires: nothing
        //Returns: nothing
        private void askQuestion()
        {
            questionBox.Text = qaStructure[questionCount].Question;
            answerBox.ReadOnly = false;
            submitButton.Enabled = true;
        }
        
        //Purpose: Accepts the users input answer and evaluates it.
        //Increments players number of spins if their answer is correct.
        //Requires: object sender, EventArgs
        //Returns: nothing
        private void submitButton_Click(object sender, EventArgs e)
        {
            submitButton.Enabled = false;
            userAns = answerBox.Text.ToLower();
            correctAns = qaStructure[questionCount].Answer.ToLower();

            if (userAns == correctAns)
            {
                verdictLabel.ForeColor = Color.Lime;
                verdictLabel.Text = "CORRECT!";
                //INCREMENT PLAYER SPINS HERE


            }
            else
            {
                verdictLabel.ForeColor = Color.Red;
                verdictLabel.Text = "WRONG!";
            }

            if ((questionCount + 1) % 3 == 0)
                nextButton.Text = "Finish";

            questionCount++;

            //CHANGE THIS LATER; PERHAPS WHEN ALL QUESTIONS RUN OUT THE GAME IS DONE
            //OR PLAYERS CAN PICK NUMBER OF ROUNDS IDK
            
            if (questionCount >= num_questions) //only check number of questions in input file, not the whole array
                questionCount = 0;
            
            nextButton.Enabled = true;
        }

        //Purpose: Clears the answer and verdict boxes; calls the 
        //askQuestion function
        //Requires: object sender, EventArgs
        //Returns: nothing
        private void nextButton_Click(object sender, EventArgs e)
        {
            answerBox.Clear();
            verdictLabel.Text = "";

            if(nextButton.Text != "Finish")
               askQuestion();
            else
               this.Close();
        }

        public void startQuestioning()
        {
            //call show method here
            this.Show();

            //we can do other stuff here if we want, like calculate number of spins for user and return to
            //the game form
        }

        //Purpose: generate MAX_DISPLAY_Q rnadom questions
        //Requires: none
        //Returns: none
        private void shuffleQuestions()
        {
            Random rand = new Random();
            int rand_index;
            QAStructure temp;
            for(int i = 0; i < num_questions; i++)
            {
                rand_index = rand.Next() % num_questions;

                temp = qaStructure[i];
                qaStructure[i] = qaStructure[rand_index];
                qaStructure[rand_index] = temp;
            }
            
        }
    }
}
