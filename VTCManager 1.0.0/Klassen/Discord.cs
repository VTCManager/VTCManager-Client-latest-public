using DiscordRPC;

namespace VTCManager_1._0._0
{
    class Discord
    {
        private DiscordRpcClient client;
        public bool active;
        private Utilities utils;

        public Discord()
        {
            this.utils = new Utilities();
            if (!string.IsNullOrEmpty(utils.Reg_Lesen("Config", "Discord_Active")))
            {
                if (utils.Reg_Lesen("Config", "Discord_Active") == "false")
                {
                    this.active = false;
                }
                else
                {
                    this.active = true;
                }
            }
            else
            {
                utils.Reg_Schreiben("Discord_Active", "true", "Config");
                this.active = true;
            }
            if (this.active)
            {
                if (Utilities.IsDiscordRunning == true)
                {

                    client = new DiscordRpcClient(Information.DiscordAppID);
                    client.Initialize();
                    client.SetPresence(new RichPresence()
                    {
                        Details = "Starte...",
                        Assets = new Assets()
                        {
                            LargeImageKey = Information.DiscordLargeImageKey,
                            LargeImageText = "Beyond the limits",
                            SmallImageKey = Information.DiscordSmallImageKey,
                            SmallImageText = "VTCManager Version " + Information.ClientVersion
                        }

                    });
                }
            }
        }
        public void onTour(string destination, string depature, string freight, string weight)
        {
            if (this.active)
            {
                RichPresence rpc = new RichPresence()
                {
                    Details = "Fracht: " + freight + "(" + weight + "t)",
                    State = "von " + depature + " nach " + destination,

                    Assets = new Assets()
                    {
                        LargeImageKey = Information.DiscordLargeImageKey,
                        LargeImageText = "Beyond the limits",
                        SmallImageKey = Information.DiscordSmallImageKey,
                        SmallImageText = "VTCManager Version " + Information.ClientVersion
                    }
                };
                rpc = rpc.WithTimestamps(Timestamps.Now);
                client.SetPresence(rpc);
                client.Invoke();
            }
        }
        public void noTour()
        {
            if (this.active)
            {
                RichPresence rpc = new RichPresence()
                {
                    Details = "Frei wie der Wind",

                    Assets = new Assets()
                    {
                        LargeImageKey = Information.DiscordLargeImageKey,
                        LargeImageText = "Beyond the limits",
                        SmallImageKey = Information.DiscordSmallImageKey,
                        SmallImageText = "VTCManager Version " + Information.ClientVersion
                    }
                };
                client.SetPresence(rpc);
                client.Invoke();
            }
        }
    }
}
