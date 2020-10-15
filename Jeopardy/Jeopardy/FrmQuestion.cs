using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Net.Http.Headers;
using System.IO;
using System.Net;

namespace Jeopardy
{
    public partial class FrmQuestion : Form
    {
        public static string strAnswer { get; private set; } = "Null";
        public static string strQuestion { get; private set; } = "Null";
        private const string strBUZZED = "Team {0} Hit the Buzzer!";
        private const string strTIMER = "Time: ";
        private const string strTIMERS_UP = "Times up!";
        private const string strFRM_JEOPARDY = "FrmJeopardy";
        private const string strFRM_ANSWER = "FrmAnswer";
        private const string strWINNING_TEAM = "{0} won the game with {1} points!" + 
                                               "  Would you like to start a new game?";
        private const string strWINNING_TITLE = "Winner!";
        private const string strSUBTRACT = "Subtract";
        private const string strADD = "Add";
        private const decimal decTIME_SECONDS = 30.0m;
        private static decimal decCurrent_Time = 0.0m;
        private static int intCurrentTeam = 2;

        //Team1 = 0
        //Team2 = 1
        //Null = 2

        public FrmQuestion()
        {
            InitializeComponent();
        }

        private void Buzzer(int intTeam)
        {
            tmrMain.Stop();
            PlaySound.PlaySounds(Properties.Resources.Buzzed);
            intCurrentTeam = intTeam;
            txtTeam.Text = string.Format(strBUZZED, FrmJeopardy.Teams[intTeam].strName);
            btnShowAnswer.Enabled = true;
            btnShowAnswer.Focus();
        }

        private void SetAnswer(object sender, EventArgs e)
        {
            int intPointValue = 0;
            int intWinners = 0;
            Team tempTeam;
            Button btnCurrent = (sender as Button);

            if ((string)btnCurrent.Tag == strADD)
            {
                intPointValue = FrmJeopardy.Teams[intCurrentTeam].intPoints + FrmJeopardy.intPointValue;
                PlaySound.PlaySounds(Properties.Resources.Correct);
            }
            else if ((string)btnCurrent.Tag == strSUBTRACT)
            {
                intPointValue = FrmJeopardy.Teams[intCurrentTeam].intPoints - FrmJeopardy.intPointValue;
                PlaySound.PlaySounds(Properties.Resources.Wrong);
            }

            tempTeam = new Team(FrmJeopardy.Teams[intCurrentTeam].strName, intPointValue);
            FrmJeopardy.Teams[intCurrentTeam] = tempTeam;
            (Application.OpenForms[strFRM_JEOPARDY] as FrmJeopardy).UpdateForm();
            Close();

            if (Application.OpenForms[strFRM_ANSWER] != null)
            {
                (Application.OpenForms[strFRM_ANSWER] as FrmAnswer).Close();
            }
            if (GameIsOver())
            {
                intWinners = (FrmJeopardy.Teams[0].intPoints > FrmJeopardy.Teams[1].intPoints ? 0 : 1);
                string strMessage = String.Format(strWINNING_TEAM, FrmJeopardy.Teams[intWinners].strName,
                                FrmJeopardy.Teams[intWinners].intPoints);
                if (MessageBox.Show(strMessage, strWINNING_TITLE,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    (Application.OpenForms[strFRM_JEOPARDY] as FrmJeopardy).ResetForm();
                }
            }
        }

        private bool GameIsOver()
        {
            bool bolIsOver = false;

            if (++FrmJeopardy.intGameCount == 20) //Total buttons
            {
                bolIsOver = true;
            }
            return bolIsOver;
        }

        private void CheckKeyPress(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Q))
            {
                Buzzer(0);
            }
            else if ((e.KeyCode == Keys.P))
            {
                Buzzer(1);
            }
        }

        public void InitializeQuestion(string strQuestion)
        {
            txtQuestion.Text = strQuestion;
        }

        private void btnNoGuess_Click(object sender, EventArgs e)
        {
            int intWinners = 0;

            Close();
            if (Application.OpenForms[strFRM_ANSWER] != null)
            {
                (Application.OpenForms[strFRM_ANSWER] as FrmAnswer).Close();
            }
            if (GameIsOver())
            {
                intWinners = (FrmJeopardy.Teams[0].intPoints > FrmJeopardy.Teams[1].intPoints ? 0 : 1);
                string strMessage = String.Format(strWINNING_TEAM, FrmJeopardy.Teams[intWinners].strName,
                                FrmJeopardy.Teams[intWinners].intPoints);
                if (MessageBox.Show(strMessage, strWINNING_TITLE,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    (Application.OpenForms[strFRM_JEOPARDY] as FrmJeopardy).ResetForm();
                }
            }
        }
        private void FrmQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Q))
            {
                Buzzer(0);
            }
            else if ((e.KeyCode == Keys.P))
            {
                Buzzer(1);
            }
        }
        private void tmrMain_Tick(object sender, EventArgs e)
        {
            decCurrent_Time -= .1m;
            lblTimer.Text = strTIMER + decCurrent_Time.ToString();

            if (decCurrent_Time == 0)
            {
                tmrMain.Stop();
                PlaySound.PlaySounds(Properties.Resources.TimesUp);
                MessageBox.Show(strTIMERS_UP, strTIMERS_UP, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnShowAnswer.Enabled = true;
                btnNoGuess.Enabled = true;
                btnShowAnswer.Focus();
            }
        }
        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            FrmAnswer frmAnswer;
            tmrMain.Stop();
            strAnswer = FrmJeopardy.strAnswer;
            strQuestion = FrmJeopardy.strQuestion;
            if (intCurrentTeam != 2)
            {
                btnCorrect.Enabled = true;
                btnIncorrect.Enabled = true;
                btnCorrect.Focus();
            }
            frmAnswer = new FrmAnswer();
            frmAnswer.Show();
        }
        private void FrmQuestion_Load(object sender, EventArgs e)
        {
            decCurrent_Time = decTIME_SECONDS;
            PlaySound.PlaySounds(Properties.Resources.JeopardyMain);
            intCurrentTeam = 2;
            txtQuestion.Focus();
            tmrMain.Start();
        }

    }
}
