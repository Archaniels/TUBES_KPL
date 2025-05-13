/*

====================================================================================================
Tugas besar konstruksi perangkat lunak dikerjakan berkelompok dengan aturan sebagai berikut: 
====================================================================================================

• Mengkonstruksi sebagian fitur dari perangkat lunak dengan bahasa pemrograman C# dan .NET. 
  Pada tugas besar CLO2 ini, fitur aplikasi diperbolehkan apabila masih terpisah (tidak terhubung 
  antara satu halaman dengan halaman yang lain) dan tidak memiliki tampilan GUI windows Form. 

• Setiap kelompok bertanggung jawab untuk menerapkan hal-hal sebagai berikut: (10% total nilai) 
  1. Mempunyai github atau remote repository yang berisi source code project  
  2. Mengumpulkan laporan tugas besar yang berisi beberapa hal yang akan disebutkan pada bagian selanjutnya 

• Setiap anggota kelompok bertanggung jawab untuk menerapkan hal berikut: (70% total nilai, penilaian dilakukan per individu/mahasiswa) 
  1. Menggunakan github atau version control lainnya untuk konstruksi (dibuktikan dengan adanya branch pribadi dan commit ke branch utama/main/master) 
  2. Melakukan unit testing pada modul/kode yang dibuat 
  3. Melakukan performance testing pada bagian kode (halaman aplikasi) yang dibuat 
  4. Menggunakan teknik konstruksi Defensive programming/design by contract (DbC) 
  5. Menerapkan dua teknik konstruksi di bawah ini pada bagian yang dibuat (masing-masing teknik berikut hanya boleh digunakan oleh maksimal dua mahasiswa): 
     - Automata 
     - Table-driven construction 
     - Parameterization/generics 
     - Runtime configuration 
     - Code reuse/library 
     - API 

• Pada LMS nanti akan disediakan halaman submit tugas besar dokumen laporan tugas besar: 
  1. Deskripsi singkat mengenai aplikasi tugas besar 
  2. Daftar anggota kelompok beserta teknik konstruksi yang dipilih (nilai per individu) 
  3. Unit testing yang dilakukan beserta penjelasan kode singkat untuk unit testing yang dilakukan (nilai per individu) 
  4. Penjelasan dan hasil performance testing (nilai per individu) 
  5. Implementasi design by contract (nilai per individu) 

• Sesi presentasi dan tanya jawab akan dilakukan pada pertemuan ke-11. Pada sesi ini, tanya jawab tiap anggota kelompok akan diberikan pertanyaan 
  yang relevan dengan bagian yang dikerjakan. Setiap anggota akan diminta menjelaskan bagian yang dikerjakan, termasuk source code yang telah dibuat.
  (20% total nilai, penilaian per individu) 



====================================================================================================
                                           PROGRAM
====================================================================================================

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using TUBES_KPL.Authentication.Config;
using TUBES_KPL.Authentication.Model;
using TUBES_KPL.Authentication.Requests;
using TUBES_KPL.Authentication.Services;
using TUBES_KPL.Authentication.Validator;

namespace TUBES_KPL
{
    class Program
    {
        private static AuthenticationService authService;
        private static UserData currentUser = null;

        public static void Main(string[] args)
        {
            // memuat konfigurasi dan inisialisasi service
            var config = AuthenticationConfig.Instance;
            authService = new AuthenticationService(config);

            MainMenu();
        }

        private static void MainMenu()
        {
            bool status = true;

            while (status)
            {
                Console.Clear();
                TampilanHeader();

                if (currentUser == null)
                {
                    GuestMenu();
                }
                else
                {
                    UserMenu();
                }

                Console.Write("\nPilihan Anda: ");
                string choice = Console.ReadLine();

                if (currentUser == null)
                {
                    status = PilihanGuest(choice);
                }
                else
                {
                    status = PilihanUser(choice);
                }

                if (status)
                {
                    Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                    Console.ReadLine();
                }
            }
        }

        private static void TampilanHeader()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║                  Re:Tide                   ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            if (currentUser != null)
            {
                Console.WriteLine($"\nSelamat datang, {currentUser.Username} [{currentUser.Role}]");
            }
            Console.WriteLine();
        }

        private static void GuestMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              Menu Utama - Guest            ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Tentang Aplikasi");
            Console.WriteLine("0. Keluar");
        }

        private static void UserMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              Menu Utama - Admin            ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine("1. Profil Saya");

            if (currentUser.Role == "Admin")
            {
                Console.WriteLine("2. Pengaturan Website");
                Console.WriteLine("3. Manajemen User");
            }
            else
            {
                Console.WriteLine("2. Catalog Produk");
                Console.WriteLine("3. Keranjang Belanja");
            }

            Console.WriteLine("4. Pembayaran");
            Console.WriteLine("5. Donasi");
            Console.WriteLine("9. Logout");
            Console.WriteLine("0. Keluar");
        }

        private static bool PilihanGuest(string choice)
        {
            switch (choice)
            {
                case "1":
                    LoginMenu();
                    return true;

                case "2":
                    RegisterMenu();
                    return true;

                case "3":
                    ShowAbout();
                    return true;

                case "0":
                    Console.WriteLine("\nTerima kasih telah menggunakan aplikasi kami!");
                    return false;

                default:
                    Console.WriteLine("\nPilihan tidak valid!");
                    return true;
            }
        }

        private static bool PilihanUser(string choice)
        {
            switch (choice)
            {
                case "1":
                    ShowProfile();
                    return true;
                case "0":
                    Console.WriteLine($"\nTerima kasih {currentUser.Username}!");
                    return false;

                default:
                    Console.WriteLine("\nPilihan tidak valid!");
                    return true;
            }
        }

        private static void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║                     Login                  ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine("(Ketik 'cancel' untuk membatalkan)\n");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (username.ToLower() == "cancel")
            {
                Console.WriteLine("\nLogin dibatalkan.");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            currentUser = authService.Login(loginRequest);

            if (currentUser != null)
            {
                Console.WriteLine($"\n✓ Login berhasil! Selamat datang {currentUser.Username}");
            }
            else
            {
                Console.WriteLine("\n✗ Login gagal! Username atau password salah.");
            }
        }

        private static void RegisterMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║                   Register                 ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine("(Ketik 'cancel' untuk membatalkan)\n");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (username.ToLower() == "cancel")
            {
                Console.WriteLine("\nRegistrasi dibatalkan.");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Konfirmasi Password: ");
            string confirmPassword = Console.ReadLine();

            if (password != confirmPassword)
            {
                Console.WriteLine("\n Password tidak cocok!");
                return;
            }

            Console.Write("Role (Customer/Admin) [default: Customer]: ");
            string role = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(role))
            {
                role = "Customer";
            }

            var registerRequest = new RegisterRequest
            {
                Username = username,
                Password = password,
                Email = email,
                Role = role
            };

            if (authService.Register(registerRequest))
            {
                Console.WriteLine("\n✓ Registrasi berhasil! Silakan login.");
            }
            else
            {
                Console.WriteLine("\n✗ Registrasi gagal!");
            }
        }

        private static void ShowProfile()
        {
            Console.Clear();
            Console.WriteLine("=== PROFIL SAYA ===");
            Console.WriteLine($"Username : {currentUser.Username}");
            Console.WriteLine($"Email    : {currentUser.Email}");
            Console.WriteLine($"Role     : {currentUser.Role}");
        }

        private static void ShowAbout()
        {
            Console.Clear();
            Console.WriteLine("=== TENTANG APLIKASI ===");
            Console.WriteLine("Re:Tide");
            Console.WriteLine();
            Console.WriteLine("Fitur yang tersedia:");
            Console.WriteLine("1. Authentication (Login/Register)");
            Console.WriteLine("2. Pengaturan Website (Admin only)");
            Console.WriteLine();
            Console.WriteLine("Teknik Konstruksi yang diterapkan:");
            Console.WriteLine("- Automata");
            Console.WriteLine("- Runtime Configuration");
            Console.WriteLine("- Code Reuse/Library");
            Console.WriteLine("- Design by Contract");
        }

        private static void Logout()
        {
            currentUser = null;
            Console.WriteLine("\n✓ Logout berhasil!");
        }
    }
}
