using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TUBES_KPL.PengaturanWebsite.Config
{
    public class PengaturanWebsiteConfig
    {
        public string WebsiteName { get; set; } = "Re:Tide";
        public string WebsiteDescription { get; set; } = "Platform untuk pembelian dan donasi produk daur ulang";
        public bool MaintenanceMode { get; set; } = false;

        private static readonly string ConfigFile = "website_config.json";

        private static PengaturanWebsiteConfig _instance;

        // Singleton pattern untuk runtime configuration
        public static PengaturanWebsiteConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = LoadConfiguration();
                }
                return _instance;
            }
        }

        private static PengaturanWebsiteConfig LoadConfiguration()
        {
            if (File.Exists(ConfigFile))
            {
                try
                {
                    string json = File.ReadAllText(ConfigFile);
                    var config = JsonSerializer.Deserialize<PengaturanWebsiteConfig>(json);
                    if (config != null)
                    {
                        return config;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("[ERROR] Error dalam memuat konfigurasi website: ");
                    Console.WriteLine("\n" + e.Message);
                }
            }

            // Buat konfigurasi default jika file tidak ada atau tidak bisa diload
            var defaultConfig = new PengaturanWebsiteConfig();
            SaveConfiguration(defaultConfig);
            return defaultConfig;
        }

        // simpan configuration ke file
        public static void SaveConfiguration(PengaturanWebsiteConfig config)
        {
            try
            {
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigFile, json);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] Error dalam menyimpan konfigurasi website: ");
                Console.WriteLine("\n" + e.Message);
            }
        }

        // update configuration saat runtime
        public static void UpdateConfiguration(Action<PengaturanWebsiteConfig> update)
        {
            update(Instance);
            SaveConfiguration(Instance);
        }
    }
}