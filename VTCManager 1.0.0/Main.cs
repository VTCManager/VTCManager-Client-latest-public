using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using SCSSdkClient;
using VTCManager_1._0._0.Objekte;

namespace VTCManager_1._0._0
{
    public class Main : Form
    {
        private API api = new API();
        private Utilities utils = new Utilities();
        private SettingsManager settings;
        public Dictionary<string, string> lastJobDictionary = new Dictionary<string, string>();
        public SCSSdkTelemetry Telemetry;
        private IContainer components;
        private System.Timers.Timer send_tour_status;
        private Panel panel2;
        private Timer send_location;
        public MenuStrip menuStrip1;
        private ToolStripMenuItem einstellungenToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem;
        private ToolStripMenuItem topMenuAccount;
        private ToolStripMenuItem topmenuwebsite;
        private Panel panel4;
        private Label speed_lb;
        private Label cargo_lb;
        private Label depature_lb;
        private Label destination_lb;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private Label truck_lb;
        private Label label1;
        public Label label2;
        private Sound sound;
        private TableLayoutPanel tableLayoutPanel1;
        private string traffic_response;
        private Label status_jb_canc_lb;
        private double num1;
        private double num2;
        private Label version_lb;
        private ToolStripMenuItem MenuAbmeldenButton;
        private ToolStripMenuItem creditsToolStripMenuItem;
        public bool discordRPCalreadrunning;
        public bool stillTheSameJob;
        private NotifyIcon TaskBar_Icon;
        private ContextMenuStrip contextTaskbar;
        private ToolStripMenuItem öffnenToolStripMenuItem;
        private ToolStripMenuItem einstellungenToolStripMenuItem1;
        private ToolStripMenuItem webseiteToolStripMenuItem;
        private ToolStripMenuItem überToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem1;
        private LinkLabel linkLabel1;
        private ToolStripMenuItem GUI_SIZE_BUTTON;
        private GroupBox groupStatistiken;
        private Label user_company_lb;
        private Label statistic_panel_topic;
        private Label act_bank_balance_lb;
        private Label driven_tours_lb;
        private GroupBox groupVerkehr;
        private Button truckersMP_Button;
        private ToolStripMenuItem eventsToolStripMenuItem;

        // GUI by Thommy
        public int GUI_SIZE = 1;
        public static string truckersMP_Link;
        private ToolStripMenuItem lbl_Overlay;
        public static int truckersMP_autorun;
        public static int overlay_ist_offen = 0;
        private ToolStripMenuItem darkToolStripMenuItem;
        public static int overlay_Opacity;
        public Timer updateTraffic;
        private Label lbl_Reload_Time;
        public int Is_DarkMode_On;
        public Label lbl_Revision;
        private ToolStripMenuItem serverstatusToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel WebServer_Status_label;
        private ToolStripStatusLabel Label_DB_Server;
        public int reload = 20;
        public Timer anti_AFK_TIMER;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem oldCar1ToolStripMenuItem;
        private ToolStripMenuItem oldCar2ToolStripMenuItem;
        private ToolStripMenuItem oldCar3ToolStripMenuItem;
        private ToolStripMenuItem oldCar4ToolStripMenuItem;
        private ToolStripMenuItem keinsToolStripMenuItem;
        public int anti_afk_on_off;
        private Label label3;
        private PictureBox ets2_button;
        private PictureBox ats_button;
        public static string labelRevision;
        public int GameRuns;
        public string labelkmh;
        public bool refuel_beendet;
        private int jobrunningcounter;
        private Discord discord;
        private PictureBox pictureBox1;
        private Label label5;
        private ProgressBar Luft_Progress;
        private PictureBox pictureBox2;
        private Label label4;
        private Label Rest_KM_Label;
        private PictureBox Batterie_ICON;
        private PictureBox Handbremse_ICON;
        private PictureBox Motorbremse_ICON;
        private PictureBox Retarder_ICON;
        private Label label6;
        public GroupBox Dashboard_1;
        private ProgressBar progressBar_F;
        private string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager");
        private string logFile = @"\log.txt";
        public int spender = 0;
        private User user;
        private Job job;
        private ToolStripMenuItem frachtmarktToolStripMenuItem;
        private ToolStripStatusLabel User_Patreon_State;
        private bool jobStarted;

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        public Main(User user)
        {
            job = new Job();
            //Daten werden übernommen von Login.cs
            this.user = user;
            
            //Laden des Sound-Systems
            this.sound = new Sound(user.translation);

            //Benutzerkonfiguration laden
            this.settings = new SettingsManager();
            settings.LoadConfiguration();
            //UI init
            this.InitializeComponent();
            this.InitializeTranslation();
            try
            {
                this.load_traffic();
            }
            catch (Exception e)
            {
                utils.Log("Fehler beim Abrufen der Verkehrsdaten");
                MessageBox.Show("Fehler: Fehler beim Abrufen der Verkehrsdaten" + e.Message);
            }
            this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);

            //Telemetry Handler setzen
            this.Telemetry = new SCSSdkTelemetry();
            this.Telemetry.Data += this.Telemetry_Data;
            this.Telemetry.JobStarted += this.TelemetryOnJobStarted;
            this.Telemetry.JobCancelled += this.TelemetryJobCancelled;
            this.Telemetry.JobDelivered += this.TelemetryJobDelivered;
            //this.Telemetry.Fined += this.TelemetryFined;
            this.Telemetry.Tollgate += this.TelemetryTollgate;
            this.Telemetry.Ferry += this.TelemetryFerry;
            this.Telemetry.Train += this.TelemetryTrain;
            this.Telemetry.Refuel += this.TelemetryRefuel;
            //this.Telemetry.RefuelEnd += TelemetryRefuelEnd;
            //this.Telemetry.RefuelPayed += TelemetryRefuelPayed;
            if (this.Telemetry.Error == null)
                return;
            int num = (int)MessageBox.Show("Fehler beim Ausführen von:" + this.Telemetry.Map + "\r\n" + this.Telemetry.Error.Message + "\r\n\r\nStacktrace:\r\n" + this.Telemetry.Error.StackTrace);
        }

        private void InitializeTranslation()
        {
            //Labels müssen hier aufgrund von Problemen im Designer initialisiert werden
            this.label1.Text = user.translation.traffic_main_lb;
            this.einstellungenToolStripMenuItem.Text = user.translation.settings_lb;
            this.beendenToolStripMenuItem.Text = user.translation.exit_lb;
            this.topMenuAccount.Text = user.translation.topmenuaccount_lb;
            this.statistic_panel_topic.Text = user.translation.statistic_panel_topic + user.username.ToUpper();
            this.driven_tours_lb.Text = user.translation.driven_tours_lb + user.driven_tours;
            this.act_bank_balance_lb.Text = user.translation.act_bank_balance + user.bank_balance + "€";
            this.user_company_lb.Text = user.translation.user_company_lb + user.company;
            //this.version_lb.Text = translation.version;
            this.MenuAbmeldenButton.Text = user.translation.logout;


        }

        private void load_traffic()
        {
            //Tabelle zurücksetzen
            /*this.tableLayoutPanel1.Visible = false;
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.Location = new Point(13, 78);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";

            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new Size(518, 179);
            this.tableLayoutPanel1.TabIndex = 4;
            //Verkehrsdaten holen
            API api = new API();
            Dictionary<string, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("server", settings.tmp_server);
            postParameters.Add("game", "ets2");
            //Verkehsdaten konvertieren und in Tabelle einsetzen
            this.traffic_response = this.api.HTTPSRequestGet(this.api.trucky_api_server + this.api.get_traffic_path, postParameters).ToString();
            var truckyTopTraffic = TruckyTopTraffic.FromJson(this.traffic_response);
            this.AddItem(truckyTopTraffic.Response[0].Name, truckyTopTraffic.Response[0].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[1].Name, truckyTopTraffic.Response[1].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[2].Name, truckyTopTraffic.Response[2].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[3].Name, truckyTopTraffic.Response[3].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[4].Name, truckyTopTraffic.Response[4].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[5].Name, truckyTopTraffic.Response[5].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[6].Name, truckyTopTraffic.Response[6].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[7].Name, truckyTopTraffic.Response[7].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[8].Name, truckyTopTraffic.Response[8].Players.ToString());
            // Verkehr Label aktualisieren
            if (settings.tmp_server == "sim1") { label2.Text = "Server: Simulation 1"; }
            if (settings.tmp_server == "sim2") { label2.Text = "Server: Simulation 2"; }
            if (settings.tmp_server == "arc1") { label2.Text = "Server: Arcade 1"; }
            if (settings.tmp_server == "eupromods1") { label2.Text = "Server: ProMods 1"; }
            if (settings.tmp_server == "eupromods2") { label2.Text = "Server: ProMods 2"; }
            //Tabelle sichtbar 
            this.tableLayoutPanel1.Visible = true;*/
        }


        private void AddItem(string road, string traffic)
        {
            //Funktion für Verkehrstabelle
            RowStyle temp = tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1];
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            tableLayoutPanel1.Controls.Add(new Label() { Text = road }, 0, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.Controls.Add(new Label() { Text = traffic }, 1, tableLayoutPanel1.RowCount - 1);
        }

        //Telemetry Handler
        private void TelemetryOnJobFinished(object sender, EventArgs args) => job.jobFinished = true;

        private void TelemetryOnJobStarted(object sender, EventArgs e) => 
            job.jobStarted = true;



        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new TelemetryData(Telemetry_Data), data, updated);
                    return;
                }
                else
                {
                    //Läuft das Spiel?
                    if (Utilities.IsGameRunning)
                    {
                        //falls Daten erhalten werden
                        if (data.SdkActive)
                        {
                            user.CoordinateX = data.TruckValues.CurrentValues.PositionValue.Position.X;
                            user.CoordinateZ = data.TruckValues.CurrentValues.PositionValue.Position.Y;
                            user.Geschwindigkeit = (float)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                            user.Spiel = data.Game.ToString();

                            Motorbremse_ICON.Visible = (data.TruckValues.CurrentValues.MotorValues.BrakeValues.MotorBrake) ? true : false;
                            Handbremse_ICON.Visible = (data.TruckValues.CurrentValues.MotorValues.BrakeValues.ParkingBrake) ? true : false;
                            Retarder_ICON.Visible = (((float)data.ControlValues.InputValues.Brake) >= 0.1) ? true : false;

                            //Text sichtbar
                            this.truck_lb.Visible = true;
                            this.cargo_lb.Visible = true;
                            destination_lb.Visible = true;
                            depature_lb.Visible = true;

                            truckersMP_Button.Visible = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad"))) ? false : true;

                            if (data.Paused == false)
                            {
                                GameRuns = 1;

                                job.currentPercentage = (((((double)data.NavigationValues.NavigationDistance / 1000) / (double)data.JobValues.PlannedDistanceKm) * 100) - 100) * -1;

                                // ###################   FUEL PROGRESS    ##############################################
                                progressBar_F.Maximum = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.Fuel);
                                progressBar_F.Value = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount);


                                // #########################   REST KM    ##############################################
                                Rest_KM_Label.Text = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Range + " KM (" + Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount) + " L)";

                                // #########################   LUFTDRUCK   #############################################
                                Luft_Progress.Maximum = 150;
                                Luft_Progress.Value = (int)data.TruckValues.CurrentValues.MotorValues.BrakeValues.AirPressure;

                                // #########################   STRECKENVERLAUF   #######################################
                                labelkmh = (data.Game.ToString() == "Ets2") ? " KM/H" : " mp/h";

                                if (data.Game.ToString() == "Ets2")
                                {
                                    speed_lb.Text = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph + labelkmh;
                                    user.Geschwindigkeit = (float)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                                }
                                else
                                {
                                    speed_lb.Text = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Mph + labelkmh;
                                    user.Geschwindigkeit = (float)data.TruckValues.CurrentValues.DashboardValues.Speed.Mph;
                                }

                                // ##########################  AUSGABE TRUCK MODEL etc.  #############################
                                truck_lb.Text = "Dein Truck: " + data.TruckValues.ConstantsValues.Brand + ", Modell: " + data.TruckValues.ConstantsValues.Name;

                                // ##############################   JOB DATA   ####################################
                                if (data.JobValues.CargoLoaded == false)
                                {
                                    this.discord.noTour();
                                    cargo_lb.Text = "Leerfahrt";
                                    destination_lb.Visible = false;
                                    depature_lb.Text = "";
                                }
                                else
                                {
                                    if (this.jobrunningcounter == 30)
                                    {
                                        Console.WriteLine("tiick");
                                        this.api.HTTPSRequestPost(this.api.api_server + this.api.job_update_path, new Dictionary<string, string>()
                                    {
                                        { "authcode", user.authcode },
                                        { "job_id", job.ID.ToString() },
                                        { "percentage", job.currentPercentage.ToString() }
                                    }, false).ToString();
                                        this.jobrunningcounter = 0;
                                        utils.Log("Tour Tick: " + user.authcode + " - " + job.ID + " - " + job.currentPercentage.ToString());
                                    }
                                    this.jobrunningcounter++;
                                }

                            }
                            else
                            {
                                speed_lb.Text = user.translation.waiting_for_ets;
                            }
                            bool flag;
                            using (Dictionary<string, string>.Enumerator enumerator = this.lastJobDictionary.GetEnumerator())
                                flag = !enumerator.MoveNext();

                        }
                        else
                        {
                            this.truck_lb.Visible = false;
                            this.cargo_lb.Visible = false;
                            destination_lb.Visible = false;
                            depature_lb.Visible = false;
                            this.speed_lb.Text = user.translation.waiting_for_ets;
                        }
                    }

                //label_25:
                        double num2;
                    if (job.jobStarted)
                    {
                        job = new Job();
                        job.jobStarted = false;
                        this.lastJobDictionary.Clear();
                        this.sound.Play(sound.ton_tour_gestartet);
                        job.totalDistance = (int)data.NavigationValues.NavigationDistance;
                        num2 = (double)data.JobValues.Income * 0.15;
                        this.cargo_lb.Text = "Deine Fracht: " + ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString() + " Tonnen " + data.JobValues.CargoValues.Name;
                        this.depature_lb.Text = "Von: " + data.JobValues.CitySource + " ( " + data.JobValues.CompanySource + " ) nach: " + data.JobValues.CityDestination + " ( " + data.JobValues.CompanyDestination + " )";
                        job.fuelatstart = data.TruckValues.ConstantsValues.CapacityValues.Fuel;

                        Dictionary<string, string> postParameters = new Dictionary<string, string>();
                        postParameters.Add("authcode", user.authcode);
                        postParameters.Add("cargo", data.JobValues.CargoValues.Name);
                        postParameters.Add("weight", ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString());
                        postParameters.Add("depature", data.JobValues.CitySource);
                        postParameters.Add("depature_company", data.JobValues.CompanySource);
                        postParameters.Add("destination_company", data.JobValues.CompanyDestination);
                        postParameters.Add("destination", data.JobValues.CityDestination);
                        postParameters.Add("truck_manufacturer", data.TruckValues.ConstantsValues.Brand);
                        postParameters.Add("truck_model", data.TruckValues.ConstantsValues.Name);
                        postParameters.Add("distance", data.JobValues.PlannedDistanceKm.ToString());
                        job.ID = Convert.ToInt32(this.api.HTTPSRequestPost(this.api.api_server + this.api.new_job_path, postParameters, true).ToString());

                        utils.Log("Tour START: " + user.authcode + ", Cargo: " + data.JobValues.CargoValues.Name + ", " + ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString() + " Tonnen, Startort: " + data.JobValues.CitySource + ", Start-Firma: " + data.JobValues.CompanySource + ", Zielort: " + data.JobValues.CityDestination + ", Ziel-Firma: " + data.JobValues.CompanyDestination + ", LKW: " + data.TruckValues.ConstantsValues.Brand + " " + data.TruckValues.ConstantsValues.Name + ", Strecke: " + data.JobValues.PlannedDistanceKm.ToString() + " KM");
                                    

                        Dictionary<string, string> lastJobDictionary = this.lastJobDictionary;
                        this.lastJobDictionary.Add("cargo", data.JobValues.CargoValues.Name);
                        this.lastJobDictionary.Add("source", data.JobValues.CitySource);
                        this.lastJobDictionary.Add("destination", data.JobValues.CityDestination);
                        this.lastJobDictionary.Add("income", data.JobValues.Income.ToString());
                        this.lastJobDictionary.Add("weight", data.JobValues.CargoValues.Mass.ToString());

                        this.discord.onTour(data.JobValues.CityDestination, data.JobValues.CitySource, data.JobValues.CargoValues.Name, ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString());
                        job.CitySource = data.JobValues.CitySource;
                        job.CityDestination = data.JobValues.CityDestination;
                        this.send_tour_status.Enabled = true;
                        this.send_tour_status.Start();
                    }

                    if (job.jobFinished)
                    {
                        if (this.lastJobDictionary["cargo"] == data.JobValues.CargoValues.Name && this.lastJobDictionary["source"] == data.JobValues.CitySource && this.lastJobDictionary["destination"] == data.JobValues.CityDestination)
                        {
                            this.send_tour_status.Enabled = false;
                            this.sound.Play(sound.ton_tour_beendet);
                            this.send_tour_status.Enabled = false;
                            job.jobFinished = false;
                            job.fuelatend = (float)data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                            job.fuelconsumption = job.fuelatstart - job.fuelatend;
                            Console.WriteLine(job.fuelconsumption);
                            Dictionary<string, string> postParameters = new Dictionary<string, string>();
                            postParameters.Add("authcode", user.authcode);
                            postParameters.Add("job_id", job.ID.ToString());
                            Dictionary<string, string> dictionary2 = postParameters;
                            num2 = Math.Floor((double)data.TruckValues.CurrentValues.DamageValues.Transmission * 100.0 / 1.0);
                            string str3 = num2.ToString();
                            dictionary2.Add("trailer_damage", str3);
                            postParameters.Add("income", data.JobValues.Income.ToString());
                            if (job.fuelconsumption > data.TruckValues.ConstantsValues.CapacityValues.Fuel)
                            {
                                postParameters.Add("refueled", "true");
                            }
                            postParameters.Add("fuelconsumption", job.fuelconsumption.ToString());

                            Console.WriteLine(this.api.HTTPSRequestPost(this.api.api_server + this.api.finishjob_path, postParameters, true).ToString());
                            job.jobFinished = false;
                            utils.Log("Tour FINISH: " + user.authcode + ", " + job.ID + ", Einkommen: " + data.JobValues.Income.ToString() + " €" + ", Damage: " + str3);
                            job.clear();
                            this.destination_lb.Text = "";
                            this.depature_lb.Text = "";
                        //this.cargo_lb.Text = translation.no_cargo_lb;
                    


                        }
                    }
                    job.invertedDistance = job.totalDistance - (int)Math.Round((double)data.NavigationValues.NavigationDistance, 0);
                }
            }
            catch {  }
        }

        private void send_tour_status_Tick(object sender, EventArgs e)
        {
            job.jobRunning = true;
            this.locationupdate();
            
        }

        private void locationupdate()
        {
            Dashboard_1.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard") == "1") ? true : false;
            double num3 = user.rotation;
                Dictionary<string, string> postParameters = new Dictionary<string, string>();
                Dictionary<string, string> dictionary1 = postParameters;


                num1 = user.CoordinateX;
                string str1 = num1.ToString();
                dictionary1.Add("coordinate_x", str1);
                Dictionary<string, string> dictionary2 = postParameters;
                num1 = user.CoordinateZ;
                string str2 = num1.ToString();
                dictionary2.Add("coordinate_y", str2);
                Dictionary<string, string> dictionary3 = postParameters;
                num2 = -(num3 * 180.0 / Math.PI);
                string str3 = num2.ToString();
                dictionary3.Add("rotation", str3);
                postParameters.Add("authcode", user.authcode);
                postParameters.Add("percentage", job.currentPercentage.ToString());
                postParameters.Add("game", user.Spiel);

                this.api.HTTPSRequestPost(this.api.api_server + this.api.loc_update_path, postParameters, false).ToString();
                utils.Log("Tour UPDATE: " + user.authcode + ", " + job.ID + ", " + job.currentPercentage.ToString() + ", " + user.Spiel);
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            TaskBar_Icon.Dispose();
            utils.Log("<INFO> CLIENT_CLOSED");
        }

        private void send_location_Tick(object sender, EventArgs e)
        {
            job.locationUpdate = true;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.send_tour_status = new System.Timers.Timer();
            this.send_location = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lbl_Overlay = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.status_jb_canc_lb = new System.Windows.Forms.Label();
            this.truck_lb = new System.Windows.Forms.Label();
            this.destination_lb = new System.Windows.Forms.Label();
            this.depature_lb = new System.Windows.Forms.Label();
            this.cargo_lb = new System.Windows.Forms.Label();
            this.speed_lb = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Dashboard_1 = new System.Windows.Forms.GroupBox();
            this.progressBar_F = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.Rest_KM_Label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Luft_Progress = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.version_lb = new System.Windows.Forms.Label();
            this.TaskBar_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextTaskbar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.webseiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupStatistiken = new System.Windows.Forms.GroupBox();
            this.user_company_lb = new System.Windows.Forms.Label();
            this.statistic_panel_topic = new System.Windows.Forms.Label();
            this.act_bank_balance_lb = new System.Windows.Forms.Label();
            this.driven_tours_lb = new System.Windows.Forms.Label();
            this.groupVerkehr = new System.Windows.Forms.GroupBox();
            this.lbl_Reload_Time = new System.Windows.Forms.Label();
            this.updateTraffic = new System.Windows.Forms.Timer(this.components);
            this.lbl_Revision = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.WebServer_Status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_DB_Server = new System.Windows.Forms.ToolStripStatusLabel();
            this.User_Patreon_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.anti_AFK_TIMER = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.ats_button = new System.Windows.Forms.PictureBox();
            this.ets2_button = new System.Windows.Forms.PictureBox();
            this.truckersMP_Button = new System.Windows.Forms.Button();
            this.Retarder_ICON = new System.Windows.Forms.PictureBox();
            this.Batterie_ICON = new System.Windows.Forms.PictureBox();
            this.Handbremse_ICON = new System.Windows.Forms.PictureBox();
            this.Motorbremse_ICON = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverstatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenuAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbmeldenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.topmenuwebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GUI_SIZE_BUTTON = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frachtmarktToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.Dashboard_1.SuspendLayout();
            this.contextTaskbar.SuspendLayout();
            this.groupStatistiken.SuspendLayout();
            this.groupVerkehr.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ats_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ets2_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Retarder_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Batterie_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Handbremse_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Motorbremse_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // send_tour_status
            // 
            this.send_tour_status.Enabled = true;
            this.send_tour_status.Interval = 15000D;
            this.send_tour_status.SynchronizingObject = this;
            this.send_tour_status.Elapsed += new System.Timers.ElapsedEventHandler(this.send_tour_status_Tick);
            // 
            // send_location
            // 
            this.send_location.Enabled = true;
            this.send_location.Interval = 15000;
            this.send_location.Tick += new System.EventHandler(this.send_location_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.topMenuAccount,
            this.topmenuwebsite,
            this.eventsToolStripMenuItem,
            this.GUI_SIZE_BUTTON,
            this.lbl_Overlay,
            this.darkToolStripMenuItem,
            this.toolStripMenuItem1,
            this.frachtmarktToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1388, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lbl_Overlay
            // 
            this.lbl_Overlay.Name = "lbl_Overlay";
            this.lbl_Overlay.Size = new System.Drawing.Size(76, 28);
            this.lbl_Overlay.Text = "Overlay";
            this.lbl_Overlay.Visible = false;
            this.lbl_Overlay.Click += new System.EventHandler(this.overlayToolStripMenuItem_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(342, 342);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(145, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(powered by Truckyapp.com)";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Verkehr";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "...";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.status_jb_canc_lb);
            this.panel2.Controls.Add(this.truck_lb);
            this.panel2.Controls.Add(this.destination_lb);
            this.panel2.Controls.Add(this.depature_lb);
            this.panel2.Controls.Add(this.cargo_lb);
            this.panel2.Controls.Add(this.speed_lb);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel2.Location = new System.Drawing.Point(540, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 582);
            this.panel2.TabIndex = 2;
            // 
            // status_jb_canc_lb
            // 
            this.status_jb_canc_lb.AutoSize = true;
            this.status_jb_canc_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.status_jb_canc_lb.Location = new System.Drawing.Point(148, 245);
            this.status_jb_canc_lb.Name = "status_jb_canc_lb";
            this.status_jb_canc_lb.Size = new System.Drawing.Size(0, 19);
            this.status_jb_canc_lb.TabIndex = 6;
            // 
            // truck_lb
            // 
            this.truck_lb.AutoSize = true;
            this.truck_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.truck_lb.Location = new System.Drawing.Point(47, 110);
            this.truck_lb.Name = "truck_lb";
            this.truck_lb.Size = new System.Drawing.Size(48, 19);
            this.truck_lb.TabIndex = 5;
            this.truck_lb.Text = "Truck: ";
            // 
            // destination_lb
            // 
            this.destination_lb.AutoSize = true;
            this.destination_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.destination_lb.Location = new System.Drawing.Point(47, 166);
            this.destination_lb.Name = "destination_lb";
            this.destination_lb.Size = new System.Drawing.Size(0, 19);
            this.destination_lb.TabIndex = 3;
            // 
            // depature_lb
            // 
            this.depature_lb.AutoSize = true;
            this.depature_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.depature_lb.Location = new System.Drawing.Point(47, 149);
            this.depature_lb.Name = "depature_lb";
            this.depature_lb.Size = new System.Drawing.Size(78, 19);
            this.depature_lb.TabIndex = 2;
            this.depature_lb.Text = "Departure: ";
            // 
            // cargo_lb
            // 
            this.cargo_lb.AutoSize = true;
            this.cargo_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cargo_lb.Location = new System.Drawing.Point(47, 129);
            this.cargo_lb.Name = "cargo_lb";
            this.cargo_lb.Size = new System.Drawing.Size(55, 19);
            this.cargo_lb.TabIndex = 1;
            this.cargo_lb.Text = "Freight:";
            // 
            // speed_lb
            // 
            this.speed_lb.AutoSize = true;
            this.speed_lb.BackColor = System.Drawing.Color.Transparent;
            this.speed_lb.Font = new System.Drawing.Font("Verdana", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed_lb.Location = new System.Drawing.Point(186, 51);
            this.speed_lb.Name = "speed_lb";
            this.speed_lb.Size = new System.Drawing.Size(0, 42);
            this.speed_lb.TabIndex = 0;
            this.speed_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(39, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 179);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.Dashboard_1);
            this.panel4.Location = new System.Drawing.Point(1097, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(284, 582);
            this.panel4.TabIndex = 4;
            // 
            // Dashboard_1
            // 
            this.Dashboard_1.Controls.Add(this.progressBar_F);
            this.Dashboard_1.Controls.Add(this.Retarder_ICON);
            this.Dashboard_1.Controls.Add(this.label6);
            this.Dashboard_1.Controls.Add(this.Batterie_ICON);
            this.Dashboard_1.Controls.Add(this.Handbremse_ICON);
            this.Dashboard_1.Controls.Add(this.Rest_KM_Label);
            this.Dashboard_1.Controls.Add(this.Motorbremse_ICON);
            this.Dashboard_1.Controls.Add(this.label5);
            this.Dashboard_1.Controls.Add(this.Luft_Progress);
            this.Dashboard_1.Controls.Add(this.pictureBox1);
            this.Dashboard_1.Controls.Add(this.pictureBox2);
            this.Dashboard_1.Controls.Add(this.label4);
            this.Dashboard_1.Location = new System.Drawing.Point(5, 328);
            this.Dashboard_1.Name = "Dashboard_1";
            this.Dashboard_1.Size = new System.Drawing.Size(276, 245);
            this.Dashboard_1.TabIndex = 15;
            this.Dashboard_1.TabStop = false;
            this.Dashboard_1.Text = "Dashboard";
            // 
            // progressBar_F
            // 
            this.progressBar_F.Location = new System.Drawing.Point(9, 124);
            this.progressBar_F.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar_F.Name = "progressBar_F";
            this.progressBar_F.Size = new System.Drawing.Size(249, 23);
            this.progressBar_F.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Air Pressure";
            // 
            // Rest_KM_Label
            // 
            this.Rest_KM_Label.AutoSize = true;
            this.Rest_KM_Label.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rest_KM_Label.Location = new System.Drawing.Point(44, 102);
            this.Rest_KM_Label.Name = "Rest_KM_Label";
            this.Rest_KM_Label.Size = new System.Drawing.Size(52, 18);
            this.Rest_KM_Label.TabIndex = 6;
            this.Rest_KM_Label.Text = "0000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 9);
            this.label5.TabIndex = 5;
            this.label5.Text = "0        10         20         30        40        50        60        70        " +
    "80        90        100    ";
            // 
            // Luft_Progress
            // 
            this.Luft_Progress.Location = new System.Drawing.Point(10, 197);
            this.Luft_Progress.MarqueeAnimationSpeed = 0;
            this.Luft_Progress.Name = "Luft_Progress";
            this.Luft_Progress.Size = new System.Drawing.Size(248, 23);
            this.Luft_Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Luft_Progress.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 9);
            this.label4.TabIndex = 2;
            this.label4.Text = "0        10         20         30        40        50        60        70        " +
    "80        90        100    ";
            // 
            // version_lb
            // 
            this.version_lb.AutoSize = true;
            this.version_lb.Location = new System.Drawing.Point(1287, 9);
            this.version_lb.Name = "version_lb";
            this.version_lb.Size = new System.Drawing.Size(0, 13);
            this.version_lb.TabIndex = 5;
            // 
            // TaskBar_Icon
            // 
            this.TaskBar_Icon.BalloonTipText = "VTC-Manager läuft im Hintergrund";
            this.TaskBar_Icon.BalloonTipTitle = "VTC-Manager";
            this.TaskBar_Icon.ContextMenuStrip = this.contextTaskbar;
            this.TaskBar_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskBar_Icon.Icon")));
            this.TaskBar_Icon.Text = "VTC-Manager";
            this.TaskBar_Icon.Visible = true;
            // 
            // contextTaskbar
            // 
            this.contextTaskbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.öffnenToolStripMenuItem,
            this.einstellungenToolStripMenuItem1,
            this.webseiteToolStripMenuItem,
            this.überToolStripMenuItem,
            this.beendenToolStripMenuItem1});
            this.contextTaskbar.Name = "contextTaskbar";
            this.contextTaskbar.Size = new System.Drawing.Size(146, 114);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen";
            // 
            // einstellungenToolStripMenuItem1
            // 
            this.einstellungenToolStripMenuItem1.Name = "einstellungenToolStripMenuItem1";
            this.einstellungenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.einstellungenToolStripMenuItem1.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem1.Click += new System.EventHandler(this.einstellungenToolStripMenuItem1_Click);
            // 
            // webseiteToolStripMenuItem
            // 
            this.webseiteToolStripMenuItem.Name = "webseiteToolStripMenuItem";
            this.webseiteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.webseiteToolStripMenuItem.Text = "Webseite";
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.überToolStripMenuItem.Text = "Über...";
            // 
            // beendenToolStripMenuItem1
            // 
            this.beendenToolStripMenuItem1.Name = "beendenToolStripMenuItem1";
            this.beendenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.beendenToolStripMenuItem1.Text = "Beenden";
            // 
            // groupStatistiken
            // 
            this.groupStatistiken.BackColor = System.Drawing.Color.Transparent;
            this.groupStatistiken.Controls.Add(this.ats_button);
            this.groupStatistiken.Controls.Add(this.ets2_button);
            this.groupStatistiken.Controls.Add(this.truckersMP_Button);
            this.groupStatistiken.Controls.Add(this.user_company_lb);
            this.groupStatistiken.Controls.Add(this.statistic_panel_topic);
            this.groupStatistiken.Controls.Add(this.act_bank_balance_lb);
            this.groupStatistiken.Controls.Add(this.driven_tours_lb);
            this.groupStatistiken.Location = new System.Drawing.Point(0, 35);
            this.groupStatistiken.Name = "groupStatistiken";
            this.groupStatistiken.Size = new System.Drawing.Size(534, 178);
            this.groupStatistiken.TabIndex = 6;
            this.groupStatistiken.TabStop = false;
            // 
            // user_company_lb
            // 
            this.user_company_lb.AutoSize = true;
            this.user_company_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.user_company_lb.Location = new System.Drawing.Point(16, 86);
            this.user_company_lb.Name = "user_company_lb";
            this.user_company_lb.Size = new System.Drawing.Size(178, 19);
            this.user_company_lb.TabIndex = 5;
            this.user_company_lb.Text = "angestellt bei: Selbstständig";
            // 
            // statistic_panel_topic
            // 
            this.statistic_panel_topic.AutoSize = true;
            this.statistic_panel_topic.BackColor = System.Drawing.Color.Transparent;
            this.statistic_panel_topic.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statistic_panel_topic.Location = new System.Drawing.Point(12, 22);
            this.statistic_panel_topic.Name = "statistic_panel_topic";
            this.statistic_panel_topic.Size = new System.Drawing.Size(174, 30);
            this.statistic_panel_topic.TabIndex = 2;
            this.statistic_panel_topic.Text = "User\'s  Statistiken";
            this.statistic_panel_topic.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // act_bank_balance_lb
            // 
            this.act_bank_balance_lb.AutoSize = true;
            this.act_bank_balance_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.act_bank_balance_lb.Location = new System.Drawing.Point(16, 67);
            this.act_bank_balance_lb.Name = "act_bank_balance_lb";
            this.act_bank_balance_lb.Size = new System.Drawing.Size(139, 19);
            this.act_bank_balance_lb.TabIndex = 4;
            this.act_bank_balance_lb.Text = "aktueller Kontostand:";
            // 
            // driven_tours_lb
            // 
            this.driven_tours_lb.AutoSize = true;
            this.driven_tours_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.driven_tours_lb.Location = new System.Drawing.Point(16, 48);
            this.driven_tours_lb.Name = "driven_tours_lb";
            this.driven_tours_lb.Size = new System.Drawing.Size(119, 19);
            this.driven_tours_lb.TabIndex = 3;
            this.driven_tours_lb.Text = "gefahrene Touren:";
            // 
            // groupVerkehr
            // 
            this.groupVerkehr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupVerkehr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupVerkehr.BackColor = System.Drawing.Color.Transparent;
            this.groupVerkehr.Controls.Add(this.lbl_Reload_Time);
            this.groupVerkehr.Controls.Add(this.tableLayoutPanel1);
            this.groupVerkehr.Controls.Add(this.label1);
            this.groupVerkehr.Controls.Add(this.linkLabel1);
            this.groupVerkehr.Controls.Add(this.label2);
            this.groupVerkehr.Location = new System.Drawing.Point(0, 243);
            this.groupVerkehr.Name = "groupVerkehr";
            this.groupVerkehr.Size = new System.Drawing.Size(537, 367);
            this.groupVerkehr.TabIndex = 7;
            this.groupVerkehr.TabStop = false;
            // 
            // lbl_Reload_Time
            // 
            this.lbl_Reload_Time.AutoSize = true;
            this.lbl_Reload_Time.Location = new System.Drawing.Point(12, 345);
            this.lbl_Reload_Time.Name = "lbl_Reload_Time";
            this.lbl_Reload_Time.Size = new System.Drawing.Size(16, 13);
            this.lbl_Reload_Time.TabIndex = 6;
            this.lbl_Reload_Time.Text = "...";
            // 
            // updateTraffic
            // 
            this.updateTraffic.Enabled = true;
            this.updateTraffic.Interval = 30000;
            this.updateTraffic.Tick += new System.EventHandler(this.updateTraffic_Tick);
            // 
            // lbl_Revision
            // 
            this.lbl_Revision.AutoSize = true;
            this.lbl_Revision.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Revision.Location = new System.Drawing.Point(1312, 9);
            this.lbl_Revision.Name = "lbl_Revision";
            this.lbl_Revision.Size = new System.Drawing.Size(16, 13);
            this.lbl_Revision.TabIndex = 8;
            this.lbl_Revision.Text = "...";
            this.lbl_Revision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WebServer_Status_label,
            this.Label_DB_Server,
            this.User_Patreon_State});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1388, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // WebServer_Status_label
            // 
            this.WebServer_Status_label.Name = "WebServer_Status_label";
            this.WebServer_Status_label.Size = new System.Drawing.Size(10, 17);
            this.WebServer_Status_label.Text = ".";
            this.WebServer_Status_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_DB_Server
            // 
            this.Label_DB_Server.Name = "Label_DB_Server";
            this.Label_DB_Server.Size = new System.Drawing.Size(10, 17);
            this.Label_DB_Server.Text = ".";
            // 
            // User_Patreon_State
            // 
            this.User_Patreon_State.Name = "User_Patreon_State";
            this.User_Patreon_State.Size = new System.Drawing.Size(24, 17);
            this.User_Patreon_State.Text = "Pat";
            // 
            // anti_AFK_TIMER
            // 
            this.anti_AFK_TIMER.Interval = 240000;
            this.anti_AFK_TIMER.Tick += new System.EventHandler(this.anti_AFK_TIMER_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(1261, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Version:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ats_button
            // 
            this.ats_button.Image = global::VTCManager_1._0._0.Properties.Resources.ats2l;
            this.ats_button.Location = new System.Drawing.Point(17, 124);
            this.ats_button.Name = "ats_button";
            this.ats_button.Size = new System.Drawing.Size(100, 54);
            this.ats_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ats_button.TabIndex = 8;
            this.ats_button.TabStop = false;
            this.ats_button.Visible = false;
            this.ats_button.Click += new System.EventHandler(this.ats_button_Click);
            // 
            // ets2_button
            // 
            this.ets2_button.Image = global::VTCManager_1._0._0.Properties.Resources._280px_Ets2_logo;
            this.ets2_button.Location = new System.Drawing.Point(225, 124);
            this.ets2_button.Name = "ets2_button";
            this.ets2_button.Size = new System.Drawing.Size(100, 54);
            this.ets2_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ets2_button.TabIndex = 7;
            this.ets2_button.TabStop = false;
            this.ets2_button.Visible = false;
            this.ets2_button.Click += new System.EventHandler(this.ets2_button_Click);
            // 
            // truckersMP_Button
            // 
            this.truckersMP_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.truckersMP_Button.BackColor = System.Drawing.Color.Transparent;
            this.truckersMP_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("truckersMP_Button.BackgroundImage")));
            this.truckersMP_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.truckersMP_Button.FlatAppearance.BorderSize = 0;
            this.truckersMP_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.truckersMP_Button.Location = new System.Drawing.Point(450, 124);
            this.truckersMP_Button.Margin = new System.Windows.Forms.Padding(0);
            this.truckersMP_Button.Name = "truckersMP_Button";
            this.truckersMP_Button.Size = new System.Drawing.Size(84, 54);
            this.truckersMP_Button.TabIndex = 6;
            this.truckersMP_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.truckersMP_Button.UseVisualStyleBackColor = false;
            this.truckersMP_Button.Click += new System.EventHandler(this.truckersMP_Button_Click);
            // 
            // Retarder_ICON
            // 
            this.Retarder_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.retarder1;
            this.Retarder_ICON.Location = new System.Drawing.Point(136, 21);
            this.Retarder_ICON.Name = "Retarder_ICON";
            this.Retarder_ICON.Size = new System.Drawing.Size(63, 53);
            this.Retarder_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Retarder_ICON.TabIndex = 12;
            this.Retarder_ICON.TabStop = false;
            // 
            // Batterie_ICON
            // 
            this.Batterie_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_autobatterie_80;
            this.Batterie_ICON.Location = new System.Drawing.Point(10, 21);
            this.Batterie_ICON.Name = "Batterie_ICON";
            this.Batterie_ICON.Size = new System.Drawing.Size(63, 53);
            this.Batterie_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Batterie_ICON.TabIndex = 7;
            this.Batterie_ICON.TabStop = false;
            // 
            // Handbremse_ICON
            // 
            this.Handbremse_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.bremse_schwarz;
            this.Handbremse_ICON.Location = new System.Drawing.Point(73, 21);
            this.Handbremse_ICON.Name = "Handbremse_ICON";
            this.Handbremse_ICON.Size = new System.Drawing.Size(63, 53);
            this.Handbremse_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Handbremse_ICON.TabIndex = 8;
            this.Handbremse_ICON.TabStop = false;
            // 
            // Motorbremse_ICON
            // 
            this.Motorbremse_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_motor_128;
            this.Motorbremse_ICON.Location = new System.Drawing.Point(195, 21);
            this.Motorbremse_ICON.Name = "Motorbremse_ICON";
            this.Motorbremse_ICON.Size = new System.Drawing.Size(63, 53);
            this.Motorbremse_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Motorbremse_ICON.TabIndex = 9;
            this.Motorbremse_ICON.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_benzin_24;
            this.pictureBox1.Location = new System.Drawing.Point(11, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_luftdruck_24;
            this.pictureBox2.Location = new System.Drawing.Point(10, 168);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 27);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem,
            this.creditsToolStripMenuItem,
            this.beendenToolStripMenuItem,
            this.serverstatusToolStripMenuItem});
            this.dateiToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dateiToolStripMenuItem.Image")));
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            this.dateiToolStripMenuItem.ToolTipText = "Hauptmenü";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItemClick);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.creditsToolStripMenuItem.Text = "Über...";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.CreditsToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItemClick);
            // 
            // serverstatusToolStripMenuItem
            // 
            this.serverstatusToolStripMenuItem.Name = "serverstatusToolStripMenuItem";
            this.serverstatusToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.serverstatusToolStripMenuItem.Text = "Serverstatus (Inaktiv)";
            this.serverstatusToolStripMenuItem.Click += new System.EventHandler(this.serverstatusToolStripMenuItem_Click);
            // 
            // topMenuAccount
            // 
            this.topMenuAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAbmeldenButton});
            this.topMenuAccount.Image = ((System.Drawing.Image)(resources.GetObject("topMenuAccount.Image")));
            this.topMenuAccount.Name = "topMenuAccount";
            this.topMenuAccount.Size = new System.Drawing.Size(102, 28);
            this.topMenuAccount.Text = "Account";
            this.topMenuAccount.ToolTipText = "Client an/abmelden";
            // 
            // MenuAbmeldenButton
            // 
            this.MenuAbmeldenButton.Name = "MenuAbmeldenButton";
            this.MenuAbmeldenButton.Size = new System.Drawing.Size(151, 26);
            this.MenuAbmeldenButton.Text = "Abmelden";
            this.MenuAbmeldenButton.Click += new System.EventHandler(this.MenuAbmeldenButton_Click);
            // 
            // topmenuwebsite
            // 
            this.topmenuwebsite.Image = ((System.Drawing.Image)(resources.GetObject("topmenuwebsite.Image")));
            this.topmenuwebsite.Name = "topmenuwebsite";
            this.topmenuwebsite.Size = new System.Drawing.Size(101, 28);
            this.topmenuwebsite.Text = "Website";
            this.topmenuwebsite.ToolTipText = "Gehe zu unserer Homepage";
            this.topmenuwebsite.Click += new System.EventHandler(this.topMenuWebsiteClick);
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eventsToolStripMenuItem.Image")));
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(91, 28);
            this.eventsToolStripMenuItem.Text = "Events";
            this.eventsToolStripMenuItem.ToolTipText = "Zeige aktuelle Events (in Bearbeitung)";
            this.eventsToolStripMenuItem.Visible = false;
            // 
            // GUI_SIZE_BUTTON
            // 
            this.GUI_SIZE_BUTTON.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GUI_SIZE_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GUI_SIZE_BUTTON.Image")));
            this.GUI_SIZE_BUTTON.Name = "GUI_SIZE_BUTTON";
            this.GUI_SIZE_BUTTON.Size = new System.Drawing.Size(36, 28);
            this.GUI_SIZE_BUTTON.Text = "Button_Groesse";
            this.GUI_SIZE_BUTTON.ToolTipText = "Ansicht verkleinern / vergrößern";
            this.GUI_SIZE_BUTTON.Click += new System.EventHandler(this.buttonGroesseToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_film_noir_50;
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            this.darkToolStripMenuItem.ToolTipText = "Komm auf die Dunkle Seite";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oldCar1ToolStripMenuItem,
            this.oldCar2ToolStripMenuItem,
            this.oldCar3ToolStripMenuItem,
            this.oldCar4ToolStripMenuItem,
            this.keinsToolStripMenuItem});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(36, 28);
            // 
            // oldCar1ToolStripMenuItem
            // 
            this.oldCar1ToolStripMenuItem.Name = "oldCar1ToolStripMenuItem";
            this.oldCar1ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar1ToolStripMenuItem.Text = "Old Car 1";
            this.oldCar1ToolStripMenuItem.Click += new System.EventHandler(this.oldCar1ToolStripMenuItem_Click);
            // 
            // oldCar2ToolStripMenuItem
            // 
            this.oldCar2ToolStripMenuItem.Name = "oldCar2ToolStripMenuItem";
            this.oldCar2ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar2ToolStripMenuItem.Text = "Old Car 2";
            this.oldCar2ToolStripMenuItem.Click += new System.EventHandler(this.oldCar2ToolStripMenuItem_Click);
            // 
            // oldCar3ToolStripMenuItem
            // 
            this.oldCar3ToolStripMenuItem.Name = "oldCar3ToolStripMenuItem";
            this.oldCar3ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar3ToolStripMenuItem.Text = "Old Car 3";
            this.oldCar3ToolStripMenuItem.Click += new System.EventHandler(this.oldCar3ToolStripMenuItem_Click);
            // 
            // oldCar4ToolStripMenuItem
            // 
            this.oldCar4ToolStripMenuItem.Name = "oldCar4ToolStripMenuItem";
            this.oldCar4ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar4ToolStripMenuItem.Text = "Old Car 4";
            this.oldCar4ToolStripMenuItem.Click += new System.EventHandler(this.oldCar4ToolStripMenuItem_Click);
            // 
            // keinsToolStripMenuItem
            // 
            this.keinsToolStripMenuItem.Name = "keinsToolStripMenuItem";
            this.keinsToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.keinsToolStripMenuItem.Text = "Keins";
            this.keinsToolStripMenuItem.Click += new System.EventHandler(this.keinsToolStripMenuItem_Click);
            // 
            // frachtmarktToolStripMenuItem
            // 
            this.frachtmarktToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.frachtmarktToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.gabelstapler_64;
            this.frachtmarktToolStripMenuItem.Name = "frachtmarktToolStripMenuItem";
            this.frachtmarktToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            this.frachtmarktToolStripMenuItem.Visible = false;
            this.frachtmarktToolStripMenuItem.Click += new System.EventHandler(this.frachtmarktToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1388, 642);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_Revision);
            this.Controls.Add(this.groupVerkehr);
            this.Controls.Add(this.groupStatistiken);
            this.Controls.Add(this.version_lb);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VTC-Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing_1);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.Dashboard_1.ResumeLayout(false);
            this.Dashboard_1.PerformLayout();
            this.contextTaskbar.ResumeLayout(false);
            this.groupStatistiken.ResumeLayout(false);
            this.groupStatistiken.PerformLayout();
            this.groupVerkehr.ResumeLayout(false);
            this.groupVerkehr.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ats_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ets2_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Retarder_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Batterie_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Handbremse_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Motorbremse_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public System.Windows.Controls.Orientation Orientation { get; set; }

        private void topMenuWebsiteClick(object sender, EventArgs e)
        {
            Process.Start("https://vtc.northwestvideo.de/");
        }


        private void beendenToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
            utils.Log("<INFO> CLIENT_CLOSED");
        }

        private void einstellungenToolStripMenuItemClick(object sender, EventArgs e)
        {
            SettingsWindow Settingswindow = new SettingsWindow(user.translation);
            Settingswindow.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            Settingswindow.ShowDialog();
            utils.Log("<INFO> SETTINGS_OPEN");
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void MenuAbmeldenButton_Click(object sender, EventArgs e)
        {
            utils.Log("<INFO> USER_LOGGED_OFF");
            this.settings.DeleteConfig();
            Application.Restart();

        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utils.Log("<INFO> WINDOW_UEBER_OPEN");
            Ueber ueber = new Ueber();
            ueber.ShowDialog();
        }


        // Edit by Thommy
        private void Main_Load(object sender, EventArgs e)
        {
           
            this.discord = new Discord();
            lbl_Revision.Text = "2.1.1";
            labelRevision = lbl_Revision.Text;

            /// ######################   GEHT NOCH NICHT, DESHALB AUSBLENDEN    ###################
            Motorbremse_ICON.Visible = false;

            User_Patreon_State.Text = "PT: " + user.patreon_state.ToString();

            if(user.patreon_state >= 2)
            {
                frachtmarktToolStripMenuItem.Visible = Visible;
            }


            // ################## CHECK ob der AFK Text bei nicht Spendern stimmt ##################
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN")))
                utils.Reg_Schreiben("ANTI_AFK_AN", "0", "TruckersMP_Autorun");

            if (spender == 0)
                utils.Reg_Schreiben("ANTI_AFK", "VTCManager wünscht Gute und Sichere Fahrt!", "TruckersMP_Autorun");
            // ##################   ANTI AFK ENDE ##################################################


            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "GroupBox_Diagnostic")))
                utils.Reg_Schreiben("Diagnostic", "0", "TruckersMP_Autorun");

            anti_AFK_TIMER.Enabled = (Convert.ToInt32(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN")) == 1) ? true : false;
            utils.Log("<INFO> ANTI_AFK_TEXT > " + anti_AFK_TIMER.Enabled);

            // ####################   VERSION IN REG SCHREIBEN   ###################################
            utils.Reg_Schreiben("Version", labelRevision.ToString(), "TruckersMP_Autorun");


            // ####################   ZEIGE PATH WINDOW WENN ETS2 PFAD NICHT VORHANDEN   ###########
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad")))
            {
                ETS2_Pfad_Window win = new ETS2_Pfad_Window();
                win.Show();
                win.Focus();
                this.WindowState = FormWindowState.Minimized;
                return;
            }
            else
            {
                ets2_button.Visible = true;
                ToolTip tt = new ToolTip();
                tt.SetToolTip(this.ets2_button, "Starte ETS2 im Singleplayer !");

                string dest_leer = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
               
                if (!File.Exists(dest_leer + @"\bin\win_x64\plugins\scs-telemetry.dll"))
                {
                    utils.Log("<ERROR> DLL NOT IN PLUGINS");
                    if (!Directory.Exists(dest_leer + @"\bin\win_x64\plugins")) 
                    {
                        utils.Log("<ERROR> FOLDER PLUGINS IN ETS2 NOT AVAIBLE");
                        Directory.CreateDirectory(dest_leer + @"\bin\win_x64\plugins"); 
                        utils.Log("<ERROR> FOLDER PLUGINS CREATED"); 
                    }
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_leer + @"\bin\win_x64\plugins\scs-telemetry.dll");
                    utils.Log("<INFO> DLL IN PLUGINS FOLDER COPIED!");
                } else
                {
                    // ################  Für Diagnostikzwecke  ###########################
                    // logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager");
                    if (!File.Exists(logDirectory + logFile))
                    {
                        Directory.CreateDirectory(logDirectory);
                        File.Create(logDirectory + logFile);
                    }
                    // ################    LEERE LOG DATEI BEIM START   ###################
                    if (File.Exists(logDirectory + logFile))
                        File.WriteAllText(logDirectory + logFile, String.Empty);

                    //  ###############    ALLE PLUGINS IN REG SCHREIBEN   ################
                    string Plugins_ETS = "";
                    DirectoryInfo di = new DirectoryInfo(dest_leer + @"\bin\win_x64\plugins");
                    foreach (var fi in di.GetFiles())
                    { Plugins_ETS += fi.Name + "  "; }
                    utils.Reg_Schreiben("Plugins ETS", Plugins_ETS, "TruckersMP_Autorun");
                    utils.Log("<INFO> WRITE ALL PLUGINS IN REGISTRY");
                    // ################  Diagnostikzwecke ENDE  ###########################
                }


                string dest_leer2 = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
                if (!string.IsNullOrEmpty(dest_leer2))
                {
                    utils.Log("<INFO> ATS PATH IS NOT NULL OR EMPTY");
                    ats_button.Visible = true;
                    ToolTip tt2 = new ToolTip();
                    tt2.SetToolTip(this.ats_button, "Starte ATS im Singleplayer !");

                    if (!Directory.Exists(dest_leer2 + @"\bin\win_x64\plugins")) { Directory.CreateDirectory(dest_leer2 + @"\bin\win_x64\plugins"); }

                    if (!File.Exists(dest_leer2 + @"\bin\win_x64\plugins\scs-telemetry.dll"))
                    {
                        File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_leer2 + @"\bin\win_x64\plugins\scs-telemetry.dll");
                    }
                    else
                    {
                        // ################  Für Diagnostikzwecke  ###########################
                        string Plugins_ATS = "";
                        DirectoryInfo di = new DirectoryInfo(dest_leer2 + @"\bin\win_x64\plugins");
                        foreach (var fi in di.GetFiles())
                        { Plugins_ATS += fi.Name + "  "; }
                        utils.Reg_Schreiben("Plugins ATS", Plugins_ATS, "TruckersMP_Autorun");
                        utils.Log("<INFO> ALL ATS PLUGIN WRITE IN REGISTRY");
                        // ################  Diagnostikzwecke ENDE  ###########################
                    }

                }
            }

            // ###################################### TELEMETRY COPY END ###########################


            // Background Changer
            string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
            if(string.IsNullOrEmpty(hintergrund))
                        utils.Reg_Schreiben("Background", "", "TruckersMP_Autorun");

            string hintergrund2 = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
            if (hintergrund2.ToString() == "oldcar1") { this.BackgroundImage = Properties.Resources.oldcar1; }
            else if (hintergrund2 == "oldcar2") { this.BackgroundImage = Properties.Resources.oldcar2; }
            else if (hintergrund2 == "oldcar3") { this.BackgroundImage = Properties.Resources.oldcar3; }
            else if (hintergrund2 == "oldcar4") { this.BackgroundImage = Properties.Resources.oldcar4; }
            else { this.BackgroundImage = null; }
            // Background Changer ENDE 


            Dashboard_1.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard") == "1") ? true : false;
            utils.Reg_Schreiben("Reload_Traffic_Sekunden", "300", "TruckersMP_Autorun");
            lbl_Reload_Time.Text = "Reload-Interval: " + reload + " Sek.";


            if(Utilities.IsGameRunning == false)
            {
                speed_lb.Text = user.translation.loading_text;
                truck_lb.Visible = false;
                destination_lb.Visible = false;
                depature_lb.Visible = false;
                cargo_lb.Visible = false;
            } else
            {
                truck_lb.Visible = true;
                destination_lb.Visible = true;
                depature_lb.Visible = true;
                cargo_lb.Visible = true;
            }


        }





        private void truckersMP_Button_Click(object sender, EventArgs e)
        {
            truckersMP_Link = utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            if (truckersMP_Link != null)
            {
                utils.Log("<INFO> TRUCKERSMP OPENING");
                Process.Start(truckersMP_Link);
            } else
            {
                utils.Log("<ERROR> LINK2TMP IS MISSING->MESSAGEBOXING | TODO GERM-ENG VERSION");
                MessageBox.Show("Kein Link zu Truckers-MP angegeben!\nBitte schaue in den Einstellungen nach.", "Kein Link!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            utils.Log("<INFO> TRUCKYAPP CLICKED");
            Process.Start("https://truckyapp.com/");
        }

        private void einstellungenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form sw = new SettingsWindow(user.translation);
            sw.ShowDialog();

        }


        private static Image GetImageFromURL(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            return Image.FromStream(stream);
        }

        private void buttonGroesseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GUI_SIZE == 1)
            {
                GUI_SIZE = 0;
                this.groupStatistiken.Visible = false;
                this.groupVerkehr.Visible = false;
                this.Size = new Size(581, 661);
                this.panel2.Location = new Point(5, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/expand.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
                this.BackgroundImage = null;
                utils.Log("<INFO> GUI SIZE 1>0");
            }
            else
            {
                GUI_SIZE = 1;
                this.groupStatistiken.Visible = true;
                this.groupVerkehr.Visible = true;
                this.Size = new Size(1404, 681);
                this.panel2.Location = new Point(540, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/komprimieren.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
                string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
                if (hintergrund.ToString() == "oldcar1") { this.BackgroundImage = Properties.Resources.oldcar1; }
                else if (hintergrund == "oldcar2") { this.BackgroundImage = Properties.Resources.oldcar2; }
                else if (hintergrund == "oldcar3") { this.BackgroundImage = Properties.Resources.oldcar3; }
                else if (hintergrund == "oldcar4") { this.BackgroundImage = Properties.Resources.oldcar4; }
                else { this.BackgroundImage = null; }
                utils.Log("<INFO> GUI SIZE 0>1");
            }


        }

        private void overlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            utils.Log("<INFO> APPLICATION EXIT");
        }



        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Is_DarkMode_On == 0)
            {
                Is_DarkMode_On = 1;
                this.BackgroundImage = null;
                menuStrip1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                ForeColor = System.Drawing.Color.LightGray;
                utils.Log("<INFO> DARK MODE ON");
            } else
            {
                Is_DarkMode_On = 0;
                string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
                if (hintergrund.ToString() == "oldcar1") { this.BackgroundImage = Properties.Resources.oldcar1; }
                else if (hintergrund == "oldcar2") { this.BackgroundImage = Properties.Resources.oldcar2; }
                else if (hintergrund == "oldcar3") { this.BackgroundImage = Properties.Resources.oldcar3; }
                else if (hintergrund == "oldcar4") { this.BackgroundImage = Properties.Resources.oldcar4; }
                else { this.BackgroundImage = null; }

                menuStrip1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                ForeColor = System.Drawing.Color.Black;
                utils.Log("<INFO> DARK MODE OFF");
            }
        }

        private void updateTraffic_Tick(object sender, EventArgs e)
        {
            updateTraffic.Interval = reload * 1000;
            lbl_Reload_Time.Text = "Reload-Interval: " + reload + " Sek.";
            this.load_traffic();

            // Serverstatus in Statusleiste anzeigen
            Servercheck sc = new Servercheck();
            var green = new Bitmap(Properties.Resources.iconfinder_bulled_green_1930264);
            var red = new Bitmap(Properties.Resources.iconfinder_bullet_red_84435);
            // Webserver-Check
            try
            {
                WebServer_Status_label.Text = "←Webserver";
                WebServer_Status_label.Image = (sc.WS_Check() == true) ? green : red;
                utils.Log("<INFO> SERVER_WS: " + sc.WS_Check());
            } catch (Exception Fehler_Server)
            {
                utils.Log("<ERROR> WEBSERVER NOT AVAILABLE | TODO GER-ENG TRANSLATE");
                MessageBox.Show("Keine Verbindung zum Webserver\n" + Fehler_Server.Message, "Fehler Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // DB-Check
            try
            {
                Label_DB_Server.Text = "←Datenbank";
                Label_DB_Server.Image = (sc.DB_Check() == true) ? green : red;
                utils.Log("<INFO> SERVER_DB: " + sc.DB_Check());
            }
            catch (Exception Fehler_Server)
            {
                utils.Log("<ERROR> DATABASE_SERVER NOT AVAILABLE | TODO GER-ENG TRANSLATE");
                MessageBox.Show("Keine Verbindung zum Datenbankserver\n" + Fehler_Server.Message, "Fehler Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void serverstatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void anti_AFK_TIMER_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_RELOAD")))
                utils.Reg_Schreiben("ANTI_AFK_RELOAD", "3", "TruckersMP_Autorun");

            anti_AFK_TIMER.Interval = Convert.ToInt32(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_RELOAD").ToString()) * 60000;

            if(GameRuns == 1)
            {
                if (user.Geschwindigkeit < 1)
                {
                    SendKeys.Send("y");
                    SendKeys.Send(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK"));
                    SendKeys.Send("{Enter}");

                }
            }
        }

        private void oldCar1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.menuStrip1.BackColor = Color.Transparent;
            this.BackgroundImage = Properties.Resources.oldcar1;
            utils.Reg_Schreiben("Background", "oldcar1", "TruckersMP_Autorun");

            Dashboard_1.Location = new System.Drawing.Point(5, 334);

        }

        private void oldCar2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar2;
            utils.Reg_Schreiben("Background", "oldcar2", "TruckersMP_Autorun");
            Dashboard_1.Location = new System.Drawing.Point(5, 10);
        }

        private void oldCar3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar3;
            utils.Reg_Schreiben("Background", "oldcar3", "TruckersMP_Autorun");

            Dashboard_1.Location = new System.Drawing.Point(5, 334);
        }

        private void oldCar4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar4;
            utils.Reg_Schreiben("Background", "oldcar4", "TruckersMP_Autorun");

            Dashboard_1.Location = new System.Drawing.Point(5, 334);
        }

        private void keinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = null;
            utils.Reg_Schreiben("Background", "", "TruckersMP_Autorun");
        }


       

        private void ets2_button_Click(object sender, EventArgs e) =>
            Process.Start(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") +  @"\bin\win_X64\eurotrucks2.exe");
       

        private void ats_button_Click(object sender, EventArgs e) =>
            Process.Start(utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") + @"\bin\win_X64\amtrucks.exe");
       


        private void TelemetryJobCancelled(object sender, EventArgs e)
        {
            this.jobStarted = false;
            job.cancel(sound,utils,user);
        }

        

        private void TelemetryJobDelivered(object sender, EventArgs e) =>
            job.jobFinished = true;

        private void TelemetryFined(object sender, EventArgs e) =>
            MessageBox.Show("Fined");
        private void TelemetryTollgate(object sender, EventArgs e)
        {
            Thommy th3 = new Thommy(); 
            th3.Sende_TollGate(user.authcode, job.Tollgate_Payment, 1);
        
        }
            

        private void TelemetryFerry(object sender, EventArgs e) =>
            job.Ferry = true;

        private void TelemetryTrain(object sender, EventArgs e) =>
            job.Train = true;

        private void TelemetryRefuel(object sender, EventArgs e) 
        {
                Thommy th3 = new Thommy();
                th3.Sende_Refuel(user.authcode, job.Tollgate_Payment, job.ID.ToString());

        }

        private void frachtmarktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frachtmarkt fm = new Frachtmarkt();
            fm.Show();
        }




    }



}

