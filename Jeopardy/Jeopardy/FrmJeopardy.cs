using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Jeopardy
{
    #region Structures
    
    public struct Categories
    {
        public string strCategoryOne;
        public string strCategoryTwo;
        public string strCategoryThree;
        public string strCategoryFour;

        public Categories(string strCategoryOne_, string strCategoryTwo_, string strCategoryThree_, string strCategoryFour_)
        {
            strCategoryOne = strCategoryOne_;
            strCategoryTwo = strCategoryTwo_;
            strCategoryThree = strCategoryThree_;
            strCategoryFour = strCategoryFour_;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", strCategoryOne, strCategoryTwo, strCategoryThree, strCategoryFour);
        }
    }
    public struct Team
    {
        public string strName;
        public int intPoints;

        public Team(string strName_, int intPoints_)
        {
            strName = strName_;
            intPoints = intPoints_;
        }

        public override string ToString()
        {
            return string.Format("Team {0} currently has {1} points!", strName, intPoints);
        }
    }
    public struct Jeopardy
    {
        public string strCategory;
        public int intPoints;
        public string strQuestion;
        public string strAnswer;

        public Jeopardy(string strCategory_, int intPoints_, string strQuestion_, string strAnswer_)
        {
            strCategory = strCategory_;
            intPoints = intPoints_;
            strQuestion = strQuestion_;
            strAnswer = strAnswer_;
        }
    }
    #endregion
    public partial class FrmJeopardy : Form
    {
        #region Public Properties
        public readonly List<Jeopardy> JeopardyList = new List<Jeopardy>();
        public static Categories CategoryList { get; private set; }
        public static List<Team> Teams { get; private set; } = new List<Team>();
        public static string strAnswer { get; private set; } = "Null";
        public static string strQuestion { get; private set; } = "Null";
        public static int intPointValue { get; private set; } = 0;
        public static bool bolIsRotated { get; private set; } = false;
        #endregion
        #region Class Fields
        public static int intGameCount = 0;
        private static bool bolResettingForm = false;
        private const int intDblJeopardyMultiplier = 2;
        private const string strFRM_INITIALIZE = "FrmInitialize";
        private const string strLOGGER_FILE = @"\logs.txt";
        private const string strLOGGER_TEXT = @"{0}: {1}{2}{0}: {3}{2}{0}: {4}{2}";
        private const string strUSER_ERROR = "An unknown error has occured, please contact Sergeant Sherlund. " +
                                             "Your application will now close.";
        private const string strFILE_ERROR = "Invalid file format, please ensure there's atleast{0}1. Four" +
                                             " Categories{0}2. Atleast 5 Questions per category (100-500)";
        private const string strACCESS_ERROR = "cannot access the file";
        private const string strFILE_ACCESS_ERROR = "Unable to open an already opened file. Please close the Jeopardy " +
                                                    "Question file (.CSV) and reopen the application.";
        private const string strERROR_TITLE = "Error";
        private const string strTOTAL_POINTS = "Total Points: ";
        private const string strDOUBLE_JEOPARDY = "Double Jeopardy Question!";
        private const string strDOUBLE_TITLE = "Double Jeopardy";
        private const string strDOUBLE = "Double";
        private const string strTEAM = "Team: ";
        #endregion

        /// <summary>
        /// This method will be called by the btnStart() method on FrmInitialize(). It will set the current teams to reflect
        /// on the FrmJeopardy(), Search through the given .CSV File and gather all of the Categories, Points, Questions
        /// and Answers into a List<Jeopardy> items.
        /// </summary>
        /// <param name="teamOne">
        /// Defined by the txtFirst on FrmInitialize(), used to set the team on FrmJeopardy.
        /// </param>
        /// <param name="teamTwo">
        /// Defined by the txtSecond on FrmInitialize(), used to set the team on FrmJeopardy.
        /// </param>
        /// <param name="strFileLocation">
        /// Defined by ofdMain() InitializeGame() method inside a FileStream to then
        /// search the contents of the file for Categories, Points, Questions, and Answers
        /// </param>
        public FrmJeopardy(Team teamOne, Team teamTwo, string strFileLocation)
        {
            InitializeComponent();
            SetTeams(teamOne, teamTwo);
            InitializeGame(strFileLocation);
            SetDoubleJep();
            if (!(BolCheckFile(CategoryList.ToString(), JeopardyList)))
            {
                MessageBox.Show(string.Format(strFILE_ERROR, Environment.NewLine),
                    strERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Restart();
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// This method will cycle through each Jeopardy (Structure -> Category, Points, Question, Answer) inside the 
        /// List <Jeopardy> called "JeopardyList", and find a question that matches the point value and category value
        /// from the button click event.
        /// </summary>
        /// <param name="strCategory">
        /// This Parameter allows the method to compare the given (string)Category to the (string)Category inside the 
        /// List of Jeopardy Items.
        /// </param>
        /// <param name="intPoint">This Parameter allows the method to compare the given (int)Points to the (int)Points
        /// inside the list of Jeopardy Items.
        /// </param>
        /// <param name="bolDblJep"> 
        /// If the current button clicked is a double jeopardy event, the bool will be true. This is required because double
        /// jeopardy items are 2x the point value, so IOT look the point value up, we need to set it back to the original
        /// value.
        /// </param>
        /// <returns>Returns a (string)strQuestion which is the question found by comparing category and points inside
        /// the list of Jeopardy structs.
        /// </returns>
        private string GetQuestion(string strCategory, int intPoint, bool bolDblJep, bool bolisRotated)
        {
            List<Jeopardy> TempList = new List<Jeopardy>();
            Random rndMain = new Random();
            string strOutput;
            int intRandomNumber = 0;

            if (bolDblJep)
            {
                intPoint /= intDblJeopardyMultiplier;
            }

            foreach (Jeopardy TempJep in JeopardyList)
            {
                if (TempJep.strCategory == strCategory && TempJep.intPoints == intPoint)
                {
                    TempList.Add(TempJep);
                }
            }

            intRandomNumber = rndMain.Next(0, TempList.Count - 1);
            strAnswer = TempList[intRandomNumber].strAnswer;
            strQuestion = TempList[intRandomNumber].strQuestion;
            strOutput = (bolisRotated) ? strAnswer : strQuestion;

            return strOutput;
        }
        /// <summary>
        /// This method is used to set Three random Jeopardy buttons to be "Double Jeopardy" buttons.
        /// the selected buttons Tag value is set to the string "Double" defined by the constant strDOUBLE.
        /// It will later be read on the method tied to each buttons click event argument.
        /// </summary>
        private void SetDoubleJep()
        {
            //Button ranges for double Jeopardy
            //Assigning numbers correlating to button 1-20
            Random rndButton = new Random();
            int intFirstRange = rndButton.Next(1, 6);
            int intSecondRange = rndButton.Next(7, 12);
            int intThirdRange = rndButton.Next(13, 20);
            int intButtonNumber = 0;

            //Sorting through each control on the form.
            foreach (Control ctrlTemp in Controls)
            {
                if (ctrlTemp is GroupBox)
                {
                    //Each button is held within a groupbox, so we need to go through the controls of each groupbox to find
                    //The buttons.
                    foreach (Control btnTemp in ctrlTemp.Controls)
                    {
                        if (btnTemp is Button)
                        {
                            //Each button is assigned a number (1-20) set as the Tag value.
                            intButtonNumber = Convert.ToInt32(btnTemp.Tag);
                            if (intButtonNumber == intFirstRange || intButtonNumber == intSecondRange ||
                                intButtonNumber == intThirdRange)
                            {
                                btnTemp.Tag = strDOUBLE;
                            }
                        }
                    }
                }

            }
        }
        /// <summary>
        /// This method Initializes the guts of the game. The stream reader will read from the file depicted from the
        /// user on FrmInitialize(). First, it will grab the first line on the file(containing the categories) and store 
        /// it in a Categories Structure. Then, it will scope all the rest of the lines individually and store the values in
        /// a list of Jeopardy structures (Category, Point, Question, Answer).
        /// The '@' represents a comma, and a '~' represents a new line.
        /// </summary>
        /// <param name="strFilename">
        /// Defined by the OpenFileDialog on FrmInitialize().
        /// </param>
        public void InitializeGame(string strFilename)
        {
            FileStream fsMain = null;
            StreamReader srMain = null;
            Jeopardy TempJeopardy;
            string[] strTempCategory;
            string[] strTempJeopardy;

            try
            {
                fsMain = new FileStream(strFilename, FileMode.Open, FileAccess.Read);
                srMain = new StreamReader(fsMain);

                strTempCategory = srMain.ReadLine().Split(',');
                CategoryList = new Categories(strTempCategory[0],
                    strTempCategory[1], strTempCategory[2], strTempCategory[3]);

                grpCategory1.Text = CategoryList.strCategoryOne;
                grpCategory2.Text = CategoryList.strCategoryTwo;
                grpCategory3.Text = CategoryList.strCategoryThree;
                grpCategory4.Text = CategoryList.strCategoryFour;

                while (srMain.Peek() != -1)
                {
                    strTempJeopardy = srMain.ReadLine().Split(',');
                    TempJeopardy = new Jeopardy(strTempJeopardy[0], Convert.ToInt16(strTempJeopardy[1]),
                        strTempJeopardy[2].Replace('@', ',').Replace("~", Environment.NewLine),
                        strTempJeopardy[3].Replace('@', ',').Replace("~", Environment.NewLine));
                    JeopardyList.Add(TempJeopardy);
                }

                bolResettingForm = false;
            }
            catch (Exception exAnyException)
            {
                if (exAnyException.Message.Contains(strACCESS_ERROR))
                {
                    MessageBox.Show(strFILE_ACCESS_ERROR, strERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Restart();
                    Environment.Exit(2);
                }
                else
                {
                    fsMain = new FileStream(Application.StartupPath + strLOGGER_FILE,
                        FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter swLogger = new StreamWriter(fsMain);
                    swLogger.WriteLine(string.Format(strLOGGER_TEXT,
                        DateTime.Now, exAnyException.Message, Environment.NewLine, exAnyException.StackTrace, exAnyException.TargetSite));

                    swLogger.Dispose();
                    fsMain.Dispose();

                    MessageBox.Show(strUSER_ERROR, strERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Restart();
                    Environment.Exit(2);
                }
            }
            finally
            {
                if (srMain != null)
                {
                    srMain.Dispose();
                    fsMain.Dispose();
                }
            }
        }

        private bool BolCheckFile(string strCategories, List<Jeopardy> jepList)
        {
            string[] strCategoryArray = strCategories.Split(',');
            bool bolHasAll = true;

            if (strCategoryArray.Length == 4)
            {
                foreach (string strCategory in strCategoryArray)
                {
                    int intPoint100 = 0, intPoint200 = 0, intPoint300 = 0, intPoint400 = 0, intPoint500 = 0;
                    foreach (Jeopardy jepTemp in jepList)
                    {
                        if (jepTemp.intPoints == 100 && jepTemp.strCategory == strCategory) ++intPoint100;
                        if (jepTemp.intPoints == 200 && jepTemp.strCategory == strCategory) ++intPoint200;
                        if (jepTemp.intPoints == 300 && jepTemp.strCategory == strCategory) ++intPoint300;
                        if (jepTemp.intPoints == 400 && jepTemp.strCategory == strCategory) ++intPoint400;
                        if (jepTemp.intPoints == 500 && jepTemp.strCategory == strCategory) ++intPoint500;
                    }
                    if (intPoint100 < 1 || intPoint200 < 1 || intPoint300 < 1 || intPoint400 < 1 || intPoint500 < 1)
                    {
                        bolHasAll = false;
                        break;
                    }
                }
            }
            else
            {
                bolHasAll = false;
            }

            return bolHasAll;
        }

        public void SetTeams(Team teamOne, Team teamTwo)
        {
            Teams.Add(teamOne);
            Teams.Add(teamTwo);
            grpTeam1.Text = strTEAM + teamOne.strName;
            grpTeam2.Text = strTEAM + teamTwo.strName;

            lblTeam1.Text = strTOTAL_POINTS + teamOne.intPoints;
            lblTeam2.Text = strTOTAL_POINTS + teamTwo.intPoints;
        }

        private void ClickQuestion(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                FrmQuestion frmQuestion = new FrmQuestion();
                Button btnCurrent = (sender as Button);
                string strCategory = "Null";
                bool bolDblJeopardy = false;
                intPointValue = Convert.ToInt16(btnCurrent.Text);

                if (btnCurrent.Name.Contains("Cat1")) strCategory = CategoryList.strCategoryOne;
                if (btnCurrent.Name.Contains("Cat2")) strCategory = CategoryList.strCategoryTwo;
                if (btnCurrent.Name.Contains("Cat3")) strCategory = CategoryList.strCategoryThree;
                if (btnCurrent.Name.Contains("Cat4")) strCategory = CategoryList.strCategoryFour;

                if (btnCurrent.Tag != null && (string)btnCurrent.Tag == "Double")
                {
                    PlaySound.PlaySounds(Properties.Resources.DailyDouble);
                    intPointValue *= intDblJeopardyMultiplier;
                    bolDblJeopardy = true;
                    if(MessageBox.Show(strDOUBLE_JEOPARDY, strDOUBLE_TITLE,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        if (PlaySound.spMain != null) PlaySound.spMain.Stop();
                        btnCurrent.Enabled = false;
                        frmQuestion.InitializeQuestion(GetQuestion(strCategory, intPointValue, bolDblJeopardy, bolIsRotated));
                        frmQuestion.Show();
                    }
                }
                else
                {
                    btnCurrent.Enabled = false;
                    frmQuestion.InitializeQuestion(GetQuestion(strCategory, intPointValue, bolDblJeopardy, bolIsRotated));
                    frmQuestion.Show();
                }

            }
        }
        /// <summary>
        /// Method called from FrmQuestion() to update the point values for each Team after a question is answered.
        /// TODO: Figure out how to update the controls of another clas (Form) that's already initialized without having
        /// to call a public method.
        /// </summary>
        public void UpdateForm()
        {
            lblTeam1.Text = strTOTAL_POINTS + Teams[0].intPoints;
            lblTeam2.Text = strTOTAL_POINTS + Teams[1].intPoints;
        }
        /// <summary>
        /// This method is used to reset the form back to its original state, to allow another game to be run.
        /// </summary>
        public void ResetForm()
        {
            bolResettingForm = true;
            JeopardyList.Clear();
            Teams.Clear();
            intGameCount = 0;
            (Application.OpenForms[strFRM_INITIALIZE] as FrmInitialize).Show();
            Close();
        }
        /// <summary>
        /// If the game is being reset, this method will check before exiting the entire application. This prevents the
        /// program from being completely terminated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmJeopardy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(bolResettingForm))
            {
                Application.Exit();
            }
        }

        private void lblRotate_Click(object sender, EventArgs e)
        {
            if (lblRotate.Text == "Q to A")
            {
                lblRotate.Text = "A to Q";
                bolIsRotated = false;
            }
            else
            {
                lblRotate.Text = "Q to A";
                bolIsRotated = true;
            }


        }

    }
}
