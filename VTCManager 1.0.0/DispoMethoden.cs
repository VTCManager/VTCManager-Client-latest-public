using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VTCManager_1._0._0
{
    class DispoMethoden
    {



        public void Suche_Profil_Pfad()
        {
       

        }


        public void FillProfiles()
        {
     
        }
        public string FromHexToString(string _hex)
        {
            try
            {
                byte[] raw = new byte[_hex.Length / 2];
                for (int i = 0; i < raw.Length; i++)
                {
                    raw[i] = Convert.ToByte(_hex.Substring(i * 2, 2), 16);
                }

                return Encoding.UTF8.GetString(raw); //UTF8
            }
            catch
            {
                return null;
            }
        }

    }
}

