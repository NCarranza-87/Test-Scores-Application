using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestScoreApp
{
    public partial class frmTestScore : Form
    {
        /********************************************************************
         * For this project, create a from that accpets score from the user, 
         * displays the total, count and average of the scores, and display 
         * a dialog box that lists the scores.
         *******************************************************************/

        //ArrayList to store test scores
        List<int> testScores = new List<int>();

        public frmTestScore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When application is loaded; txtScoreTotal, txtScoreCount
        /// and txtAverage are invoking the ReadOnly method and if set
        /// to true. It will only display the corresponding values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTestScore_Load(object sender, EventArgs e)
        {
            txtScoreTotal.ReadOnly = true;
            txtScoreCount.ReadOnly = true;
            txtAverage.ReadOnly = true;
        }

        /// <summary>
        /// when user clicks on the Add button, a text
        /// score is add to the ArrayList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            bool isEmpty = true;

            if (IsPresent(txtScore))
            {
                MessageBox.Show("Your score textbox is empty.", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
                txtScore.Focus();
            }

            if (IsDataValid())
            {
                //code to check if input data is valid.
                testScores.Add(Convert.ToInt32(txtScore.Text));
                MessageBox.Show("Test score have been added.");

                displayScoreTotal();
                displayScoreCount();
                displayAverage();
            }

            if(string.IsNullOrWhiteSpace(txtScore.Text))
            {
                MessageBox.Show("Please enter a test score.", "Enter test score!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtScore.Clear();
                txtScore.Focus();
            }

            txtScore.Clear();
            txtScore.Focus();
        }

        /// <summary>
        /// check to see if the use has enter any
        /// data in the test score textbox
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool IsPresent(TextBox box)
        {
            if(txtScore.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// check if the user data is a valid input
        /// </summary>
        /// <returns> return true if scoreInput is  withing range, return false if 
        /// scoreInput is not within range</returns>
        private bool IsDataValid()
        {
            //get the user input
            string scoreInput = txtScore.Text;

            bool IsInputValid = false;

            //check is input valid
            if (isDecimal(scoreInput))
            {
                if(IsWithinRange(Convert.ToDecimal(scoreInput), 0, 100))
                {
                    IsInputValid = true;
                }
                else
                {
                    DialogResult error = MessageBox.Show("Invalid input!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if(error == DialogResult.OK)
                    {
                        txtScore.Clear();
                        txtScore.Focus();
                    }
                }
            }
            return IsInputValid;
        }

        private bool IsWithinRange(decimal testNum, decimal minVal, decimal maxVal)
        {
            if (testNum >= minVal && testNum <= maxVal)
            {
                return true;
            }
            return false;
        }

        private bool isDecimal(string inputVal)
        {
            if(decimal.TryParse(inputVal, out decimal number))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// When Display Scores button is clicked on a
        /// message box will displays all of the test scores
        /// in the ArrayList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisplayScores_Click(object sender, EventArgs e)
        {
            if (testScores.Count == 0)
            {
                //display an message box that shows
                //an error that states ther are no
                //elements within the ArrayList
                MessageBox.Show("There are currently no test score!", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string displayTestScoreList = string.Join("\n", testScores);
                MessageBox.Show(displayTestScoreList, "Test Scores:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            
        }

        /// <summary>
        /// computes and displays the testScore 
        /// list as the user enters a test score
        /// </summary>
        private void displayAverage()
        {
            int sum = 0;
            int average = 0;

            for (int i = 0; i < testScores.Count; i++)
            {
                sum += testScores[i];
                average = sum / testScores.Count;
            }

            txtAverage.Text = average.ToString();
        }

        /// <summary>
        /// displays the number of elements 
        /// within the list
        /// </summary>
        private void displayScoreCount()
        {
            int scoreCount = 0;

            for (int i = 0; i < testScores.Count; i++)
            {
                scoreCount = testScores.Count;
            }

            txtScoreCount.Text = scoreCount.ToString();
        }

        /// <summary>
        /// display and computes the total of
        /// all the scores within the list
        /// </summary>
        private void displayScoreTotal()
        {
            int scoreTotal = 0;

            for (int i = 0; i < testScores.Count(); i++)
            {
                scoreTotal += testScores[i];
            }

            //print the score total
            txtScoreTotal.Text = scoreTotal.ToString();
        }

        /// <summary>
        /// When Clear Scores button is clicked on the text scores
        /// in the ArrayList is clear, then a message box will prompt
        /// the user stating that score are cleared from ArrayList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearScores_Click(object sender, EventArgs e)
        {
            testScores.Clear();
            txtScoreTotal.Clear();
            txtScoreCount.Clear();
            txtAverage.Clear();
            MessageBox.Show("Test scores have been cleared from list!");
        }

        /// <summary>
        /// When Exit button is clicked on; BtnExit_Click
        /// will calls CloseFormApplication()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            CloseFormApplication();
        }

        /// <summary>
        /// When BtnExit_Click calls CloseFOrmApplication()
        /// the user will be prompt to close the application.
        /// If res equals Yes, the application will close.
        /// </summary>
        private void CloseFormApplication()
        {
            DialogResult res = MessageBox.Show("Do you wish to quit?", "Close Appilcation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
