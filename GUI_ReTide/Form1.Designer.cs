namespace GUI_ReTide
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            usernameBox = new TextBox();
            label3 = new Label();
            passwordBox = new TextBox();
            label4 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.WindowText;
            label1.Location = new Point(440, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(140, 41);
            label1.TabIndex = 0;
            label1.Text = "Re:Tide";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.WindowText;
            label2.Location = new Point(459, 50);
            label2.Name = "label2";
            label2.Size = new Size(92, 41);
            label2.TabIndex = 1;
            label2.Text = "Login";
            label2.Click += label2_Click;
            // 
            // usernameBox
            // 
            usernameBox.Location = new Point(387, 145);
            usernameBox.Name = "usernameBox";
            usernameBox.Size = new Size(231, 31);
            usernameBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.HighlightText;
            label3.ForeColor = SystemColors.InfoText;
            label3.Location = new Point(459, 117);
            label3.Name = "label3";
            label3.Size = new Size(91, 25);
            label3.TabIndex = 3;
            label3.Text = "Username";
            label3.Click += label3_Click;
            // 
            // passwordBox
            // 
            passwordBox.Location = new Point(387, 218);
            passwordBox.Name = "passwordBox";
            passwordBox.Size = new Size(231, 31);
            passwordBox.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.HighlightText;
            label4.ForeColor = SystemColors.InfoText;
            label4.Location = new Point(459, 190);
            label4.Name = "label4";
            label4.Size = new Size(87, 25);
            label4.TabIndex = 5;
            label4.Text = "Password";
            label4.Click += label4_Click;
            // 
            // button1
            // 
            button1.ForeColor = SystemColors.InfoText;
            button1.Location = new Point(452, 270);
            button1.Name = "button1";
            button1.Size = new Size(99, 35);
            button1.TabIndex = 6;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1578, 841);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(passwordBox);
            Controls.Add(label3);
            Controls.Add(usernameBox);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.Window;
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox usernameBox;
        private Label label3;
        private TextBox passwordBox;
        private Label label4;
        private Button button1;
    }
}
