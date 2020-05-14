﻿using System;
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
            this.translation = new Translation(ci.DisplayName);
            this.InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void InitializeComponent()
        {
            this.settings = new SettingsManager();

            ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Main));
            this.login_panel = new Panel();
            this.submit_login = new Button();
            this.label2 = new Label(); //Email Label
            this.label1 = new Label(); //Passwirt label
            this.password_input = new TextBox();
            this.email_input = new TextBox();
            this.version_text = new Label();
            this.progressBardownload = new ProgressBar();
            this.login_panel.SuspendLayout();
            this.SuspendLayout();
            //this.login_panel.Controls.Add((Control)this.logo);
            this.login_panel.Controls.Add((Control)this.submit_login);
            this.login_panel.Controls.Add((Control)this.label2);
            this.login_panel.Controls.Add((Control)this.label1);
            this.login_panel.Controls.Add((Control)this.password_input);
            this.login_panel.Controls.Add((Control)this.email_input);
            this.login_panel.Location = new Point(4, 9);
            this.login_panel.Name = "login_panel";
            this.login_panel.Size = new Size(375, 377);
            this.login_panel.TabIndex = 6;
            //this.logo.BackgroundImage = (Image)Resources.logo;
            //this.logo.BackgroundImageLayout = ImageLayout.Zoom;
            //this.logo.Location = new Point(60, 75);
            // this.logo.Name = "logo";
            //this.logo.Size = new Size(250, 66);
            //this.logo.TabIndex = 6;
            //this.logo.TabStop = false;
            this.submit_login.FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);
            this.submit_login.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 150, 150);
            this.submit_login.FlatAppearance.MouseOverBackColor = Color.FromArgb(204, 204, 204);
            this.submit_login.FlatStyle = FlatStyle.Flat;
            this.submit_login.Location = new Point(60, 274);
            this.submit_login.Name = "submit_login";
            this.submit_login.Size = new Size(250, 25);
            this.submit_login.TabIndex = 4;
            this.submit_login.Text = translation.login;
            this.submit_login.UseVisualStyleBackColor = true;
            this.submit_login.Click += new EventHandler(this.submit_login_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(57, 151);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = translation.password;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(57, 100);
            this.label1.Name = "label1";
            this.label1.Size = new Size(131, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = this.translation.login_username;
            this.password_input.Location = new Point(60, 167);
            this.password_input.Name = "password_input";
            this.password_input.Size = new Size(250, 22);
            this.password_input.TabIndex = 1;
            this.password_input.UseSystemPasswordChar = true;
            this.password_input.KeyUp += new KeyEventHandler(this.password_input_KeyPress);
            this.email_input.Location = new Point(60, 116);
            this.email_input.Name = "email_input";
            this.email_input.Size = new Size(250, 22);
            this.email_input.TabIndex = 0;
            this.version_text.AutoSize = true;
            this.version_text.Location = new Point(280, 3);
            this.version_text.Name = "version_text";
            this.version_text.Size = new Size(101, 13);
            this.version_text.TabIndex = 7;
            this.version_text.Text = "Version: 1.1.1 Alpha";
            this.version_text.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(384, 411);
            this.Controls.Add((Control)this.login_panel);
            this.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.Name = nameof(Main);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "VTCManager";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Load += new EventHandler(this.Main_Load);
            this.Resize += new EventHandler(this.Main_Resize);
            this.login_panel.ResumeLayout(false);
            this.login_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
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
            this.login(this.email_input.Text, Utilities.Sha256(this.password_input.Text));
        }

        private void password_input_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            this.login(this.email_input.Text, Utilities.Sha256(this.password_input.Text));
            e.Handled = true;
        }

        private void login(string theEmail, string thePassword)
        {
            this.authCode = Authenticate(theEmail, thePassword);
            if (this.authCode.Equals("Error: PIN_Invalid") || this.authCode.Equals("Error: User_Invalid") || this.authCode.Equals("Error: Serverside"))
            {
                this.login_panel.Visible = true;
                int num = (int)MessageBox.Show(translation.login_failed);
            }
            else
            {
                this.preferences.Config.SaveLoginData = "yes";
                this.preferences.Config.Account = theEmail;
                this.preferences.Config.Password = thePassword;
                this.preferences.SaveConfig();
                this.login_panel.Visible = false;
                string[] strArray = this.api.HTTPSRequestPost(this.api.api_server + this.api.load_data_path, new Dictionary<string, string>()
                {
                  {
                    "authcode",
                    this.authCode
                  }
                }, true).ToString().Split(',');

                if (this.authCode.Equals("Error: PIN_Invalid") || this.authCode.Equals("Error: User_Invalid") || this.authCode.Equals("Error: Serverside"))
                {
                    this.login_panel.Visible = true;
                    MessageBox.Show(translation.login_failed);
                }
                if (string.IsNullOrEmpty(this.authCode))
                {
                    Application.Exit();
                }
                //Check für Beta
                //Bei normalen Benutzern ist der String leer
                /*if (String.IsNullOrEmpty(strArray[7]))
                    Application.Exit();*/
                User user = new User(Convert.ToInt32(strArray[0]), strArray[1], strArray[2], strArray[3], Convert.ToInt32(strArray[4]), Convert.ToInt32(strArray[5]), Convert.ToInt32(strArray[6]), this.authCode);
                this.Hide();

                Main Mainwindow = new Main(user);
                Mainwindow.ShowDialog();
            }
        }



        private void Main_Load(object sender, EventArgs e)
        {
            this.preferences.CreateConfig();
            this.preferences.LoadConfig();
            if (!(this.preferences.Config.SaveLoginData == "yes"))
                return;
            this.login(this.preferences.Config.Account, this.preferences.Config.Password);
        }


        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                if (this.WindowState != FormWindowState.Normal)
                    return;
            }
        }
    }
}
