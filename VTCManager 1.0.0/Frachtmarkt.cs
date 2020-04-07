using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    public partial class Frachtmarkt : Form
    {
        DispoMethoden dp = new DispoMethoden();
        Utilities utils = new Utilities();
        Translation trans;
        API api = new API();

        public string folderETS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\profiles";
        public string folderATS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\American Truck Simulator\profiles";
        private string allcities_response;
        private Dictionary<string, string> postParameters;
        private string traffic_response;
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

            // string alleStaedte = @"https://api.truckyapp.com/v2/map/cities/all";


            Lade_Von_Staedte();
            Lade_Nach_Staedte();

        }

        public void Lade_Von_Staedte()
        {
            if (File.Exists("Dateien/cities.CSV"))
            {
                string line;
                StreamReader staedte = new StreamReader("Dateien/cities.CSV");
                while ((line = staedte.ReadLine()) != null)
                {
                    System.Globalization.TextInfo ti = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
                    string[] teile = line.Split(';');
                    Combo_From_City.Items.Add(ti.ToTitleCase(teile[0]));
                }
                staedte.Close();
            }
        }

        public void Lade_Nach_Staedte()
        {
            if (File.Exists("Dateien/cities.CSV"))
            {
                string line;
                StreamReader staedte = new StreamReader("Dateien/cities.CSV");
                while ((line = staedte.ReadLine()) != null)
                {
                    System.Globalization.TextInfo ti = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
                    string[] teile = line.Split(';');
                    Combo_To_City.Items.Add(ti.ToTitleCase(teile[0]));
                }
                staedte.Close();
            }
        }


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
                if(i == 0) { comboBoxProfiles.Text = trans.Frachtmarkt_no_profiles; comboBoxProfiles.Enabled = false; } else { comboBoxProfiles.Enabled = true; }
                comboBoxProfiles.SelectedIndex = 0;
            }
            catch
            {
                utils.Log("Keine Profilordner in ETS gefunden  !");
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
                utils.Log("Keine Profilordner in ATS gefunden !");
            }
        }


        private void Frachtmarkt_Load_1(object sender, EventArgs e)
        {
            // TODO -> Vorwauswahl anhand des Games aus der Main.cs
            Radio_Button_ETS2.Checked = true;
        }

        private void Radio_Button_ETS2_CheckedChanged(object sender, EventArgs e)
        {
            Backup_Game_Sii_ETS();
            Lade_ETS_Profile();
            this.utils.Log("ETS2 Profile wurden im Frachtmarkt geladen!");
      
        }

        private void Radio_Button_ATS_CheckedChanged(object sender, EventArgs e)
        {
            Backup_Game_Sii_ATS();
            Lade_ATS_Profile();
            this.utils.Log("ATS Profile wurden im Frachtmarkt geladen!");
        }

        public void Backup_Game_Sii_ETS()
        {
            string[] dirs = Directory.GetDirectories(folderETS);
            foreach (string dir in dirs)
            {
                ordnername = dir.Substring(dir.LastIndexOf("\\") + 1);
                string SiiSourcePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro truck Simulator 2\profiles\" + ordnername + @"\save\autosave\game.sii";
                string SiiDestPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro truck Simulator 2\profiles\" + ordnername + @"\save\autosave\VTC_Backup.sii";
                try
                {
                    File.Copy(SiiSourcePath, SiiDestPath, true);
                }
                catch { utils.Log("Keine Profile für Backup in ETS gefunden!"); }
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
                } catch { utils.Log("Keine Profile für Backup in ATS gefunden!"); }
                
            }
        }

    }




}
