namespace Press_Your_Luck_Game
{
    partial class QuestionAnswerForm
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
            this.answerBox = new System.Windows.Forms.TextBox();
            this.questionBox = new System.Windows.Forms.RichTextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.verdictLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // answerBox
            // 
            this.answerBox.Location = new System.Drawing.Point(104, 120);
            this.answerBox.Name = "answerBox";
            this.answerBox.Size = new System.Drawing.Size(241, 20);
            this.answerBox.TabIndex = 0;
            this.answerBox.Text = "Give Answer Here!";
            this.answerBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // questionBox
            // 
            this.questionBox.BackColor = System.Drawing.SystemColors.Window;
            this.questionBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.questionBox.Font = new System.Drawing.Font("Modern No. 20", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionBox.Location = new System.Drawing.Point(23, 37);
            this.questionBox.Name = "questionBox";
            this.questionBox.ReadOnly = true;
            this.questionBox.Size = new System.Drawing.Size(322, 62);
            this.questionBox.TabIndex = 2;
            this.questionBox.Text = "Press the \'start\' button to start answering questions.";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(23, 120);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(212, 157);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 4;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // verdictLabel
            // 
            this.verdictLabel.Font = new System.Drawing.Font("Impact", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verdictLabel.ForeColor = System.Drawing.Color.Lime;
            this.verdictLabel.Location = new System.Drawing.Point(35, 157);
            this.verdictLabel.Name = "verdictLabel";
            this.verdictLabel.Size = new System.Drawing.Size(171, 45);
            this.verdictLabel.TabIndex = 5;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(294, 157);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // QuestionAnswerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.verdictLabel);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.questionBox);
            this.Controls.Add(this.answerBox);
            this.Name = "QuestionAnswerForm";
            this.Text = "QuestionAnswerForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QuestionAnswerForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox answerBox;
        private System.Windows.Forms.RichTextBox questionBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label verdictLabel;
        private System.Windows.Forms.Button nextButton;
    }
}