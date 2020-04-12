using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    public partial class Frachtmarkt : Form
    {
        DispoMethoden dp = new DispoMethoden();
        Utilities utils = new Utilities();
        API api = new API();
        Translation trans;
        

        public int FM_Patreon_State { get; set; }
        public string folderETS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles";
        public string folderATS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\American Truck Simulator\profiles";
        public string ordnername;

        public Frachtmarkt()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.InstalledUICulture;
            this.trans = new Translation(ci.DisplayName);

            Label_From_City.Text = trans.Frachtmarkt_from_City;
            Label_From_Company.Text = trans.Frachtmarkt_from_Company;
            Label_To_City.Text = trans.Frachtmarkt_to_City;
            Label_To_Company.Text = trans.Frachtmarkt_to_Company;

            Lade_Von_Staedte();
            Lade_Nach_Staedte();
            Lade_ETS_Profile();
            KopiereDecrypterinFolder();
        }


        private void KopiereDecrypterinFolder()
        {
            
            string ordner = dp.FromStringToHex(comboBoxProfiles.Text).ToUpper();
            string dateiname = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles\" + ordner + @"\save\autosave\SII_Decrypt.exe";
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles\" + ordner + @"\save\autosave";
            if (!File.Exists(dateiname))
                File.Copy(Application.StartupPath + @"\Resources\SII_Decrypt.exe", destination + @"\SII_Decrypt.exe"); utils.Log("<INFO> SII_DECRYPTER in Profilordner: " + destination + " kopiert -> [Frachtmarkt.cs@48]");

   
        }


        public void Lade_Von_Staedte()
        {
            string values = api.HTTPSRequestGet(api.api_server + api.get_cities_path);
            string[] values2 = values.Split(';');

            foreach (string word in values2)
                Combo_From_City.Items.Add(word);

        }

        public void Lade_Nach_Staedte()
        {

        }

        private void Combo_From_City_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stadtname = Combo_From_City.Text;
            string answer = this.api.HTTPSRequestPost(this.api.api_server + this.api.load_firmen_in_city, new Dictionary<string, string>()
                                    {
                                        { "stadtname", stadtname.ToString() }
                                    }, false).ToString();
            Combo_From_Company.Items.Add(answer);
        }

        #region LadeProfile

        public void Lade_ETS_Profile()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(folderETS);
                comboBoxProfiles.Items.Clear();
                comboBoxProfiles.Text = "";
                int i = 0;
                foreach (string dir in dirs)
                {
                    i++;
                    ordnername = dir.Substring(dir.LastIndexOf("\\") + 1);

                    comboBoxProfiles.Items.Add(dp.FromHexToString(ordnername));
                }
                if (i == 0) { comboBoxProfiles.Text = trans.Frachtmarkt_no_profiles; comboBoxProfiles.Enabled = false; } else { comboBoxProfiles.Enabled = true; }
                comboBoxProfiles.SelectedIndex = 0;

                Decrypt_SII(dp.FromStringToHex(comboBoxProfiles.Text).ToUpper());
                    MessageBox.Show("Send");

                
            }
            catch
            {
                utils.Log("<ERROR> Keine Profilordner in ETS gefunden  ! [Frachtmartkt.cs->85]");
            }
        }

        public void Lade_ATS_Profile()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(folderATS);
                comboBoxProfiles.Items.Clear();
                comboBoxProfiles.Text = "";
                int i = 0;
                foreach (string dir in dirs)
                {
                    i++;
                    ordnername = dir.Substring(dir.LastIndexOf("\\") + 1);
                    comboBoxProfiles.Items.Add(dp.FromHexToString(ordnername));
                }
                if (i == 0) { comboBoxProfiles.Text = trans.Frachtmarkt_no_profiles; comboBoxProfiles.Enabled = false; } else { comboBoxProfiles.Enabled = true; }
                comboBoxProfiles.SelectedIndex = 0;
            }
            catch
            {
                utils.Log("<ERROR> Keine Profilordner in ATS gefunden ! [Frachtmarkt.cs->111]");
            }
        }

#endregion

        private void Frachtmarkt_Load_1(object sender, EventArgs e)
        {
            // TODO -> Vorwauswahl anhand des Games aus der Main.cs
            Radio_Button_ETS2.Checked = true;
            if (FM_Patreon_State >= 2)
                 textBox_Money.Visible = true; lbl_Guhaben.Visible = true;
           
        }

        #region RadioButton
        private void Radio_Button_ETS2_CheckedChanged(object sender, EventArgs e)
        {
            Backup_Game_Sii_ETS();
            Lade_ETS_Profile();
            this.utils.Log("<INFO> ETS2 Profile wurden im Frachtmarkt geladen! [Frachtmartkt.cs->147]");
        }

        private void Radio_Button_ATS_CheckedChanged(object sender, EventArgs e)
        {
            Backup_Game_Sii_ATS();
            Lade_ATS_Profile();
            this.utils.Log("<INFO> ATS Profile wurden im Frachtmarkt geladen! [Frachtmartkt.cs->157]");
        }
        #endregion

        #region BACKUP ETS / ATS
        public void Backup_Game_Sii_ETS()
        {
            string[] dirs = Directory.GetDirectories(folderETS);
            foreach (string dir in dirs)
            {
                ordnername = dir.Substring(dir.LastIndexOf("\\") + 1);
                string SiiSourcePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles\" + ordnername + @"\save\autosave\game.sii";
                string SiiDestPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles\" + ordnername + @"\save\autosave\VTC_Backup.sii";
                try
                {
                    File.Copy(SiiSourcePath, SiiDestPath, true);

                }
                catch { utils.Log("<ERROR> Keine Profile für Backup in ETS gefunden! [Frachtmartkt.cs->164]"); }
            }
        }

        public void Backup_Game_Sii_ATS()
        {
            string[] dirs = Directory.GetDirectories(folderATS);
            foreach (string dir in dirs)
            {
                ordnername = dir.Substring(dir.LastIndexOf("\\") + 1);
                string SiiSourcePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\American Truck Simulator\profiles\" + ordnername + @"\save\autosave\game.sii";
                string SiiDestPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\American Truck Simulator\profiles\" + ordnername + @"\save\autosave\VTC_Backup.sii";
                try
                {
                    File.Copy(SiiSourcePath, SiiDestPath, true);
                }
                catch { utils.Log("<ERROR> Keine Profile für Backup in ATS gefunden! [Frachtmartkt.cs->181]"); }

            }
        }
        #endregion

        private void Button_Setze_Money_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Schreibe *.sii Datei...");
        }

        private void Decrypt_SII(string ordnername)
        {
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles\" + ordnername + @"\save\autosave\";
            Process.Start(destination + "SII_Decrypt.exe game.sii");

           
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
