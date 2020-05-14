using DiscordRPC;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;

namespace VTCManager_1._0._0.Klassen
{
    class SendMail
    {
        Utilities utils = new Utilities();
        public void SendeMail()
        {
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG_COPY.txt");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG_COPY.txt");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\DLC_Log.txt");

            Erstelle_DLC_LOG();

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("vtc_diag@web.de"),
                Subject = "Log Datei aus Client von " + utils.Reg_Lesen("TruckersMP_Autorun", "usr"),
                Body = "Hier die LOG Dateien",
            };
            mail.To.Add("devlogs@northwestvideo.de");
        
            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG_COPY.txt", true);
            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG_COPY.txt", true);


            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG_COPY.txt"));
            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG_COPY.txt"));
            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\DLC_Log.txt"));
            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\game.log.txt"));

            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.web.de", 587); 
            
            try
            {
                client.Credentials = new System.Net.NetworkCredential("vtc_diag@web.de", "VtcDiagnostic");
                client.EnableSsl = true; 
                client.Send(mail);

                
                MessageBox.Show("E-Mail wurde gesendet !" + Environment.NewLine + Environment.NewLine, "Daten gesendet !", MessageBoxButtons.OK, MessageBoxIcon.Information);
               //Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Senden der E-Mail" + Environment.NewLine + ex.Message);
            }

            mail.Dispose();
        }


        private void Erstelle_DLC_LOG()
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
        public string Read(string KeyName)
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.OpenSubKey(@"Software\VTCManager\TruckersMP_Autorun");
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

    }
}
