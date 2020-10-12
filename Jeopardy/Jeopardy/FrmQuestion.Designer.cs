using System.Drawing;

namespace Jeopardy
{
    partial class FrmQuestion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.lblTeam2 = new System.Windows.Forms.Label();
            this.lblTeam1 = new System.Windows.Forms.Label();
            this.btnShowAnswer = new System.Windows.Forms.Button();
            this.btnIncorrect = new System.Windows.Forms.Button();
            this.btnCorrect = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.grpQuestion = new System.Windows.Forms.GroupBox();
            this.txtTeam = new System.Windows.Forms.TextBox();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.grpButtons.SuspendLayout();
            this.grpQuestion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.lblTeam2);
            this.grpButtons.Controls.Add(this.lblTeam1);
            this.grpButtons.Controls.Add(this.btnShowAnswer);
            this.grpButtons.Controls.Add(this.btnIncorrect);
            this.grpButtons.Controls.Add(this.btnCorrect);
            this.grpButtons.Controls.Add(this.lblTimer);
            this.grpButtons.Location = new System.Drawing.Point(12, 443);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(859, 118);
            this.grpButtons.TabIndex = 4;
            this.grpButtons.TabStop = false;
            // 
            // lblTeam2
            // 
            this.lblTeam2.AutoSize = true;
            this.lblTeam2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeam2.Location = new System.Drawing.Point(678, 16);
            this.lblTeam2.Name = "lblTeam2";
            this.lblTeam2.Size = new System.Drawing.Size(165, 60);
            this.lblTeam2.TabIndex = 5;
            this.lblTeam2.Text = "Team 2:\r\nPress \'p\' to buzz!";
            // 
            // lblTeam1
            // 
            this.lblTeam1.AutoSize = true;
            this.lblTeam1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeam1.Location = new System.Drawing.Point(6, 16);
            this.lblTeam1.Name = "lblTeam1";
            this.lblTeam1.Size = new System.Drawing.Size(165, 60);
            this.lblTeam1.TabIndex = 4;
            this.lblTeam1.Text = "Team 1:\r\nPress \'q\' to buzz!";
            // 
            // btnShowAnswer
            // 
            this.btnShowAnswer.Enabled = false;
            this.btnShowAnswer.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAnswer.Location = new System.Drawing.Point(222, 10);
            this.btnShowAnswer.Name = "btnShowAnswer";
            this.btnShowAnswer.Size = new System.Drawing.Size(426, 32);
            this.btnShowAnswer.TabIndex = 3;
            this.btnShowAnswer.Text = "Show Answer";
            this.btnShowAnswer.UseVisualStyleBackColor = true;
            this.btnShowAnswer.Click += new System.EventHandler(this.btnShowAnswer_Click);
            // 
            // btnIncorrect
            // 
            this.btnIncorrect.Enabled = false;
            this.btnIncorrect.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncorrect.Location = new System.Drawing.Point(436, 42);
            this.btnIncorrect.Name = "btnIncorrect";
            this.btnIncorrect.Size = new System.Drawing.Size(212, 51);
            this.btnIncorrect.TabIndex = 2;
            this.btnIncorrect.Tag = "Subtract";
            this.btnIncorrect.Text = "Incorrect";
            this.btnIncorrect.UseVisualStyleBackColor = true;
            this.btnIncorrect.Click += new System.EventHandler(this.SetAnswer);
            // 
            // btnCorrect
            // 
            this.btnCorrect.Enabled = false;
            this.btnCorrect.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorrect.Location = new System.Drawing.Point(222, 42);
            this.btnCorrect.Name = "btnCorrect";
            this.btnCorrect.Size = new System.Drawing.Size(212, 51);
            this.btnCorrect.TabIndex = 1;
            this.btnCorrect.Tag = "Add";
            this.btnCorrect.Text = "Correct";
            this.btnCorrect.UseVisualStyleBackColor = true;
            this.btnCorrect.Click += new System.EventHandler(this.SetAnswer);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.Tomato;
            this.lblTimer.Location = new System.Drawing.Point(671, 80);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(0, 31);
            this.lblTimer.TabIndex = 0;
            // 
            // grpQuestion
            // 
            this.grpQuestion.Controls.Add(this.txtTeam);
            this.grpQuestion.Controls.Add(this.txtQuestion);
            this.grpQuestion.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuestion.Location = new System.Drawing.Point(12, 12);
            this.grpQuestion.Name = "grpQuestion";
            this.grpQuestion.Size = new System.Drawing.Size(859, 425);
            this.grpQuestion.TabIndex = 3;
            this.grpQuestion.TabStop = false;
            this.grpQuestion.Text = "Question:";
            // 
            // txtTeam
            // 
            this.txtTeam.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtTeam.Enabled = false;
            this.txtTeam.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTeam.ForeColor = System.Drawing.Color.DarkRed;
            this.txtTeam.Location = new System.Drawing.Point(6, 355);
            this.txtTeam.Multiline = true;
            this.txtTeam.Name = "txtTeam";
            this.txtTeam.ReadOnly = true;
            this.txtTeam.Size = new System.Drawing.Size(847, 64);
            this.txtTeam.TabIndex = 1;
            this.txtTeam.TabStop = false;
            this.txtTeam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTeam.BackColor = this.txtTeam.BackColor;
            this.txtTeam.ForeColor = this.txtTeam.ForeColor;
            // 
            // txtQuestion
            // 
            this.txtQuestion.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtQuestion.Enabled = false;
            this.txtQuestion.ReadOnly = true;
            this.txtQuestion.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestion.Location = new System.Drawing.Point(6, 37);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(847, 312);
            this.txtQuestion.TabIndex = 0;
            this.txtQuestion.TabStop = false;
            this.txtQuestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuestion.BackColor = this.txtQuestion.BackColor;
            this.txtQuestion.ForeColor = this.txtTeam.ForeColor;
            // 
            // tmrMain
            // 
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // FrmQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 573);
            this.ControlBox = false;
            this.Controls.Add(this.grpButtons);
            this.Controls.Add(this.grpQuestion);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmQuestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jeopardy!";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmQuestion_KeyDown);
            this.grpButtons.ResumeLayout(false);
            this.grpButtons.PerformLayout();
            this.grpQuestion.ResumeLayout(false);
            this.grpQuestion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.Label lblTeam2;
        private System.Windows.Forms.Label lblTeam1;
        private System.Windows.Forms.Button btnShowAnswer;
        private System.Windows.Forms.Button btnIncorrect;
        private System.Windows.Forms.Button btnCorrect;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.GroupBox grpQuestion;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Timer tmrMain;
        private System.Windows.Forms.TextBox txtTeam;
    }
}