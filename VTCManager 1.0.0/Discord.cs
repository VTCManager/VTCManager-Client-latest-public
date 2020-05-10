﻿using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCManager_1._0._0
{
    class Discord
    {
        public DiscordRpcClient client;
        private String ClientVersion;

        public Discord(String ClientVersion)
        {
            if (Utilities.IsDiscordRunning == true)
            {

                client = new DiscordRpcClient("659036297561767948");
                client.Initialize();
                this.ClientVersion = ClientVersion;
                client.SetPresence(new RichPresence()
                {
                    Details = "Starte...",
                    Assets = new Assets()
                    {
                        LargeImageKey = "truck-icon",
                        LargeImageText = "Beyond the limits",
                        SmallImageKey = "vtcm-logo",
                        SmallImageText = "VTCManager Version " + ClientVersion
                    }

                });
            }
        }
        public void onTour(string destination, string depature, string freight, string weight)
        {
            RichPresence rpc = new RichPresence()
            {
                Details = "Fracht: "+freight+"("+weight+"t)",
                State = "von "+depature+" nach "+destination,

                Assets = new Assets()
                {
                    LargeImageKey = "truck-icon",
                    LargeImageText = "Beyond the limits",
                    SmallImageKey = "vtcm-logo",
                    SmallImageText = "VTCManager Version " + ClientVersion
                }
            };
            rpc = rpc.WithTimestamps(Timestamps.Now);
            client.SetPresence(rpc);
            client.Invoke();
        }
        public void noTour()
        {
            RichPresence rpc = new RichPresence()
            {
                Details = "Frei wie der Wind",

                Assets = new Assets()
                {
                    LargeImageKey = "truck-icon",
                    LargeImageText = "Beyond the limits",
                    SmallImageKey = "vtcm-logo",
                    SmallImageText = "VTCManager Version " + ClientVersion
                }
            };
            client.SetPresence(rpc);
            client.Invoke();
        }
    }
}
