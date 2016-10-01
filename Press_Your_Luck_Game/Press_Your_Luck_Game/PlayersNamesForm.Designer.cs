namespace Press_Your_Luck_Game
{
    partial class PlayersNamesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.player1Name_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.player2Name_textBox = new System.Windows.Forms.TextBox();
            this.playersNames_done_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player 1 Name";
            // 
            // player1Name_textBox
            // 
            this.player1Name_textBox.Location = new System.Drawing.Point(60, 73);
            this.player1Name_textBox.Name = "player1Name_textBox";
            this.player1Name_textBox.Size = new System.Drawing.Size(165, 20);
            this.player1Name_textBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Player 2 Name";
            // 
            // player2Name_textBox
            // 
            this.player2Name_textBox.Location = new System.Drawing.Point(60, 155);
            this.player2Name_textBox.Name = "player2Name_textBox";
            this.player2Name_textBox.Size = new System.Drawing.Size(165, 20);
            this.player2Name_textBox.TabIndex = 3;
            // 
            // playersNames_done_button
            // 
            this.playersNames_done_button.Location = new System.Drawing.Point(101, 200);
            this.playersNames_done_button.Name = "playersNames_done_button";
            this.playersNames_done_button.Size = new System.Drawing.Size(75, 23);
            this.playersNames_done_button.TabIndex = 4;
            this.playersNames_done_button.Text = "Start Game!";
            this.playersNames_done_button.UseVisualStyleBackColor = true;
            this.playersNames_done_button.Click += new System.EventHandler(this.playersNames_done_button_Click);
            // 
            // PlayersNamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.playersNames_done_button);
            this.Controls.Add(this.player2Name_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.player1Name_textBox);
            this.Controls.Add(this.label1);
            this.Name = "PlayersNamesForm";
            this.Text = "Enter Players\' Names";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox player1Name_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox player2Name_textBox;
        private System.Windows.Forms.Button playersNames_done_button;
    }
}