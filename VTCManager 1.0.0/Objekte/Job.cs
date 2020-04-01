using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCManager_1._0._0.Objekte
{
    class Job
    {
        public int ID;
        public bool jobStarted = false;
        public bool jobRunning = false;
        public float fuelatend;
        public float fuelconsumption;
        public bool jobFinished = false;
        public bool locationUpdate = false;
        public int totalDistance;
        public int invertedDistance;
        public int lastNotZeroDistance;
        public float lastCargoDamage;
        public double currentPercentage;
        public int updatedPercentage;
        public int fuelValue;
        public bool ownTrailerAttached;
       
        public string CityDestination;
        public string CitySource;
        public bool Tollgate;
        public float Tollgate_Payment;
        public bool Ferry;
        public bool Train;
        public string Refuel_Amount;
        public string Strafe;
        public string Faehre;
        public string FaehreKosten;
        public float fuelatstart;

        public Job()
        {
            //init
            this.ID = 0;
            this.jobStarted = false;
            this.jobRunning = false;
            this.fuelatend = 0;
            this.fuelconsumption = 0;
            this.jobFinished = false;
            this.locationUpdate = false;
            this.totalDistance = 0;
            this.invertedDistance = 0;
            this.lastNotZeroDistance = 0;
            this.lastCargoDamage = 0;
            this.currentPercentage = 0;
            this.updatedPercentage = 0;
            this.fuelValue = 0;
            this.ownTrailerAttached = false;
            this.CityDestination = "";
            this.CitySource = "";
            this.Tollgate = false;
            this.Tollgate_Payment = 0;
            this.Ferry = false;
            this.Train = false;
            this.Refuel_Amount = "";
            this.Strafe = "";
            this.Faehre = "";
            this.FaehreKosten = "";
            this.fuelatstart = 0;
        }

        public void clear()
        {
            //Variablen reseten
            this.ID = 0;
            this.jobStarted = false;
            this.jobRunning = false;
            this.fuelatend = 0;
            this.fuelconsumption = 0;
            this.jobFinished = false;
            this.locationUpdate = false;
            this.totalDistance = 0;
            this.invertedDistance = 0;
            this.lastNotZeroDistance = 0;
            this.lastCargoDamage = 0;
            this.currentPercentage = 0;
            this.updatedPercentage = 0;
            this.fuelValue = 0;
            this.ownTrailerAttached = false;
            this.CityDestination = "";
            this.CitySource = "";
            this.Tollgate = false;
            this.Tollgate_Payment = 0;
            this.Ferry = false;
            this.Train = false;
            this.Refuel_Amount = "";
            this.Strafe = "";
            this.Faehre = "";
            this.FaehreKosten = "";
            this.fuelatstart = 0;
        }

        public void cancel(Sound sound, Utilities utils, User user)
        {
            jobRunning = false;
            sound.Play(sound.ton_fehler);
            API api = new API();
            api.HTTPSRequestPost(api.api_server + api.canceltourpath, new Dictionary<string, string>()
              {
                { "authcode", user.authcode },
                { "job_id", ID.ToString() }
              }, true).ToString();
            utils.Log("Tour Cancel: " + user.authcode + " - " + ID);
            clear();
        }
    }
}
