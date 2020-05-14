using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using VTCManager_1._0._0.Klassen;

namespace VTCManager_1._0._0
{
    class SettingsWindow : Form
    {
        public Utilities utils = new Utilities();
        Logging Logging = new Logging();

        private ComboBox comboBox1;
        private Label label1;
        private SettingsManager data;
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
        private Button GameLog_oeffnen;
        private CheckBox Diagnostic_Checkbox;
        private GroupBox groupBox2;
        private CheckBox Chk_Dashboard;
        private CheckBox Autostart_Checkbox;
        private FolderBrowserDialog ETS2_folderBrowserDialog;
        private FolderBrowserDialog ATS_folderBrowserDialog;
        private Button Reg_Reset;
        private PictureBox patreon_image;
        private PictureBox team_image;
        private GroupBox Group_Box_Indiv_Texte;
        private Label label12;
        private TextBox Num3_Text;
        private Label label11;
        private TextBox Num2_Text;
        private Label label10;
        private TextBox Num1_Text;
        private Label label14;
        private CheckBox checkBox_NUM_LOCK;
        private Button button2;
        private ImageList Image_List_1;
        private System.ComponentModel.IContainer components;
        private CheckBox chk_Programm_Ontop;
        private CheckBox discordrpccheckbox;
        public int Patreon;
        private bool restart_required;

        public SettingsWindow(Translation translation, int patreon)
        {
            data = new SettingsManager();

            InitializeComponent();
            save_button.Text = translation.settings_window_save_button;
            groupBox1.Text = translation.settings_window_groupBox1text;
            btn_TruckersMP_suchen.Text = translation.btn_TruckersMP_suchentext;
            label3.Text = translation.settings_window_label3text;
            comboBox1.Text = data.tmp_server;
            Patreon = patreon;
            checkBox_NUM_LOCK.Text = translation.Settings_CheckBox_NUMPAD_ONOFF;
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            comboBox1 = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            save_button = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btn_TruckersMP_suchen = new System.Windows.Forms.GroupBox();
            Ats_Suche = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            ATS_Pfad_Textbox = new System.Windows.Forms.TextBox();
            Ets_Suche = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            ETS2_Pfad_Textbox = new System.Windows.Forms.TextBox();
            truckersMP_Pfad_TextBox = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            group_Overlay = new System.Windows.Forms.GroupBox();
            label6 = new System.Windows.Forms.Label();
            num_Overlay_Transparenz = new System.Windows.Forms.NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            combo_Bildschirme = new System.Windows.Forms.ComboBox();
            tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            groupBox_AntiAFK = new System.Windows.Forms.GroupBox();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            reload_antiafk = new System.Windows.Forms.NumericUpDown();
            chk_antiafk_on_off = new System.Windows.Forms.CheckBox();
            txt_Anti_AFK_Text = new System.Windows.Forms.TextBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            Settings_Windows_Label_Settings = new System.Windows.Forms.Label();
            GameLog_suchen = new System.Windows.Forms.Button();
            Image_List_1 = new System.Windows.Forms.ImageList(components);
            GroupBox_Diagnostic = new System.Windows.Forms.GroupBox();
            button2 = new System.Windows.Forms.Button();
            Reg_Reset = new System.Windows.Forms.Button();
            GameLog_oeffnen = new System.Windows.Forms.Button();
            Diagnostic_Checkbox = new System.Windows.Forms.CheckBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            discordrpccheckbox = new System.Windows.Forms.CheckBox();
            chk_Programm_Ontop = new System.Windows.Forms.CheckBox();
            Autostart_Checkbox = new System.Windows.Forms.CheckBox();
            Chk_Dashboard = new System.Windows.Forms.CheckBox();
            ETS2_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ATS_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            patreon_image = new System.Windows.Forms.PictureBox();
            team_image = new System.Windows.Forms.PictureBox();
            Group_Box_Indiv_Texte = new System.Windows.Forms.GroupBox();
            checkBox_NUM_LOCK = new System.Windows.Forms.CheckBox();
            label14 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            Num3_Text = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            Num2_Text = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            Num1_Text = new System.Windows.Forms.TextBox();
            groupBox1.SuspendLayout();
            btn_TruckersMP_suchen.SuspendLayout();
            group_Overlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(num_Overlay_Transparenz)).BeginInit();
            groupBox_AntiAFK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(reload_antiafk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            GroupBox_Diagnostic.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(patreon_image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(team_image)).BeginInit();
            Group_Box_Indiv_Texte.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] {
            "Simulation 1",
            "Simulation 2",
            "Arcade",
            "EU Promods 1",
            "EU Promods 2"});
            comboBox1.Location = new System.Drawing.Point(171, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(136, 21);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Simulation 1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(102, 13);
            label1.TabIndex = 1;
            label1.Text = "TruckersMP-Server:";
            // 
            // save_button
            // 
            save_button.Location = new System.Drawing.Point(686, 545);
            save_button.Name = "save_button";
            save_button.Size = new System.Drawing.Size(149, 41);
            save_button.TabIndex = 3;
            save_button.Text = "Einstellungen Speichern...";
            save_button.UseVisualStyleBackColor = true;
            save_button.Click += new System.EventHandler(save_button_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Location = new System.Drawing.Point(288, 129);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(321, 62);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server Einstellungen";
            // 
            // btn_TruckersMP_suchen
            // 
            btn_TruckersMP_suchen.Controls.Add(Ats_Suche);
            btn_TruckersMP_suchen.Controls.Add(label4);
            btn_TruckersMP_suchen.Controls.Add(ATS_Pfad_Textbox);
            btn_TruckersMP_suchen.Controls.Add(Ets_Suche);
            btn_TruckersMP_suchen.Controls.Add(label2);
            btn_TruckersMP_suchen.Controls.Add(ETS2_Pfad_Textbox);
            btn_TruckersMP_suchen.Controls.Add(truckersMP_Pfad_TextBox);
            btn_TruckersMP_suchen.Controls.Add(button1);
            btn_TruckersMP_suchen.Controls.Add(label3);
            btn_TruckersMP_suchen.Location = new System.Drawing.Point(12, 129);
            btn_TruckersMP_suchen.Name = "btn_TruckersMP_suchen";
            btn_TruckersMP_suchen.Size = new System.Drawing.Size(270, 186);
            btn_TruckersMP_suchen.TabIndex = 6;
            btn_TruckersMP_suchen.TabStop = false;
            btn_TruckersMP_suchen.Text = "Game und Multiplayer";
            // 
            // Ats_Suche
            // 
            Ats_Suche.Location = new System.Drawing.Point(241, 128);
            Ats_Suche.Name = "Ats_Suche";
            Ats_Suche.Size = new System.Drawing.Size(25, 22);
            Ats_Suche.TabIndex = 16;
            Ats_Suche.Text = "...";
            Ats_Suche.UseVisualStyleBackColor = true;
            Ats_Suche.Click += new System.EventHandler(Ats_Suche_Click);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(7, 114);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(70, 13);
            label4.TabIndex = 15;
            label4.Text = "Pfad zu ATS:";
            // 
            // ATS_Pfad_Textbox
            // 
            ATS_Pfad_Textbox.Location = new System.Drawing.Point(7, 130);
            ATS_Pfad_Textbox.Name = "ATS_Pfad_Textbox";
            ATS_Pfad_Textbox.Size = new System.Drawing.Size(228, 20);
            ATS_Pfad_Textbox.TabIndex = 14;
            // 
            // Ets_Suche
            // 
            Ets_Suche.Location = new System.Drawing.Point(241, 81);
            Ets_Suche.Name = "Ets_Suche";
            Ets_Suche.Size = new System.Drawing.Size(25, 22);
            Ets_Suche.TabIndex = 13;
            Ets_Suche.Text = "...";
            Ets_Suche.UseVisualStyleBackColor = true;
            Ets_Suche.Click += new System.EventHandler(Ets_Suche_Click);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 67);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(79, 13);
            label2.TabIndex = 12;
            label2.Text = "Pfad zu ETS 2:";
            // 
            // ETS2_Pfad_Textbox
            // 
            ETS2_Pfad_Textbox.Location = new System.Drawing.Point(7, 83);
            ETS2_Pfad_Textbox.Name = "ETS2_Pfad_Textbox";
            ETS2_Pfad_Textbox.Size = new System.Drawing.Size(228, 20);
            ETS2_Pfad_Textbox.TabIndex = 11;
            // 
            // truckersMP_Pfad_TextBox
            // 
            truckersMP_Pfad_TextBox.Location = new System.Drawing.Point(7, 37);
            truckersMP_Pfad_TextBox.Name = "truckersMP_Pfad_TextBox";
            truckersMP_Pfad_TextBox.Size = new System.Drawing.Size(228, 20);
            truckersMP_Pfad_TextBox.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(241, 36);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(25, 22);
            button1.TabIndex = 9;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(7, 20);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(107, 13);
            label3.TabIndex = 0;
            label3.Text = "Pfad zu TruckersMP:";
            // 
            // group_Overlay
            // 
            group_Overlay.Controls.Add(label6);
            group_Overlay.Controls.Add(num_Overlay_Transparenz);
            group_Overlay.Controls.Add(label5);
            group_Overlay.Controls.Add(combo_Bildschirme);
            group_Overlay.Location = new System.Drawing.Point(551, 12);
            group_Overlay.Name = "group_Overlay";
            group_Overlay.Size = new System.Drawing.Size(270, 100);
            group_Overlay.TabIndex = 7;
            group_Overlay.TabStop = false;
            group_Overlay.Text = "Overlay Einstellungen";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(131, 56);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(69, 13);
            label6.TabIndex = 3;
            label6.Text = "Transparenz:";
            // 
            // num_Overlay_Transparenz
            // 
            num_Overlay_Transparenz.Location = new System.Drawing.Point(206, 54);
            num_Overlay_Transparenz.Name = "num_Overlay_Transparenz";
            num_Overlay_Transparenz.Size = new System.Drawing.Size(59, 20);
            num_Overlay_Transparenz.TabIndex = 2;
            num_Overlay_Transparenz.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(10, 31);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(84, 13);
            label5.TabIndex = 1;
            label5.Text = "Gaming-Monitor:";
            // 
            // combo_Bildschirme
            // 
            combo_Bildschirme.FormattingEnabled = true;
            combo_Bildschirme.Location = new System.Drawing.Point(96, 28);
            combo_Bildschirme.Name = "combo_Bildschirme";
            combo_Bildschirme.Size = new System.Drawing.Size(170, 21);
            combo_Bildschirme.TabIndex = 0;
            // 
            // tmp_Trucker
            // 
            tmp_Trucker.FileName = "T";
            // 
            // groupBox_AntiAFK
            // 
            groupBox_AntiAFK.Controls.Add(label9);
            groupBox_AntiAFK.Controls.Add(label8);
            groupBox_AntiAFK.Controls.Add(label7);
            groupBox_AntiAFK.Controls.Add(reload_antiafk);
            groupBox_AntiAFK.Controls.Add(chk_antiafk_on_off);
            groupBox_AntiAFK.Controls.Add(txt_Anti_AFK_Text);
            groupBox_AntiAFK.Location = new System.Drawing.Point(288, 198);
            groupBox_AntiAFK.Name = "groupBox_AntiAFK";
            groupBox_AntiAFK.Size = new System.Drawing.Size(547, 117);
            groupBox_AntiAFK.TabIndex = 9;
            groupBox_AntiAFK.TabStop = false;
            groupBox_AntiAFK.Text = "Anti - AFK";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(10, 19);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(31, 13);
            label9.TabIndex = 5;
            label9.Text = "Text:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(495, 70);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(38, 13);
            label8.TabIndex = 4;
            label8.Text = "in Min.";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(395, 70);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(44, 13);
            label7.TabIndex = 3;
            label7.Text = "Reload:";
            // 
            // reload_antiafk
            // 
            reload_antiafk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            reload_antiafk.Location = new System.Drawing.Point(441, 66);
            reload_antiafk.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            reload_antiafk.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            reload_antiafk.Name = "reload_antiafk";
            reload_antiafk.Size = new System.Drawing.Size(52, 20);
            reload_antiafk.TabIndex = 2;
            reload_antiafk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            reload_antiafk.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            reload_antiafk.ValueChanged += new System.EventHandler(numericUpDown1_ValueChanged);
            // 
            // chk_antiafk_on_off
            // 
            chk_antiafk_on_off.AutoSize = true;
            chk_antiafk_on_off.Location = new System.Drawing.Point(10, 63);
            chk_antiafk_on_off.Name = "chk_antiafk_on_off";
            chk_antiafk_on_off.Size = new System.Drawing.Size(106, 17);
            chk_antiafk_on_off.TabIndex = 1;
            chk_antiafk_on_off.Text = "Anti AFK An/Aus";
            chk_antiafk_on_off.UseVisualStyleBackColor = true;
            chk_antiafk_on_off.CheckedChanged += new System.EventHandler(chk_antiafk_on_off_CheckedChanged);
            // 
            // txt_Anti_AFK_Text
            // 
            txt_Anti_AFK_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt_Anti_AFK_Text.Enabled = false;
            txt_Anti_AFK_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txt_Anti_AFK_Text.Location = new System.Drawing.Point(10, 33);
            txt_Anti_AFK_Text.Name = "txt_Anti_AFK_Text";
            txt_Anti_AFK_Text.Size = new System.Drawing.Size(523, 22);
            txt_Anti_AFK_Text.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = global::VTCManager_1._0._0.Properties.Resources.einstellungen;
            pictureBox1.Location = new System.Drawing.Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(100, 100);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // Settings_Windows_Label_Settings
            // 
            Settings_Windows_Label_Settings.AutoSize = true;
            Settings_Windows_Label_Settings.Font = new System.Drawing.Font("Verdana", 27.75F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, 0);
            Settings_Windows_Label_Settings.Location = new System.Drawing.Point(118, 33);
            Settings_Windows_Label_Settings.Name = "Settings_Windows_Label_Settings";
            Settings_Windows_Label_Settings.Size = new System.Drawing.Size(59, 45);
            Settings_Windows_Label_Settings.TabIndex = 11;
            Settings_Windows_Label_Settings.Text = "...";
            // 
            // GameLog_suchen
            // 
            GameLog_suchen.BackColor = System.Drawing.SystemColors.ActiveCaption;
            GameLog_suchen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            GameLog_suchen.ImageIndex = 2;
            GameLog_suchen.ImageList = Image_List_1;
            GameLog_suchen.Location = new System.Drawing.Point(6, 18);
            GameLog_suchen.Name = "GameLog_suchen";
            GameLog_suchen.Size = new System.Drawing.Size(116, 40);
            GameLog_suchen.TabIndex = 12;
            GameLog_suchen.Text = "GameLog suchen";
            GameLog_suchen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            GameLog_suchen.UseVisualStyleBackColor = false;
            GameLog_suchen.Click += new System.EventHandler(GameLog_suchen_Click);
            // 
            // Image_List_1
            // 
            Image_List_1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Image_List_1.ImageStream")));
            Image_List_1.TransparentColor = System.Drawing.Color.Transparent;
            Image_List_1.Images.SetKeyName(0, "icons8-thunderbird-32.png");
            Image_List_1.Images.SetKeyName(1, "icons8-log-64.png");
            Image_List_1.Images.SetKeyName(2, "icons8-suche-64.png");
            // 
            // GroupBox_Diagnostic
            // 
            GroupBox_Diagnostic.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            GroupBox_Diagnostic.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            GroupBox_Diagnostic.Controls.Add(button2);
            GroupBox_Diagnostic.Controls.Add(Reg_Reset);
            GroupBox_Diagnostic.Controls.Add(GameLog_oeffnen);
            GroupBox_Diagnostic.Controls.Add(GameLog_suchen);
            GroupBox_Diagnostic.Cursor = System.Windows.Forms.Cursors.Help;
            GroupBox_Diagnostic.Location = new System.Drawing.Point(12, 476);
            GroupBox_Diagnostic.Name = "GroupBox_Diagnostic";
            GroupBox_Diagnostic.Size = new System.Drawing.Size(823, 67);
            GroupBox_Diagnostic.TabIndex = 13;
            GroupBox_Diagnostic.TabStop = false;
            GroupBox_Diagnostic.Text = "Diagnose-Daten";
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            button2.ImageIndex = 0;
            button2.ImageList = Image_List_1;
            button2.Location = new System.Drawing.Point(243, 18);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(117, 40);
            button2.TabIndex = 17;
            button2.Text = "Log senden";
            button2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            button2.UseVisualStyleBackColor = false;
            button2.Click += new System.EventHandler(button2_Click);
            // 
            // Reg_Reset
            // 
            Reg_Reset.BackColor = System.Drawing.Color.Red;
            Reg_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            Reg_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            Reg_Reset.ForeColor = System.Drawing.SystemColors.ButtonFace;
            Reg_Reset.Location = new System.Drawing.Point(731, 19);
            Reg_Reset.Name = "Reg_Reset";
            Reg_Reset.Size = new System.Drawing.Size(85, 31);
            Reg_Reset.TabIndex = 16;
            Reg_Reset.Text = "REG Reset";
            Reg_Reset.UseVisualStyleBackColor = false;
            Reg_Reset.Click += new System.EventHandler(Reg_Reset_Click);
            // 
            // GameLog_oeffnen
            // 
            GameLog_oeffnen.BackColor = System.Drawing.SystemColors.ActiveCaption;
            GameLog_oeffnen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            GameLog_oeffnen.ImageIndex = 1;
            GameLog_oeffnen.ImageList = Image_List_1;
            GameLog_oeffnen.Location = new System.Drawing.Point(130, 18);
            GameLog_oeffnen.Name = "GameLog_oeffnen";
            GameLog_oeffnen.Size = new System.Drawing.Size(107, 40);
            GameLog_oeffnen.TabIndex = 13;
            GameLog_oeffnen.Text = "GameLog öffnen";
            GameLog_oeffnen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            GameLog_oeffnen.UseVisualStyleBackColor = false;
            GameLog_oeffnen.Click += new System.EventHandler(GameLog_oeffnen_Click);
            // 
            // Diagnostic_Checkbox
            // 
            Diagnostic_Checkbox.AutoSize = true;
            Diagnostic_Checkbox.Location = new System.Drawing.Point(13, 569);
            Diagnostic_Checkbox.Name = "Diagnostic_Checkbox";
            Diagnostic_Checkbox.Size = new System.Drawing.Size(76, 17);
            Diagnostic_Checkbox.TabIndex = 14;
            Diagnostic_Checkbox.Text = "Diagnostic";
            Diagnostic_Checkbox.UseVisualStyleBackColor = true;
            Diagnostic_Checkbox.CheckedChanged += new System.EventHandler(Diagnostic_Checkbox_CheckedChanged);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(discordrpccheckbox);
            groupBox2.Controls.Add(chk_Programm_Ontop);
            groupBox2.Controls.Add(Autostart_Checkbox);
            groupBox2.Controls.Add(Chk_Dashboard);
            groupBox2.Location = new System.Drawing.Point(12, 322);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(270, 100);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Sonstiges";
            // 
            // discordrpccheckbox
            // 
            discordrpccheckbox.AutoSize = true;
            discordrpccheckbox.Location = new System.Drawing.Point(10, 79);
            discordrpccheckbox.Name = "discordrpccheckbox";
            discordrpccheckbox.Size = new System.Drawing.Size(251, 17);
            discordrpccheckbox.TabIndex = 3;
            discordrpccheckbox.Text = "DiscordRPC aktivieren (automatischer Neustart)";
            discordrpccheckbox.UseVisualStyleBackColor = true;
            discordrpccheckbox.CheckedChanged += new System.EventHandler(discordrpccheckbox_CheckedChanged);
            // 
            // chk_Programm_Ontop
            // 
            chk_Programm_Ontop.AutoSize = true;
            chk_Programm_Ontop.Location = new System.Drawing.Point(10, 60);
            chk_Programm_Ontop.Name = "chk_Programm_Ontop";
            chk_Programm_Ontop.Size = new System.Drawing.Size(177, 17);
            chk_Programm_Ontop.TabIndex = 2;
            chk_Programm_Ontop.Text = "Programm immer im Vordergrund";
            chk_Programm_Ontop.UseVisualStyleBackColor = true;
            chk_Programm_Ontop.CheckedChanged += new System.EventHandler(chk_Programm_Ontop_CheckedChanged);
            // 
            // Autostart_Checkbox
            // 
            Autostart_Checkbox.AutoSize = true;
            Autostart_Checkbox.Location = new System.Drawing.Point(10, 42);
            Autostart_Checkbox.Name = "Autostart_Checkbox";
            Autostart_Checkbox.Size = new System.Drawing.Size(171, 17);
            Autostart_Checkbox.TabIndex = 1;
            Autostart_Checkbox.Text = "Programm mit Windows starten";
            Autostart_Checkbox.UseVisualStyleBackColor = true;
            Autostart_Checkbox.CheckedChanged += new System.EventHandler(Autostart_Checkbox_CheckedChanged);
            // 
            // Chk_Dashboard
            // 
            Chk_Dashboard.AutoSize = true;
            Chk_Dashboard.Location = new System.Drawing.Point(10, 23);
            Chk_Dashboard.Name = "Chk_Dashboard";
            Chk_Dashboard.Size = new System.Drawing.Size(124, 17);
            Chk_Dashboard.TabIndex = 0;
            Chk_Dashboard.Text = "Dashboard anzeigen";
            Chk_Dashboard.UseVisualStyleBackColor = true;
            Chk_Dashboard.CheckedChanged += new System.EventHandler(Chk_Dashboard_CheckedChanged);
            // 
            // ETS2_folderBrowserDialog
            // 
            ETS2_folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // patreon_image
            // 
            patreon_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            patreon_image.Location = new System.Drawing.Point(12, 425);
            patreon_image.Name = "patreon_image";
            patreon_image.Padding = new System.Windows.Forms.Padding(10);
            patreon_image.Size = new System.Drawing.Size(52, 50);
            patreon_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            patreon_image.TabIndex = 16;
            patreon_image.TabStop = false;
            // 
            // team_image
            // 
            team_image.Location = new System.Drawing.Point(70, 425);
            team_image.Name = "team_image";
            team_image.Size = new System.Drawing.Size(52, 50);
            team_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            team_image.TabIndex = 17;
            team_image.TabStop = false;
            // 
            // Group_Box_Indiv_Texte
            // 
            Group_Box_Indiv_Texte.Controls.Add(checkBox_NUM_LOCK);
            Group_Box_Indiv_Texte.Controls.Add(label14);
            Group_Box_Indiv_Texte.Controls.Add(label12);
            Group_Box_Indiv_Texte.Controls.Add(Num3_Text);
            Group_Box_Indiv_Texte.Controls.Add(label11);
            Group_Box_Indiv_Texte.Controls.Add(Num2_Text);
            Group_Box_Indiv_Texte.Controls.Add(label10);
            Group_Box_Indiv_Texte.Controls.Add(Num1_Text);
            Group_Box_Indiv_Texte.Location = new System.Drawing.Point(288, 322);
            Group_Box_Indiv_Texte.Name = "Group_Box_Indiv_Texte";
            Group_Box_Indiv_Texte.Size = new System.Drawing.Size(547, 125);
            Group_Box_Indiv_Texte.TabIndex = 18;
            Group_Box_Indiv_Texte.TabStop = false;
            Group_Box_Indiv_Texte.Text = "Spender Individual Text";
            // 
            // checkBox_NUM_LOCK
            // 
            checkBox_NUM_LOCK.AutoSize = true;
            checkBox_NUM_LOCK.Location = new System.Drawing.Point(388, 99);
            checkBox_NUM_LOCK.Name = "checkBox_NUM_LOCK";
            checkBox_NUM_LOCK.Size = new System.Drawing.Size(158, 17);
            checkBox_NUM_LOCK.TabIndex = 14;
            checkBox_NUM_LOCK.Text = "NUM Lock Button anzeigen";
            checkBox_NUM_LOCK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBox_NUM_LOCK.UseVisualStyleBackColor = true;
            checkBox_NUM_LOCK.CheckedChanged += new System.EventHandler(checkBox_NUM_LOCK_CheckedChanged);
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label14.Location = new System.Drawing.Point(63, 101);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(109, 12);
            label14.TabIndex = 13;
            label14.Text = "jeweils max. 100 Zeichen";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.White;
            label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label12.Location = new System.Drawing.Point(2, 75);
            label12.Name = "label12";
            label12.Padding = new System.Windows.Forms.Padding(5);
            label12.Size = new System.Drawing.Size(50, 25);
            label12.TabIndex = 11;
            label12.Text = "NUM3";
            label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num3_Text
            // 
            Num3_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Num3_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Num3_Text.Location = new System.Drawing.Point(62, 76);
            Num3_Text.MaxLength = 100;
            Num3_Text.Name = "Num3_Text";
            Num3_Text.Size = new System.Drawing.Size(478, 22);
            Num3_Text.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = System.Drawing.Color.White;
            label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label11.Location = new System.Drawing.Point(2, 47);
            label11.Name = "label11";
            label11.Padding = new System.Windows.Forms.Padding(5);
            label11.Size = new System.Drawing.Size(50, 25);
            label11.TabIndex = 9;
            label11.Text = "NUM2";
            label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num2_Text
            // 
            Num2_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Num2_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Num2_Text.Location = new System.Drawing.Point(62, 48);
            Num2_Text.MaxLength = 100;
            Num2_Text.Name = "Num2_Text";
            Num2_Text.Size = new System.Drawing.Size(478, 22);
            Num2_Text.TabIndex = 8;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.White;
            label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label10.Location = new System.Drawing.Point(2, 19);
            label10.Name = "label10";
            label10.Padding = new System.Windows.Forms.Padding(5);
            label10.Size = new System.Drawing.Size(50, 25);
            label10.TabIndex = 7;
            label10.Text = "NUM1";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num1_Text
            // 
            Num1_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Num1_Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Num1_Text.Location = new System.Drawing.Point(62, 20);
            Num1_Text.MaxLength = 100;
            Num1_Text.Name = "Num1_Text";
            Num1_Text.Size = new System.Drawing.Size(478, 22);
            Num1_Text.TabIndex = 6;
            // 
            // SettingsWindow
            // 
            ClientSize = new System.Drawing.Size(847, 599);
            Controls.Add(Group_Box_Indiv_Texte);
            Controls.Add(team_image);
            Controls.Add(patreon_image);
            Controls.Add(groupBox2);
            Controls.Add(Diagnostic_Checkbox);
            Controls.Add(GroupBox_Diagnostic);
            Controls.Add(Settings_Windows_Label_Settings);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox_AntiAFK);
            Controls.Add(group_Overlay);
            Controls.Add(btn_TruckersMP_suchen);
            Controls.Add(groupBox1);
            Controls.Add(save_button);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            Name = "SettingsWindow";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Einstellungen";
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(SettingsWindow_FormClosed);
            Load += new System.EventHandler(SettingsWindow_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            btn_TruckersMP_suchen.ResumeLayout(false);
            btn_TruckersMP_suchen.PerformLayout();
            group_Overlay.ResumeLayout(false);
            group_Overlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(num_Overlay_Transparenz)).EndInit();
            groupBox_AntiAFK.ResumeLayout(false);
            groupBox_AntiAFK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(reload_antiafk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            GroupBox_Diagnostic.ResumeLayout(false);
            GroupBox_Diagnostic.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(patreon_image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(team_image)).EndInit();
            Group_Box_Indiv_Texte.ResumeLayout(false);
            Group_Box_Indiv_Texte.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }


        private void save_button_Click(object sender, EventArgs e)
        {
            // #######   HOTKEYS   ################


            utils.Reg_Schreiben("NUM1", Num1_Text.Text, "TruckersMP_Autorun");
            utils.Reg_Schreiben("NUM2", Num2_Text.Text, "TruckersMP_Autorun");
            utils.Reg_Schreiben("NUM3", Num3_Text.Text, "TruckersMP_Autorun");



            // #######   HOTKEYS ENDE  ################

            if (comboBox1.Text == "Simulation 1")
            {
                selected_server_tm = "sim1";
                data.tmp_server = selected_server_tm;
                // Edit by Thommy

                utils.Reg_Schreiben("verkehr_SERVER", "sim1", "TruckersMP_Autorun");

            }
            else if (comboBox1.Text == "Simulation 2")
            {
                selected_server_tm = "sim2";
                data.tmp_server = selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "sim2", "TruckersMP_Autorun");

            }
            else if (comboBox1.Text == "Arcade")
            {
                selected_server_tm = "arc1";
                data.tmp_server = selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "arc1", "TruckersMP_Autorun");
            }
            else if (comboBox1.Text == "EU Promods 1")
            {
                selected_server_tm = "eupromods1";
                data.tmp_server = selected_server_tm;
                utils.Reg_Schreiben("verkehr_SERVER", "eupromods1", "TruckersMP_Autorun");
            }
            else if (comboBox1.Text == "EU Promods 2")
            {
                selected_server_tm = "eupromods2";
                data.tmp_server = selected_server_tm;
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
            if (restart_required)
            {
                Application.Restart();
            }
            Close();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "OnTop")))
            {
                utils.Reg_Schreiben("OnTop", "0", "TruckersMP_Autorun");
            }

            GroupBox_Diagnostic.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "Diagnostic") == "1") ? true : false;
            checkBox_NUM_LOCK.CheckState = (utils.Reg_Lesen("TruckersMP_Autorun", "NUM_LOCK_SHOW") == "1") ? CheckState.Checked : CheckState.Unchecked;
            Chk_Dashboard.CheckState = (utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard") == "1") ? CheckState.Checked : CheckState.Unchecked;
            Group_Box_Indiv_Texte.Visible = (Patreon >= 2) ? true : false;
            chk_Programm_Ontop.CheckState = (utils.Reg_Lesen("TruckersMP_Autorun", "OnTop") == "1") ? CheckState.Checked : CheckState.Unchecked;
            discordrpccheckbox.CheckState = (utils.Reg_Lesen("Config", "Discord_Active") == "true") ? CheckState.Checked : CheckState.Unchecked;

            // ##################   HOTKEYS   ######################
            Num1_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM1");
            Num2_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM2");
            Num3_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM3");
            // ##################  AUTOSTART   #####################
            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "VTCManager.appref-ms")))
            {
                utils.Reg_Schreiben("Autostart", "1", "TruckersMP_Autorun");
                Autostart_Checkbox.CheckState = CheckState.Checked;
            }
            else
            {
                utils.Reg_Schreiben("Autostart", "0", "TruckersMP_Autorun");
                Autostart_Checkbox.CheckState = CheckState.Unchecked;
            }

            group_Overlay.Visible = false;
            // Settings_Windows_Label_Settings.Text = translation.settings_window_titel_text; ######### GEHT NICHT ############
            Settings_Windows_Label_Settings.Text = "Einstellungen";

            // #############  DASHBOARD ############################
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard")))
            {
                utils.Reg_Schreiben("Dashboard", "0", "TruckersMP_Autorun");
            }

            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad")))
            {
                MessageBox.Show("der Pfad zu TruckersMP stimmt nicht" + Environment.NewLine + "Bitte korrigiere diesen im folgenden Fenster", "Fehler TruckersMP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            {
                ATS_folderBrowserDialog.SelectedPath = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            }

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
                {
                    utils.Reg_Schreiben("ANTI_AFK", "VTCManager wünscht Gute und Sichere Fahrt!", "TruckersMP_Autorun");
                }

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
                {
                    System.Diagnostics.Process.Start("explorer", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2");
                }
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
                {
                    System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\game.log.txt");
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLOG("<ERROR> Methode GameLog_oeffnen_Click in SettingsWindow.cs -> " + ex.Message + " [SettingsWindow.cs->836]");
                MessageBox.Show("Der Pfad zur GameLog wurde nicht gefunden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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



        private void Autostart_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Autostart_Checkbox.CheckState == CheckState.Checked)
            {
                addAutoStartRegistry();

            }
            else
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
            {
                return false;
            }
            else
            {
                return true;
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Logging Logs = new Logging();
            Logs.SystemDaten_Laden();

            DialogResult result = MessageBox.Show("Ihnen ist Bewusst, dass einige Daten über ihren Account und auch einige Daten über das Spiel Euro Truck Simulator 2 sowie American Truck Simulator via E-Mail an unsere derzeit aktiven Developer gesendet werden und diese Einsicht in die Daten haben. Mit Klick auf 'JA' geben Sie ihr Einverständnis dazu.", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SendMail sm = new SendMail();
                sm.SendeMail();
            }
            else
            {
                MessageBox.Show("Es wurden keine Daten an uns Gesendet !" + Environment.NewLine + "Leider können wir ohne diese Daten keinen Support gewähren.", "Daten", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void chk_Programm_Ontop_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Programm_Ontop.CheckState == CheckState.Checked)
            {
                utils.Reg_Schreiben("OnTop", "1", "TruckersMP_Autorun");
            }
            else
            {
                utils.Reg_Schreiben("OnTop", "0", "TruckersMP_Autorun");
            }
        }

        private void discordrpccheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (discordrpccheckbox.CheckState == CheckState.Checked)
            {
                utils.Reg_Schreiben("Discord_Active", "true", "Config");
                restart_required = true;
            }
            else
            {
                utils.Reg_Schreiben("Discord_Active", "false", "Config");
                restart_required = true;
            }
        }
    }
}

