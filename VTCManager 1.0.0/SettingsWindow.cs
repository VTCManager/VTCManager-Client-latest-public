﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    class SettingsWindow : Form
    {
        public Utilities utils = new Utilities();
        Logging Logging = new Logging();

        private ComboBox comboBox1;
        private Label label1;
        private SettingsManager data;
        private Translation translation;
        private Main mi;
        private Button save_button;
        private GroupBox groupBox1;
        private GroupBox btn_TruckersMP_suchen;
        private Label label3;
        private GroupBox group_Overlay;
        private ComboBox combo_Bildschirme;
        private Label label6;
        private NumericUpDown num_Overlay_Transparenz;
        private Label label5;
        private Button button1;
        private string selected_server_tm;
        private System.Windows.Forms.OpenFileDialog tmp_Trucker;
        private TextBox truckersMP_Pfad_TextBox;
        private GroupBox groupBox_AntiAFK;
        private TextBox txt_Anti_AFK_Text;
        private CheckBox chk_antiafk_on_off;
        private Button Ats_Suche;
        private Label label4;
        private TextBox ATS_Pfad_Textbox;
        private Button Ets_Suche;
        private Label label2;
        private TextBox ETS2_Pfad_Textbox;
        private Label label8;
        private Label label7;
        private NumericUpDown reload_antiafk;
        private PictureBox pictureBox1;
        private Label label9;
        private Label Settings_Windows_Label_Settings;
        private Button GameLog_suchen;
        private GroupBox GroupBox_Diagnostic;
        private Button Registry_anzeigen;
        private Button GameLog_oeffnen;
        private CheckBox Diagnostic_Checkbox;
        private GroupBox groupBox2;
        private CheckBox Chk_Dashboard;
        private Button VTC_Button;
        private CheckBox Autostart_Checkbox;
        private FolderBrowserDialog ETS2_folderBrowserDialog;
        private FolderBrowserDialog ATS_folderBrowserDialog;
        private Button Reg_Reset;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private PictureBox patreon_image;
        private PictureBox team_image;
        private GroupBox Group_Box_Indiv_Texte;
        private Label label13;
        private Label label12;
        private TextBox Num3_Text;
        private Label label11;
        private TextBox Num2_Text;
        private Label label10;
        private TextBox Num1_Text;
        private Label label14;
        private CheckBox checkBox_NUM_LOCK;
        public int Patreon;

        public SettingsWindow(Translation translation, int patreon)
        {
            this.data = new SettingsManager();

            this.InitializeComponent();
            this.save_button.Text = translation.settings_window_save_button;
            this.groupBox1.Text = translation.settings_window_groupBox1text;
            this.btn_TruckersMP_suchen.Text = translation.btn_TruckersMP_suchentext;
            this.label3.Text = translation.settings_window_label3text;
            this.comboBox1.Text = this.data.tmp_server;
            this.Patreon = patreon;
            checkBox_NUM_LOCK.Text = translation.Settings_CheckBox_NUMPAD_ONOFF;
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_TruckersMP_suchen = new System.Windows.Forms.GroupBox();
            this.Ats_Suche = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ATS_Pfad_Textbox = new System.Windows.Forms.TextBox();
            this.Ets_Suche = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ETS2_Pfad_Textbox = new System.Windows.Forms.TextBox();
            this.truckersMP_Pfad_TextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.group_Overlay = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.num_Overlay_Transparenz = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_Bildschirme = new System.Windows.Forms.ComboBox();
            this.tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_AntiAFK = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.reload_antiafk = new System.Windows.Forms.NumericUpDown();
            this.chk_antiafk_on_off = new System.Windows.Forms.CheckBox();
            this.txt_Anti_AFK_Text = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Settings_Windows_Label_Settings = new System.Windows.Forms.Label();
            this.GameLog_suchen = new System.Windows.Forms.Button();
            this.GroupBox_Diagnostic = new System.Windows.Forms.GroupBox();
            this.Reg_Reset = new System.Windows.Forms.Button();
            this.VTC_Button = new System.Windows.Forms.Button();
            this.Registry_anzeigen = new System.Windows.Forms.Button();
            this.GameLog_oeffnen = new System.Windows.Forms.Button();
            this.Diagnostic_Checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Autostart_Checkbox = new System.Windows.Forms.CheckBox();
            this.Chk_Dashboard = new System.Windows.Forms.CheckBox();
            this.ETS2_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ATS_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.patreon_image = new System.Windows.Forms.PictureBox();
            this.team_image = new System.Windows.Forms.PictureBox();
            this.Group_Box_Indiv_Texte = new System.Windows.Forms.GroupBox();
            this.checkBox_NUM_LOCK = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Num3_Text = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Num2_Text = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Num1_Text = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.btn_TruckersMP_suchen.SuspendLayout();
            this.group_Overlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Overlay_Transparenz)).BeginInit();
            this.groupBox_AntiAFK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reload_antiafk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.GroupBox_Diagnostic.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patreon_image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.team_image)).BeginInit();
            this.Group_Box_Indiv_Texte.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Simulation 1",
            "Simulation 2",
            "Arcade",
            "EU Promods 1",
            "EU Promods 2"});
            this.comboBox1.Location = new System.Drawing.Point(171, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Simulation 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TruckersMP-Server:";
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(686, 545);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(149, 41);
            this.save_button.TabIndex = 3;
            this.save_button.Text = "Einstellungen Speichern...";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(288, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 62);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Einstellungen";
            // 
            // btn_TruckersMP_suchen
            // 
            this.btn_TruckersMP_suchen.Controls.Add(this.Ats_Suche);
            this.btn_TruckersMP_suchen.Controls.Add(this.label4);
            this.btn_TruckersMP_suchen.Controls.Add(this.ATS_Pfad_Textbox);
            this.btn_TruckersMP_suchen.Controls.Add(this.Ets_Suche);
            this.btn_TruckersMP_suchen.Controls.Add(this.label2);
            this.btn_TruckersMP_suchen.Controls.Add(this.ETS2_Pfad_Textbox);
            this.btn_TruckersMP_suchen.Controls.Add(this.truckersMP_Pfad_TextBox);
            this.btn_TruckersMP_suchen.Controls.Add(this.button1);
            this.btn_TruckersMP_suchen.Controls.Add(this.label3);
            this.btn_TruckersMP_suchen.Location = new System.Drawing.Point(12, 129);
            this.btn_TruckersMP_suchen.Name = "btn_TruckersMP_suchen";
            this.btn_TruckersMP_suchen.Size = new System.Drawing.Size(270, 186);
            this.btn_TruckersMP_suchen.TabIndex = 6;
            this.btn_TruckersMP_suchen.TabStop = false;
            this.btn_TruckersMP_suchen.Text = "Game und Multiplayer";
            // 
            // Ats_Suche
            // 
            this.Ats_Suche.Location = new System.Drawing.Point(241, 128);
            this.Ats_Suche.Name = "Ats_Suche";
            this.Ats_Suche.Size = new System.Drawing.Size(25, 22);
            this.Ats_Suche.TabIndex = 16;
            this.Ats_Suche.Text = "...";
            this.Ats_Suche.UseVisualStyleBackColor = true;
            this.Ats_Suche.Click += new System.EventHandler(this.Ats_Suche_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Pfad zu ATS:";
            // 
            // ATS_Pfad_Textbox
            // 
            this.ATS_Pfad_Textbox.Location = new System.Drawing.Point(7, 130);
            this.ATS_Pfad_Textbox.Name = "ATS_Pfad_Textbox";
            this.ATS_Pfad_Textbox.Size = new System.Drawing.Size(228, 20);
            this.ATS_Pfad_Textbox.TabIndex = 14;
            // 
            // Ets_Suche
            // 
            this.Ets_Suche.Location = new System.Drawing.Point(241, 81);
            this.Ets_Suche.Name = "Ets_Suche";
            this.Ets_Suche.Size = new System.Drawing.Size(25, 22);
            this.Ets_Suche.TabIndex = 13;
            this.Ets_Suche.Text = "...";
            this.Ets_Suche.UseVisualStyleBackColor = true;
            this.Ets_Suche.Click += new System.EventHandler(this.Ets_Suche_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Pfad zu ETS 2:";
            // 
            // ETS2_Pfad_Textbox
            // 
            this.ETS2_Pfad_Textbox.Location = new System.Drawing.Point(7, 83);
            this.ETS2_Pfad_Textbox.Name = "ETS2_Pfad_Textbox";
            this.ETS2_Pfad_Textbox.Size = new System.Drawing.Size(228, 20);
            this.ETS2_Pfad_Textbox.TabIndex = 11;
            // 
            // truckersMP_Pfad_TextBox
            // 
            this.truckersMP_Pfad_TextBox.Location = new System.Drawing.Point(7, 37);
            this.truckersMP_Pfad_TextBox.Name = "truckersMP_Pfad_TextBox";
            this.truckersMP_Pfad_TextBox.Size = new System.Drawing.Size(228, 20);
            this.truckersMP_Pfad_TextBox.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(241, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 22);
            this.button1.TabIndex = 9;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pfad zu TruckersMP:";
            // 
            // group_Overlay
            // 
            this.group_Overlay.Controls.Add(this.label6);
            this.group_Overlay.Controls.Add(this.num_Overlay_Transparenz);
            this.group_Overlay.Controls.Add(this.label5);
            this.group_Overlay.Controls.Add(this.combo_Bildschirme);
            this.group_Overlay.Location = new System.Drawing.Point(551, 12);
            this.group_Overlay.Name = "group_Overlay";
            this.group_Overlay.Size = new System.Drawing.Size(270, 100);
            this.group_Overlay.TabIndex = 7;
            this.group_Overlay.TabStop = false;
            this.group_Overlay.Text = "Overlay Einstellungen";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(131, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Transparenz:";
            // 
            // num_Overlay_Transparenz
            // 
            this.num_Overlay_Transparenz.Location = new System.Drawing.Point(206, 54);
            this.num_Overlay_Transparenz.Name = "num_Overlay_Transparenz";
            this.num_Overlay_Transparenz.Size = new System.Drawing.Size(59, 20);
            this.num_Overlay_Transparenz.TabIndex = 2;
            this.num_Overlay_Transparenz.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Gaming-Monitor:";
            // 
            // combo_Bildschirme
            // 
            this.combo_Bildschirme.FormattingEnabled = true;
            this.combo_Bildschirme.Location = new System.Drawing.Point(96, 28);
            this.combo_Bildschirme.Name = "combo_Bildschirme";
            this.combo_Bildschirme.Size = new System.Drawing.Size(170, 21);
            this.combo_Bildschirme.TabIndex = 0;
            // 
            // tmp_Trucker
            // 
            this.tmp_Trucker.FileName = "T";
            // 
            // groupBox_AntiAFK
            // 
            this.groupBox_AntiAFK.Controls.Add(this.label9);
            this.groupBox_AntiAFK.Controls.Add(this.label8);
            this.groupBox_AntiAFK.Controls.Add(this.label7);
            this.groupBox_AntiAFK.Controls.Add(this.reload_antiafk);
            this.groupBox_AntiAFK.Controls.Add(this.chk_antiafk_on_off);
            this.groupBox_AntiAFK.Controls.Add(this.txt_Anti_AFK_Text);
            this.groupBox_AntiAFK.Location = new System.Drawing.Point(288, 198);
            this.groupBox_AntiAFK.Name = "groupBox_AntiAFK";
            this.groupBox_AntiAFK.Size = new System.Drawing.Size(547, 117);
            this.groupBox_AntiAFK.TabIndex = 9;
            this.groupBox_AntiAFK.TabStop = false;
            this.groupBox_AntiAFK.Text = "Anti - AFK";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Text:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(495, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "in Min.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(395, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Reload:";
            // 
            // reload_antiafk
            // 
            this.reload_antiafk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reload_antiafk.Location = new System.Drawing.Point(441, 66);
            this.reload_antiafk.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.reload_antiafk.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.reload_antiafk.Name = "reload_antiafk";
            this.reload_antiafk.Size = new System.Drawing.Size(52, 20);
            this.reload_antiafk.TabIndex = 2;
            this.reload_antiafk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.reload_antiafk.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.reload_antiafk.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // chk_antiafk_on_off
            // 
            this.chk_antiafk_on_off.AutoSize = true;
            this.chk_antiafk_on_off.Location = new System.Drawing.Point(10, 63);
            this.chk_antiafk_on_off.Name = "chk_antiafk_on_off";
            this.chk_antiafk_on_off.Size = new System.Drawing.Size(106, 17);
            this.chk_antiafk_on_off.TabIndex = 1;
            this.chk_antiafk_on_off.Text = "Anti AFK An/Aus";
            this.chk_antiafk_on_off.UseVisualStyleBackColor = true;
            this.chk_antiafk_on_off.CheckedChanged += new System.EventHandler(this.chk_antiafk_on_off_CheckedChanged);
            // 
            // txt_Anti_AFK_Text
            // 
            this.txt_Anti_AFK_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Anti_AFK_Text.Enabled = false;
            this.txt_Anti_AFK_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Anti_AFK_Text.Location = new System.Drawing.Point(10, 33);
            this.txt_Anti_AFK_Text.Name = "txt_Anti_AFK_Text";
            this.txt_Anti_AFK_Text.Size = new System.Drawing.Size(523, 22);
            this.txt_Anti_AFK_Text.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VTCManager_1._0._0.Properties.Resources.einstellungen;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Settings_Windows_Label_Settings
            // 
            this.Settings_Windows_Label_Settings.AutoSize = true;
            this.Settings_Windows_Label_Settings.Font = new System.Drawing.Font("Verdana", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings_Windows_Label_Settings.Location = new System.Drawing.Point(118, 33);
            this.Settings_Windows_Label_Settings.Name = "Settings_Windows_Label_Settings";
            this.Settings_Windows_Label_Settings.Size = new System.Drawing.Size(59, 45);
            this.Settings_Windows_Label_Settings.TabIndex = 11;
            this.Settings_Windows_Label_Settings.Text = "...";
            // 
            // GameLog_suchen
            // 
            this.GameLog_suchen.Location = new System.Drawing.Point(6, 19);
            this.GameLog_suchen.Name = "GameLog_suchen";
            this.GameLog_suchen.Size = new System.Drawing.Size(116, 33);
            this.GameLog_suchen.TabIndex = 12;
            this.GameLog_suchen.Text = "GameLog suchen";
            this.GameLog_suchen.UseVisualStyleBackColor = true;
            this.GameLog_suchen.Click += new System.EventHandler(this.GameLog_suchen_Click);
            // 
            // GroupBox_Diagnostic
            // 
            this.GroupBox_Diagnostic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Diagnostic.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.GroupBox_Diagnostic.Controls.Add(this.Reg_Reset);
            this.GroupBox_Diagnostic.Controls.Add(this.VTC_Button);
            this.GroupBox_Diagnostic.Controls.Add(this.Registry_anzeigen);
            this.GroupBox_Diagnostic.Controls.Add(this.GameLog_oeffnen);
            this.GroupBox_Diagnostic.Controls.Add(this.GameLog_suchen);
            this.GroupBox_Diagnostic.Cursor = System.Windows.Forms.Cursors.Help;
            this.GroupBox_Diagnostic.Location = new System.Drawing.Point(12, 482);
            this.GroupBox_Diagnostic.Name = "GroupBox_Diagnostic";
            this.GroupBox_Diagnostic.Size = new System.Drawing.Size(823, 58);
            this.GroupBox_Diagnostic.TabIndex = 13;
            this.GroupBox_Diagnostic.TabStop = false;
            this.GroupBox_Diagnostic.Text = "Diagnose-Daten";
            // 
            // Reg_Reset
            // 
            this.Reg_Reset.Location = new System.Drawing.Point(243, 20);
            this.Reg_Reset.Name = "Reg_Reset";
            this.Reg_Reset.Size = new System.Drawing.Size(85, 31);
            this.Reg_Reset.TabIndex = 16;
            this.Reg_Reset.Text = "REG Reset";
            this.Reg_Reset.UseVisualStyleBackColor = true;
            this.Reg_Reset.Click += new System.EventHandler(this.Reg_Reset_Click);
            // 
            // VTC_Button
            // 
            this.VTC_Button.Location = new System.Drawing.Point(338, 19);
            this.VTC_Button.Name = "VTC_Button";
            this.VTC_Button.Size = new System.Drawing.Size(117, 33);
            this.VTC_Button.TabIndex = 15;
            this.VTC_Button.Text = "DLC Log erstellen";
            this.VTC_Button.UseVisualStyleBackColor = true;
            this.VTC_Button.Click += new System.EventHandler(this.VTC_Button_Click);
            // 
            // Registry_anzeigen
            // 
            this.Registry_anzeigen.Location = new System.Drawing.Point(457, 19);
            this.Registry_anzeigen.Name = "Registry_anzeigen";
            this.Registry_anzeigen.Size = new System.Drawing.Size(140, 33);
            this.Registry_anzeigen.TabIndex = 14;
            this.Registry_anzeigen.Text = "Registry Werte anzeigen";
            this.Registry_anzeigen.UseVisualStyleBackColor = true;
            this.Registry_anzeigen.Click += new System.EventHandler(this.Registry_anzeigen_Click);
            // 
            // GameLog_oeffnen
            // 
            this.GameLog_oeffnen.Location = new System.Drawing.Point(128, 19);
            this.GameLog_oeffnen.Name = "GameLog_oeffnen";
            this.GameLog_oeffnen.Size = new System.Drawing.Size(107, 33);
            this.GameLog_oeffnen.TabIndex = 13;
            this.GameLog_oeffnen.Text = "GameLog öffnen";
            this.GameLog_oeffnen.UseVisualStyleBackColor = true;
            this.GameLog_oeffnen.Click += new System.EventHandler(this.GameLog_oeffnen_Click);
            // 
            // Diagnostic_Checkbox
            // 
            this.Diagnostic_Checkbox.AutoSize = true;
            this.Diagnostic_Checkbox.Location = new System.Drawing.Point(13, 569);
            this.Diagnostic_Checkbox.Name = "Diagnostic_Checkbox";
            this.Diagnostic_Checkbox.Size = new System.Drawing.Size(76, 17);
            this.Diagnostic_Checkbox.TabIndex = 14;
            this.Diagnostic_Checkbox.Text = "Diagnostic";
            this.Diagnostic_Checkbox.UseVisualStyleBackColor = true;
            this.Diagnostic_Checkbox.CheckedChanged += new System.EventHandler(this.Diagnostic_Checkbox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Autostart_Checkbox);
            this.groupBox2.Controls.Add(this.Chk_Dashboard);
            this.groupBox2.Location = new System.Drawing.Point(12, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 100);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sonstiges";
            // 
            // Autostart_Checkbox
            // 
            this.Autostart_Checkbox.AutoSize = true;
            this.Autostart_Checkbox.Location = new System.Drawing.Point(10, 44);
            this.Autostart_Checkbox.Name = "Autostart_Checkbox";
            this.Autostart_Checkbox.Size = new System.Drawing.Size(171, 17);
            this.Autostart_Checkbox.TabIndex = 1;
            this.Autostart_Checkbox.Text = "Programm mit Windows starten";
            this.Autostart_Checkbox.UseVisualStyleBackColor = true;
            this.Autostart_Checkbox.CheckedChanged += new System.EventHandler(this.Autostart_Checkbox_CheckedChanged);
            // 
            // Chk_Dashboard
            // 
            this.Chk_Dashboard.AutoSize = true;
            this.Chk_Dashboard.Location = new System.Drawing.Point(10, 20);
            this.Chk_Dashboard.Name = "Chk_Dashboard";
            this.Chk_Dashboard.Size = new System.Drawing.Size(124, 17);
            this.Chk_Dashboard.TabIndex = 0;
            this.Chk_Dashboard.Text = "Dashboard anzeigen";
            this.Chk_Dashboard.UseVisualStyleBackColor = true;
            this.Chk_Dashboard.CheckedChanged += new System.EventHandler(this.Chk_Dashboard_CheckedChanged);
            // 
            // ETS2_folderBrowserDialog
            // 
            this.ETS2_folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // patreon_image
            // 
            this.patreon_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.patreon_image.Location = new System.Drawing.Point(12, 429);
            this.patreon_image.Name = "patreon_image";
            this.patreon_image.Padding = new System.Windows.Forms.Padding(10);
            this.patreon_image.Size = new System.Drawing.Size(52, 50);
            this.patreon_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.patreon_image.TabIndex = 16;
            this.patreon_image.TabStop = false;
            // 
            // team_image
            // 
            this.team_image.Location = new System.Drawing.Point(70, 429);
            this.team_image.Name = "team_image";
            this.team_image.Size = new System.Drawing.Size(52, 50);
            this.team_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.team_image.TabIndex = 17;
            this.team_image.TabStop = false;
            // 
            // Group_Box_Indiv_Texte
            // 
            this.Group_Box_Indiv_Texte.Controls.Add(this.checkBox_NUM_LOCK);
            this.Group_Box_Indiv_Texte.Controls.Add(this.label14);
            this.Group_Box_Indiv_Texte.Controls.Add(this.label13);
            this.Group_Box_Indiv_Texte.Controls.Add(this.label12);
            this.Group_Box_Indiv_Texte.Controls.Add(this.Num3_Text);
            this.Group_Box_Indiv_Texte.Controls.Add(this.label11);
            this.Group_Box_Indiv_Texte.Controls.Add(this.Num2_Text);
            this.Group_Box_Indiv_Texte.Controls.Add(this.label10);
            this.Group_Box_Indiv_Texte.Controls.Add(this.Num1_Text);
            this.Group_Box_Indiv_Texte.Location = new System.Drawing.Point(288, 322);
            this.Group_Box_Indiv_Texte.Name = "Group_Box_Indiv_Texte";
            this.Group_Box_Indiv_Texte.Size = new System.Drawing.Size(547, 156);
            this.Group_Box_Indiv_Texte.TabIndex = 18;
            this.Group_Box_Indiv_Texte.TabStop = false;
            this.Group_Box_Indiv_Texte.Text = "Spender Individual Text";
            // 
            // checkBox_NUM_LOCK
            // 
            this.checkBox_NUM_LOCK.AutoSize = true;
            this.checkBox_NUM_LOCK.Location = new System.Drawing.Point(179, 101);
            this.checkBox_NUM_LOCK.Name = "checkBox_NUM_LOCK";
            this.checkBox_NUM_LOCK.Size = new System.Drawing.Size(158, 17);
            this.checkBox_NUM_LOCK.TabIndex = 14;
            this.checkBox_NUM_LOCK.Text = "NUM Lock Button anzeigen";
            this.checkBox_NUM_LOCK.UseVisualStyleBackColor = true;
            this.checkBox_NUM_LOCK.CheckedChanged += new System.EventHandler(this.checkBox_NUM_LOCK_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(63, 101);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "jeweils max. 100 Zeichen";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(368, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(173, 23);
            this.label13.TabIndex = 12;
            this.label13.Text = "Coming Soon...";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(2, 75);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(5);
            this.label12.Size = new System.Drawing.Size(50, 25);
            this.label12.TabIndex = 11;
            this.label12.Text = "NUM3";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num3_Text
            // 
            this.Num3_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num3_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Num3_Text.Location = new System.Drawing.Point(62, 76);
            this.Num3_Text.MaxLength = 100;
            this.Num3_Text.Name = "Num3_Text";
            this.Num3_Text.Size = new System.Drawing.Size(478, 22);
            this.Num3_Text.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(2, 47);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(5);
            this.label11.Size = new System.Drawing.Size(50, 25);
            this.label11.TabIndex = 9;
            this.label11.Text = "NUM2";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num2_Text
            // 
            this.Num2_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num2_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Num2_Text.Location = new System.Drawing.Point(62, 48);
            this.Num2_Text.MaxLength = 100;
            this.Num2_Text.Name = "Num2_Text";
            this.Num2_Text.Size = new System.Drawing.Size(478, 22);
            this.Num2_Text.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(2, 19);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(5);
            this.label10.Size = new System.Drawing.Size(50, 25);
            this.label10.TabIndex = 7;
            this.label10.Text = "NUM1";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num1_Text
            // 
            this.Num1_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num1_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Num1_Text.Location = new System.Drawing.Point(62, 20);
            this.Num1_Text.MaxLength = 100;
            this.Num1_Text.Name = "Num1_Text";
            this.Num1_Text.Size = new System.Drawing.Size(478, 22);
            this.Num1_Text.TabIndex = 6;
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(847, 599);
            this.Controls.Add(this.Group_Box_Indiv_Texte);
            this.Controls.Add(this.team_image);
            this.Controls.Add(this.patreon_image);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Diagnostic_Checkbox);
            this.Controls.Add(this.GroupBox_Diagnostic);
            this.Controls.Add(this.Settings_Windows_Label_Settings);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox_AntiAFK);
            this.Controls.Add(this.group_Overlay);
            this.Controls.Add(this.btn_TruckersMP_suchen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.save_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Einstellungen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsWindow_FormClosed);
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.btn_TruckersMP_suchen.ResumeLayout(false);
            this.btn_TruckersMP_suchen.PerformLayout();
            this.group_Overlay.ResumeLayout(false);
            this.group_Overlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Overlay_Transparenz)).EndInit();
            this.groupBox_AntiAFK.ResumeLayout(false);
            this.groupBox_AntiAFK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reload_antiafk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.GroupBox_Diagnostic.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patreon_image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.team_image)).EndInit();
            this.Group_Box_Indiv_Texte.ResumeLayout(false);
            this.Group_Box_Indiv_Texte.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void save_button_Click(object sender, EventArgs e)
        {
            // #######   HOTKEYS   ################


            utils.Reg_Schreiben("NUM1", Num1_Text.Text, "TruckersMP_Autorun");
            utils.Reg_Schreiben("NUM2", Num2_Text.Text, "TruckersMP_Autorun");
            utils.Reg_Schreiben("NUM3", Num3_Text.Text, "TruckersMP_Autorun");



            // #######   HOTKEYS ENDE  ################

            if (this.comboBox1.Text == "Simulation 1")
            {
                this.selected_server_tm = "sim1";
                this.data.tmp_server = this.selected_server_tm;
                // Edit by Thommy

                utils.Reg_Schreiben("verkehr_SERVER", "sim1", "TruckersMP_Autorun");

            }
            else if (this.comboBox1.Text == "Simulation 2")
            {
                this.selected_server_tm = "sim2";
                this.data.tmp_server = this.selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "sim2", "TruckersMP_Autorun");

            }
            else if (this.comboBox1.Text == "Arcade")
            {
                this.selected_server_tm = "arc1";
                this.data.tmp_server = this.selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "arc1", "TruckersMP_Autorun");
            }
            else if (this.comboBox1.Text == "EU Promods 1")
            {
                this.selected_server_tm = "eupromods1";
                this.data.tmp_server = this.selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "eupromods1", "TruckersMP_Autorun");
            }
            else if (this.comboBox1.Text == "EU Promods 2")
            {
                this.selected_server_tm = "eupromods2";
                this.data.tmp_server = this.selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "eupromods2", "TruckersMP_Autorun");
            }


            // ANTI_AFK
            // TODO-- SPENDER 


            utils.Reg_Schreiben("ANTI_AFK", txt_Anti_AFK_Text.Text, "TruckersMP_Autorun");
            if (chk_antiafk_on_off.CheckState == CheckState.Checked)
            {
                if (txt_Anti_AFK_Text.Text == "")
                {
                    utils.Reg_Schreiben("ANTI_AFK", "VTCManager wünscht Gute und Sichere Fahrt!", "TruckersMP_Autorun");
                }
                utils.Reg_Schreiben("ANTI_AFK_AN", "1", "TruckersMP_Autorun");
            }
            else
            {
                utils.Reg_Schreiben("ANTI_AFK_AN", "0", "TruckersMP_Autorun");
            }
            this.Close();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            GroupBox_Diagnostic.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "Diagnostic") == "1") ? true : false;
            checkBox_NUM_LOCK.CheckState = (utils.Reg_Lesen("TruckersMP_Autorun", "NUM_LOCK_SHOW") == "1") ? CheckState.Checked : CheckState.Unchecked;
            Chk_Dashboard.CheckState = (utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard") == "1") ? CheckState.Checked : CheckState.Unchecked;
            Group_Box_Indiv_Texte.Visible = (Patreon >= 2) ? true : false;


            // ##################   HOTKEYS   ######################
            Num1_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM1");
            Num2_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM2");
            Num3_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM3");
            // ##################  AUTOSTART   #####################
            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "VTCManager.appref-ms")))
            {
                utils.Reg_Schreiben("Autostart", "1", "TruckersMP_Autorun");
                Autostart_Checkbox.CheckState = CheckState.Checked;
            } else
            {
                utils.Reg_Schreiben("Autostart", "0", "TruckersMP_Autorun");
                Autostart_Checkbox.CheckState = CheckState.Unchecked;
            }

                group_Overlay.Visible = false;
            // Settings_Windows_Label_Settings.Text = translation.settings_window_titel_text; ######### GEHT NICHT ############
            Settings_Windows_Label_Settings.Text = "Einstellungen";

            // #############  DASHBOARD ############################
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard")))
                utils.Reg_Schreiben("Dashboard", "0", "TruckersMP_Autorun");


   
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad")))
                MessageBox.Show("der Pfad zu TruckersMP stimmt nicht" + Environment.NewLine + "Bitte korrigiere diesen im folgenden Fenster", "Fehler TruckersMP", MessageBoxButtons.OK, MessageBoxIcon.Error);
       

            string wert27 = utils.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER");
            string wert28 = utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            string wert30 = utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN");
            string wert31 = utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK");

            ETS2_Pfad_Textbox.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            ETS2_Pfad_Textbox.Enabled = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad"))) ? true : false;

            ATS_Pfad_Textbox.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            ATS_Pfad_Textbox.Enabled = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad"))) ? true : false;

            if (wert28 != null)
            {
                truckersMP_Pfad_TextBox.Text = wert28;
                truckersMP_Pfad_TextBox.Enabled = false;
            }
            else
            {
                truckersMP_Pfad_TextBox.Text = "";
            }

            // Server COMBO vorauswahl
            if (wert27 == null) { comboBox1.Text = "Simulation 1"; utils.Reg_Schreiben("verkehr_SERVER", "sim1", "TruckersMP_Autorun"); }
            if (wert27 == "sim1") { comboBox1.Text = "Simulation 1"; }
            if (wert27 == "sim2") { comboBox1.Text = "Simulation 2"; }
            if (wert27 == "arc1") { comboBox1.Text = "Arcade 1"; }
            if (wert27 == "eupromods1") { comboBox1.Text = "EU Promods 1"; }
            if (wert27 == "eupromods2") { comboBox1.Text = "EU Promods 2"; }


            // ANTI_AFK
            chk_antiafk_on_off.CheckState = (wert30 == "1") ? CheckState.Checked : CheckState.Unchecked;
            txt_Anti_AFK_Text.Text = (wert31 != null) ? wert31.ToString() : "";

            // Listbox mit Bildschirmen füllen

            combo_Bildschirme.Items.Clear();
            combo_Bildschirme.Items.Add(Screen.AllScreens.GetUpperBound(0));
            combo_Bildschirme.Items.Add(Screen.AllScreens[0].DeviceName);
            if (Screen.AllScreens.GetUpperBound(0) == 1)
            {
                combo_Bildschirme.Items.Add(Screen.AllScreens[1].DeviceName);
            }
            if (Screen.AllScreens.GetUpperBound(0) == 2)
            {
                combo_Bildschirme.Items.Add(Screen.AllScreens[2].DeviceName);
            }
            if (Screen.AllScreens.GetUpperBound(0) == 3)
            {
                combo_Bildschirme.Items.Add(Screen.AllScreens[3].DeviceName);
            }

            // Variablen abrufen von Main
            txt_Anti_AFK_Text.Enabled = (Patreon >= 2) ? true : false;

            switch (Patreon)
            {
                case 1:
                    patreon_image.Image = Properties.Resources.pat1;
                    ToolTip tip1 = new ToolTip();
                    tip1.SetToolTip(patreon_image, "Patreon Level 1");
                    break;
                case 2:
                    patreon_image.Image = Properties.Resources.pat2;
                    ToolTip tip2 = new ToolTip();
                    tip2.SetToolTip(patreon_image, "Patreon Level 2");
                    break;
                case 3:
                    patreon_image.Image = Properties.Resources.pat3;
                    ToolTip tip3 = new ToolTip();
                    tip3.SetToolTip(patreon_image, "Patreon Level 3 -> Vielen Dank !");
                    break;
                case 0:
                    patreon_image.Visible = false;
                    break;
            }
            

        }





        private void SettingsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            tmp_Trucker.InitialDirectory =
            tmp_Trucker.Filter = "TruckersMP Launcher (launcher.exe)|launcher.exe|Alle Dateien (*.*)|*.*";
            if (tmp_Trucker.ShowDialog() == DialogResult.OK)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("TruckersMP_Pfad", tmp_Trucker.FileName, "TruckersMP_Autorun");
                truckersMP_Pfad_TextBox.Text = tmp_Trucker.FileName.ToString();
            }

        }

        private void Ets_Suche_Click(object sender, EventArgs e)
        {
            ETS2_folderBrowserDialog.SelectedPath = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            if (ETS2_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ETS2_Pfad_Textbox.Text = ETS2_folderBrowserDialog.SelectedPath.ToString();
                ETS2_Pfad_Textbox.Enabled = false;
                utils.Reg_Schreiben("ETS2_Pfad", ETS2_folderBrowserDialog.SelectedPath.ToString(), "TruckersMP_Autorun");
            }
        }

        private void Ats_Suche_Click(object sender, EventArgs e)
        {
            ATS_folderBrowserDialog.SelectedPath = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad")))
                ATS_folderBrowserDialog.SelectedPath = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");

            if (ATS_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ATS_Pfad_Textbox.Text = ATS_folderBrowserDialog.SelectedPath.ToString();
                ATS_Pfad_Textbox.Enabled = false;
                utils.Reg_Schreiben("ATS_Pfad", ATS_folderBrowserDialog.SelectedPath.ToString(), "TruckersMP_Autorun");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) =>
            utils.Reg_Schreiben("ANTI_AFK_RELOAD", reload_antiafk.Value.ToString(), "TruckersMP_Autorun");

        private void chk_antiafk_on_off_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_antiafk_on_off.CheckState == CheckState.Checked)
            {
                if (txt_Anti_AFK_Text.Text == "")
                    utils.Reg_Schreiben("ANTI_AFK", "VTCManager wünscht Gute und Sichere Fahrt!", "TruckersMP_Autorun");

                utils.Reg_Schreiben("ANTI_AFK_AN", "1", "TruckersMP_Autorun");
                utils.Reg_Schreiben("ANTI_AFK_RELOAD", reload_antiafk.Value.ToString(), "TruckersMP_Autorun");
            }
            else
            {
                utils.Reg_Schreiben("ANTI_AFK_AN", "0", "TruckersMP_Autorun");
                utils.Reg_Schreiben("ANTI_AFK_RELOAD", reload_antiafk.Value.ToString(), "TruckersMP_Autorun");
            }
        }

        private void GameLog_suchen_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2"))
                    System.Diagnostics.Process.Start("explorer", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2");
            }
            catch (Exception ex)
            {
                Logging.WriteLOG("<ERROR> Methode GameLog_suchen_Click in SettingsWindow.cs -> " + ex.Message + " [SettingsWindow.cs->807]");
                MessageBox.Show("Der Pfad zum GameLog Ordner wurde nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }

        private void Diagnostic_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Diagnostic_Checkbox.CheckState == CheckState.Checked)
            {
                utils.Reg_Schreiben("Diagnostic", "1", "TruckersMP_Autorun");
                GroupBox_Diagnostic.Visible = true;
            }
            else
            {
                utils.Reg_Schreiben("Diagnostic", "0", "TruckersMP_Autorun");
                GroupBox_Diagnostic.Visible = false;
            }
        }

        private void GameLog_oeffnen_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\game.log.txt"))
                    System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\game.log.txt");
            }
            catch (Exception ex) 
            {
                Logging.WriteLOG("<ERROR> Methode GameLog_oeffnen_Click in SettingsWindow.cs -> " + ex.Message + " [SettingsWindow.cs->836]");
                MessageBox.Show("Der Pfad zur GameLog wurde nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }

        private void Registry_anzeigen_Click(object sender, EventArgs e)
        {
           MessageBox.Show( "ANTI-AFK: " + Read("ANTI_AFK") + Environment.NewLine +
               "ANTI_AFK_AN: " + Read("ANTI_AFK_AN") + Environment.NewLine +
               "Background: " + Read("Background") + Environment.NewLine +
               "Diagnostic: " + Read("Diagnostic") + Environment.NewLine +
               "ETS Pfad: " + Read("ETS2_Pfad") + Environment.NewLine +
               "ATS Pfad: " + Read("ATS_Pfad") + Environment.NewLine +
               "Plugins ETS: " + Read("Plugins ETS") + Environment.NewLine +
               "Plugins ATS: " + Read("Plugins ATS") + Environment.NewLine +
               "Reload Traffic Sek: " + Read("Reload_Traffic_Sekunden") + Environment.NewLine +
               "Verkehr Server: " + Read("verkehr_SERVER") + Environment.NewLine +
               "Version: " + Read("Version") + Environment.NewLine);
        }



        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(@"Software\VTCManager\TruckersMP_Autorun");
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    Logging.WriteLOG("<ERROR> Methode Read in SettingsWindow.cs -> " + e.Message + " [SettingsWindow.cs->881]");
                    MessageBox.Show(e.Message, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        private void Chk_Dashboard_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Dashboard.CheckState == CheckState.Checked)
            {
                utils.Reg_Schreiben("Dashboard", "1", "TruckersMP_Autorun");
            }
            else
            {
                utils.Reg_Schreiben("Dashboard", "0", "TruckersMP_Autorun");
            }
        }

        private void VTC_Button_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\DLC_Log.txt");

            DirectoryInfo d = new DirectoryInfo(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad")); 
            FileInfo[] Files = d.GetFiles("dlc_*"); 
            List<string> myCollection = new List<string>();
            foreach (FileInfo file in Files)
            {
                myCollection.Add(file.Name + Environment.NewLine);
            }
            string a = String.Join("", myCollection);

            File.WriteAllText(path, a);

            DirectoryInfo d2 = new DirectoryInfo(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") + @"\bin\win_x64");
            FileInfo[] Files2 = d2.GetFiles("*.*");
            List<string> myCollection2 = new List<string>();
            foreach (FileInfo file2 in Files2)
            {
                myCollection2.Add(file2.Name + Environment.NewLine);
            }
            string a2 = String.Join("", myCollection2);
            File.AppendAllText(path, " ------------------------------    GAME VERZEICHNIS   ------------------------------------" + Environment.NewLine);
            File.AppendAllText(path, a2);


            DirectoryInfo d3 = new DirectoryInfo(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") + @"\bin\win_x64\plugins");
            FileInfo[] Files3 = d3.GetFiles("*.*");
            List<string> myCollection3 = new List<string>();
            foreach (FileInfo file3 in Files3)
            {
                myCollection3.Add(file3.Name + Environment.NewLine);
            }
            string a3 = String.Join("", myCollection3);
            File.AppendAllText(path, " ------------------------------    PLUGIN VERZEICHNIS   ------------------------------------" + Environment.NewLine);
            File.AppendAllText(path, a3);

            File.AppendAllText(path, " ------------------------------        REGISTRY         ------------------------------------" + Environment.NewLine);
            File.AppendAllText(path, "ANTI-AFK: " + Read("ANTI_AFK") + Environment.NewLine +
                "ANTI_AFK_AN: " + Read("ANTI_AFK_AN") + Environment.NewLine +
                "Background: " + Read("Background") + Environment.NewLine +
                "Diagnostic: " + Read("Diagnostic") + Environment.NewLine +
                "ETS Pfad: " + Read("ETS2_Pfad") + Environment.NewLine +
                "ATS Pfad: " + Read("ATS_Pfad") + Environment.NewLine +
                "Plugins ETS: " + Read("Plugins ETS") + Environment.NewLine +
                "Plugins ATS: " + Read("Plugins ATS") + Environment.NewLine +
                "Reload Traffic Sek: " + Read("Reload_Traffic_Sekunden") + Environment.NewLine +
                "Verkehr Server: " + Read("verkehr_SERVER") + Environment.NewLine +
                "Version: " + Read("Version") + Environment.NewLine);

        }

        private void Autostart_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(Autostart_Checkbox.CheckState == CheckState.Checked)
            {
                addAutoStartRegistry();
    
            } else
            {
                delAutoStartRegistry();
            }
            
        }

        public static void addAutoStartRegistry()
        {
            if (!File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "VTCManager.appref-ms")))
            {
                System.IO.File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\VTCManager.appref-ms", System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "VTCManager.appref-ms"));
                Utilities ut = new Utilities();
                ut.Reg_Schreiben("Autostart", "1", "TruckersMP_Autorun");
                MessageBox.Show("Dein Programm wird jetzt beim Systemstart ausgeführt !", "AutoRun", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static void delAutoStartRegistry()
        {
            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "VTCManager.appref-ms")))
            {
                File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "VTCManager.appref-ms"));
                Utilities ut = new Utilities();
                ut.Reg_Schreiben("Autostart", "0", "TruckersMP_Autorun");
                MessageBox.Show("Dein Programm wird jetzt nicht mehr beim Systemstart ausgeführt !", "AutoRun", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Reg_Reset_Click(object sender, EventArgs e)
        {
            if (SubKeyExist(@"Software\VTCManager"))
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\VTCManager");
                MessageBox.Show("Alle Einstellungen wurden entfernt!" + Environment.NewLine + Environment.NewLine + "Dein Client wird jetzt Neu gestartet... " + Environment.NewLine + "Thommy - VTCM_DEV", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Es sind keine Einstellungen vorhanden!" + Environment.NewLine + "Bei Fragen wende dich an den Support des VTCM-Client" + Environment.NewLine + Environment.NewLine + "Thommy - VTCM_DEV", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SubKeyExist(string Subkey)
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(Subkey);
            if (myKey == null)
                return false;
            else
                return true;
        }

        private void checkBox_NUM_LOCK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_NUM_LOCK.CheckState == CheckState.Checked)
            {
                utils.Reg_Schreiben("NUM_LOCK_SHOW", "1", "TruckersMP_Autorun");
            }
            else
            {
                utils.Reg_Schreiben("NUM_LOCK_SHOW", "0", "TruckersMP_Autorun");
            }
        }
    }
}

