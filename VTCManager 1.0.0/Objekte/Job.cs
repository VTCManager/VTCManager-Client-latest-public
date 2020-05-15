using System;
using System.Collections.Generic;

namespace VTCManager_1._0._0.Objekte
{
    class Job
    {
        Logging Logging = new Logging();
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
        public DateTime resttime;
        public double truck_damage;
        public double trailer_damage;
        public float cargo_damage;
        public Job()
        {
            //init
            ID = 0;
            jobStarted = false;
            jobRunning = false;
            fuelatend = 0;
            fuelconsumption = 0;
            jobFinished = false;
            locationUpdate = false;
            totalDistance = 0;
            invertedDistance = 0;
            lastNotZeroDistance = 0;
            lastCargoDamage = 0;
            currentPercentage = 0;
            updatedPercentage = 0;
            fuelValue = 0;
            ownTrailerAttached = false;
            CityDestination = "";
            CitySource = "";
            Tollgate = false;
            Tollgate_Payment = 0;
            Ferry = false;
            Train = false;
            Refuel_Amount = "";
            Strafe = "";
            Faehre = "";
            FaehreKosten = "";
            fuelatstart = 0;
            truck_damage = 0.0;
            trailer_damage = 0.0;
            cargo_damage = 0;
        }

        public void clear()
        {
            //Variablen reseten
            ID = 0;
            jobStarted = false;
            jobRunning = false;
            fuelatend = 0;
            fuelconsumption = 0;
            jobFinished = false;
            locationUpdate = false;
            totalDistance = 0;
            invertedDistance = 0;
            lastNotZeroDistance = 0;
            lastCargoDamage = 0;
            currentPercentage = 0;
            updatedPercentage = 0;
            fuelValue = 0;
            ownTrailerAttached = false;
            CityDestination = "";
            CitySource = "";
            Tollgate = false;
            Tollgate_Payment = 0;
            Ferry = false;
            Train = false;
            Refuel_Amount = "";
            Strafe = "";
            Faehre = "";
            FaehreKosten = "";
            fuelatstart = 0;
        }

        public void cancel(Sound sound, User user)
        {
            jobRunning = false;
            sound.Play(sound.ton_fehler);
            API api = new API();
            api.HTTPSRequestPost(api.api_server + api.canceltourpath, new Dictionary<string, string>()
              {
                { "authcode", user.authcode },
                { "job_id", ID.ToString() }
              }, true).ToString();
            Logging.WriteLOG("Tour Cancel: " + user.authcode + " - " + ID);
            clear();
        }
    }
}
