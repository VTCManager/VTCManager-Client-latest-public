using System.Net.NetworkInformation;



namespace VTCManager_1._0._0
{

    class Servercheck
    {
        public bool WS_Check()
        {
            PingReply pingReply;
            using (var ping = new Ping())
            {
                pingReply = ping.Send("vtc.northwestvideo.de");
            }

            return pingReply.Status == IPStatus.Success;

        }

        public bool DB_Check()
        {
            PingReply pingReply;
            using (var ping = new Ping())
            {
                pingReply = ping.Send("194.13.81.113");
            }

            return pingReply.Status == IPStatus.Success;

        }
    }
}
