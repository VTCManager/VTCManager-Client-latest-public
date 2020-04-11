using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace VTCManager_1._0._0
{
    class Utilities
    {
        //Fields
        private static long _lastCheckTime;
        private static bool _cachedRunningFlag;
        private static string _chachedGame;
        private static bool _DiscordRunningFlag;
        CultureInfo ci = CultureInfo.InstalledUICulture;
        private string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager");
        private string logFile = @"\log.txt";

        //Properties
        public static string LastRunningGameName { get; set; }


        internal static string BytesToString(byte[] bytes) =>
            ((bytes != null) ? Encoding.UTF8.GetString(bytes, 0, Array.FindIndex<byte>(bytes, b => b == 0)) : string.Empty);
        internal static DateTime MinutesToDate(int minutes) =>
            SecondsToDate(minutes * 60);
        internal static DateTime SecondsToDate(int seconds)
        {
            if (seconds < 0)
            {
                seconds = 0;
            }
            return new DateTime(seconds * 0x98_9680L, DateTimeKind.Utc);
        }

        public static string Sha256(string randomString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString)))
            {
                builder.Append(num2.ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool IsGameRunning
        {
            get
            {
                if ((DateTime.Now - new DateTime(Interlocked.Read(ref _lastCheckTime))) > TimeSpan.FromSeconds(3.0))
                {
                    Interlocked.Exchange(ref _lastCheckTime, DateTime.Now.Ticks);
                    Process[] processes = Process.GetProcesses();
                    int index = 0;
                    while (true)
                    {
                        if (index < processes.Length)
                        {
                            Process process = processes[index];
                            try
                            {
                                if (process.MainWindowTitle.StartsWith("Euro Truck Simulator 2") || (process.ProcessName == "eurotrucks2") || (process.ProcessName == "amtrucks"))
                                {
                                    _cachedRunningFlag = true;
                                    if (process.ProcessName == "eurotrucks2")
                                    {
                                        LastRunningGameName = "ETS2";
                                    }else if (process.ProcessName == "amtrucks")
                                    {
                                        LastRunningGameName = "ATS";
                                    }
                                    return _cachedRunningFlag;
                                }
                              

                            }
                            catch
                            {
                            }
                            index++;
                            continue;
                        }
                        else
                        {
                            _cachedRunningFlag = false;
                        }
                        break;
                    }
                }
                return _cachedRunningFlag;
            }
        }

        public static bool IsDiscordRunning
        {
            get
            {
                if ((DateTime.Now - new DateTime(Interlocked.Read(ref _lastCheckTime))) > TimeSpan.FromSeconds(3.0))
                {
                    Interlocked.Exchange(ref _lastCheckTime, DateTime.Now.Ticks);
                    Process[] processes = Process.GetProcesses();
                    int index = 0;
                    while (true)
                    {
                        if (index < processes.Length)
                        {
                            Process process = processes[index];
                            try
                            {
                                if (process.ProcessName == "Discord")
                                {
                                    _DiscordRunningFlag = true;
      
                                    return _DiscordRunningFlag;
                                }

                            }
                            catch{}
                            index++;
                            continue;
                        }
                        else
                        {
                     
                            _DiscordRunningFlag = false;
                        }
                        break;
                    }
                }
                return _DiscordRunningFlag;
            }
        }

        /*
        public static string WhichGameIsRunning
        {
            get
            {
                foreach (Process process in Process.GetProcesses())
                {
                    try
                    {
                        if (process.MainWindowTitle.StartsWith("Euro Truck Simulator 2") && (process.ProcessName == "eurotrucks2"))
                        {
                            _chachedGame = "ets2";
                        }
                        else if (process.MainWindowTitle.StartsWith("American Truck Simulator") && (process.ProcessName == "amtrucks"))
                        {
                            _chachedGame = "ats";
                        } 
                    }
                    catch
                    {
                        _chachedGame = "none";
                    }
                }
                return _chachedGame;
            }
        } */


        // Edit by Thommy
        public void Reg_Schreiben(string name, string wert, string ordner)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key.CreateSubKey("VTCManager");
            key = key.OpenSubKey("VTCManager", true);
            key.CreateSubKey(ordner);
            key = key.OpenSubKey(ordner, true);
            key.SetValue(name, wert);

        }

        public string Reg_Lesen(string ordner, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VTCManager\" + ordner);
                return key.GetValue(value).ToString();
            } catch (Exception ex) {
                Log("<ERROR> Methode Reg_Lesen in Utilities.cs -> " + ex.Message + ex.StackTrace + "Given String->Folder:" + ordner + " + Value: " + value + " in [Utilities.cs->185]");
                return null;
            }
        }


        public void Log(string text)
        {
            Translation trans = new Translation(ci.DisplayName);
            if(File.Exists(logDirectory + logFile))
            {
                try
                {
                    File.AppendAllText(logDirectory + logFile, "<" + DateTime.Now + "> " + text + Environment.NewLine);
                } catch (Exception ex)
                {
                    Log("<ERROR> Methode LOG in Utilities.cs -> " + ex.Message + ex.StackTrace + "Given String: "+ text + " [Utilities.cs->201]");
                }

            }

        }



    }
}
