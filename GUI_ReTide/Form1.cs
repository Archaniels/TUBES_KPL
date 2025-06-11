using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using TUBES_KPL.API.Models;
using TUBES_KPL.API.Services;
using TUBES_KPL.Authentication.Config;
using TUBES_KPL.Authentication.Model;
using TUBES_KPL.Authentication.Requests;
using TUBES_KPL.Authentication.Services;
using TUBES_KPL.PengaturanWebsite.Config;
using TUBES_KPL.PengaturanWebsite.Services;

namespace GUI_ReTide
{
    public partial class Form1 : Form
    {
        // Services
        private AuthenticationService authService;
        private PengaturanWebsiteService websiteService;
        private IGenericService<Donation> donationService;
        private IGenericService<Product> productService;
        private GenericService<Pembelian> purchaseService;
        private List<Artikel> artikelList;

        // Current user
        private UserData currentUser = null;

        // File paths
        private const string DonationFilePath = "donations.json";
        private const string ProductFilePath = "products.json";
        private const string PurchaseFilePath = "purchases.json";

        // UI Components for different panels
        private Panel loginPanel;
        private Panel registerPanel;
        private Panel mainPanel;
        private Panel profilePanel;
        private Panel productPanel;
        private Panel donationPanel;
        private Panel adminPanel;
        private Panel artikelPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeServices();
            SetupUI();
            ShowLoginPanel();
        }

        private void InitializeServices()
        {
            // Initialize authentication service
            var config = AuthenticationConfig.Instance;
            authService = new AuthenticationService(config);

            // Initialize website service
            websiteService = new PengaturanWebsiteService();

            // Initialize donation service
            var donationList = File.Exists(DonationFilePath)
                ? JsonSerializer.Deserialize<List<Donation>>(File.ReadAllText(DonationFilePath)) ?? new List<Donation>()
                : new List<Donation>();
            donationService = new GenericService<Donation>(donationList);

            // Initialize product service
            var productsList = Product.LoadFromFile(ProductFilePath);
            productService = new GenericService<Product>(productsList);

            // Initialize purchase service
            var purchaseList = File.Exists(PurchaseFilePath)
                ? Pembelian.LoadFromFile(PurchaseFilePath)
                : new List<Pembelian>();
            purchaseService = new GenericService<Pembelian>(purchaseList);
            
            // Initialize article list
            artikelList = new List<Artikel>
            {
                new Artikel { Id = 1, Judul = "Pengelolaan Sampah", Isi = "Cara mengelola sampah plastik dengan efektif untuk mengurangi dampak lingkungan. Sampah plastik merupakan salah satu masalah lingkungan terbesar yang dihadapi dunia saat ini. Dengan menerapkan prinsip 3R (Reduce, Reuse, Recycle), kita dapat berkontribusi dalam mengurangi jumlah sampah plastik yang berakhir di lautan dan tempat pembuangan akhir.", TanggalPublikasi = DateTime.Now.AddDays(-5) },
                new Artikel { Id = 2, Judul = "Konservasi Air", Isi = "Tips hemat air untuk rumah tangga dan bagaimana setiap orang dapat berkontribusi dalam konservasi air. Air adalah sumber daya berharga yang semakin langka di banyak bagian dunia. Dengan mengadopsi kebiasaan hemat air seperti memperbaiki kebocoran, menggunakan shower timer, dan mengumpulkan air hujan, kita dapat membantu melestarikan sumber daya air untuk generasi mendatang.", TanggalPublikasi = DateTime.Now.AddDays(-2) },
                new Artikel { Id = 3, Judul = "Energi Terbarukan", Isi = "Perkembangan terbaru dalam teknologi energi terbarukan dan bagaimana hal ini dapat membantu mengurangi ketergantungan pada bahan bakar fosil. Energi matahari, angin, dan air menawarkan alternatif berkelanjutan untuk kebutuhan energi kita. Dengan investasi yang tepat dan dukungan kebijakan, energi terbarukan dapat menjadi solusi utama dalam mengatasi perubahan iklim.", TanggalPublikasi = DateTime.Now.AddDays(-1) }
            };
        }

        private void SetupUI()
        {
            // Set form properties
            this.Text = PengaturanWebsiteConfig.Instance.WebsiteName;
            this.Size = new Size(1000, 600);

            // Create panels
            CreateLoginPanel();
            CreateRegisterPanel();
            CreateMainPanel();
            CreateProfilePanel();
            CreateProductPanel();
            CreateDonationPanel();
            CreateAdminPanel();
            CreateArtikelPanel();
        }

        #region Panel Creation Methods

        private void CreateArtikelPanel()
        {
            artikelPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "Articles",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(400, 20)
            };

            var artikelListView = new ListView
            {
                Name = "lvArtikels",
                Location = new Point(50, 70),
                Size = new Size(900, 300),
                View = View.Details,
                FullRowSelect = true,
                MultiSelect = false
            };
            artikelListView.Columns.Add("ID", 50);
            artikelListView.Columns.Add("Title", 300);
            artikelListView.Columns.Add("Publication Date", 150);
            artikelListView.SelectedIndexChanged += ArtikelListView_SelectedIndexChanged;

            var artikelDetailPanel = new Panel
            {
                Name = "pnlArtikelDetail",
                Location = new Point(50, 380),
                Size = new Size(900, 150),
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            var artikelTitleLabel = new Label
            {
                Name = "lblArtikelTitle",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            var artikelDateLabel = new Label
            {
                Name = "lblArtikelDate",
                AutoSize = true,
                Location = new Point(10, 35)
            };

            var artikelContentLabel = new Label
            {
                Name = "lblArtikelContent",
                AutoSize = true,
                MaximumSize = new Size(880, 0),
                Location = new Point(10, 60)
            };

            artikelDetailPanel.Controls.AddRange(new Control[] { artikelTitleLabel, artikelDateLabel, artikelContentLabel });

            var backButton = new Button
            {
                Text = "Back to Main Menu",
                Location = new Point(400, 540),
                Size = new Size(200, 40)
            };
            backButton.Click += (s, e) => ShowMainPanel();

            artikelPanel.Controls.AddRange(new Control[] { titleLabel, artikelListView, artikelDetailPanel, backButton });
            this.Controls.Add(artikelPanel);
        }

        private void CreateLoginPanel()
        {
            loginPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "Login to " + PengaturanWebsiteConfig.Instance.WebsiteName,
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(350, 100)
            };

            var usernameLabel = new Label
            {
                Text = "Username:",
                AutoSize = true,
                Location = new Point(300, 170)
            };

            var usernameTextBox = new TextBox
            {
                Name = "txtLoginUsername",
                Location = new Point(400, 170),
                Size = new Size(200, 25)
            };

            var passwordLabel = new Label
            {
                Text = "Password:",
                AutoSize = true,
                Location = new Point(300, 210)
            };

            var passwordTextBox = new TextBox
            {
                Name = "txtLoginPassword",
                Location = new Point(400, 210),
                Size = new Size(200, 25),
                PasswordChar = '*'
            };

            var loginButton = new Button
            {
                Text = "Login",
                Location = new Point(400, 260),
                Size = new Size(100, 30)
            };
            loginButton.Click += LoginButton_Click;

            var registerLinkLabel = new LinkLabel
            {
                Text = "Don't have an account? Register here",
                AutoSize = true,
                Location = new Point(350, 310)
            };
            registerLinkLabel.Click += (s, e) => ShowRegisterPanel();

            loginPanel.Controls.AddRange(new Control[] { titleLabel, usernameLabel, usernameTextBox, passwordLabel, passwordTextBox, loginButton, registerLinkLabel });
            this.Controls.Add(loginPanel);
        }

        private void CreateRegisterPanel()
        {
            registerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "Register for " + PengaturanWebsiteConfig.Instance.WebsiteName,
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(320, 80)
            };

            var usernameLabel = new Label
            {
                Text = "Username:",
                AutoSize = true,
                Location = new Point(300, 150)
            };

            var usernameTextBox = new TextBox
            {
                Name = "txtRegUsername",
                Location = new Point(400, 150),
                Size = new Size(200, 25)
            };

            var emailLabel = new Label
            {
                Text = "Email:",
                AutoSize = true,
                Location = new Point(300, 190)
            };

            var emailTextBox = new TextBox
            {
                Name = "txtRegEmail",
                Location = new Point(400, 190),
                Size = new Size(200, 25)
            };

            var passwordLabel = new Label
            {
                Text = "Password:",
                AutoSize = true,
                Location = new Point(300, 230)
            };

            var passwordTextBox = new TextBox
            {
                Name = "txtRegPassword",
                Location = new Point(400, 230),
                Size = new Size(200, 25),
                PasswordChar = '*'
            };

            var confirmPasswordLabel = new Label
            {
                Text = "Confirm Password:",
                AutoSize = true,
                Location = new Point(260, 270)
            };

            var confirmPasswordTextBox = new TextBox
            {
                Name = "txtRegConfirmPassword",
                Location = new Point(400, 270),
                Size = new Size(200, 25),
                PasswordChar = '*'
            };

            var registerButton = new Button
            {
                Text = "Register",
                Location = new Point(400, 320),
                Size = new Size(100, 30)
            };
            registerButton.Click += RegisterButton_Click;

            var loginLinkLabel = new LinkLabel
            {
                Text = "Already have an account? Login here",
                AutoSize = true,
                Location = new Point(350, 370)
            };
            loginLinkLabel.Click += (s, e) => ShowLoginPanel();

            registerPanel.Controls.AddRange(new Control[] { titleLabel, usernameLabel, usernameTextBox, emailLabel, emailTextBox, passwordLabel, passwordTextBox, confirmPasswordLabel, confirmPasswordTextBox, registerButton, loginLinkLabel });
            this.Controls.Add(registerPanel);
        }

        private void CreateMainPanel()
        {
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var welcomeLabel = new Label
            {
                Name = "lblWelcome",
                Text = "Welcome to " + PengaturanWebsiteConfig.Instance.WebsiteName,
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(350, 50)
            };

            var descriptionLabel = new Label
            {
                Text = PengaturanWebsiteConfig.Instance.WebsiteDescription,
                AutoSize = true,
                Location = new Point(350, 90)
            };

            var profileButton = new Button
            {
                Text = "My Profile",
                Location = new Point(350, 150),
                Size = new Size(200, 40)
            };
            profileButton.Click += (s, e) => ShowProfilePanel();

            var productsButton = new Button
            {
                Text = "Product Catalog",
                Location = new Point(350, 210),
                Size = new Size(200, 40)
            };
            productsButton.Click += (s, e) => ShowProductPanel();

            var donationButton = new Button
            {
                Text = "Donations",
                Location = new Point(350, 270),
                Size = new Size(200, 40)
            };
            donationButton.Click += (s, e) => ShowDonationPanel();

            var artikelButton = new Button
            {
                Text = "Articles",
                Location = new Point(350, 330),
                Size = new Size(200, 40)
            };
            artikelButton.Click += (s, e) => ShowArtikelPanel();

            var adminButton = new Button
            {
                Name = "btnAdmin",
                Text = "Admin Panel",
                Location = new Point(350, 390),
                Size = new Size(200, 40),
                Visible = false
            };
            adminButton.Click += (s, e) => ShowAdminPanel();

            var logoutButton = new Button
            {
                Text = "Logout",
                Location = new Point(350, 450),
                Size = new Size(200, 40)
            };
            logoutButton.Click += LogoutButton_Click;

            mainPanel.Controls.AddRange(new Control[] { welcomeLabel, descriptionLabel, profileButton, productsButton, donationButton, artikelButton, adminButton, logoutButton });
            this.Controls.Add(mainPanel);
        }

        private void CreateProfilePanel()
        {
            profilePanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "My Profile",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(400, 50)
            };

            var usernameLabel = new Label
            {
                Text = "Username:",
                AutoSize = true,
                Location = new Point(300, 120)
            };

            var usernameValueLabel = new Label
            {
                Name = "lblProfileUsername",
                AutoSize = true,
                Location = new Point(400, 120)
            };

            var emailLabel = new Label
            {
                Text = "Email:",
                AutoSize = true,
                Location = new Point(300, 160)
            };

            var emailValueLabel = new Label
            {
                Name = "lblProfileEmail",
                AutoSize = true,
                Location = new Point(400, 160)
            };

            var roleLabel = new Label
            {
                Text = "Role:",
                AutoSize = true,
                Location = new Point(300, 200)
            };

            var roleValueLabel = new Label
            {
                Name = "lblProfileRole",
                AutoSize = true,
                Location = new Point(400, 200)
            };

            var backButton = new Button
            {
                Text = "Back to Main Menu",
                Location = new Point(350, 300),
                Size = new Size(200, 40)
            };
            backButton.Click += (s, e) => ShowMainPanel();

            profilePanel.Controls.AddRange(new Control[] { titleLabel, usernameLabel, usernameValueLabel, emailLabel, emailValueLabel, roleLabel, roleValueLabel, backButton });
            this.Controls.Add(profilePanel);
        }

        private void CreateProductPanel()
        {
            productPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "Product Catalog",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(400, 50)
            };

            var productListView = new ListView
            {
                Name = "lvProducts",
                Location = new Point(200, 100),
                Size = new Size(600, 300),
                View = View.Details,
                FullRowSelect = true
            };
            productListView.Columns.Add("ID", 50);
            productListView.Columns.Add("Product Name", 200);
            productListView.Columns.Add("Price", 100);
            productListView.Columns.Add("Stock", 100);

            var quantityLabel = new Label
            {
                Text = "Quantity:",
                AutoSize = true,
                Location = new Point(300, 420)
            };

            var quantityNumericUpDown = new NumericUpDown
            {
                Name = "nudQuantity",
                Location = new Point(370, 420),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };

            var buyButton = new Button
            {
                Text = "Buy Product",
                Location = new Point(470, 420),
                Size = new Size(120, 30)
            };
            buyButton.Click += BuyButton_Click;

            var backButton = new Button
            {
                Text = "Back to Main Menu",
                Location = new Point(350, 470),
                Size = new Size(200, 40)
            };
            backButton.Click += (s, e) => ShowMainPanel();

            productPanel.Controls.AddRange(new Control[] { titleLabel, productListView, quantityLabel, quantityNumericUpDown, buyButton, backButton });
            this.Controls.Add(productPanel);
        }

        private void CreateDonationPanel()
        {
            donationPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "Donations",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(400, 50)
            };

            var donationListView = new ListView
            {
                Name = "lvDonations",
                Location = new Point(200, 100),
                Size = new Size(600, 200),
                View = View.Details,
                FullRowSelect = true
            };
            donationListView.Columns.Add("ID", 50);
            donationListView.Columns.Add("Donor Name", 200);
            donationListView.Columns.Add("Amount", 150);

            var donorNameLabel = new Label
            {
                Text = "Donor Name:",
                AutoSize = true,
                Location = new Point(300, 320)
            };

            var donorNameTextBox = new TextBox
            {
                Name = "txtDonorName",
                Location = new Point(400, 320),
                Size = new Size(200, 25)
            };

            var amountLabel = new Label
            {
                Text = "Amount:",
                AutoSize = true,
                Location = new Point(300, 360)
            };

            var amountNumericUpDown = new NumericUpDown
            {
                Name = "nudDonationAmount",
                Location = new Point(400, 360),
                Size = new Size(200, 25),
                Minimum = 1,
                Maximum = 1000000,
                DecimalPlaces = 2,
                Value = 10
            };

            var donateButton = new Button
            {
                Text = "Make Donation",
                Location = new Point(400, 400),
                Size = new Size(120, 30)
            };
            donateButton.Click += DonateButton_Click;

            var backButton = new Button
            {
                Text = "Back to Main Menu",
                Location = new Point(350, 450),
                Size = new Size(200, 40)
            };
            backButton.Click += (s, e) => ShowMainPanel();

            donationPanel.Controls.AddRange(new Control[] { titleLabel, donationListView, donorNameLabel, donorNameTextBox, amountLabel, amountNumericUpDown, donateButton, backButton });
            this.Controls.Add(donationPanel);
        }

        private void CreateAdminPanel()
        {
            adminPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                ForeColor = SystemColors.ControlText
            };

            var titleLabel = new Label
            {
                Text = "Admin Panel",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(400, 20)
            };

            // Create TabControl for different admin functions
            var adminTabControl = new TabControl
            {
                Name = "tabAdminControl",
                Location = new Point(50, 60),
                Size = new Size(900, 430)
            };

            // Website Settings Tab
            var websiteSettingsTab = new TabPage("Website Settings");
            CreateWebsiteSettingsTab(websiteSettingsTab);
            
            // Purchase History Tab
            var purchaseHistoryTab = new TabPage("Purchase History");
            CreatePurchaseHistoryTab(purchaseHistoryTab);
            
            // Product Management Tab
            var productManagementTab = new TabPage("Product Management");
            CreateProductManagementTab(productManagementTab);

            adminTabControl.TabPages.Add(websiteSettingsTab);
            adminTabControl.TabPages.Add(purchaseHistoryTab);
            adminTabControl.TabPages.Add(productManagementTab);

            var backButton = new Button
            {
                Text = "Back to Main Menu",
                Location = new Point(400, 510),
                Size = new Size(200, 40)
            };
            backButton.Click += (s, e) => ShowMainPanel();

            adminPanel.Controls.AddRange(new Control[] { titleLabel, adminTabControl, backButton });
            this.Controls.Add(adminPanel);
        }

        private void CreateWebsiteSettingsTab(TabPage tab)
        {
            // Create TabControl for website settings categories
            var settingsTabControl = new TabControl
            {
                Name = "tabSettingsControl",
                Location = new Point(20, 20),
                Size = new Size(850, 350),
                Dock = DockStyle.Fill,
                Padding = new Point(20, 10)
            };

            // General Settings Tab
            var generalSettingsTab = new TabPage("General Settings");
            CreateGeneralSettingsPanel(generalSettingsTab);

            // Content Settings Tab
            var contentSettingsTab = new TabPage("Content Settings");
            CreateContentSettingsPanel(contentSettingsTab);

            settingsTabControl.TabPages.Add(generalSettingsTab);
            settingsTabControl.TabPages.Add(contentSettingsTab);

            tab.Controls.Add(settingsTabControl);
        }

        private void CreateGeneralSettingsPanel(TabPage tab)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var websiteNameLabel = new Label
            {
                Text = "Website Name:",
                AutoSize = true,
                Location = new Point(30, 40)
            };

            var websiteNameTextBox = new TextBox
            {
                Name = "txtWebsiteName",
                Location = new Point(180, 40),
                Size = new Size(300, 25),
                Text = PengaturanWebsiteConfig.Instance.WebsiteName
            };

            var maintenanceLabel = new Label
            {
                Text = "Maintenance Mode:",
                AutoSize = true,
                Location = new Point(30, 80)
            };

            var maintenanceCheckBox = new CheckBox
            {
                Name = "chkMaintenance",
                Location = new Point(180, 80),
                Checked = PengaturanWebsiteConfig.Instance.MaintenanceMode
            };

            var saveGeneralButton = new Button
            {
                Name = "btnSaveGeneral",
                Text = "Save General Settings",
                Location = new Point(180, 130),
                Size = new Size(200, 30)
            };
            saveGeneralButton.Click += SaveGeneralSettingsButton_Click;

            panel.Controls.AddRange(new Control[] { websiteNameLabel, websiteNameTextBox, maintenanceLabel, maintenanceCheckBox, saveGeneralButton });
            tab.Controls.Add(panel);
        }

        private void CreateContentSettingsPanel(TabPage tab)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var websiteDescLabel = new Label
            {
                Text = "Website Description:",
                AutoSize = true,
                Location = new Point(30, 40)
            };

            var websiteDescTextBox = new TextBox
            {
                Name = "txtWebsiteDesc",
                Location = new Point(180, 40),
                Size = new Size(400, 80),
                Multiline = true,
                Text = PengaturanWebsiteConfig.Instance.WebsiteDescription
            };

            var saveContentButton = new Button
            {
                Name = "btnSaveContent",
                Text = "Save Content Settings",
                Location = new Point(180, 150),
                Size = new Size(200, 30)
            };
            saveContentButton.Click += SaveContentSettingsButton_Click;

            panel.Controls.AddRange(new Control[] { websiteDescLabel, websiteDescTextBox, saveContentButton });
            tab.Controls.Add(panel);
        }

        private void CreatePurchaseHistoryTab(TabPage tab)
        {
            var purchaseHistoryLabel = new Label
            {
                Text = "Purchase History:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            var purchaseListView = new ListView
            {
                Name = "lvPurchases",
                Location = new Point(20, 50),
                Size = new Size(850, 300),
                View = View.Details,
                FullRowSelect = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(20, 50, 20, 20)
            };
            purchaseListView.Columns.Add("ID", 50);
            purchaseListView.Columns.Add("Customer", 100);
            purchaseListView.Columns.Add("Product", 150);
            purchaseListView.Columns.Add("Quantity", 70);
            purchaseListView.Columns.Add("Total", 100);
            purchaseListView.Columns.Add("Date", 120);

            tab.Controls.AddRange(new Control[] { purchaseHistoryLabel, purchaseListView });
        }

        #endregion

        #region Panel Visibility Methods

        private void ShowLoginPanel()
        {
            HideAllPanels();
            loginPanel.Visible = true;
        }

        private void ShowRegisterPanel()
        {
            HideAllPanels();
            registerPanel.Visible = true;
        }

        private void ShowMainPanel()
        {
            HideAllPanels();
            UpdateMainPanel();
            mainPanel.Visible = true;
        }

        private void ShowProfilePanel()
        {
            if (currentUser == null) return;

            HideAllPanels();
            UpdateProfilePanel();
            profilePanel.Visible = true;
        }

        private void ShowProductPanel()
        {
            HideAllPanels();
            LoadProducts();
            productPanel.Visible = true;
        }

        private void ShowDonationPanel()
        {
            HideAllPanels();
            LoadDonations();
            donationPanel.Visible = true;
        }

        private void ShowAdminPanel()
        {
            if (currentUser == null || currentUser.Role != "Admin") return;

            HideAllPanels();
            LoadPurchaseHistory();
            LoadAdminProducts();
            adminPanel.Visible = true;
        }

        private void HideAllPanels()
        {
            loginPanel.Visible = false;
            registerPanel.Visible = false;
            mainPanel.Visible = false;
            profilePanel.Visible = false;
            productPanel.Visible = false;
            donationPanel.Visible = false;
            adminPanel.Visible = false;
            artikelPanel.Visible = false;
        }

        private void ShowArtikelPanel()
        {
            HideAllPanels();
            LoadArtikels();
            artikelPanel.Visible = true;
        }

        private void LoadArtikels()
        {
            var artikelListView = artikelPanel.Controls.Find("lvArtikels", true).FirstOrDefault() as ListView;
            if (artikelListView == null) return;

            artikelListView.Items.Clear();

            foreach (var artikel in artikelList)
            {
                var item = new ListViewItem(artikel.Id.ToString());
                item.SubItems.Add(artikel.Judul);
                item.SubItems.Add(artikel.TanggalPublikasi.ToString("dd-MM-yyyy"));
                item.Tag = artikel;
                artikelListView.Items.Add(item);
            }

            // Hide detail panel when loading articles
            var detailPanel = artikelPanel.Controls.Find("pnlArtikelDetail", true).FirstOrDefault() as Panel;
            if (detailPanel != null)
            {
                detailPanel.Visible = false;
            }
        }

        private void ArtikelListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItems.Count == 0) return;

            var selectedItem = listView.SelectedItems[0];
            var artikel = selectedItem.Tag as Artikel;

            var detailPanel = artikelPanel.Controls.Find("pnlArtikelDetail", true).FirstOrDefault() as Panel;
            var titleLabel = detailPanel.Controls.Find("lblArtikelTitle", true).FirstOrDefault() as Label;
            var dateLabel = detailPanel.Controls.Find("lblArtikelDate", true).FirstOrDefault() as Label;
            var contentLabel = detailPanel.Controls.Find("lblArtikelContent", true).FirstOrDefault() as Label;

            if (detailPanel != null && titleLabel != null && dateLabel != null && contentLabel != null)
            {
                titleLabel.Text = artikel.Judul;
                dateLabel.Text = $"Published: {artikel.TanggalPublikasi:dd-MM-yyyy}";
                contentLabel.Text = artikel.Isi;
                detailPanel.Visible = true;
            }
        }

        #endregion

        #region Update UI Methods

        private void UpdateMainPanel()
        {
            if (currentUser == null) return;

            var welcomeLabel = mainPanel.Controls.Find("lblWelcome", true).FirstOrDefault() as Label;
            if (welcomeLabel != null)
            {
                welcomeLabel.Text = $"Welcome, {currentUser.Username}!";
            }

            var adminButton = mainPanel.Controls.Find("btnAdmin", true).FirstOrDefault() as Button;
            if (adminButton != null)
            {
                adminButton.Visible = currentUser.Role == "Admin";
            }
        }

        private void UpdateProfilePanel()
        {
            if (currentUser == null) return;

            var usernameLabel = profilePanel.Controls.Find("lblProfileUsername", true).FirstOrDefault() as Label;
            var emailLabel = profilePanel.Controls.Find("lblProfileEmail", true).FirstOrDefault() as Label;
            var roleLabel = profilePanel.Controls.Find("lblProfileRole", true).FirstOrDefault() as Label;

            if (usernameLabel != null) usernameLabel.Text = currentUser.Username;
            if (emailLabel != null) emailLabel.Text = currentUser.Email;
            if (roleLabel != null) roleLabel.Text = currentUser.Role;
        }

        private void LoadProducts()
        {
            var productListView = productPanel.Controls.Find("lvProducts", true).FirstOrDefault() as ListView;
            if (productListView == null) return;

            productListView.Items.Clear();
            var products = productService.GetAll();

            foreach (var product in products)
            {
                var item = new ListViewItem(product.Id.ToString());
                item.SubItems.Add(product.ProductName);
                item.SubItems.Add(product.Price.ToString("C"));
                item.SubItems.Add(product.Stock.ToString());
                item.Tag = product;

                productListView.Items.Add(item);
            }
        }

        private void LoadDonations()
        {
            var donationListView = donationPanel.Controls.Find("lvDonations", true).FirstOrDefault() as ListView;
            if (donationListView == null) return;

            donationListView.Items.Clear();
            var donations = donationService.GetAll();

            foreach (var donation in donations)
            {
                var item = new ListViewItem(donation.Id.ToString());
                item.SubItems.Add(donation.DonorName);
                item.SubItems.Add(donation.Amount.ToString("C"));
                item.Tag = donation;

                donationListView.Items.Add(item);
            }
        }

        private void CreateProductManagementTab(TabPage tab)
        {
            // Main panel for the product management tab
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Product list view
            var productListView = new ListView
            {
                Name = "lvAdminProducts",
                Location = new Point(20, 20),
                Size = new Size(500, 300),
                View = View.Details,
                FullRowSelect = true,
                MultiSelect = false
            };
            productListView.Columns.Add("ID", 50);
            productListView.Columns.Add("Product Name", 200);
            productListView.Columns.Add("Price", 100);
            productListView.Columns.Add("Stock", 100);
            productListView.SelectedIndexChanged += ProductListView_SelectedIndexChanged;

            // Product details group
            var detailsGroup = new GroupBox
            {
                Text = "Product Details",
                Location = new Point(540, 20),
                Size = new Size(300, 300)
            };

            // Product ID
            var idLabel = new Label
            {
                Text = "ID:",
                AutoSize = true,
                Location = new Point(20, 30)
            };

            var idTextBox = new TextBox
            {
                Name = "txtProductId",
                Location = new Point(120, 30),
                Size = new Size(150, 25),
                ReadOnly = true
            };

            // Product Name
            var nameLabel = new Label
            {
                Text = "Product Name:",
                AutoSize = true,
                Location = new Point(20, 70)
            };

            var nameTextBox = new TextBox
            {
                Name = "txtProductName",
                Location = new Point(120, 70),
                Size = new Size(150, 25)
            };

            // Product Price
            var priceLabel = new Label
            {
                Text = "Price:",
                AutoSize = true,
                Location = new Point(20, 110)
            };

            var priceNumericUpDown = new NumericUpDown
            {
                Name = "nudProductPrice",
                Location = new Point(120, 110),
                Size = new Size(150, 25),
                Minimum = 0,
                Maximum = 1000000,
                DecimalPlaces = 2,
                ThousandsSeparator = true
            };

            // Product Stock
            var stockLabel = new Label
            {
                Text = "Stock:",
                AutoSize = true,
                Location = new Point(20, 150)
            };

            var stockNumericUpDown = new NumericUpDown
            {
                Name = "nudProductStock",
                Location = new Point(120, 150),
                Size = new Size(150, 25),
                Minimum = 0,
                Maximum = 10000
            };

            // Buttons
            var addButton = new Button
            {
                Name = "btnAddProduct",
                Text = "Add New",
                Location = new Point(20, 200),
                Size = new Size(80, 30)
            };
            addButton.Click += AddProductButton_Click;

            var updateButton = new Button
            {
                Name = "btnUpdateProduct",
                Text = "Update",
                Location = new Point(110, 200),
                Size = new Size(80, 30),
                Enabled = false
            };
            updateButton.Click += UpdateProductButton_Click;

            var deleteButton = new Button
            {
                Name = "btnDeleteProduct",
                Text = "Delete",
                Location = new Point(200, 200),
                Size = new Size(80, 30),
                Enabled = false
            };
            deleteButton.Click += DeleteProductButton_Click;

            var clearButton = new Button
            {
                Name = "btnClearProduct",
                Text = "Clear",
                Location = new Point(110, 240),
                Size = new Size(80, 30)
            };
clearButton.Click += (s, e) => ClearProductFields();

            // Add controls to the details group
            detailsGroup.Controls.AddRange(new Control[] { 
                idLabel, idTextBox, 
                nameLabel, nameTextBox, 
                priceLabel, priceNumericUpDown, 
                stockLabel, stockNumericUpDown, 
                addButton, updateButton, deleteButton, clearButton 
            });

            // Add controls to the panel
            panel.Controls.AddRange(new Control[] { productListView, detailsGroup });

            // Add the panel to the tab
            tab.Controls.Add(panel);
        }

        private void LoadProductManagement()
        {
            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null || tabControl.TabPages.Count < 3) return;

            var productTab = tabControl.TabPages[2];
            var productListView = productTab.Controls.Find("lvAdminProducts", true).FirstOrDefault() as ListView;
            if (productListView == null) return;

            productListView.Items.Clear();
            var products = productService.GetAll();

            foreach (var product in products)
            {
                var item = new ListViewItem(product.Id.ToString());
                item.SubItems.Add(product.ProductName);
                item.SubItems.Add(product.Price.ToString("C"));
                item.SubItems.Add(product.Stock.ToString());
                item.Tag = product;

                productListView.Items.Add(item);
            }
            
            // Clear product details fields
            ClearProductFields();
        }
        
        private void LoadAdminProducts()
        {
            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null || tabControl.TabPages.Count < 3) return;

            var productTab = tabControl.TabPages[2];
            var productListView = productTab.Controls.Find("lvAdminProducts", true).FirstOrDefault() as ListView;
            if (productListView == null) return;

            productListView.Items.Clear();
            var products = productService.GetAll();

            foreach (var product in products)
            {
                var item = new ListViewItem(product.Id.ToString());
                item.SubItems.Add(product.ProductName);
                item.SubItems.Add(product.Price.ToString("C"));
                item.SubItems.Add(product.Stock.ToString());
                item.Tag = product;

                productListView.Items.Add(item);
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null || currentUser.Role != "Admin")
            {
                MessageBox.Show("You must be an admin to manage products.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var productTab = tabControl.TabPages[2]; // Product Management tab
            var nameTextBox = productTab.Controls.Find("txtProductName", true).FirstOrDefault() as TextBox;
            var priceNumericUpDown = productTab.Controls.Find("nudProductPrice", true).FirstOrDefault() as NumericUpDown;
            var stockNumericUpDown = productTab.Controls.Find("nudProductStock", true).FirstOrDefault() as NumericUpDown;

            if (nameTextBox == null || priceNumericUpDown == null || stockNumericUpDown == null) return;

            string productName = nameTextBox.Text.Trim();
            decimal price = priceNumericUpDown.Value;
            int stock = (int)stockNumericUpDown.Value;

            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Product name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create new product
            var products = productService.GetAll();
            int newId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;

            var newProduct = new Product
            {
                Id = newId,
                ProductName = productName,
                Price = price,
                Stock = stock
            };

            // Add to service and save to file
            productService.Add(newProduct);
            Product.SaveToFile(ProductFilePath, productService.GetAll());

            MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear fields and refresh list
            ClearProductFields();
            LoadAdminProducts();
        }

        private void UpdateProductButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null || currentUser.Role != "Admin")
            {
                MessageBox.Show("You must be an admin to manage products.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var productTab = tabControl.TabPages[2]; // Product Management tab
            var idTextBox = productTab.Controls.Find("txtProductId", true).FirstOrDefault() as TextBox;
            var nameTextBox = productTab.Controls.Find("txtProductName", true).FirstOrDefault() as TextBox;
            var priceNumericUpDown = productTab.Controls.Find("nudProductPrice", true).FirstOrDefault() as NumericUpDown;
            var stockNumericUpDown = productTab.Controls.Find("nudProductStock", true).FirstOrDefault() as NumericUpDown;

            if (idTextBox == null || nameTextBox == null || priceNumericUpDown == null || stockNumericUpDown == null) return;

            if (!int.TryParse(idTextBox.Text, out int productId))
            {
                MessageBox.Show("Invalid product ID!", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string productName = nameTextBox.Text.Trim();
            decimal price = priceNumericUpDown.Value;
            int stock = (int)stockNumericUpDown.Value;

            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Product name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Find the product in the list
            var products = productService.GetAll();
            int productIndex = products.FindIndex(p => p.Id == productId);

            if (productIndex >= 0)
            {
                var updatedProduct = new Product
                {
                    Id = productId,
                    ProductName = productName,
                    Price = price,
                    Stock = stock
                };

                // Update in service and save to file
                productService.Update(productIndex, updatedProduct);
                Product.SaveToFile(ProductFilePath, productService.GetAll());

                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields and refresh list
                ClearProductFields();
                LoadAdminProducts();
            }
            else
            {
                MessageBox.Show("Product not found!", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null || currentUser.Role != "Admin")
            {
                MessageBox.Show("You must be an admin to manage products.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var productTab = tabControl.TabPages[2]; // Product Management tab
            var idTextBox = productTab.Controls.Find("txtProductId", true).FirstOrDefault() as TextBox;

            if (idTextBox == null) return;

            if (!int.TryParse(idTextBox.Text, out int productId))
            {
                MessageBox.Show("Invalid product ID!", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm deletion
            var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Find the product in the list
                var products = productService.GetAll();
                int productIndex = products.FindIndex(p => p.Id == productId);

                if (productIndex >= 0)
                {
                    // Remove from service and save to file
                    productService.Remove(productIndex);
                    Product.SaveToFile(ProductFilePath, productService.GetAll());

                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields and refresh list
                    ClearProductFields();
                    LoadAdminProducts();
                }
                else
                {
                    MessageBox.Show("Product not found!", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProductListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var productTab = tabControl.TabPages[2]; // Product Management tab
            var productListView = productTab.Controls.Find("lvAdminProducts", true).FirstOrDefault() as ListView;
            
            var idTextBox = productTab.Controls.Find("txtProductId", true).FirstOrDefault() as TextBox;
            var nameTextBox = productTab.Controls.Find("txtProductName", true).FirstOrDefault() as TextBox;
            var priceNumericUpDown = productTab.Controls.Find("nudProductPrice", true).FirstOrDefault() as NumericUpDown;
            var stockNumericUpDown = productTab.Controls.Find("nudProductStock", true).FirstOrDefault() as NumericUpDown;
            var updateButton = productTab.Controls.Find("btnUpdateProduct", true).FirstOrDefault() as Button;
            var deleteButton = productTab.Controls.Find("btnDeleteProduct", true).FirstOrDefault() as Button;

            if (productListView == null || idTextBox == null || nameTextBox == null || 
                priceNumericUpDown == null || stockNumericUpDown == null || 
                updateButton == null || deleteButton == null) return;

            if (productListView.SelectedItems.Count > 0)
            {
                var selectedItem = productListView.SelectedItems[0];
                var product = selectedItem.Tag as Product;

                if (product != null)
                {
                    idTextBox.Text = product.Id.ToString();
                    nameTextBox.Text = product.ProductName;
                    priceNumericUpDown.Value = product.Price;
                    stockNumericUpDown.Value = product.Stock;

                    updateButton.Enabled = true;
                    deleteButton.Enabled = true;
                }
            }
            else
            {
                ClearProductFields();
                updateButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void ClearProductFields_Click(object sender, EventArgs e)
        {
            ClearProductFields();
        }

        private void ClearProductFields()
        {
            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var productTab = tabControl.TabPages[2]; // Product Management tab
            var idTextBox = productTab.Controls.Find("txtProductId", true).FirstOrDefault() as TextBox;
            var nameTextBox = productTab.Controls.Find("txtProductName", true).FirstOrDefault() as TextBox;
            var priceNumericUpDown = productTab.Controls.Find("nudProductPrice", true).FirstOrDefault() as NumericUpDown;
            var stockNumericUpDown = productTab.Controls.Find("nudProductStock", true).FirstOrDefault() as NumericUpDown;
            var updateButton = productTab.Controls.Find("btnUpdateProduct", true).FirstOrDefault() as Button;
            var deleteButton = productTab.Controls.Find("btnDeleteProduct", true).FirstOrDefault() as Button;

            if (idTextBox != null) idTextBox.Text = "";
            if (nameTextBox != null) nameTextBox.Text = "";
            if (priceNumericUpDown != null) priceNumericUpDown.Value = 0;
            if (stockNumericUpDown != null) stockNumericUpDown.Value = 0;
            if (updateButton != null) updateButton.Enabled = false;
            if (deleteButton != null) deleteButton.Enabled = false;

            // Deselect any selected item in the list view
            var productListView = productTab.Controls.Find("lvAdminProducts", true).FirstOrDefault() as ListView;
            if (productListView != null) productListView.SelectedItems.Clear();
        }
        
        // private void ClearProductFields_Click(object sender, EventArgs e)
        // {
        //     ClearProductFields();
        // }

        private void LoadPurchaseHistory()
    {
        var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
        if (tabControl == null || tabControl.TabPages.Count < 2) return;

        var purchaseTab = tabControl.TabPages[1];
        var purchaseListView = purchaseTab.Controls.Find("lvPurchases", true).FirstOrDefault() as ListView;
        if (purchaseListView == null) return;

        purchaseListView.Items.Clear();
        var purchases = purchaseService.GetAll();

        foreach (var purchase in purchases)
        {
            var item = new ListViewItem(purchase.Id.ToString());
            item.SubItems.Add(purchase.CustomerName);
            item.SubItems.Add(purchase.ProductName);
            item.SubItems.Add(purchase.Quantity.ToString());
            item.SubItems.Add(purchase.TotalPrice.ToString("C"));
            item.SubItems.Add(purchase.PurchaseDate.ToString("g"));
            item.Tag = purchase;

            purchaseListView.Items.Add(item);
        }
        }

        #endregion

        #region Event Handlers

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var usernameTextBox = loginPanel.Controls.Find("txtLoginUsername", true).FirstOrDefault() as TextBox;
            var passwordTextBox = loginPanel.Controls.Find("txtLoginPassword", true).FirstOrDefault() as TextBox;

            if (usernameTextBox == null || passwordTextBox == null) return;

            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            currentUser = authService.Login(loginRequest);

            if (currentUser != null)
            {
                MessageBox.Show($"Welcome, {currentUser.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowMainPanel();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var usernameTextBox = registerPanel.Controls.Find("txtRegUsername", true).FirstOrDefault() as TextBox;
            var emailTextBox = registerPanel.Controls.Find("txtRegEmail", true).FirstOrDefault() as TextBox;
            var passwordTextBox = registerPanel.Controls.Find("txtRegPassword", true).FirstOrDefault() as TextBox;
            var confirmPasswordTextBox = registerPanel.Controls.Find("txtRegConfirmPassword", true).FirstOrDefault() as TextBox;

            if (usernameTextBox == null || emailTextBox == null || passwordTextBox == null || confirmPasswordTextBox == null) return;

            string username = usernameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string confirmPassword = confirmPasswordTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required!", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var registerRequest = new RegisterRequest
            {
                Username = username,
                Email = email,
                Password = password,
                Role = "Customer" // Default role
            };

            if (authService.Register(registerRequest))
            {
                MessageBox.Show("Registration successful! You can now login.", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowLoginPanel();
            }
            //else
            //{
            //    MessageBox.Show("Registration failed! Username may already exist or validation failed.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            currentUser = null;
            MessageBox.Show("You have been logged out.", "Logout Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowLoginPanel();
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You must be logged in to make a purchase.", "Authentication Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productListView = productPanel.Controls.Find("lvProducts", true).FirstOrDefault() as ListView;
            var quantityNumericUpDown = productPanel.Controls.Find("nudQuantity", true).FirstOrDefault() as NumericUpDown;

            if (productListView == null || quantityNumericUpDown == null || productListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to purchase.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = productListView.SelectedItems[0];
            var product = selectedItem.Tag as Product;
            int quantity = (int)quantityNumericUpDown.Value;

            if (product == null) return;

            if (product.Stock < quantity)
            {
                MessageBox.Show("Not enough stock available!", "Purchase Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update product stock
    product.Stock -= quantity;
    
    // Find the index of the product in the list
    var products = productService.GetAll();
    int productIndex = products.FindIndex(p => p.Id == product.Id);
    
    if (productIndex >= 0)
    {
        productService.Update(productIndex, product);
        Product.SaveToFile(ProductFilePath, productService.GetAll());

        // Create purchase record
        var purchases = purchaseService.GetAll();
        int newId = purchases.Count > 0 ? purchases.Max(x => x.Id) + 1 : 1;

        var purchase = new Pembelian
        {
            Id = newId,
            CustomerName = currentUser.Username,
            ProductId = product.Id,
            ProductName = product.ProductName,
            Quantity = quantity,
            TotalPrice = product.Price * quantity,
            PurchaseDate = DateTime.Now
        };

        purchaseService.Add(purchase);
        Pembelian.SaveToFile(PurchaseFilePath, purchaseService.GetAll());

        MessageBox.Show($"Purchase successful! You bought {quantity} x {product.ProductName} for {purchase.TotalPrice:C}",
        "Purchase Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    } else {
        MessageBox.Show("Error: Product not found in the list.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

            
            // Refresh product list
            LoadProducts();
        }

        private void DonateButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You must be logged in to make a donation.", "Authentication Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var donorNameTextBox = donationPanel.Controls.Find("txtDonorName", true).FirstOrDefault() as TextBox;
            var amountNumericUpDown = donationPanel.Controls.Find("nudDonationAmount", true).FirstOrDefault() as NumericUpDown;

            if (donorNameTextBox == null || amountNumericUpDown == null) return;

            string donorName = donorNameTextBox.Text.Trim();
            decimal amount = amountNumericUpDown.Value;

            if (string.IsNullOrWhiteSpace(donorName))
            {
                MessageBox.Show("Please enter a donor name.", "Donation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create donation record
            var donations = donationService.GetAll();
            int newId = donations.Count > 0 ? donations.Max(x => x.Id) + 1 : 1;

            var donation = new Donation
            {
                Id = newId,
                DonorName = donorName,
                Amount = amount
            };

            donationService.Add(donation);

            // Save to file
            var updatedDonations = donationService.GetAll();
            var json = JsonSerializer.Serialize(updatedDonations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DonationFilePath, json);

            MessageBox.Show($"Thank you for your donation of {amount:C}!", "Donation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear fields and refresh list
            donorNameTextBox.Text = "";
            amountNumericUpDown.Value = 10;
            LoadDonations();
        }

        private void SaveGeneralSettingsButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null || currentUser.Role != "Admin")
            {
                MessageBox.Show("You must be an admin to change website settings.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var settingsTab = tabControl.TabPages[0];
            var settingsTabControl = settingsTab.Controls.Find("tabSettingsControl", true).FirstOrDefault() as TabControl;
            if (settingsTabControl == null) return;

            var generalTab = settingsTabControl.TabPages[0];
            
            var websiteNameTextBox = generalTab.Controls.Find("txtWebsiteName", true).FirstOrDefault() as TextBox;
            var maintenanceCheckBox = generalTab.Controls.Find("chkMaintenance", true).FirstOrDefault() as CheckBox;

            if (websiteNameTextBox == null || maintenanceCheckBox == null) return;

            string websiteName = websiteNameTextBox.Text.Trim();
            bool maintenanceMode = maintenanceCheckBox.Checked;

            if (string.IsNullOrWhiteSpace(websiteName))
            {
                MessageBox.Show("Website name cannot be empty!", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update website configuration
            PengaturanWebsiteConfig.UpdateConfiguration(config =>
            {
                config.WebsiteName = websiteName;
                config.MaintenanceMode = maintenanceMode;
            });

            MessageBox.Show("General settings updated successfully!", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Update form title
            this.Text = PengaturanWebsiteConfig.Instance.WebsiteName;
        }

        private void SaveContentSettingsButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null || currentUser.Role != "Admin")
            {
                MessageBox.Show("You must be an admin to change website settings.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabControl = adminPanel.Controls.Find("tabAdminControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null) return;

            var settingsTab = tabControl.TabPages[0];
            var settingsTabControl = settingsTab.Controls.Find("tabSettingsControl", true).FirstOrDefault() as TabControl;
            if (settingsTabControl == null) return;

            var contentTab = settingsTabControl.TabPages[1];
            
            var websiteDescTextBox = contentTab.Controls.Find("txtWebsiteDesc", true).FirstOrDefault() as TextBox;

            if (websiteDescTextBox == null) return;

            string websiteDesc = websiteDescTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(websiteDesc))
            {
                MessageBox.Show("Website description cannot be empty!", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update website configuration
            PengaturanWebsiteConfig.UpdateConfiguration(config =>
            {
                config.WebsiteDescription = websiteDesc;
            });

            MessageBox.Show("Content settings updated successfully!", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            // Existing event handler
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
