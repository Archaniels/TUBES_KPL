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
            // Remove or comment out this line
            // label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            // Remove or comment out all these label1 properties
            // label1.AutoSize = true;
            // label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            // label1.ForeColor = SystemColors.WindowText;
            // label1.Location = new Point(620, 139);
            // label1.Name = "label1";
            // label1.Size = new Size(117, 35);
            // label1.TabIndex = 0;
            // label1.Text = "Re:Tide";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1262, 673);
            // Remove this line that adds the label to the form
            // Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Remove this line
        // private Label label1;
    }
}
