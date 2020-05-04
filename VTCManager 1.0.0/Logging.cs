using System;
using System.Globalization;
using System.IO;

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


        public void WriteLOG(string text)
        {
            Translation trans = new Translation(ci.DisplayName);
            try
            {
                if (File.Exists(logDirectory + logFile))
                {
                    try
                    {
                        File.AppendAllText(logDirectory + logFile, "<" + DateTime.Now + "> " + text + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        WriteLOG("<ERROR> Methode LOG in Utilities.cs -> " + ex.Message + ex.StackTrace + "Given String: " + text + " [Utilities.cs->204]");
                    }

                }
            }
            catch { }

        }

    }


}
