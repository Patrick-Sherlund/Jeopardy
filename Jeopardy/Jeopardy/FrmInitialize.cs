/*----------------------------------------------------------------------
 * Programmer: Sherlund, Patrick R.
 * Date: October 11, 2020
 * Revision: 1.0
 * Description: Jeopardy will run along the same concepts as the real "Jeopardy!" game. It allows the user to input a .CSV
 * file containing Categories, Point values, Questions and Answers that will be referenced throughout the entire game.
 * Two teams are allowed, and both will compete to win!
 * ----------------------------------------------------------------------
 * Revision: 1.1
 * Date: October 12, 2020
 * Description: Added an input file check, verify that there's 4 categories, and each category has atleast 5 questions corresponding to
 * the 5 point values (100, 200, 300, 400, 500).
 * ----------------------------------------------------------------------
 * BUGS: NONE
 * Date: 11 October, 2020
 * ----------------------------------------------------------------------
 * TODO: 
 * 1.  Add sound tracks for the "Jeopardy!" Theme song to play in the background, Sound Track for the Buzzer, 
 * sound track for the cound down after buzzer is hit, sound track for correct / incorrect answer.
 * ----------------------------------------------------------------------
 */
using System;
using System.Windows.Forms;

namespace Jeopardy
{
    public partial class FrmInitialize : Form
    {
        private const string strFILE_FILTER = "(Comma Deliminated) *.csv|*.csv";
        private const string strINPUT_ERROR = "Please enter a name for each team!";
        private const string strERROR = "Error";
        private static string strFileName = "questions.csv";
        private FrmJeopardy FrmJeopardy;
        private Team teamOne;
        private Team teamTwo;

        public FrmInitialize()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdMain = new OpenFileDialog();
            ofdMain.FileName = strFileName;
            ofdMain.Filter = strFILE_FILTER;
            ofdMain.InitialDirectory = Application.StartupPath;

            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                strFileName = ofdMain.FileName;
                btnStart.Enabled = true;
                btnStart.Focus();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(txtFirst.Text)) && !(string.IsNullOrWhiteSpace(txtSecond.Text)))
            {
                teamOne = new Team(txtFirst.Text, 0);
                teamTwo = new Team(txtSecond.Text, 0);
                txtFirst.Clear();
                txtSecond.Clear();
                txtFirst.Focus();
                btnStart.Enabled = false;
                FrmJeopardy = new FrmJeopardy(teamOne, teamTwo, strFileName);
                FrmJeopardy.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(strINPUT_ERROR, strERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
