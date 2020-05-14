using System;
using System.Text;

namespace VTCManager_1._0._0
{
    class DispoMethoden
    {

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


        public string FromStringToHex(string hexstring)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char t in hexstring)
            {
                //Note: X for upper, x for lower case letters
                sb.Append(Convert.ToInt32(t).ToString("x"));
            }
            return sb.ToString();
        }


    }
}

