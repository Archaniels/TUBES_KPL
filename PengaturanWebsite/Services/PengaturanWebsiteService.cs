using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUBES_KPL.PengaturanWebsite.Config;

namespace TUBES_KPL.PengaturanWebsite.Services
{
    public class PengaturanWebsiteService
    {
        private void DisplayHeader()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║           Pengaturan Website               ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              MENU PENGATURAN               ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine("1. Pengaturan Umum");
            Console.WriteLine("2. Pengaturan Tampilan");
            Console.WriteLine("3. Pengaturan Konten");
            Console.WriteLine("4. Pengaturan Performa");
            Console.WriteLine("0. Kembali");
            Console.WriteLine();
        }

        

        private void SaveSettings()
        {
            Console.WriteLine("Menyimpan pengaturan...");
            PengaturanWebsiteConfig.SaveConfiguration(_config);
            Console.WriteLine("Pengaturan berhasil disimpan!");
            System.Threading.Thread.Sleep(1500); // Pause sebentar agar pesan dapat dibaca
        }
    }
}
