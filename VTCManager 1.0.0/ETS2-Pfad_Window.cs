using System;
using System.IO;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    public partial class ETS2_Pfad_Window : Form
    {

        private readonly Utilities utils = new Utilities();
        readonly Logging Logging = new Logging();
        private string initial_ETS;
        private string initial_ATS;
        public ETS2_Pfad_Window()
        {


            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ETS2_Pfad_Window_Load(object sender, EventArgs e)
        {

            ats_pfad.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            ets_pfad.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");

            if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2")) { initial_ETS = @"C:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2"; }
            else if (Directory.Exists(@"D:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2")) { initial_ETS = @"D:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2"; }
            else if (Directory.Exists(@"E:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2")) { initial_ETS = @"E:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2"; }
            else { initial_ETS = @"C:\"; }


            if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator")) { initial_ATS = @"C:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator"; }
            else if (Directory.Exists(@"D:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator")) { initial_ATS = @"D:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator"; }
            else if (Directory.Exists(@"E:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator")) { initial_ATS = @"E:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator"; }
            else { initial_ATS = @"C:\"; }
        }

        private void btn_Suche_ETS_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_ETS.SelectedPath = initial_ETS;
            var pfad_suchen = folderBrowserDialog_ETS.ShowDialog();


            if (pfad_suchen == DialogResult.OK)
            {
                utils.Reg_Schreiben("ETS2_Pfad", folderBrowserDialog_ETS.SelectedPath, "TruckersMP_Autorun");
                ets_pfad.Text = folderBrowserDialog_ETS.SelectedPath.ToString();

                // Telemetry kopierens();
                string dest_leer = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
                if (!Directory.Exists(dest_leer + @"\bin\win_x64\plugins")) { Directory.CreateDirectory(dest_leer + @"\bin\win_x64\plugins"); }

                string dest_Path = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") + @"\bin\win_x64\plugins";
                try
                {
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_Path + @"\scs-telemetry.dll");

                }
                catch (Exception ex)
                {
                    Logging.WriteLOG("<ERROR> Methode btn_Suche_ETS_Click in ETS2_Pfad_Window.cs -> " + ex.Message);
                }

            }
        }



        private void btn_Suche_ATS_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_ATS.SelectedPath = initial_ATS;
            var pfad_suchen = folderBrowserDialog_ATS.ShowDialog();
            if (pfad_suchen == DialogResult.OK)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("ATS_Pfad", folderBrowserDialog_ATS.SelectedPath, "TruckersMP_Autorun");
                ats_pfad.Text = folderBrowserDialog_ATS.SelectedPath.ToString();

                // Telemetry kopieren
                Utilities util2 = new Utilities();
                string dest_leer = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
                if (!Directory.Exists(dest_leer + @"\bin\win_x64\plugins")) { Directory.CreateDirectory(dest_leer + @"\bin\win_x64\plugins"); }

                string dest_Path = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") + @"\bin\win_x64\plugins";

                try
                {
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_Path + @"\scs-telemetry.dll");
                }
                catch (Exception ex)
                {
                    Logging.WriteLOG("<ERROR> Methode btn_Suche_ATS_Click in ETS2_Pfad_Window.cs -> " + ex.Message);
                }


            }
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            Utilities util2 = new Utilities();
            ets_pfad.Text = util2.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            ats_pfad.Text = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");

            // ############# ETS2 Pfad check ###############
            if (!File.Exists(ets_pfad.Text + @"\bin\win_x64\eurotrucks2.exe")) { MessageBox.Show("Der Pfad von ETS ist falsch ! " + Environment.NewLine + "Bitte gib den richtigen Pfad an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            // ########## ATS Pfad Check ###################
            /*if(!string.IsNullOrEmpty(ats_pfad.Text))
            {
                if (!File.Exists(ats_pfad.Text + @"\bin\win_x64\amtrucks.exe")) { MessageBox.Show("Der Pfad von ATS ist falsch ! " + Environment.NewLine + "Bitte gib den richtigen Pfad an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
           } */

            WindowState = FormWindowState.Minimized;
            MessageBox.Show("Die Einstellungen wurden Gespeichert!" + Environment.NewLine + Environment.NewLine + "Der Client wird neu Gestartet...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void ETS2_Pfad_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
