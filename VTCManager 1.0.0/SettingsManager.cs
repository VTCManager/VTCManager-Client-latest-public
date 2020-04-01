using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace VTCManager_1._0._0
{
    class SettingsManager
    {
        private string settingsDirectory;
        private string settingsFile;
        public static string userFolder;
        private Utilities utils = new Utilities();
        public string geschwindigkeits_modus;
        public string tmp_server;
        public SettingsManager()
        {
            this.settingsDirectory = Path.Combine(userFolder, ".vtcmanager");
            this.settingsFile = "settings.xml";
            this.SConfigFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".xml";
            this.Config = new SettingsDataObject();
        }
        static SettingsManager()
        {
            userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        public void CreateConfig()
        {
            if (!Directory.Exists(this.settingsDirectory))
            {
                Directory.CreateDirectory(this.settingsDirectory);
            }
            if (!File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
            {
                File.Create(Path.Combine(this.settingsDirectory, this.settingsFile)).Dispose();
                string[] contents = new string[] { "<SettingsDataObject></SettingsDataObject>" };
                File.AppendAllLines(Path.Combine(this.settingsDirectory, this.settingsFile), contents);
            }
        }

        public void LoadConfig()
        {
            if (File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
            {
                StreamReader textReader = File.OpenText(Path.Combine(this.settingsDirectory, this.settingsFile));
                object obj2 = new XmlSerializer(this.Config.GetType()).Deserialize(textReader);
                this.Config = (SettingsDataObject)obj2;
                textReader.Close();
            }
        }

        public void SaveConfig()
        {
            StreamWriter writer = File.CreateText(Path.Combine(this.settingsDirectory, this.settingsFile));
            Type type = this.Config.GetType();
            if (type.IsSerializable)
            {
                new XmlSerializer(type).Serialize((TextWriter)writer, this.Config);
                writer.Close();
            }
        }

        public void DeleteConfig()
        {
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
                {
                    // If file found, delete it    
                    File.Delete(Path.Combine(this.settingsDirectory, this.settingsFile));
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }


        // Properties
        public SettingsDataObject Config { get; set; }

        public string SConfigFileName { get; }

        public void LoadConfiguration()
        {
            geschwindigkeits_modus = utils.Reg_Lesen("Config", "geschwindigkeits_modus");
            if (string.IsNullOrEmpty(geschwindigkeits_modus))
            {
                utils.Reg_Schreiben("geschwindigkeits_modus", "kmh", "Config");
                geschwindigkeits_modus = "kmh";
            }

            tmp_server = utils.Reg_Lesen("Config", "TMP_server");
            if (string.IsNullOrEmpty(tmp_server))
            {
                utils.Reg_Schreiben("TMP_server", "sim1", "Config");
                tmp_server = "sim1";
            }
        }

    }
}

