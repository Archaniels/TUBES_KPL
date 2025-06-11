using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_ReTide
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            // This runs when the form loads
            // You can leave it empty if nothing needs to happen
        }

        private void artikelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Artikel artikel = new Artikel();
            artikel.ShowDialog();
            this.Show();
        }

        private void produkButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Produk produk = new Produk();
            produk.ShowDialog();
            this.Show();
        }

        private void donasiButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Donasi donasi = new Donasi();
            donasi.ShowDialog();
            this.Show();
        }
    }
}
