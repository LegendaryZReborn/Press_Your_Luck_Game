/*
QuestionAnswerForm.cs
By Anthony Enem and Cavaughn Browne
CMPS 4143: Contemporary programming Languages
10/06/2016

This forms handles the questioning of a user. A file containing questions and answers
is first read in and shuffled, then on calling the startQuestioning method, the form
resets to begin questioning for a player.
*/

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
        private int num_questions; //number of questions read from the input file
        private string fileDir = "..\\..\\luckfile.txt";
        private static QAStructure[] qaStructure
             = new QAStructure[MAX_QUESTIONS];
        //number of questions answered since last startQuestioning call
        private static int questionCount = 0;
        //used to index into question and answers array
        private static int questionIndex = 0;
        //number of correct answers since startQuestioning was called 
        private int correctAnswers = 0;
        //maximum questions to ask player at a time
        private const int MAX_QUESTIONS_ASK = 3; 

        public QuestionAnswerForm(/*PressYourLuckGameForm game_form*/)
        {
            InitializeComponent();
            this.ControlBox = false;
            // game_user_form = game_form;
            submitButton.Enabled = false;
            nextButton.Enabled = false;
            answerBox.ReadOnly = true;
            num_questions = readQuestions(fileDir);
            if(num_questions < 1)
            {
                MessageBox.Show("No Questions and answers in file", 
                    "Invalid Input file", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            else
            {
                //shuffle questions to make them random
                shuffleQuestions();
            }
        }


        //Purpose: reads all questions and answers from file and 
        //returns the number of pairs read
        //Requires: none
        //Returns: number of paIrs of questions and answers in the file
        private int readQuestions(string file)
        {
            StreamReader streamReader;

            //try reading from file first
            try
            {
                streamReader = new StreamReader(file);
                int count = 0;
                //read all questions and answers and keep track of number read
                do    
                {
                    ++count;
                    qaStructure[count] = new QAStructure();
                    qaStructure[count].Question = streamReader.ReadLine();
                    qaStructure[count].Answer = streamReader.ReadLine();

                }while(!streamReader.EndOfStream && count < MAX_QUESTIONS);

                //return count of pairs of questions and answers
                return count;
            }
            catch (Exception exception)
            {
                //notify user that file could not be read
                MessageBox.Show("Error" + exception.Data, "Error!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; //exit from program
            }

        }

        //Purpose: resets form to begin questioning player
        //Requires: none
        //Returns: none
        public void startQuestioning(string name)
        {
            //call show method here
            this.Text = "Questions for " + name;
            questionBox.Text = 
                "Press the 'start' button to start answering questions.";
            answerBox.ReadOnly = true;
            startButton.Enabled = true;
            submitButton.Enabled = false;
            nextButton.Enabled = false;
            correctAnswers = 0;
            questionCount = 0;
            answerBox.Clear();
        }


        //Purpose: Disables the start button and calls askQuestions()
        //Requires: object sender, EventArgs
        //Returns: nothing
        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            answerBox.ReadOnly = false;
            nextButton.Text = "Next";
            answerBox.Clear();
            askQuestion();
        }

        //Purpose: Asks the user a questions 
        //Requires: nothing
        //Returns: nothing
        private void askQuestion()
        {
            questionBox.Text = qaStructure[questionIndex].Question;
            answerBox.ReadOnly = false;
            submitButton.Enabled = true;
            nextButton.Enabled = false;
        }

        //Purpose: Accepts the users input answer and evaluates it.
        //Increments players number of spins if their answer is correct.
        //Requires: object sender, EventArgs
        //Returns: nothing
        private void submitButton_Click(object sender, EventArgs e)
        {
            submitButton.Enabled = false;
            userAns = answerBox.Text.ToLower();
            correctAns = qaStructure[questionIndex].Answer.ToLower();

            if (userAns == correctAns)
            {
                verdictLabel.ForeColor = Color.Lime;
                verdictLabel.Text = "CORRECT!";
                //INCREMENT PLAYER SPINS HERE
                ++correctAnswers;

            }
            else
            {
                verdictLabel.ForeColor = Color.Red;
                verdictLabel.Text = "WRONG!";
            }

            if (questionCount == MAX_QUESTIONS_ASK - 1)
                nextButton.Text = "Finish";

            questionIndex = (questionIndex + 1) % num_questions;
            nextButton.Enabled = true;
          

        }

        //Purpose: handles event when next button is clicked
        //Requires: object sender, EventArgs
        //Returns: nothing
        private void nextButton_Click(object sender, EventArgs e)
        {
            answerBox.Clear();
            verdictLabel.Text = "";

            //if text for next button is not finish
            if (nextButton.Text != "Finish")
            {
                //advance to next question index and question count
                questionIndex = (questionIndex + 1) % num_questions;
                ++questionCount;

                //check if qwe have asked the maximum questions allowed
                if (questionCount == MAX_QUESTIONS_ASK)
                {
                    //Done asking questions if maximum number is reached
                    nextButton.Text = "Finish";
                    submitButton.Enabled = false;
                }
                else
                {
                    //else continue asking questions
                    askQuestion();
                }
            }
            else
            {
                //if next button is finish, close form
                this.Close();
            }
        }

        //Purpose: shuffles the questions read from file
        //Requires: none
        //Returns: none
        private void shuffleQuestions()
        {
            //instantiate random object
            Random rand = new Random();
            int rand_index;

            QAStructure temp;

            //loop through number of questions in file
            for(int i = 0; i < num_questions; i++)
            {
                //get a valid random index
                rand_index = rand.Next() % num_questions;

                //swap question in current index with question in random index
                temp = qaStructure[i];
                qaStructure[i] = qaStructure[rand_index];
                qaStructure[rand_index] = temp;
            }
            
        }

        //get property CorrectAnswers
        //Returns: number of correct answers by player so far
        public int CorrectAnswers
        {
            get
            {
                return correctAnswers;
            }

        }

       
    }
}
