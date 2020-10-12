﻿using System;
using System.Windows.Forms;

namespace Jeopardy
{
    public partial class FrmAnswer : Form
    {
        public FrmAnswer()
        {
            InitializeComponent();
        }

        private void FrmAnswer_Load(object sender, EventArgs e)
        {
            lblAnswer.Text = FrmQuestion.strQuestion;
            txtAnswer.Text = FrmQuestion.strAnswer;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}