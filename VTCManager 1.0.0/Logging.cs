using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Management;

namespace VTCManager_1._0._0
{
    class Logging
    {

        private readonly string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager");
        public string logFile = @"\VTC_LOG.txt";
        CultureInfo ci = CultureInfo.InstalledUICulture;

        public void Clear_Log_File()
        {
            if (File.Exists(logDirectory + logFile))
                File.WriteAllText(logDirectory + logFile, String.Empty);
        }

        public void Make_Log_File()
        {
            if (!File.Exists(logDirectory + logFile))
            {
                try
                {
                    Directory.CreateDirectory(logDirectory);
                    this.WriteLOG("<ERROR> LOG-Directory Create successful ");
                }
                catch (Exception ex)
                {
                    this.WriteLOG("<ERROR> Log Directory Create Error ! " + ex.StackTrace);
                }

                try
                {
                    File.Create(logDirectory + logFile);
                    this.WriteLOG("<ERROR> LOG-File Create successful ");
                }
                catch (Exception ex)
                {
                    this.WriteLOG("<ERROR> LOG-File Create Error ! " + ex.StackTrace);

                }
            }
        }


        public void WriteLOG(string text, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string file = null)
        {
            Translation trans = new Translation(ci.DisplayName);
            try
            {
                if (File.Exists(logDirectory + logFile))
                {
                    try
                    {
                       
                        File.AppendAllText(logDirectory + logFile, "<" + DateTime.Now + "> " + text + " - File " + file + " :: " + lineNumber + "; Caller: " + caller + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("<ERROR> " + ex.Message + ex.StackTrace + " - Given String: " + text + " [Logging.cs->65]", "Fehler beim Schreiben in Log", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex2) {
                MessageBox.Show("<ERROR> " + ex2.Message + ex2.StackTrace + " - Given String: " + text + " [Logging.cs->71]", "Fehler beim Schreiben in Log", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void SystemDaten_Laden()
        {
           
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor");
            ManagementObjectCollection queryCollection1 = cpu.Get();
            foreach (ManagementObject mo in queryCollection1)
            {
                this.WriteLOG("<SYSTEM> " + mo["name"].ToString() + " @ " + mo["DataWidth"] + " Bit");
            }
        }


    }


}