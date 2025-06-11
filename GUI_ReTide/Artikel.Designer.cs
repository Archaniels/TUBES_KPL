namespace GUI_ReTide
{
    partial class Artikel
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
            label1 = new Label();
            backButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.WindowText;
            label1.Location = new Point(334, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(124, 41);
            label1.TabIndex = 2;
            label1.Text = "Artikel";
            label1.Click += label1_Click;
            // 
            // backButton
            // 
            backButton.ForeColor = SystemColors.InfoText;
            backButton.Location = new Point(12, 403);
            backButton.Name = "backButton";
            backButton.Size = new Size(99, 35);
            backButton.TabIndex = 7;
            backButton.Text = "Kembali";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += backButton_Click;
            // 
            // Artikel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(backButton);
            Controls.Add(label1);
            Name = "Artikel";
            Text = "Artikel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button backButton;
    }
}