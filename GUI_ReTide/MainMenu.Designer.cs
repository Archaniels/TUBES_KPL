namespace GUI_ReTide
{
    partial class MainMenu
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
            artikelButton = new Button();
            produkButton = new Button();
            donasiButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.WindowText;
            label1.Location = new Point(315, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(140, 41);
            label1.TabIndex = 1;
            label1.Text = "Re:Tide";
            // 
            // artikelButton
            // 
            artikelButton.Font = new Font("Segoe UI", 10F);
            artikelButton.ForeColor = SystemColors.InfoText;
            artikelButton.Location = new Point(326, 53);
            artikelButton.Name = "artikelButton";
            artikelButton.Size = new Size(119, 33);
            artikelButton.TabIndex = 7;
            artikelButton.Text = "Artikel";
            artikelButton.UseVisualStyleBackColor = true;
            artikelButton.Click += artikelButton_Click;
            // 
            // produkButton
            // 
            produkButton.Font = new Font("Segoe UI", 10F);
            produkButton.ForeColor = SystemColors.InfoText;
            produkButton.Location = new Point(326, 92);
            produkButton.Name = "produkButton";
            produkButton.Size = new Size(119, 33);
            produkButton.TabIndex = 8;
            produkButton.Text = "Produk";
            produkButton.UseVisualStyleBackColor = true;
            produkButton.Click += produkButton_Click;
            // 
            // donasiButton
            // 
            donasiButton.Font = new Font("Segoe UI", 10F);
            donasiButton.ForeColor = SystemColors.InfoText;
            donasiButton.Location = new Point(326, 131);
            donasiButton.Name = "donasiButton";
            donasiButton.Size = new Size(119, 33);
            donasiButton.TabIndex = 9;
            donasiButton.Text = "Donasi";
            donasiButton.UseVisualStyleBackColor = true;
            donasiButton.Click += donasiButton_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(donasiButton);
            Controls.Add(produkButton);
            Controls.Add(artikelButton);
            Controls.Add(label1);
            Name = "MainMenu";
            Text = "Main Menu";
            Load += MainMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button artikelButton;
        private Button produkButton;
        private Button donasiButton;
    }
}