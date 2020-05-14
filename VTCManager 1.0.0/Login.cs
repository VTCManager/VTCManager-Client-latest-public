using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VTCManager_1._0._0.Objekte;

namespace VTCManager_1._0._0
{

    public class Login : Form
    {
        private API api = new API();
        private SettingsManager preferences = new SettingsManager();
        private Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
        public string userID = "0";
        public string userCompany = "0";
        public string jobID = "0";
        public string authCode = "false";
        public Dictionary<string, string> lastJobDictionary = new Dictionary<string, string>();
        private Panel login_panel;
        private Label label2;
        private Label label1;
        private TextBox password_input;
        private TextBox email_input;
        private Button submit_login;
        private Label version_text;
        private ProgressBar progressBardownload;
        private Translation translation;
        private SettingsManager settings;
        public bool debug = false;
        // Edit by Thommy
        public int spender = 0;

        public Login(bool debug)
        {
            this.debug = debug;
            CultureInfo ci = CultureInfo.InstalledUICulture;
            translation = new Translation(ci.DisplayName);
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Main_FormClosing);
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void InitializeComponent()
        {
            settings = new SettingsManager();

            ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Main));
            login_panel = new Panel();
            submit_login = new Button();
            label2 = new Label(); //Email Label
            label1 = new Label(); //Passwirt label
            password_input = new TextBox();
            email_input = new TextBox();
            version_text = new Label();
            progressBardownload = new ProgressBar();
            login_panel.SuspendLayout();
            SuspendLayout();
            //this.login_panel.Controls.Add((Control)this.logo);
            login_panel.Controls.Add(submit_login);
            login_panel.Controls.Add(label2);
            login_panel.Controls.Add(label1);
            login_panel.Controls.Add(password_input);
            login_panel.Controls.Add(email_input);
            login_panel.Location = new Point(4, 9);
            login_panel.Name = "login_panel";
            login_panel.Size = new Size(375, 377);
            login_panel.TabIndex = 6;
            //this.logo.BackgroundImage = (Image)Resources.logo;
            //this.logo.BackgroundImageLayout = ImageLayout.Zoom;
            //this.logo.Location = new Point(60, 75);
            // this.logo.Name = "logo";
            //this.logo.Size = new Size(250, 66);
            //this.logo.TabIndex = 6;
            //this.logo.TabStop = false;
            submit_login.FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);
            submit_login.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 150, 150);
            submit_login.FlatAppearance.MouseOverBackColor = Color.FromArgb(204, 204, 204);
            submit_login.FlatStyle = FlatStyle.Flat;
            submit_login.Location = new Point(60, 274);
            submit_login.Name = "submit_login";
            submit_login.Size = new Size(250, 25);
            submit_login.TabIndex = 4;
            submit_login.Text = translation.login;
            submit_login.UseVisualStyleBackColor = true;
            submit_login.Click += new EventHandler(submit_login_Click);
            label2.AutoSize = true;
            label2.Location = new Point(57, 151);
            label2.Name = "label2";
            label2.Size = new Size(53, 13);
            label2.TabIndex = 3;
            label2.Text = translation.password;
            label1.AutoSize = true;
            label1.Location = new Point(57, 100);
            label1.Name = "label1";
            label1.Size = new Size(131, 13);
            label1.TabIndex = 2;
            label1.Text = translation.login_username;
            password_input.Location = new Point(60, 167);
            password_input.Name = "password_input";
            password_input.Size = new Size(250, 22);
            password_input.TabIndex = 1;
            password_input.UseSystemPasswordChar = true;
            password_input.KeyUp += new KeyEventHandler(password_input_KeyPress);
            email_input.Location = new Point(60, 116);
            email_input.Name = "email_input";
            email_input.Size = new Size(250, 22);
            email_input.TabIndex = 0;
            version_text.AutoSize = true;
            version_text.Location = new Point(280, 3);
            version_text.Name = "version_text";
            version_text.Size = new Size(101, 13);
            version_text.TabIndex = 7;
            version_text.Text = "Version: 1.1.1 Alpha";
            version_text.TextAlign = ContentAlignment.MiddleRight;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(384, 411);
            Controls.Add(login_panel);
            Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = nameof(Main);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VTCManager";
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            Load += new EventHandler(Main_Load);
            Resize += new EventHandler(Main_Resize);
            login_panel.ResumeLayout(false);
            login_panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        public static string Authenticate(string email, string password)
        {
            API api = new API();
            return api.HTTPSRequestPost(api.api_server + api.login_path, new Dictionary<string, string>()
              {
                { "username", email },
                { "password", password }
              }, true).ToString();
        }

        private void submit_login_Click(object sender, EventArgs e)
        {
            login(email_input.Text, Utilities.Sha256(password_input.Text));
        }

        private void password_input_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
            {
                return;
            }

            login(email_input.Text, Utilities.Sha256(password_input.Text));
            e.Handled = true;
        }

        private void login(string theEmail, string thePassword)
        {
            authCode = Authenticate(theEmail, thePassword);
            if (authCode.Equals("Error: PIN_Invalid") || authCode.Equals("Error: User_Invalid") || authCode.Equals("Error: Serverside"))
            {
                login_panel.Visible = true;
                int num = (int)MessageBox.Show(translation.login_failed);
            }
            else
            {
                preferences.Config.SaveLoginData = "yes";
                preferences.Config.Account = theEmail;
                preferences.Config.Password = thePassword;
                preferences.SaveConfig();
                login_panel.Visible = false;
                string[] strArray = api.HTTPSRequestPost(api.api_server + api.load_data_path, new Dictionary<string, string>()
                {
                  {
                    "authcode",
                    authCode
                  }
                }, true).ToString().Split(',');

                if (authCode.Equals("Error: PIN_Invalid") || authCode.Equals("Error: User_Invalid") || authCode.Equals("Error: Serverside"))
                {
                    login_panel.Visible = true;
                    MessageBox.Show(translation.login_failed);
                }
                if (string.IsNullOrEmpty(authCode))
                {
                    Application.Exit();
                }
                //Check für Beta
                //Bei normalen Benutzern ist der String leer
                /*if (String.IsNullOrEmpty(strArray[7]))
                    Application.Exit();*/
                User user = new User(Convert.ToInt32(strArray[0]), strArray[1], strArray[2], strArray[3], Convert.ToInt32(strArray[4]), Convert.ToInt32(strArray[5]), Convert.ToInt32(strArray[6]), authCode);
                Hide();

                Main Mainwindow = new Main(user);
                Mainwindow.ShowDialog();
            }
        }



        private void Main_Load(object sender, EventArgs e)
        {
            preferences.CreateConfig();
            preferences.LoadConfig();
            if (!(preferences.Config.SaveLoginData == "yes"))
            {
                return;
            }

            login(preferences.Config.Account, preferences.Config.Password);
        }


        private void Main_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
            else
            {
                if (WindowState != FormWindowState.Normal)
                {
                    return;
                }
            }
        }
    }
}
