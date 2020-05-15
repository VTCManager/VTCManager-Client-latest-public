using System;
using System.IO;
using System.Net;
using System.Text;

namespace VTCManager_1._0._0
{
    class Thommy
    {
        public static string MeineVersion;
        private API api = new API();
        Logging Logs = new Logging();
        public string Aktuelle_Version_lesen()
        {
            Utilities util = new Utilities();
            MeineVersion = util.Reg_Lesen("TruckersMP_Autorun", "Version");
            return MeineVersion;
        }



        public void Sende_TollGate(string authcode, float payment, int tournummer)
        {
            var request = (HttpWebRequest)WebRequest.Create(api.api_server + api.tollgate_path);
            var postData = "authcode=" + authcode.ToString();
            postData += "&payment=" + payment;
            postData += "&tourid=" + tournummer;

            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Logs.WriteLOG("<INFO> Tollgate Send " + postData.ToString() + Environment.NewLine);
        }

        public void Sende_Refuel(string authcode, float payment, string tournummer)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://vtc.zwpc.de/tankkosten.php");
            var postData = "authcode=" + authcode.ToString();
            postData += "&payment=" + payment;
            postData += "&tourid=" + tournummer;

            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Logs.WriteLOG("<INFO> Refuel Send " + postData.ToString() + Environment.NewLine);
        }


        public void Sende_Faehre(string authcode, float payment, int tournummer)
        {
            Logs.WriteLOG("<INFO> Ferry Used ");
        }

        public void Loesche_Alte_DLL()
        {
            Utilities utils = new Utilities();
            string dest_leer = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            string dest_leer2 = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            try
            {
                File.Delete(dest_leer + @"\bin\win_x64\plugins\scs-telemetry.dll");
            }
            catch (Exception ex)
            {
                Logs.WriteSystemLOG("<ERROR DELETE> scs-telemetry.dll in ETS2" + ex.Message + Environment.NewLine);
            }

            try
            {
                File.Delete(dest_leer2 + @"\bin\win_x64\plugins\scs-telemetry.dll");
            }
            catch (Exception ex)
            {
                Logs.WriteSystemLOG("<ERROR DELETE> scs-telemetry.dll in ATS" + ex.Message + Environment.NewLine);
            }
        }

    }
}
