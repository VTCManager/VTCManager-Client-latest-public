using System;
using System.Threading;
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
        public string systemlogFile = @"\VTC_SYSTEM_LOG.txt";
        int MemSize = 0;
        int mCap = 0;
        MemoryInfo memoryInfo = new MemoryInfo(true);

        public void Clear_Log_File()
        {
            if (File.Exists(logDirectory + logFile))
                File.WriteAllText(logDirectory + logFile, String.Empty);

            if(File.Exists(logDirectory + systemlogFile))
                File.WriteAllText(logDirectory + systemlogFile, String.Empty);
        }

        public void Make_Log_File()
        {
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            if (!File.Exists(logDirectory + logFile))
                    File.Create(logDirectory + logFile);

            if(!File.Exists(logDirectory + systemlogFile))
                File.Create(logDirectory + systemlogFile);

        }


        public void WriteLOG(string text, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string file = null)
        {
            try
            {
                if (File.Exists(logDirectory + logFile))
                {
                    try
                    {
                        Thread.Sleep(200);
                        File.AppendAllText(logDirectory + logFile, "<" + DateTime.Now + "> " + text + " - File " + file + " :: " + lineNumber + "; Caller: " + caller + Environment.NewLine);
                    }
                    catch (Exception ex) {
                        MessageBox.Show("<ERROR> " + ex.Message + ex.StackTrace + " - Given String: " + text + " [Logging.cs->56]", "Fehler beim Schreiben in Log", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch {}
        }


        public void WriteSystemLOG(string text, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string file = null)
        {
            try
            {
                if (File.Exists(logDirectory + systemlogFile))
                {
                    try
                    {
                        Thread.Sleep(100);
                        File.AppendAllText(logDirectory + systemlogFile, "<" + DateTime.Now + "> " + text + " - File " + file + " :: " + lineNumber + "; Caller: " + caller + Environment.NewLine);
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("<ERROR> " + ex.Message + ex.StackTrace + " - Given String: " + text + " [Logging.cs->79]", "Fehler beim Schreiben in System-Log", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                } 
            }
            catch {}

        }

        public void SystemDaten_Laden()
        {
           
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("SELECT * FROM Win32_processor");
            ManagementObjectCollection queryCollection1 = cpu.Get();
            foreach (ManagementObject mo in queryCollection1)
            {
                this.WriteSystemLOG("<SYSTEM CPU> " + mo["name"].ToString() + " @ " + mo["DataWidth"] + " Bit");
            }

            ManagementObjectSearcher BitVersion = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
            ManagementObjectCollection queryCollection2 = BitVersion.Get();
            foreach(ManagementObject m1 in queryCollection2)
            {
                this.WriteSystemLOG ("<SYSTEM OS> Name: " + m1["name"] + "; Architekture: " + m1["OSArchitecture"] + "; Users: " + m1["NumberOfUsers"] + "; Lang: " + m1["MUILanguages"]);
            }


            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                this.WriteSystemLOG("<RAM INFO> " + memoryInfo.MemoryLoad + "% RAM Belegt; " + ((ulong)queryObj["TotalPhysicalMemory"]/1024/1024/1000).ToString() + "GB " + RamInfo.RamType + " RAM Gesamt");
            }

           




        }
    }


}