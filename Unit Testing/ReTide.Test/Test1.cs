using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using TUBES_KPL;
using TUBES_KPL.Authentication.Config;
using TUBES_KPL.Authentication.Model;
using TUBES_KPL.Authentication.Requests;
using TUBES_KPL.Authentication.Services;
using TUBES_KPL.PengaturanWebsite.Config;
using static TUBES_KPL.PengaturanWebsite.Model.PengaturanWebsiteModel;

namespace ReTide.Test
{
    [TestClass]
    public sealed class AuthenticationTest
    {
        private AuthenticationService _authService;
        private readonly string _testUserFile = "test_users.json";
        private readonly string _origUserFile = "users.json";
        private readonly string _configFile = "authentication_config.json";
        private readonly string _origConfigFile = "authentication_config.json.backup";

        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists(_origUserFile))
            {
                File.Copy(_origUserFile, _origUserFile + ".backup", true);
                File.Delete(_origUserFile);
            }

            if (File.Exists(_configFile))
            {
                File.Copy(_configFile, _origConfigFile, true);
                File.Delete(_configFile);
            }

            var config = new AuthenticationConfig
            {
                MinPasswordLength = 8,
                MinUsernameLength = 3,
                RequireEmailValidation = false
            };

            _authService = new AuthenticationService(config);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_origUserFile + ".backup"))
            {
                File.Copy(_origUserFile + ".backup", _origUserFile, true);
                File.Delete(_origUserFile + ".backup");
            }

            if (File.Exists(_origConfigFile))
            {
                File.Copy(_origConfigFile, _configFile, true);
                File.Delete(_origConfigFile);
            }

            if (File.Exists(_testUserFile))
            {
                File.Delete(_testUserFile);
            }
        }

        [TestMethod]
        public void Register_DataValid()
        {
            var request = new RegisterRequest
            {
                Username = "testuser",
                Password = "Password123",
                Email = "test@gmail.com",
                Role = "Customer"
            };

            bool result = _authService.Register(request);

            Assert.IsTrue(result, "Registrasi harusnya lulus karena valid data");
        }

        [TestMethod]
        public void Register_UsernameDuplikat()
        {
            var request1 = new RegisterRequest
            {
                Username = "testuser",
                Password = "Password123",
                Email = "test@gmail.com",
                Role = "Customer"
            };

            var request2 = new RegisterRequest
            {
                Username = "testuser",
                Password = "Password456",
                Email = "another@gmail.com",
                Role = "Customer"
            };

            _authService.Register(request1);
            bool result = _authService.Register(request2);

            Assert.IsFalse(result, "Registrasi harusnya gagal karena username duplikat");
        }

        [TestMethod]
        public void Register_RoleTidakValid()
        {
            var request = new RegisterRequest
            {
                Username = "testuser",
                Password = "Password123",
                Email = "test@gmail.com",
                Role = "Developer"
            };

            bool result = _authService.Register(request);

            Assert.IsFalse(result, "Registrasi harusnya gagal karena role tidak valid.");
        }

        [TestMethod]
        public void Register_PasswordTidakValidKarenaPendek()
        {
            var request = new RegisterRequest
            {
                Username = "testuser",
                Password = "wowow",
                Email = "test@gmail.com",
                Role = "Customer"
            };

            bool result = _authService.Register(request);

            Assert.IsFalse(result, "Registrasi harusnya gagal karena password tidak valid.");
        }

        [TestMethod]
        public void Register_UsernameTidakValidKarenaPendek()
        {
            var request = new RegisterRequest
            {
                Username = "a",
                Password = "Password123",
                Email = "test@gmail.com",
                Role = "Customer"
            };

            bool result = _authService.Register(request);

            Assert.IsFalse(result, "Registrasi harusnya gagal karena username tidak valid.");
        }

        [TestMethod]
        public void Login_DataBenar()
        {
            var registerRequest = new RegisterRequest
            {
                Username = "logintest",
                Password = "Password123",
                Email = "logintest@gmail.com",
                Role = "Customer"
            };

            _authService.Register(registerRequest);

            var loginRequest = new LoginRequest
            {
                Username = "logintest",
                Password = "Password123"
            };

            UserData user = _authService.Login(loginRequest);

            Assert.IsNotNull(user, "Login harus lulus karena data benar.");
            Assert.AreEqual("logintest", user.Username, "Username harusnya sama");
            Assert.AreEqual("Customer", user.Role, "Role harusnya sama");
        }

        [TestMethod]
        public void Login_PasswordSalah()
        {
            var registerRequest = new RegisterRequest
            {
                Username = "passwordtest",
                Password = "Password123",
                Email = "passwordtest@gmail.com",
                Role = "Customer"
            };

            _authService.Register(registerRequest);

            var loginRequest = new LoginRequest
            {
                Username = "passwordtest",
                Password = "salah"
            };

            UserData user = _authService.Login(loginRequest);

            Assert.IsNull(user, "Login harus gagal karena password salah");
        }

        [TestMethod]
        public void Login_UserTidakAda()
        {
            var loginRequest = new LoginRequest
            {
                Username = "tidakada",
                Password = "Password123"
            };

            UserData user = _authService.Login(loginRequest);

            Assert.IsNull(user, "Login harus gagal karena user tidak ada.");
        }
    }

    [TestClass]
    public class PengaturanWebsiteTests
    {
        private readonly string _testConfigFile = "test_website_config.json";

        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists(_testConfigFile))
            {
                File.Delete(_testConfigFile);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_testConfigFile))
            {
                File.Delete(_testConfigFile);
            }
        }

        [TestMethod]
        public void DefaultConfig_NilaiDefaultYangBenar()
        {
            var config = new PengaturanWebsiteConfig();

            Assert.AreEqual("Re:Tide", config.WebsiteName);
            Assert.AreEqual("Platform untuk pembelian dan donasi produk daur ulang", config.WebsiteDescription);
            Assert.IsFalse(config.MaintenanceMode);
        }

        [TestMethod]
        public void SaveAndLoadConfig_ValueTetap()
        {
            var originalConfig = new PengaturanWebsiteConfig
            {
                WebsiteName = "Test Website",
                WebsiteDescription = "Test description",
                MaintenanceMode = true
            };

            string json = JsonSerializer.Serialize(originalConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_testConfigFile, json);

            string readJson = File.ReadAllText(_testConfigFile);
            var loadedConfig = JsonSerializer.Deserialize<PengaturanWebsiteConfig>(readJson);

            Assert.IsNotNull(loadedConfig);
            Assert.AreEqual("Test Website", loadedConfig.WebsiteName);
            Assert.AreEqual("Test description", loadedConfig.WebsiteDescription);
            Assert.IsTrue(loadedConfig.MaintenanceMode);
        }

        [TestMethod]
        public void UpdateConfiguration_MengubahValuesDenganBenar()
        {
            var config = new PengaturanWebsiteConfig();

            config.WebsiteName = "Update nama";
            config.WebsiteDescription = "Update deskripsi";
            config.MaintenanceMode = true;

            Assert.AreEqual("Update nama", config.WebsiteName);
            Assert.AreEqual("Update deskripsi", config.WebsiteDescription);
            Assert.IsTrue(config.MaintenanceMode);
        }

        [TestMethod]
        public void PengaturanWebsiteAutomata_TransisiDenganBenar()
        {
            var automata = new PengaturanWebsiteAutomata();

            Assert.AreEqual(PengaturanWebsiteState.MainMenu, automata.CurrentState);

            Assert.IsTrue(automata.MoveNext(PengaturanWebsiteEvent.SelectGeneral));
            Assert.AreEqual(PengaturanWebsiteState.GeneralSettings, automata.CurrentState);

            Assert.IsTrue(automata.MoveNext(PengaturanWebsiteEvent.Save));
            Assert.AreEqual(PengaturanWebsiteState.Saving, automata.CurrentState);

            Assert.IsTrue(automata.MoveNext(PengaturanWebsiteEvent.Back));
            Assert.AreEqual(PengaturanWebsiteState.MainMenu, automata.CurrentState);
        }

        [TestMethod]
        public void PengaturanWebsiteAutomata_TransisiInvalid()
        {
            var automata = new PengaturanWebsiteAutomata();

            Assert.IsTrue(automata.MoveNext(PengaturanWebsiteEvent.Quit));
            Assert.AreEqual(PengaturanWebsiteState.Exit, automata.CurrentState);

            bool result = automata.MoveNext(PengaturanWebsiteEvent.Back);

            Assert.IsFalse(result, "Harusnya false karena transisi invalid.");
            Assert.AreEqual(PengaturanWebsiteState.Exit, automata.CurrentState, "State harus tetap setelah transisi invalid.");
        }

        [TestMethod]
        public void PengaturanWebsiteAutomata_ResetSecaraNormal()
        {
            var automata = new PengaturanWebsiteAutomata();

            automata.MoveNext(PengaturanWebsiteEvent.SelectGeneral);
            automata.MoveNext(PengaturanWebsiteEvent.Save);

            Assert.AreEqual(PengaturanWebsiteState.Saving, automata.CurrentState);

            automata.Reset();

            Assert.AreEqual(PengaturanWebsiteState.MainMenu, automata.CurrentState);
        }
        }
    }
