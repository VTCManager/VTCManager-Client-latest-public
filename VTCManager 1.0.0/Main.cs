using ManagedWinapi;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using VTCManager_1._0._0.Objekte;
using Timer = System.Windows.Forms.Timer;

namespace VTCManager_1._0._0
{


    public class Main : Form
    {
        // Settings

        private API api = new API();
        private Utilities utils = new Utilities();
        private Logging Logs = new Logging();
        Thommy th = new Thommy();

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
        private Label status_jb_canc_lb;
        private double num1;
        private double num2;
        private Label version_lb;
        private ToolStripMenuItem MenuAbmeldenButton;
        private ToolStripMenuItem creditsToolStripMenuItem;
        public bool discordRPCalreadrunning;
        public bool stillTheSameJob;
        public NotifyIcon TaskBar_Icon;
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


        public int spender = 0;
        private User user;
        private Job job;
        private ToolStripMenuItem frachtmarktToolStripMenuItem;
        private ToolStripStatusLabel User_Patreon_State;
        public Label lbl_NUM3_Text;
        private Label NUM3_Label;
        public Label lbl_NUM2_Text;
        private Label NUM2_Label;
        public Label lbl_NUM1_Text;
        private Label NUM1_Label;
        private GroupBox GroupBox_Individ_Texte;
        private PictureBox NUM_LOCK_PICTURE;
        private ToolStripMenuItem dockingToolStripMenuItem;
        private ToolStripMenuItem dockTopToolStripMenuItem;
        private ToolStripMenuItem dockBottomToolStripMenuItem;
        private BackgroundWorker backgroundWorker1;
        private ImageList imageList1;
        public Label lbl_Revision;
        private readonly string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager");
        public string logFile = @"\VTC_LOG.txt";
        private Label lbl_Time_Remain;
        public string systemlogFile = @"\VTC_SYSTEM_LOG.txt";


        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = true)]
        public static extern int GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public Main(User user)
        {
            job = new Job();
            //Daten werden übernommen von Login.cs
            this.user = user;

            //Laden des Sound-Systems
            sound = new Sound(user.translation);
            Logs.WriteLOG("<INFO> Sound System geladen !");
            utils.Reg_Schreiben("usr", user.username, "TruckersMP_Autorun");
            //Benutzerkonfiguration laden
            settings = new SettingsManager();
            settings.LoadConfiguration();
            Logs.WriteLOG("<INFO> Settings Manager geladen");

            //UI init
            InitializeComponent();

            Logs.WriteLOG("<INFO> Initialisiere Component()");

            InitializeTranslation();
            Logs.WriteLOG("<INFO> Initialisiere Tranlation()");

            try
            {
                this.load_traffic();
            }
            catch (Exception e)
            {
                Logs.WriteLOG("Fehler beim Abrufen der Verkehrsdaten [Main.cs->165]");
                MessageBox.Show("Fehler: Fehler beim Abrufen der Verkehrsdaten" + e.Message);
            }

            FormClosing += new FormClosingEventHandler(Main_FormClosing);

            //Telemetry Handler setzen
            Telemetry = new SCSSdkTelemetry();
            Logs.WriteLOG("<INFO> SCSSdkTelemetry() initialisiert");

            Telemetry.Data += Telemetry_Data;
            Logs.WriteLOG("<INFO> Telemetry.Data geladen");

            Telemetry.JobStarted += TelemetryOnJobStarted;
            Logs.WriteLOG("<INFO> Telemetry.JobStarted geladen");

            Telemetry.JobCancelled += TelemetryJobCancelled;
            Logs.WriteLOG("<INFO> Telemetry.JobCancelled geladen");

            Telemetry.JobDelivered += TelemetryJobDelivered;
            Logs.WriteLOG("<INFO> Telemetry.JobDelivered geladen");

            Telemetry.Fined += TelemetryFined;
            Logs.WriteLOG("<INFO> Telemetry.Fined geladen");

            Telemetry.Tollgate += TelemetryTollgate;
            Logs.WriteLOG("<INFO> Telemetry.Tollgate geladen");

            Telemetry.Ferry += TelemetryFerry;
            Logs.WriteLOG("<INFO> Telemetry.Ferry  geladen");

            Telemetry.Train += TelemetryTrain;
            Logs.WriteLOG("<INFO> Telemetry.Train geladen");

            Telemetry.RefuelStart += TelemetryRefuel;
            Logs.WriteLOG("<INFO> Telemetry.RefuleStart geladen");

            Telemetry.RefuelEnd += TelemetryRefuelEnd;
            Logs.WriteLOG("<INFO> Telemetry.RefuleEnd geladen");

            Telemetry.RefuelPayed += TelemetryRefuelPayed;
            Logs.WriteLOG("<INFO>  Telemetry.RefulePayed geladen");

            if (Telemetry.Error == null)
            {
                return;
            }

            int num = (int)MessageBox.Show("Fehler beim Ausführen von:" + Telemetry.Map + "\r\n" + Telemetry.Error.Message + "\r\n\r\nStacktrace:\r\n" + Telemetry.Error.StackTrace);

        }

        private void TelemetryRefuelPayed(object sender, EventArgs e)
        {

        }

        private void TelemetryRefuelEnd(object sender, EventArgs e)
        {

        }

        public string Versionsnummer()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.ProductVersion;
        }

        private void InitializeTranslation()
        {
            //Labels müssen hier aufgrund von Problemen im Designer initialisiert werden
            label1.Text = user.translation.traffic_main_lb;
            einstellungenToolStripMenuItem.Text = user.translation.settings_lb;
            beendenToolStripMenuItem.Text = user.translation.exit_lb;
            topMenuAccount.Text = user.translation.topmenuaccount_lb;
            statistic_panel_topic.Text = user.translation.statistic_panel_topic + user.username.ToUpper();
            driven_tours_lb.Text = user.translation.driven_tours_lb + user.driven_tours;
            act_bank_balance_lb.Text = user.translation.act_bank_balance + user.bank_balance + user.translation.waehrung;
            user_company_lb.Text = user.translation.user_company_lb + user.company;
            MenuAbmeldenButton.Text = user.translation.logout;


        }

        private void load_traffic()
        {
            Logs.WriteLOG("TRAFFIC");
            //Tabelle zurücksetzen
            this.tableLayoutPanel1.Visible = false;
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
            var traffic_response = this.api.HTTPSRequestGet(this.api.traffic_data, postParameters).ToString();
            var truckyTopTraffic = TruckyTopTraffic.FromJson(traffic_response);
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
            this.tableLayoutPanel1.Visible = true;
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
        private void TelemetryOnJobFinished(object sender, EventArgs args)
        {
            Logs.WriteLOG("<Tour> Job wurde Erfolgreich beendet !");
            job.jobFinished = true;

        }


        private void TelemetryOnJobStarted(object sender, EventArgs e)
        {
            Logs.WriteLOG("<TOUR> Job wurde Gestartet...");
            job.jobStarted = true;

        }





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
                    // ##########################   NUM Anzeige An/Ausschalten   #####################################
                    NUM_LOCK_PICTURE.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "NUM_LOCK_SHOW") == "1") ? true : false;
                    NUM_LOCK_PICTURE.Image = (NumLock() == true) ? Properties.Resources.num_on : Properties.Resources.num_off;
                    Dashboard_1.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard") == "1") ? true : false;

                    //Läuft das Spiel?
                    if (Utilities.IsGameRunning && data.SdkActive)
                    {
                        //falls Daten erhalten werden

                        anti_AFK_TIMER.Enabled = (Convert.ToInt32(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN")) == 1) ? true : false;
                        //Logs.WriteLOG("<INFO> ANTI_AFK_TEXT > " + anti_AFK_TIMER.Enabled);

                        user.CoordinateX = data.TruckValues.CurrentValues.PositionValue.Position.X;
                        user.CoordinateZ = data.TruckValues.CurrentValues.PositionValue.Position.Y;
                        //user.Geschwindigkeit = (float)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                        user.Spiel = data.Game.ToString();

                        Motorbremse_ICON.Visible = (data.TruckValues.CurrentValues.MotorValues.BrakeValues.MotorBrake) ? true : false;
                        Handbremse_ICON.Visible = (data.TruckValues.CurrentValues.MotorValues.BrakeValues.ParkingBrake) ? true : false;
                        Retarder_ICON.Visible = (data.ControlValues.InputValues.Brake >= 0.1) ? true : false;

                        // ################## REST TIME ADDON  #####################
                        lbl_Time_Remain.Visible = (data.JobValues.CargoLoaded == true) ? true : false;
                        DateTime ingameTime = data.CommonValues.GameTime.Date;
                        job.resttime = data.JobValues.RemainingDeliveryTime.Date;
                        var Rest_Zeit = job.resttime.Subtract(ingameTime);
                        if (Rest_Zeit.TotalSeconds >= 1)
                        {
                            lbl_Time_Remain.Font = new Font("Verdana", 10);
                            lbl_Time_Remain.ForeColor = Color.Black;
                            lbl_Time_Remain.Text = user.translation.rest_text + Rest_Zeit.Hours.ToString() + user.translation.rest_time_days + Rest_Zeit.Minutes.ToString() + user.translation.rest_time_hours + Rest_Zeit.Seconds.ToString() + user.translation.rest_time_minutes + " ( " + data.JobValues.RemainingDeliveryTime.Value + " )";
                        }
                        else
                        {
                            lbl_Time_Remain.Font = new Font("Verdana", 20);
                            lbl_Time_Remain.Font = new System.Drawing.Font("Verdana", 14.75F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, 0);
                            lbl_Time_Remain.ForeColor = Color.Red;
                            lbl_Time_Remain.Text = user.translation.verspaetet;

                        }


                        // ########################################################

                        //Text sichtbar
                        truck_lb.Visible = true;
                        cargo_lb.Visible = true;
                        destination_lb.Visible = true;
                        depature_lb.Visible = true;
                        lbl_Time_Remain.Visible = true;

                        truckersMP_Button.Visible = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad"))) ? false : true;

                        GameRuns = 1;

                        if (data.Game.ToString() == "Ets2")
                        {
                            speed_lb.Text = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph + user.translation.speeding;
                            user.Geschwindigkeit = data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                            anti_AFK_TIMER.Enabled = (data.TruckValues.CurrentValues.DashboardValues.Speed.Kph < 1) ? true : false;
                        }
                        else
                        {
                            speed_lb.Text = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Mph + user.translation.speeding;
                            user.Geschwindigkeit = data.TruckValues.CurrentValues.DashboardValues.Speed.Mph;
                            anti_AFK_TIMER.Enabled = (data.TruckValues.CurrentValues.DashboardValues.Speed.Mph < 1) ? true : false;
                        }


                        // ###################   FUEL PROGRESS    ##############################################
                        progressBar_F.Maximum = Convert.ToInt32(data.TruckValues.ConstantsValues.CapacityValues.Fuel);
                        progressBar_F.Value = Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount);

                        // #########################   REST KM    ##############################################
                        Rest_KM_Label.Text = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Range + " KM (" + Convert.ToInt32(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount) + " L)";

                        // #########################   LUFTDRUCK   #############################################
                        Luft_Progress.Maximum = 150;
                        Luft_Progress.Value = (int)data.TruckValues.CurrentValues.MotorValues.BrakeValues.AirPressure;

                        // #########################   STRECKENVERLAUF   #######################################



                        job.currentPercentage = (((((double)data.NavigationValues.NavigationDistance / 1000) / data.JobValues.PlannedDistanceKm) * 100) - 100) * -1;
                        // Logs.WriteLOG("<INFO> Aktuell Tour-Prozent: " + job.currentPercentage.ToString());

                        // ##########################  AUSGABE TRUCK MODEL etc.  #############################
                        truck_lb.Text = "Dein Truck: " + data.TruckValues.ConstantsValues.Brand + ", Modell: " + data.TruckValues.ConstantsValues.Name;

                        // ##############################   JOB DATA   ####################################
                        if (data.JobValues.CargoLoaded == false)
                        {
                            discord.noTour();
                            cargo_lb.Text = user.translation.no_cargo_lb;
                            destination_lb.Visible = false;
                            depature_lb.Text = "";
                            lbl_Time_Remain.Visible = false;
                        }
                        else
                        {

                            if (jobrunningcounter == 30)
                            {
                                api.HTTPSRequestPost(api.api_server + api.job_update_path, new Dictionary<string, string>()
                                    {
                                        { "authcode", user.authcode },
                                        { "job_id", job.ID.ToString() },
                                        { "percentage", job.currentPercentage.ToString() }
                                    }, false).ToString();
                                jobrunningcounter = 0;
                                Logs.WriteLOG("<UPDATE> Tour-Tick: AUTH: " + user.authcode + ", JOB: " + job.ID + ", JOB PERCENT: " + job.currentPercentage.ToString());
                            }
                            jobrunningcounter++;
                        }


                        bool flag;
                        using (Dictionary<string, string>.Enumerator enumerator = lastJobDictionary.GetEnumerator())
                        {
                            flag = !enumerator.MoveNext();
                        }
                    }
                    else
                    {
                        lbl_Time_Remain.Visible = false;
                        truck_lb.Visible = false;
                        cargo_lb.Visible = false;
                        destination_lb.Visible = false;
                        depature_lb.Visible = false;
                        speed_lb.Text = user.translation.waiting_for_ets;
                    }
                }

                double num2;
                if (job.jobStarted)
                {


                    job = new Job();
                    DateTime dt = new DateTime();
                    Logs.WriteLOG("<INFO> New Job started @ " + dt.ToString());
                    job.jobStarted = false;
                    Logs.WriteLOG("<INFO> job.jobStarted = false");
                    this.lastJobDictionary.Clear();
                    Logs.WriteLOG("<INFO> lastJobDictionary.Clear");
                    sound.Play(sound.ton_tour_gestartet);
                    Logs.WriteLOG("<INFO> sound.Play(sound.ton_tour_gestartet)");
                    job.totalDistance = (int)data.NavigationValues.NavigationDistance;
                    Logs.WriteLOG("<INFO> job.totlaDistance = " + job.totalDistance.ToString());
                    num2 = data.JobValues.Income * 0.15;
                    Logs.WriteLOG("<INFO> job.income = " + num2.ToString());
                    cargo_lb.Text = user.translation.freight_lb + ((int)Math.Round(data.JobValues.CargoValues.Mass, 0) / 1000).ToString() + user.translation.gewicht + data.JobValues.CargoValues.Name;
                    Logs.WriteLOG("<INFO> Cargo Label: " + cargo_lb.Text);
                    depature_lb.Text = "Von: " + data.JobValues.CitySource + " ( " + data.JobValues.CompanySource + " ) nach: " + data.JobValues.CityDestination + " ( " + data.JobValues.CompanyDestination + " )";
                    job.fuelatstart = data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                    Logs.WriteLOG("<INFO> job.fuel@start = " + job.fuelatstart.ToString());

                    Dictionary<string, string> postParameters = new Dictionary<string, string>
                    { { "authcode", user.authcode },
                        { "cargo", data.JobValues.CargoValues.Name },
                        { "weight", ((int)Math.Round(data.JobValues.CargoValues.Mass, 0) / 1000).ToString() },
                        { "depature", data.JobValues.CitySource },
                        { "depature_company", data.JobValues.CompanySource },
                        { "destination_company", data.JobValues.CompanyDestination },
                        { "destination", data.JobValues.CityDestination },
                        { "truck_manufacturer", data.TruckValues.ConstantsValues.Brand },
                        { "truck_model", data.TruckValues.ConstantsValues.Name },
                        { "distance", data.JobValues.PlannedDistanceKm.ToString() }
                    };
                    job.ID = Convert.ToInt32(api.HTTPSRequestPost(api.api_server + api.new_job_path, postParameters, true).ToString());
                    Logs.WriteLOG("<INFO> PostParameters: " + postParameters.ToString());

                    Dictionary<string, string> lastJobDictionary = this.lastJobDictionary;
                    this.lastJobDictionary.Add("cargo", data.JobValues.CargoValues.Name);
                    this.lastJobDictionary.Add("source", data.JobValues.CitySource);
                    this.lastJobDictionary.Add("destination", data.JobValues.CityDestination);
                    this.lastJobDictionary.Add("income", data.JobValues.Income.ToString());
                    this.lastJobDictionary.Add("weight", data.JobValues.CargoValues.Mass.ToString());
                    Logs.WriteLOG("<INFO> lastJobDictionary = " + lastJobDictionary.ToString());

                    discord.onTour(data.JobValues.CityDestination, data.JobValues.CitySource, data.JobValues.CargoValues.Name, ((int)Math.Round(data.JobValues.CargoValues.Mass, 0) / 1000).ToString());
                    Logs.WriteLOG("<INFO> Discord Starts OnTour with Job Details");
                    job.CitySource = data.JobValues.CitySource;
                    Logs.WriteLOG("<INFO> job.CitySource = " + job.CitySource.ToString());
                    job.CityDestination = data.JobValues.CityDestination;
                    Logs.WriteLOG("<INFO> job.CityDestination = " + job.CityDestination.ToString());
                    send_tour_status.Enabled = true;
                    Logs.WriteLOG("<INFO> Send_Tour_Status.Enabled = true");
                    send_tour_status.Start();
                    Logs.WriteLOG("<INFO> Send_Tour_Status.Start()");
                    Logs.WriteLOG("Tour START LOG: " + user.authcode + ", Cargo: " + data.JobValues.CargoValues.Name + ", " + ((int)Math.Round(data.JobValues.CargoValues.Mass, 0) / 1000).ToString() + " Tonnen, Startort: " + data.JobValues.CitySource + ", Start-Firma: " + data.JobValues.CompanySource + ", Zielort: " + data.JobValues.CityDestination + ", Ziel-Firma: " + data.JobValues.CompanyDestination + ", LKW: " + data.TruckValues.ConstantsValues.Brand + " " + data.TruckValues.ConstantsValues.Name + ", Strecke: " + data.JobValues.PlannedDistanceKm.ToString() + " KM ");




                }

                if (job.jobFinished)
                {
                    if (lastJobDictionary["cargo"] == data.JobValues.CargoValues.Name && lastJobDictionary["source"] == data.JobValues.CitySource && lastJobDictionary["destination"] == data.JobValues.CityDestination)
                    {
                        send_tour_status.Enabled = false;
                        sound.Play(sound.ton_tour_beendet);

                        job.jobFinished = false;
                        job.fuelatend = data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                        job.fuelconsumption = job.fuelatstart - job.fuelatend;
                        Console.WriteLine(job.fuelconsumption);
                        Dictionary<string, string> postParameters = new Dictionary<string, string>
                        { { "authcode", user.authcode },
                            { "job_id", job.ID.ToString() }
                        };
                        Dictionary<string, string> dictionary2 = postParameters;
                        num2 = Math.Floor(data.TruckValues.CurrentValues.DamageValues.Transmission * 100.0 / 1.0);
                        string str3 = num2.ToString();
                        dictionary2.Add("trailer_damage", str3);
                        postParameters.Add("income", data.JobValues.Income.ToString());
                        if (job.fuelconsumption > data.TruckValues.ConstantsValues.CapacityValues.Fuel)
                        {
                            postParameters.Add("refueled", "true");
                        }
                        postParameters.Add("fuelconsumption", job.fuelconsumption.ToString());

                        Console.WriteLine(api.HTTPSRequestPost(api.api_server + api.finishjob_path, postParameters, true).ToString());
                        job.jobFinished = false;
                        Logs.WriteLOG("Tour FINISH: " + user.authcode + ", " + job.ID + ", Einkommen: " + data.JobValues.Income.ToString() + " €" + ", Damage: " + str3);
                        job.clear();
                        destination_lb.Text = "";
                        depature_lb.Text = "";
                        lbl_Time_Remain.Visible = false;
                        //this.cargo_lb.Text = translation.no_cargo_lb;

                    }
                }
                job.invertedDistance = job.totalDistance - (int)Math.Round(data.NavigationValues.NavigationDistance, 0);
            }
            catch { }
        }

        private void send_tour_status_Tick(object sender, EventArgs e)
        {
            job.jobRunning = true;
            locationupdate();
            // Check Ontop
            TopMost = (utils.Reg_Lesen("TruckersMP_Autorun", "OnTop") == "1") ? true : false;

        }

        private void locationupdate()
        {
            Logs.WriteLOG("<INFO> FOREGROUND: " + GetActiveWindowTitle());
            Console.WriteLine("LKW SPEED: " + user.Geschwindigkeit);

            Logs.WriteLOG("<RESTTIME> " + job.resttime + " Minuten");

            // ######   LADE HOTKEY TEXTE NEU    ######################
            lade_NUMx_TEXTE();

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

            api.HTTPSRequestPost(api.api_server + api.loc_update_path, postParameters, false).ToString();
            Logs.WriteLOG("Tour UPDATE: User: " + user.authcode + ", Job-ID: " + job.ID + ", Prozent: " + job.currentPercentage.ToString() + ", Game: " + user.Spiel);
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            TaskBar_Icon.Dispose();
            Logs.WriteLOG("<INFO> CLIENT_CLOSED");
        }

        private void send_location_Tick(object sender, EventArgs e)
        {
            job.locationUpdate = true;
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            send_tour_status = new System.Timers.Timer();
            send_location = new System.Windows.Forms.Timer(components);
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            topMenuAccount = new System.Windows.Forms.ToolStripMenuItem();
            MenuAbmeldenButton = new System.Windows.Forms.ToolStripMenuItem();
            topmenuwebsite = new System.Windows.Forms.ToolStripMenuItem();
            eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            GUI_SIZE_BUTTON = new System.Windows.Forms.ToolStripMenuItem();
            lbl_Overlay = new System.Windows.Forms.ToolStripMenuItem();
            darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            oldCar1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            oldCar2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            oldCar3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            oldCar4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            keinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            frachtmarktToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dockingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dockTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dockBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            lbl_Time_Remain = new System.Windows.Forms.Label();
            GroupBox_Individ_Texte = new System.Windows.Forms.GroupBox();
            NUM_LOCK_PICTURE = new System.Windows.Forms.PictureBox();
            lbl_NUM2_Text = new System.Windows.Forms.Label();
            lbl_NUM3_Text = new System.Windows.Forms.Label();
            NUM1_Label = new System.Windows.Forms.Label();
            NUM3_Label = new System.Windows.Forms.Label();
            lbl_NUM1_Text = new System.Windows.Forms.Label();
            NUM2_Label = new System.Windows.Forms.Label();
            status_jb_canc_lb = new System.Windows.Forms.Label();
            truck_lb = new System.Windows.Forms.Label();
            destination_lb = new System.Windows.Forms.Label();
            depature_lb = new System.Windows.Forms.Label();
            cargo_lb = new System.Windows.Forms.Label();
            speed_lb = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel4 = new System.Windows.Forms.Panel();
            Dashboard_1 = new System.Windows.Forms.GroupBox();
            progressBar_F = new System.Windows.Forms.ProgressBar();
            Retarder_ICON = new System.Windows.Forms.PictureBox();
            label6 = new System.Windows.Forms.Label();
            Batterie_ICON = new System.Windows.Forms.PictureBox();
            Handbremse_ICON = new System.Windows.Forms.PictureBox();
            Rest_KM_Label = new System.Windows.Forms.Label();
            Motorbremse_ICON = new System.Windows.Forms.PictureBox();
            label5 = new System.Windows.Forms.Label();
            Luft_Progress = new System.Windows.Forms.ProgressBar();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            label4 = new System.Windows.Forms.Label();
            version_lb = new System.Windows.Forms.Label();
            TaskBar_Icon = new System.Windows.Forms.NotifyIcon(components);
            contextTaskbar = new System.Windows.Forms.ContextMenuStrip(components);
            öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            einstellungenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            webseiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            beendenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            groupStatistiken = new System.Windows.Forms.GroupBox();
            ats_button = new System.Windows.Forms.PictureBox();
            ets2_button = new System.Windows.Forms.PictureBox();
            truckersMP_Button = new System.Windows.Forms.Button();
            user_company_lb = new System.Windows.Forms.Label();
            statistic_panel_topic = new System.Windows.Forms.Label();
            act_bank_balance_lb = new System.Windows.Forms.Label();
            driven_tours_lb = new System.Windows.Forms.Label();
            groupVerkehr = new System.Windows.Forms.GroupBox();
            lbl_Reload_Time = new System.Windows.Forms.Label();
            updateTraffic = new System.Windows.Forms.Timer(components);
            lbl_Revision = new System.Windows.Forms.Label();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            WebServer_Status_label = new System.Windows.Forms.ToolStripStatusLabel();
            Label_DB_Server = new System.Windows.Forms.ToolStripStatusLabel();
            User_Patreon_State = new System.Windows.Forms.ToolStripStatusLabel();
            anti_AFK_TIMER = new System.Windows.Forms.Timer(components);
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            imageList1 = new System.Windows.Forms.ImageList(components);
            ((System.ComponentModel.ISupportInitialize)(send_tour_status)).BeginInit();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            GroupBox_Individ_Texte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(NUM_LOCK_PICTURE)).BeginInit();
            panel4.SuspendLayout();
            Dashboard_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(Retarder_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(Batterie_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(Handbremse_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(Motorbremse_ICON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).BeginInit();
            contextTaskbar.SuspendLayout();
            groupStatistiken.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(ats_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ets2_button)).BeginInit();
            groupVerkehr.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // send_tour_status
            // 
            send_tour_status.Enabled = true;
            send_tour_status.Interval = 15000D;
            send_tour_status.SynchronizingObject = this;
            send_tour_status.Elapsed += new System.Timers.ElapsedEventHandler(send_tour_status_Tick);
            // 
            // send_location
            // 
            send_location.Enabled = true;
            send_location.Interval = 15000;
            send_location.Tick += new System.EventHandler(send_location_Tick);
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.Color.Transparent;
            menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            dateiToolStripMenuItem,
            topMenuAccount,
            topmenuwebsite,
            eventsToolStripMenuItem,
            GUI_SIZE_BUTTON,
            lbl_Overlay,
            darkToolStripMenuItem,
            toolStripMenuItem1,
            frachtmarktToolStripMenuItem,
            dockingToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1384, 32);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            einstellungenToolStripMenuItem,
            creditsToolStripMenuItem,
            beendenToolStripMenuItem});
            dateiToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dateiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dateiToolStripMenuItem.Image")));
            dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            dateiToolStripMenuItem.Size = new System.Drawing.Size(77, 28);
            dateiToolStripMenuItem.Text = "Menü";
            dateiToolStripMenuItem.ToolTipText = "Hauptmenü";
            // 
            // einstellungenToolStripMenuItem
            // 
            einstellungenToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_einstellungen_64;
            einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            einstellungenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            einstellungenToolStripMenuItem.Text = "Einstellungen";
            einstellungenToolStripMenuItem.Click += new System.EventHandler(einstellungenToolStripMenuItemClick);
            // 
            // creditsToolStripMenuItem
            // 
            creditsToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_info_64;
            creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            creditsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            creditsToolStripMenuItem.Text = "Über...";
            creditsToolStripMenuItem.Click += new System.EventHandler(CreditsToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            beendenToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_ausgang_64;
            beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            beendenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            beendenToolStripMenuItem.Text = "Beenden";
            beendenToolStripMenuItem.Click += new System.EventHandler(beendenToolStripMenuItemClick);
            // 
            // topMenuAccount
            // 
            topMenuAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            MenuAbmeldenButton});
            topMenuAccount.Image = ((System.Drawing.Image)(resources.GetObject("topMenuAccount.Image")));
            topMenuAccount.Name = "topMenuAccount";
            topMenuAccount.Size = new System.Drawing.Size(102, 28);
            topMenuAccount.Text = "Account";
            topMenuAccount.ToolTipText = "Client an/abmelden";
            // 
            // MenuAbmeldenButton
            // 
            MenuAbmeldenButton.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_logout_abgerundet_64;
            MenuAbmeldenButton.Name = "MenuAbmeldenButton";
            MenuAbmeldenButton.Size = new System.Drawing.Size(159, 30);
            MenuAbmeldenButton.Text = "Abmelden";
            MenuAbmeldenButton.Click += new System.EventHandler(MenuAbmeldenButton_Click);
            // 
            // topmenuwebsite
            // 
            topmenuwebsite.Image = ((System.Drawing.Image)(resources.GetObject("topmenuwebsite.Image")));
            topmenuwebsite.Name = "topmenuwebsite";
            topmenuwebsite.Size = new System.Drawing.Size(101, 28);
            topmenuwebsite.Text = "Website";
            topmenuwebsite.ToolTipText = "Gehe zu unserer Homepage";
            topmenuwebsite.Click += new System.EventHandler(topMenuWebsiteClick);
            // 
            // eventsToolStripMenuItem
            // 
            eventsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eventsToolStripMenuItem.Image")));
            eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            eventsToolStripMenuItem.Size = new System.Drawing.Size(91, 28);
            eventsToolStripMenuItem.Text = "Events";
            eventsToolStripMenuItem.ToolTipText = "Zeige aktuelle Events (in Bearbeitung)";
            eventsToolStripMenuItem.Visible = false;
            // 
            // GUI_SIZE_BUTTON
            // 
            GUI_SIZE_BUTTON.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            GUI_SIZE_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GUI_SIZE_BUTTON.Image")));
            GUI_SIZE_BUTTON.Name = "GUI_SIZE_BUTTON";
            GUI_SIZE_BUTTON.Size = new System.Drawing.Size(36, 28);
            GUI_SIZE_BUTTON.Text = "Button_Groesse";
            GUI_SIZE_BUTTON.ToolTipText = "Ansicht verkleinern / vergrößern";
            GUI_SIZE_BUTTON.Click += new System.EventHandler(buttonGroesseToolStripMenuItem_Click);
            // 
            // lbl_Overlay
            // 
            lbl_Overlay.Name = "lbl_Overlay";
            lbl_Overlay.Size = new System.Drawing.Size(76, 28);
            lbl_Overlay.Text = "Overlay";
            lbl_Overlay.Visible = false;
            lbl_Overlay.Click += new System.EventHandler(overlayToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            darkToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_film_noir_50;
            darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            darkToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            darkToolStripMenuItem.ToolTipText = "Komm auf die Dunkle Seite";
            darkToolStripMenuItem.Click += new System.EventHandler(darkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            oldCar1ToolStripMenuItem,
            oldCar2ToolStripMenuItem,
            oldCar3ToolStripMenuItem,
            oldCar4ToolStripMenuItem,
            keinsToolStripMenuItem});
            toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(36, 28);
            // 
            // oldCar1ToolStripMenuItem
            // 
            oldCar1ToolStripMenuItem.Name = "oldCar1ToolStripMenuItem";
            oldCar1ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            oldCar1ToolStripMenuItem.Text = "Old Car 1";
            oldCar1ToolStripMenuItem.Click += new System.EventHandler(oldCar1ToolStripMenuItem_Click);
            // 
            // oldCar2ToolStripMenuItem
            // 
            oldCar2ToolStripMenuItem.Name = "oldCar2ToolStripMenuItem";
            oldCar2ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            oldCar2ToolStripMenuItem.Text = "Old Car 2";
            oldCar2ToolStripMenuItem.Click += new System.EventHandler(oldCar2ToolStripMenuItem_Click);
            // 
            // oldCar3ToolStripMenuItem
            // 
            oldCar3ToolStripMenuItem.Name = "oldCar3ToolStripMenuItem";
            oldCar3ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            oldCar3ToolStripMenuItem.Text = "Old Car 3";
            oldCar3ToolStripMenuItem.Click += new System.EventHandler(oldCar3ToolStripMenuItem_Click);
            // 
            // oldCar4ToolStripMenuItem
            // 
            oldCar4ToolStripMenuItem.Name = "oldCar4ToolStripMenuItem";
            oldCar4ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            oldCar4ToolStripMenuItem.Text = "Old Car 4";
            oldCar4ToolStripMenuItem.Click += new System.EventHandler(oldCar4ToolStripMenuItem_Click);
            // 
            // keinsToolStripMenuItem
            // 
            keinsToolStripMenuItem.Name = "keinsToolStripMenuItem";
            keinsToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            keinsToolStripMenuItem.Text = "Keins";
            keinsToolStripMenuItem.Click += new System.EventHandler(keinsToolStripMenuItem_Click);
            // 
            // frachtmarktToolStripMenuItem
            // 
            frachtmarktToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            frachtmarktToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.gabelstapler_64;
            frachtmarktToolStripMenuItem.Name = "frachtmarktToolStripMenuItem";
            frachtmarktToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            frachtmarktToolStripMenuItem.Visible = false;
            frachtmarktToolStripMenuItem.Click += new System.EventHandler(frachtmarktToolStripMenuItem_Click);
            // 
            // dockingToolStripMenuItem
            // 
            dockingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            dockTopToolStripMenuItem,
            dockBottomToolStripMenuItem});
            dockingToolStripMenuItem.Name = "dockingToolStripMenuItem";
            dockingToolStripMenuItem.Size = new System.Drawing.Size(79, 28);
            dockingToolStripMenuItem.Text = "Docking";
            dockingToolStripMenuItem.Visible = false;
            // 
            // dockTopToolStripMenuItem
            // 
            dockTopToolStripMenuItem.Name = "dockTopToolStripMenuItem";
            dockTopToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            dockTopToolStripMenuItem.Text = "Dock Top";
            dockTopToolStripMenuItem.Click += new System.EventHandler(dockTopToolStripMenuItem_Click);
            // 
            // dockBottomToolStripMenuItem
            // 
            dockBottomToolStripMenuItem.Name = "dockBottomToolStripMenuItem";
            dockBottomToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            dockBottomToolStripMenuItem.Text = "Dock Bottom";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(342, 342);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(145, 13);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "(powered by Truckyapp.com)";
            linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(12, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(83, 30);
            label1.TabIndex = 1;
            label1.Text = "Verkehr";
            label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            label2.Location = new System.Drawing.Point(13, 41);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(18, 19);
            label2.TabIndex = 2;
            label2.Text = "...";
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.Transparent;
            panel2.Controls.Add(lbl_Time_Remain);
            panel2.Controls.Add(GroupBox_Individ_Texte);
            panel2.Controls.Add(status_jb_canc_lb);
            panel2.Controls.Add(truck_lb);
            panel2.Controls.Add(destination_lb);
            panel2.Controls.Add(depature_lb);
            panel2.Controls.Add(cargo_lb);
            panel2.Controls.Add(speed_lb);
            panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            panel2.Location = new System.Drawing.Point(540, 28);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(551, 582);
            panel2.TabIndex = 2;
            // 
            // lbl_Time_Remain
            // 
            lbl_Time_Remain.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lbl_Time_Remain.Location = new System.Drawing.Point(48, 172);
            lbl_Time_Remain.Name = "lbl_Time_Remain";
            lbl_Time_Remain.Size = new System.Drawing.Size(487, 28);
            lbl_Time_Remain.TabIndex = 14;
            lbl_Time_Remain.Text = "Restliche Lieferzeit";
            // 
            // GroupBox_Individ_Texte
            // 
            GroupBox_Individ_Texte.Controls.Add(NUM_LOCK_PICTURE);
            GroupBox_Individ_Texte.Controls.Add(lbl_NUM2_Text);
            GroupBox_Individ_Texte.Controls.Add(lbl_NUM3_Text);
            GroupBox_Individ_Texte.Controls.Add(NUM1_Label);
            GroupBox_Individ_Texte.Controls.Add(NUM3_Label);
            GroupBox_Individ_Texte.Controls.Add(lbl_NUM1_Text);
            GroupBox_Individ_Texte.Controls.Add(NUM2_Label);
            GroupBox_Individ_Texte.Location = new System.Drawing.Point(4, 378);
            GroupBox_Individ_Texte.Name = "GroupBox_Individ_Texte";
            GroupBox_Individ_Texte.Size = new System.Drawing.Size(544, 195);
            GroupBox_Individ_Texte.TabIndex = 13;
            GroupBox_Individ_Texte.TabStop = false;
            GroupBox_Individ_Texte.Text = "Individuelle Texte";
            // 
            // NUM_LOCK_PICTURE
            // 
            NUM_LOCK_PICTURE.Location = new System.Drawing.Point(467, 19);
            NUM_LOCK_PICTURE.Name = "NUM_LOCK_PICTURE";
            NUM_LOCK_PICTURE.Size = new System.Drawing.Size(64, 18);
            NUM_LOCK_PICTURE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            NUM_LOCK_PICTURE.TabIndex = 10;
            NUM_LOCK_PICTURE.TabStop = false;
            // 
            // lbl_NUM2_Text
            // 
            lbl_NUM2_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lbl_NUM2_Text.Location = new System.Drawing.Point(6, 99);
            lbl_NUM2_Text.Name = "lbl_NUM2_Text";
            lbl_NUM2_Text.Size = new System.Drawing.Size(525, 25);
            lbl_NUM2_Text.TabIndex = 10;
            lbl_NUM2_Text.Text = "label8";
            lbl_NUM2_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_NUM3_Text
            // 
            lbl_NUM3_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lbl_NUM3_Text.Location = new System.Drawing.Point(6, 156);
            lbl_NUM3_Text.Name = "lbl_NUM3_Text";
            lbl_NUM3_Text.Size = new System.Drawing.Size(525, 25);
            lbl_NUM3_Text.TabIndex = 12;
            lbl_NUM3_Text.Text = "label8";
            lbl_NUM3_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NUM1_Label
            // 
            NUM1_Label.AutoSize = true;
            NUM1_Label.Location = new System.Drawing.Point(3, 26);
            NUM1_Label.Name = "NUM1_Label";
            NUM1_Label.Size = new System.Drawing.Size(65, 13);
            NUM1_Label.TabIndex = 7;
            NUM1_Label.Text = "NUM1-Text:";
            // 
            // NUM3_Label
            // 
            NUM3_Label.AutoSize = true;
            NUM3_Label.Location = new System.Drawing.Point(3, 139);
            NUM3_Label.Name = "NUM3_Label";
            NUM3_Label.Size = new System.Drawing.Size(65, 13);
            NUM3_Label.TabIndex = 11;
            NUM3_Label.Text = "NUM3-Text:";
            // 
            // lbl_NUM1_Text
            // 
            lbl_NUM1_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lbl_NUM1_Text.Location = new System.Drawing.Point(6, 43);
            lbl_NUM1_Text.Name = "lbl_NUM1_Text";
            lbl_NUM1_Text.Size = new System.Drawing.Size(525, 25);
            lbl_NUM1_Text.TabIndex = 8;
            lbl_NUM1_Text.Text = "label8";
            lbl_NUM1_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NUM2_Label
            // 
            NUM2_Label.AutoSize = true;
            NUM2_Label.Location = new System.Drawing.Point(3, 82);
            NUM2_Label.Name = "NUM2_Label";
            NUM2_Label.Size = new System.Drawing.Size(65, 13);
            NUM2_Label.TabIndex = 9;
            NUM2_Label.Text = "NUM2-Text:";
            // 
            // status_jb_canc_lb
            // 
            status_jb_canc_lb.AutoSize = true;
            status_jb_canc_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            status_jb_canc_lb.Location = new System.Drawing.Point(148, 245);
            status_jb_canc_lb.Name = "status_jb_canc_lb";
            status_jb_canc_lb.Size = new System.Drawing.Size(0, 19);
            status_jb_canc_lb.TabIndex = 6;
            // 
            // truck_lb
            // 
            truck_lb.AutoSize = true;
            truck_lb.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            truck_lb.Location = new System.Drawing.Point(47, 110);
            truck_lb.Name = "truck_lb";
            truck_lb.Size = new System.Drawing.Size(55, 16);
            truck_lb.TabIndex = 5;
            truck_lb.Text = "Truck: ";
            // 
            // destination_lb
            // 
            destination_lb.AutoSize = true;
            destination_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            destination_lb.Location = new System.Drawing.Point(47, 166);
            destination_lb.Name = "destination_lb";
            destination_lb.Size = new System.Drawing.Size(0, 19);
            destination_lb.TabIndex = 3;
            // 
            // depature_lb
            // 
            depature_lb.AutoSize = true;
            depature_lb.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            depature_lb.Location = new System.Drawing.Point(47, 149);
            depature_lb.Name = "depature_lb";
            depature_lb.Size = new System.Drawing.Size(84, 16);
            depature_lb.TabIndex = 2;
            depature_lb.Text = "Departure: ";
            // 
            // cargo_lb
            // 
            cargo_lb.AutoSize = true;
            cargo_lb.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            cargo_lb.Location = new System.Drawing.Point(47, 129);
            cargo_lb.Name = "cargo_lb";
            cargo_lb.Size = new System.Drawing.Size(60, 16);
            cargo_lb.TabIndex = 1;
            cargo_lb.Text = "Freight:";
            // 
            // speed_lb
            // 
            speed_lb.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            speed_lb.BackColor = System.Drawing.Color.Transparent;
            speed_lb.Font = new System.Drawing.Font("Verdana", 26.25F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, 0);
            speed_lb.Location = new System.Drawing.Point(10, 43);
            speed_lb.Name = "speed_lb";
            speed_lb.Size = new System.Drawing.Size(525, 42);
            speed_lb.TabIndex = 0;
            speed_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            tableLayoutPanel1.Location = new System.Drawing.Point(39, 63);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(470, 179);
            tableLayoutPanel1.TabIndex = 4;
            tableLayoutPanel1.Visible = true;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.Transparent;
            panel4.Controls.Add(Dashboard_1);
            panel4.Location = new System.Drawing.Point(1097, 28);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(284, 582);
            panel4.TabIndex = 4;
            // 
            // Dashboard_1
            // 
            Dashboard_1.Controls.Add(progressBar_F);
            Dashboard_1.Controls.Add(Retarder_ICON);
            Dashboard_1.Controls.Add(label6);
            Dashboard_1.Controls.Add(Batterie_ICON);
            Dashboard_1.Controls.Add(Handbremse_ICON);
            Dashboard_1.Controls.Add(Rest_KM_Label);
            Dashboard_1.Controls.Add(Motorbremse_ICON);
            Dashboard_1.Controls.Add(label5);
            Dashboard_1.Controls.Add(Luft_Progress);
            Dashboard_1.Controls.Add(pictureBox1);
            Dashboard_1.Controls.Add(pictureBox2);
            Dashboard_1.Controls.Add(label4);
            Dashboard_1.Location = new System.Drawing.Point(5, 328);
            Dashboard_1.Name = "Dashboard_1";
            Dashboard_1.Size = new System.Drawing.Size(276, 245);
            Dashboard_1.TabIndex = 15;
            Dashboard_1.TabStop = false;
            Dashboard_1.Text = "Dashboard";
            // 
            // progressBar_F
            // 
            progressBar_F.Location = new System.Drawing.Point(9, 124);
            progressBar_F.Margin = new System.Windows.Forms.Padding(0);
            progressBar_F.Name = "progressBar_F";
            progressBar_F.Size = new System.Drawing.Size(249, 23);
            progressBar_F.TabIndex = 15;
            // 
            // Retarder_ICON
            // 
            Retarder_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.retarder1;
            Retarder_ICON.Location = new System.Drawing.Point(136, 21);
            Retarder_ICON.Name = "Retarder_ICON";
            Retarder_ICON.Size = new System.Drawing.Size(63, 53);
            Retarder_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            Retarder_ICON.TabIndex = 12;
            Retarder_ICON.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(39, 182);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(63, 13);
            label6.TabIndex = 14;
            label6.Text = "Air Pressure";
            // 
            // Batterie_ICON
            // 
            Batterie_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_autobatterie_80;
            Batterie_ICON.Location = new System.Drawing.Point(10, 21);
            Batterie_ICON.Name = "Batterie_ICON";
            Batterie_ICON.Size = new System.Drawing.Size(63, 53);
            Batterie_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            Batterie_ICON.TabIndex = 7;
            Batterie_ICON.TabStop = false;
            // 
            // Handbremse_ICON
            // 
            Handbremse_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.bremse_schwarz;
            Handbremse_ICON.Location = new System.Drawing.Point(73, 21);
            Handbremse_ICON.Name = "Handbremse_ICON";
            Handbremse_ICON.Size = new System.Drawing.Size(63, 53);
            Handbremse_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            Handbremse_ICON.TabIndex = 8;
            Handbremse_ICON.TabStop = false;
            // 
            // Rest_KM_Label
            // 
            Rest_KM_Label.AutoSize = true;
            Rest_KM_Label.Font = new System.Drawing.Font("Verdana", 12F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, 0);
            Rest_KM_Label.Location = new System.Drawing.Point(44, 102);
            Rest_KM_Label.Name = "Rest_KM_Label";
            Rest_KM_Label.Size = new System.Drawing.Size(47, 18);
            Rest_KM_Label.TabIndex = 6;
            Rest_KM_Label.Text = "0,00";
            // 
            // Motorbremse_ICON
            // 
            Motorbremse_ICON.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_motor_128;
            Motorbremse_ICON.Location = new System.Drawing.Point(195, 21);
            Motorbremse_ICON.Name = "Motorbremse_ICON";
            Motorbremse_ICON.Size = new System.Drawing.Size(63, 53);
            Motorbremse_ICON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            Motorbremse_ICON.TabIndex = 9;
            Motorbremse_ICON.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label5.Location = new System.Drawing.Point(7, 224);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(265, 9);
            label5.TabIndex = 5;
            label5.Text = "0        10         20         30        40        50        60        70        " +
    "80        90        100    ";
            // 
            // Luft_Progress
            // 
            Luft_Progress.Location = new System.Drawing.Point(10, 197);
            Luft_Progress.MarqueeAnimationSpeed = 0;
            Luft_Progress.Name = "Luft_Progress";
            Luft_Progress.Size = new System.Drawing.Size(248, 23);
            Luft_Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            Luft_Progress.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBox1.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_benzin_24;
            pictureBox1.Location = new System.Drawing.Point(11, 97);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(27, 27);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBox2.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_luftdruck_24;
            pictureBox2.Location = new System.Drawing.Point(10, 168);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(27, 27);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(8, 153);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(265, 9);
            label4.TabIndex = 2;
            label4.Text = "0        10         20         30        40        50        60        70        " +
    "80        90        100    ";
            // 
            // version_lb
            // 
            version_lb.AutoSize = true;
            version_lb.Location = new System.Drawing.Point(1287, 9);
            version_lb.Name = "version_lb";
            version_lb.Size = new System.Drawing.Size(0, 13);
            version_lb.TabIndex = 5;
            // 
            // TaskBar_Icon
            // 
            TaskBar_Icon.BalloonTipText = "VTC-Manager läuft im Hintergrund";
            TaskBar_Icon.BalloonTipTitle = "VTC-Manager";
            TaskBar_Icon.ContextMenuStrip = contextTaskbar;
            TaskBar_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskBar_Icon.Icon")));
            TaskBar_Icon.Text = "VTC-Manager";
            TaskBar_Icon.Visible = true;
            // 
            // contextTaskbar
            // 
            contextTaskbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            öffnenToolStripMenuItem,
            einstellungenToolStripMenuItem1,
            webseiteToolStripMenuItem,
            überToolStripMenuItem,
            beendenToolStripMenuItem1});
            contextTaskbar.Name = "contextTaskbar";
            contextTaskbar.Size = new System.Drawing.Size(146, 114);
            // 
            // öffnenToolStripMenuItem
            // 
            öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            öffnenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            öffnenToolStripMenuItem.Text = "Öffnen";
            // 
            // einstellungenToolStripMenuItem1
            // 
            einstellungenToolStripMenuItem1.Name = "einstellungenToolStripMenuItem1";
            einstellungenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            einstellungenToolStripMenuItem1.Text = "Einstellungen";
            einstellungenToolStripMenuItem1.Click += new System.EventHandler(einstellungenToolStripMenuItem1_Click);
            // 
            // webseiteToolStripMenuItem
            // 
            webseiteToolStripMenuItem.Name = "webseiteToolStripMenuItem";
            webseiteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            webseiteToolStripMenuItem.Text = "Webseite";
            // 
            // überToolStripMenuItem
            // 
            überToolStripMenuItem.Name = "überToolStripMenuItem";
            überToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            überToolStripMenuItem.Text = "Über...";
            // 
            // beendenToolStripMenuItem1
            // 
            beendenToolStripMenuItem1.Name = "beendenToolStripMenuItem1";
            beendenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            beendenToolStripMenuItem1.Text = "Beenden";
            // 
            // groupStatistiken
            // 
            groupStatistiken.BackColor = System.Drawing.Color.Transparent;
            groupStatistiken.Controls.Add(ats_button);
            groupStatistiken.Controls.Add(ets2_button);
            groupStatistiken.Controls.Add(truckersMP_Button);
            groupStatistiken.Controls.Add(user_company_lb);
            groupStatistiken.Controls.Add(statistic_panel_topic);
            groupStatistiken.Controls.Add(act_bank_balance_lb);
            groupStatistiken.Controls.Add(driven_tours_lb);
            groupStatistiken.Location = new System.Drawing.Point(0, 35);
            groupStatistiken.Name = "groupStatistiken";
            groupStatistiken.Size = new System.Drawing.Size(534, 178);
            groupStatistiken.TabIndex = 6;
            groupStatistiken.TabStop = false;
            // 
            // ats_button
            // 
            ats_button.Image = global::VTCManager_1._0._0.Properties.Resources.ats2l;
            ats_button.Location = new System.Drawing.Point(17, 124);
            ats_button.Name = "ats_button";
            ats_button.Size = new System.Drawing.Size(100, 54);
            ats_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            ats_button.TabIndex = 8;
            ats_button.TabStop = false;
            ats_button.Visible = false;
            ats_button.Click += new System.EventHandler(ats_button_Click);
            // 
            // ets2_button
            // 
            ets2_button.Image = global::VTCManager_1._0._0.Properties.Resources._280px_Ets2_logo;
            ets2_button.Location = new System.Drawing.Point(225, 124);
            ets2_button.Name = "ets2_button";
            ets2_button.Size = new System.Drawing.Size(100, 54);
            ets2_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            ets2_button.TabIndex = 7;
            ets2_button.TabStop = false;
            ets2_button.Visible = false;
            ets2_button.Click += new System.EventHandler(ets2_button_Click);
            // 
            // truckersMP_Button
            // 
            truckersMP_Button.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            truckersMP_Button.BackColor = System.Drawing.Color.Transparent;
            truckersMP_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("truckersMP_Button.BackgroundImage")));
            truckersMP_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            truckersMP_Button.FlatAppearance.BorderSize = 0;
            truckersMP_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            truckersMP_Button.Location = new System.Drawing.Point(450, 124);
            truckersMP_Button.Margin = new System.Windows.Forms.Padding(0);
            truckersMP_Button.Name = "truckersMP_Button";
            truckersMP_Button.Size = new System.Drawing.Size(84, 54);
            truckersMP_Button.TabIndex = 6;
            truckersMP_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            truckersMP_Button.UseVisualStyleBackColor = false;
            truckersMP_Button.Click += new System.EventHandler(truckersMP_Button_Click);
            // 
            // user_company_lb
            // 
            user_company_lb.AutoSize = true;
            user_company_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            user_company_lb.Location = new System.Drawing.Point(16, 86);
            user_company_lb.Name = "user_company_lb";
            user_company_lb.Size = new System.Drawing.Size(178, 19);
            user_company_lb.TabIndex = 5;
            user_company_lb.Text = "angestellt bei: Selbstständig";
            // 
            // statistic_panel_topic
            // 
            statistic_panel_topic.AutoSize = true;
            statistic_panel_topic.BackColor = System.Drawing.Color.Transparent;
            statistic_panel_topic.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            statistic_panel_topic.Location = new System.Drawing.Point(12, 22);
            statistic_panel_topic.Name = "statistic_panel_topic";
            statistic_panel_topic.Size = new System.Drawing.Size(174, 30);
            statistic_panel_topic.TabIndex = 2;
            statistic_panel_topic.Text = "User\'s  Statistiken";
            statistic_panel_topic.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // act_bank_balance_lb
            // 
            act_bank_balance_lb.AutoSize = true;
            act_bank_balance_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            act_bank_balance_lb.Location = new System.Drawing.Point(16, 67);
            act_bank_balance_lb.Name = "act_bank_balance_lb";
            act_bank_balance_lb.Size = new System.Drawing.Size(139, 19);
            act_bank_balance_lb.TabIndex = 4;
            act_bank_balance_lb.Text = "aktueller Kontostand:";
            // 
            // driven_tours_lb
            // 
            driven_tours_lb.AutoSize = true;
            driven_tours_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            driven_tours_lb.Location = new System.Drawing.Point(16, 48);
            driven_tours_lb.Name = "driven_tours_lb";
            driven_tours_lb.Size = new System.Drawing.Size(119, 19);
            driven_tours_lb.TabIndex = 3;
            driven_tours_lb.Text = "gefahrene Touren:";
            // 
            // groupVerkehr
            // 
            groupVerkehr.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            groupVerkehr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupVerkehr.BackColor = System.Drawing.Color.Transparent;
            groupVerkehr.Controls.Add(lbl_Reload_Time);
            groupVerkehr.Controls.Add(tableLayoutPanel1);
            groupVerkehr.Controls.Add(label1);
            groupVerkehr.Controls.Add(linkLabel1);
            groupVerkehr.Controls.Add(label2);
            groupVerkehr.Location = new System.Drawing.Point(0, 239);
            groupVerkehr.Name = "groupVerkehr";
            groupVerkehr.Size = new System.Drawing.Size(537, 367);
            groupVerkehr.TabIndex = 7;
            groupVerkehr.TabStop = false;
            // 
            // lbl_Reload_Time
            // 
            lbl_Reload_Time.AutoSize = true;
            lbl_Reload_Time.Location = new System.Drawing.Point(12, 345);
            lbl_Reload_Time.Name = "lbl_Reload_Time";
            lbl_Reload_Time.Size = new System.Drawing.Size(16, 13);
            lbl_Reload_Time.TabIndex = 6;
            lbl_Reload_Time.Text = "...";
            // 
            // updateTraffic
            // 
            updateTraffic.Enabled = true;
            updateTraffic.Interval = 30000;
            updateTraffic.Tick += new System.EventHandler(updateTraffic_Tick);
            // 
            // lbl_Revision
            // 
            lbl_Revision.BackColor = System.Drawing.Color.Transparent;
            lbl_Revision.Location = new System.Drawing.Point(1228, 7);
            lbl_Revision.Name = "lbl_Revision";
            lbl_Revision.Size = new System.Drawing.Size(151, 18);
            lbl_Revision.TabIndex = 8;
            lbl_Revision.Text = "...";
            lbl_Revision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = System.Drawing.Color.Transparent;
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            WebServer_Status_label,
            Label_DB_Server,
            User_Patreon_State});
            statusStrip1.Location = new System.Drawing.Point(0, 616);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1384, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // WebServer_Status_label
            // 
            WebServer_Status_label.Name = "WebServer_Status_label";
            WebServer_Status_label.Size = new System.Drawing.Size(10, 17);
            WebServer_Status_label.Text = ".";
            WebServer_Status_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_DB_Server
            // 
            Label_DB_Server.Name = "Label_DB_Server";
            Label_DB_Server.Size = new System.Drawing.Size(10, 17);
            Label_DB_Server.Text = ".";
            // 
            // User_Patreon_State
            // 
            User_Patreon_State.Image = global::VTCManager_1._0._0.Properties.Resources.patreon_24;
            User_Patreon_State.Name = "User_Patreon_State";
            User_Patreon_State.Size = new System.Drawing.Size(16, 17);
            // 
            // anti_AFK_TIMER
            // 
            anti_AFK_TIMER.Interval = 240000;
            anti_AFK_TIMER.Tick += new System.EventHandler(anti_AFK_TIMER_Tick);
            // 
            // imageList1
            // 
            imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "icons8-einstellungen-64.png");
            // 
            // Main
            // 
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            ClientSize = new System.Drawing.Size(1384, 638);
            Controls.Add(lbl_Revision);
            Controls.Add(groupVerkehr);
            Controls.Add(groupStatistiken);
            Controls.Add(version_lb);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(1404, 681);
            Name = "Main";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "VTC-Manager";
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(Main_FormClosing_1);
            Load += new System.EventHandler(Main_Load);
            ((System.ComponentModel.ISupportInitialize)(send_tour_status)).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            GroupBox_Individ_Texte.ResumeLayout(false);
            GroupBox_Individ_Texte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(NUM_LOCK_PICTURE)).EndInit();
            panel4.ResumeLayout(false);
            Dashboard_1.ResumeLayout(false);
            Dashboard_1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(Retarder_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(Batterie_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(Handbremse_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(Motorbremse_ICON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).EndInit();
            contextTaskbar.ResumeLayout(false);
            groupStatistiken.ResumeLayout(false);
            groupStatistiken.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(ats_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ets2_button)).EndInit();
            groupVerkehr.ResumeLayout(false);
            groupVerkehr.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        public System.Windows.Controls.Orientation Orientation { get; set; }

        private void topMenuWebsiteClick(object sender, EventArgs e)
        {
            Process.Start("https://vtc.northwestvideo.de/");
        }


        private void beendenToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
            Logs.WriteLOG("<INFO> CLIENT_CLOSED");
        }

        private void einstellungenToolStripMenuItemClick(object sender, EventArgs e)
        {
            SettingsWindow Settingswindow = new SettingsWindow(user.translation, user.patreon_state);
            Settingswindow.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            Settingswindow.ShowDialog();
            Logs.WriteLOG("<INFO> SETTINGS_OPEN");
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void MenuAbmeldenButton_Click(object sender, EventArgs e)
        {
            Logs.WriteLOG("<INFO> USER_LOGGED_OFF");
            settings.DeleteConfig();
            Application.Restart();

        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logs.WriteLOG("<INFO> WINDOW_UEBER_OPEN");
            Ueber ueber = new Ueber();
            ueber.ShowDialog();
        }


        // Edit by Thommy
        private void Main_Load(object sender, EventArgs e)
        {

            th.Loesche_Alte_DLL();

            // ################  Erstelle LOG File wenn nicht vorhanden  ###########################
            if (!File.Exists(logDirectory + logFile) || !File.Exists(logDirectory + systemlogFile))
            {
                Directory.CreateDirectory(logDirectory);
                // #### ERSTELLE NORMALES LOG FILE #####
                if (!File.Exists(logDirectory + logFile))
                {
                    File.Create(logDirectory + logFile);
                }
                // #### ERSTELLE SYSTEM LOG FILE #######
                if (!File.Exists(logDirectory + systemlogFile))
                {
                    File.Create(logDirectory + systemlogFile);
                }

                if (!string.IsNullOrEmpty(utils.Reg_Lesen("Data", "Log_access_failed_restarts", false)))
                {
                    int restart_count = int.Parse(utils.Reg_Lesen("Data", "Log_access_failed_restarts", false));
                    if (restart_count > 5)
                    {
                        MessageBox.Show("Sorry, but we couldn't create the log files. Please check the directory '" + logDirectory + "' and contact the VTCManager Support Team for assistance.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utilities.HardExit();
                    }
                    restart_count++;
                    utils.Reg_Schreiben("Log_access_failed_restarts", restart_count.ToString(), "Data");
                }
                else
                {
                    utils.Reg_Schreiben("Log_access_failed_restarts", "1", "Data");
                }
                Utilities.HardRestart();
            }
            Logs.Clear_Log_File();
            if (!string.IsNullOrEmpty(utils.Reg_Lesen("Data", "Log_access_failed_restarts")))
            {
                int restart_count = int.Parse(utils.Reg_Lesen("Data", "Log_access_failed_restarts"));
                Logs.WriteLOG("<WARNING>Log konnte mehr als 1mal aber weniger als 5mal nicht angelegt werden oder diese Installation ist neu. Neustarts: " + restart_count);
                utils.Reg_Schreiben("Log_access_failed_restarts", "0", "Data");
            }

            /* REVISION NUMBER NOW ON TOP ! */
            discord = new Discord();

            /// ######################   GEHT NOCH NICHT, DESHALB AUSBLENDEN    ###################
            Motorbremse_ICON.Visible = false;

            User_Patreon_State.Text = user.patreon_state.ToString();
            Frachtmarkt fm = new Frachtmarkt
            {
                FM_Patreon_State = user.patreon_state
            };

            frachtmarktToolStripMenuItem.Visible = (user.patreon_state >= 2) ? true : false;

            // ################## CHECK ob der AFK Text bei nicht Spendern stimmt ##################
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN")))
            {
                utils.Reg_Schreiben("ANTI_AFK_AN", "0", "TruckersMP_Autorun");
            }

            if (user.patreon_state == 0)
            {
                utils.Reg_Schreiben("ANTI_AFK", "VTCManager wünscht Gute und Sichere Fahrt!", "TruckersMP_Autorun");
            }

            // ##################   ANTI AFK ENDE ##################################################
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM_LOCK_SHOW")))
            {
                utils.Reg_Schreiben("NUM_LOCK_SHOW", "0", "TruckersMP_Autorun");
            }

            // ##########################   Dashboaard An/Ausschalten   ######################################
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard")))
            {
                utils.Reg_Schreiben("Dashboard", "0", "TruckersMP_Autorun");
            }

            // ##################   HOTKEY   #######################################################
            // Lade NUM_LOCK Status On/Off aus Registry
            NUM_LOCK_PICTURE.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "NUM_LOCK_SHOW") == "1") ? true : false;


            // #############  !!! Text werden bei locationupdate() neu geladen !!!  ################
            lade_NUMx_TEXTE();   // FIRST LOAD -> dann über locationupdate()


            Hotkey _Hotkey1 = new Hotkey
            {
                KeyCode = Keys.NumPad1
            };
            _Hotkey1.HotkeyPressed += new EventHandler(NUM_PAD_1_PRESSED);
            try
            {
                _Hotkey1.Enabled = true;
            }
            catch (Exception ex) { Logs.WriteLOG("<ERROR> NUM_PAD 1 Exeption " + ex.StackTrace); }

            Hotkey _Hotkey2 = new Hotkey
            {
                KeyCode = Keys.NumPad2
            };
            _Hotkey2.HotkeyPressed += new EventHandler(NUM_PAD_2_PRESSED);
            try
            {
                _Hotkey2.Enabled = true;
            }
            catch (Exception ex) { Logs.WriteLOG("<ERROR> NUM_PAD 2 Exeption " + ex.StackTrace); }

            Hotkey _Hotkey3 = new Hotkey
            {
                KeyCode = Keys.NumPad3
            };
            _Hotkey3.HotkeyPressed += new EventHandler(NUM_PAD_3_PRESSED);
            try
            {
                _Hotkey3.Enabled = true;
            }
            catch (Exception ex) { Logs.WriteLOG("<ERROR> NUM_PAD 3 Exeption " + ex.StackTrace); }

            // ##################   HOTKEY ENDE    #################################################

            // ##################   PATREON SACHEN   #################################################
            GroupBox_Individ_Texte.Visible = (user.patreon_state >= 2) ? true : false;


            // ##################   PATREON ENDE    #################################################


            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "GroupBox_Diagnostic")))
            {
                utils.Reg_Schreiben("GroupBox_Diagnostic", "0", "TruckersMP_Autorun");
            }



            // ####################   VERSION IN REG SCHREIBEN   ###################################
            utils.Reg_Schreiben("Version", Information.ClientVersion, "TruckersMP_Autorun");
            utils.Reg_Schreiben("Telemetry-Version", Information.TelemetryVersion, "TruckersMP_Autorun");

            // Version Logging
            Logs.WriteLOG("<INFO> Client-Version: " + Information.ClientVersion);
            Logs.WriteLOG("<INFO> Telemetry-Version: " + Information.TelemetryVersion);
            Logs.WriteLOG("<INFO> SCSSDK-Version: " + Information.SCSSdkVersion);



            Logs.WriteLOG("<INFO> .NET: " + RuntimeInformation.FrameworkDescription);


            // ####################   ZEIGE PATH WINDOW WENN ETS2 PFAD NICHT VORHANDEN   ###########
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad")))
            {
                Logs.WriteLOG("<INFO> ETS2 Pfad Window Open");
                ETS2_Pfad_Window win = new ETS2_Pfad_Window();
                win.Show();
                win.Focus();
                WindowState = FormWindowState.Minimized;
                return;
            }
            else
            {
                ets2_button.Visible = true;
                ToolTip tt = new ToolTip();
                tt.SetToolTip(ets2_button, "Starte ETS2 im Singleplayer !");

                string dest_leer = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");

                if (!File.Exists(dest_leer + @"\bin\win_x64\plugins\scs-telemetry.dll"))
                {
                    Logs.WriteLOG("<ERROR> DLL NOT IN PLUGINS  [Main.cs->1749]");
                    if (!Directory.Exists(dest_leer + @"\bin\win_x64\plugins"))
                    {
                        Logs.WriteLOG("<ERROR> FOLDER PLUGINS IN ETS2 NOT AVAIBLE");
                        try
                        {
                            Directory.CreateDirectory(dest_leer + @"\bin\win_x64\plugins");
                        }
                        catch (Exception ex)
                        {
                            Logs.WriteLOG("<ERROR> FOLDER PLUGINS CREATED " + ex.StackTrace);
                        }
                    }
                    try
                    {
                        File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_leer + @"\bin\win_x64\plugins\scs-telemetry.dll");
                        Logs.WriteLOG("<INFO> scs-telemetry.dll IN PLUGINS FOLDER COPIED! [Main.cs->1725]");
                    }
                    catch (Exception ex)
                    {
                        Logs.WriteLOG("<INFO> scs-telemetry.dll Not Copied in Plgins Folder! " + ex.StackTrace);
                    }

                }
                else
                {

                    //  ###############    ALLE PLUGINS IN REG SCHREIBEN   ################
                    string Plugins_ETS = "";
                    DirectoryInfo di = new DirectoryInfo(dest_leer + @"\bin\win_x64\plugins");
                    foreach (var fi in di.GetFiles())
                    { Plugins_ETS += fi.Name + "  "; }
                    utils.Reg_Schreiben("Plugins ETS", Plugins_ETS, "TruckersMP_Autorun");
                    Logs.WriteLOG("<INFO> WRITE ALL PLUGINS IN REGISTRY");
                    // ################  Diagnostikzwecke ENDE  ###########################
                }

                try
                {
                    string dest_leer2 = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
                    if (!string.IsNullOrEmpty(dest_leer2))
                    {
                        Logs.WriteLOG("<INFO> ATS PATH IS NOT NULL OR EMPTY [Main.cs->1757]");
                        ats_button.Visible = true;
                        ToolTip tt2 = new ToolTip();
                        tt2.SetToolTip(ats_button, "Starte ATS im Singleplayer !");

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
                            Logs.WriteLOG("<INFO> ALL ATS PLUGIN WRITE IN REGISTRY");
                            // ################  Diagnostikzwecke ENDE  ###########################
                        }
                    }
                    else
                    {
                        ats_button.Visible = false;
                    }
                }
                catch { }


                lbl_Revision.Text = "Client: " + Information.ClientVersion + " | Telemetry:" + Information.TelemetryVersion;
                labelRevision = Information.ClientVersion;
            }

            // ###################################### TELEMETRY COPY END ###########################


            // Background Changer
            string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
            if (string.IsNullOrEmpty(hintergrund))
            {
                utils.Reg_Schreiben("Background", "", "TruckersMP_Autorun");
            }

            string hintergrund2 = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
            if (hintergrund2.ToString() == "oldcar1") { BackgroundImage = Properties.Resources.oldcar1; }
            else if (hintergrund2 == "oldcar2") { BackgroundImage = Properties.Resources.oldcar2; }
            else if (hintergrund2 == "oldcar3") { BackgroundImage = Properties.Resources.oldcar3; }
            else if (hintergrund2 == "oldcar4") { BackgroundImage = Properties.Resources.oldcar4; }
            else { BackgroundImage = null; }
            // Background Changer ENDE 



            Dashboard_1.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "Dashboard") == "1") ? true : false;
            utils.Reg_Schreiben("Reload_Traffic_Sekunden", "300", "TruckersMP_Autorun");
            lbl_Reload_Time.Text = "Reload-Interval: " + reload + " Sek.";


            if (Utilities.IsGameRunning == true)
            {
                speed_lb.Text = user.translation.loading_text;
                Logs.WriteLOG("<INFO> Speed Label Text-> Translation loading.Text");
                truck_lb.Visible = false;
                destination_lb.Visible = false;
                depature_lb.Visible = false;
                cargo_lb.Visible = false;
                // Logs.WriteLOG("<INFO> Truck.lbl, Destination.lbl, Departure.lbl, Cargo.lbl Visible to false");
            }
            else
            {
                truck_lb.Visible = true;
                destination_lb.Visible = true;
                depature_lb.Visible = true;
                cargo_lb.Visible = true;
                // Logs.WriteLOG("<INFO> Truck.lbl, Destination.lbl, Depparture.lbl, Cargo.lbl Set to True");
            }
            updateTraffic_Tick(this, new EventArgs());


        }

        private void lade_NUMx_TEXTE()
        {
            try
            {
                if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM1")))
                {
                    utils.Reg_Schreiben("NUM1", "", "TruckersMP_Autorun");
                }

                NUM1_Label.Visible = true; lbl_NUM1_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM1");
            }
            catch
            {
                Logs.WriteLOG("Fehler beim Laden von NUM Text 1");
            }

            try
            {
                if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM2")))
                {
                    utils.Reg_Schreiben("NUM2", "", "TruckersMP_Autorun");
                }

                NUM2_Label.Visible = true; lbl_NUM2_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM2");
            }
            catch
            {
                Logs.WriteLOG("Fehler beim Laden von NUM Text 2");
            }

            try
            {
                if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM3")))
                {
                    utils.Reg_Schreiben("NUM3", "", "TruckersMP_Autorun");
                }

                NUM3_Label.Visible = true; lbl_NUM3_Text.Text = utils.Reg_Lesen("TruckersMP_Autorun", "NUM3");
            }
            catch
            {
                Logs.WriteLOG("Fehler beim Laden von NUM Text 3");
            }

        }


        void NUM_PAD_1_PRESSED(object sender, EventArgs e)
        {
            SendKeys.Send("y");
            Clipboard.Clear();
            string text1 = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM1"))) ? "..." : utils.Reg_Lesen("TruckersMP_Autorun", "NUM1");
            if (GetActiveWindowTitle().Contains("Euro Truck Simulator 2") || GetActiveWindowTitle().Contains("American Truck Simulator"))
            {
                try
                {
                    SendKeys.Send(text1);
                    SendKeys.Send("{Enter}");
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Dafür muss ETS2 oder ATS laufen !", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        void NUM_PAD_2_PRESSED(object sender, EventArgs e)
        {
            SendKeys.Send("y");
            Clipboard.Clear();
            string text2 = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM2"))) ? "..." : utils.Reg_Lesen("TruckersMP_Autorun", "NUM2");
            if (GetActiveWindowTitle().Contains("Euro Truck Simulator 2") || GetActiveWindowTitle().Contains("American Truck Simulator"))
            {
                try
                {
                    SendKeys.Send(text2);
                    SendKeys.Send("{Enter}");
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Dafür muss ETS2 oder ATS laufen !", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void NUM_PAD_3_PRESSED(object sender, EventArgs e)
        {
            SendKeys.Send("y");
            Clipboard.Clear();
            var text3 = (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "NUM3"))) ? "..." : utils.Reg_Lesen("TruckersMP_Autorun", "NUM3");
            if (GetActiveWindowTitle().Contains("Euro Truck Simulator 2") || GetActiveWindowTitle().Contains("American Truck Simulator"))
            {
                try
                {
                    SendKeys.Send(text3);
                    SendKeys.Send("{Enter}");
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Dafür muss ETS2 oder ATS laufen !", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void truckersMP_Button_Click(object sender, EventArgs e)
        {
            truckersMP_Link = utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            if (truckersMP_Link != null)
            {
                Logs.WriteLOG("<INFO> TRUCKERSMP OPENING");
                Process.Start(truckersMP_Link);
            }
            else
            {
                Logs.WriteLOG("<ERROR> LINK2TMP IS MISSING->MESSAGEBOXING | TODO GERM-ENG VERSION");
                MessageBox.Show("Kein Link zu Truckers-MP angegeben!\nBitte schaue in den Einstellungen nach.", "Kein Link!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logs.WriteLOG("<INFO> TRUCKYAPP CLICKED");
            Process.Start("https://truckyapp.com/");
        }

        private void einstellungenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form sw = new SettingsWindow(user.translation, user.patreon_state);
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
                groupStatistiken.Visible = false;
                groupVerkehr.Visible = false;
                Size = new Size(581, 661);
                panel2.Location = new Point(5, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/expand.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
                BackgroundImage = null;
                Logs.WriteLOG("<INFO> GUI SIZE 1>0");
            }
            else
            {
                GUI_SIZE = 1;
                groupStatistiken.Visible = true;
                groupVerkehr.Visible = true;
                Size = new Size(1404, 681);
                panel2.Location = new Point(540, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/komprimieren.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
                string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
                if (hintergrund.ToString() == "oldcar1") { BackgroundImage = Properties.Resources.oldcar1; }
                else if (hintergrund == "oldcar2") { BackgroundImage = Properties.Resources.oldcar2; }
                else if (hintergrund == "oldcar3") { BackgroundImage = Properties.Resources.oldcar3; }
                else if (hintergrund == "oldcar4") { BackgroundImage = Properties.Resources.oldcar4; }
                else { BackgroundImage = null; }
                Logs.WriteLOG("<INFO> GUI SIZE 0>1");
            }


        }

        private void overlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Logs.WriteLOG("<INFO> APPLICATION EXIT");
        }



        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Is_DarkMode_On == 0)
            {
                Is_DarkMode_On = 1;
                BackgroundImage = null;
                menuStrip1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                ForeColor = System.Drawing.Color.LightGray;
                Logs.WriteLOG("<INFO> DARK MODE ON");
            }
            else
            {
                Is_DarkMode_On = 0;
                string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
                if (hintergrund.ToString() == "oldcar1") { BackgroundImage = Properties.Resources.oldcar1; }
                else if (hintergrund == "oldcar2") { BackgroundImage = Properties.Resources.oldcar2; }
                else if (hintergrund == "oldcar3") { BackgroundImage = Properties.Resources.oldcar3; }
                else if (hintergrund == "oldcar4") { BackgroundImage = Properties.Resources.oldcar4; }
                else { BackgroundImage = null; }

                menuStrip1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                ForeColor = System.Drawing.Color.Black;
                Logs.WriteLOG("<INFO> DARK MODE OFF");
            }
        }

        private void updateTraffic_Tick(object sender, EventArgs e)
        {
            updateTraffic.Interval = reload * 1000;
            lbl_Reload_Time.Text = "Reload-Interval: " + reload + " Sek.";
            load_traffic();

            // Serverstatus in Statusleiste anzeigen
            Servercheck sc = new Servercheck();
            var green = new Bitmap(Properties.Resources.iconfinder_bulled_green_1930264);
            var red = new Bitmap(Properties.Resources.iconfinder_bullet_red_84435);
            // Webserver-Check
            try
            {
                WebServer_Status_label.Text = "←Webserver";
                WebServer_Status_label.Image = (sc.WS_Check() == true) ? green : red;
                Logs.WriteLOG("<INFO> SERVER_WS: " + sc.WS_Check());
            }
            catch (Exception Fehler_Server)
            {
                Logs.WriteLOG("<ERROR> WEBSERVER NOT AVAILABLE | TODO GER-ENG TRANSLATE");
                MessageBox.Show("Keine Verbindung zum Webserver\n" + Fehler_Server.Message, "Fehler Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // DB-Check
            try
            {
                Label_DB_Server.Text = "←Datenbank";
                Label_DB_Server.Image = (sc.DB_Check() == true) ? green : red;
                Logs.WriteLOG("<INFO> SERVER_DB: " + sc.DB_Check());
            }
            catch (Exception Fehler_Server)
            {
                Logs.WriteLOG("<ERROR> DATABASE_SERVER NOT AVAILABLE | TODO GER-ENG TRANSLATE");
                MessageBox.Show("Keine Verbindung zum Datenbankserver\n" + Fehler_Server.Message, "Fehler Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void serverstatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();
            try
            {
                if (GetWindowText(handle, Buff, nChars) > 0)
                {
                    return Buff.ToString();
                }
            }
            catch { }
            return null;
        }


        private void anti_AFK_TIMER_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_RELOAD")))
            {
                utils.Reg_Schreiben("ANTI_AFK_RELOAD", "3", "TruckersMP_Autorun");
            }

            anti_AFK_TIMER.Interval = Convert.ToInt32(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_RELOAD").ToString()) * 60000;


            if (GetActiveWindowTitle().Contains("Euro Truck Simulator 2") || GetActiveWindowTitle().Contains("American Truck Simulator"))
            {
                Logs.WriteLOG("<INFO> ETS/ATS FOREGROUND->ANTI_AFK ACTIVE [Main.cs->1778]");
                if (GameRuns == 1)
                {
                    Logs.WriteLOG("<INFO> Game ist Running - Anti_AFK_Online");
                    if (user.Geschwindigkeit < 1)
                    {
                        Logs.WriteLOG("<INFO> ANTI_AFK FIRED - SPEED: " + user.Geschwindigkeit);
                        SendKeys.Send("y");
                        SendKeys.Send(utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK"));
                        SendKeys.Send("{Enter}");
                    }
                }
            }
            else
            {
                Logs.WriteLOG("<INFO> ETS/ATS BACKGROUND OR OFF->ANTI_AFK INACTIVE");
            }
        }

        private void oldCar1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.BackColor = Color.Transparent;
            BackgroundImage = Properties.Resources.oldcar1;
            utils.Reg_Schreiben("Background", "oldcar1", "TruckersMP_Autorun");

            Dashboard_1.Location = new System.Drawing.Point(5, 334);

        }

        private void oldCar2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.oldcar2;
            utils.Reg_Schreiben("Background", "oldcar2", "TruckersMP_Autorun");
            Dashboard_1.Location = new System.Drawing.Point(5, 10);
        }

        private void oldCar3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.oldcar3;
            utils.Reg_Schreiben("Background", "oldcar3", "TruckersMP_Autorun");

            Dashboard_1.Location = new System.Drawing.Point(5, 334);
        }

        private void oldCar4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.oldcar4;
            utils.Reg_Schreiben("Background", "oldcar4", "TruckersMP_Autorun");

            Dashboard_1.Location = new System.Drawing.Point(5, 334);
        }

        private void keinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundImage = null;
            utils.Reg_Schreiben("Background", "", "TruckersMP_Autorun");
        }




        private void ets2_button_Click(object sender, EventArgs e) =>
            Process.Start(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") + @"\bin\win_X64\eurotrucks2.exe");


        private void ats_button_Click(object sender, EventArgs e) =>
            Process.Start(utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") + @"\bin\win_X64\amtrucks.exe");



        private void TelemetryJobCancelled(object sender, EventArgs e)
        {
            job.cancel(sound, user);
            Logs.WriteLOG("<TOUR> Tour abgebrochen !");
        }



        private void TelemetryJobDelivered(object sender, EventArgs e)
        {
            Logs.WriteLOG("<TOUR> Tour wurde abgegeben ! - Prozent: " + job.currentPercentage);
            job.jobFinished = true;
        }


        private void TelemetryFined(object sender, EventArgs e)
        {
            Logs.WriteLOG("<TOUR> Strafe erhalten !");
        }
        private void TelemetryTollgate(object sender, EventArgs e)
        {
            th.Sende_TollGate(user.authcode, job.Tollgate_Payment, 1);
            Logs.WriteLOG("<TOUR> Mautstation durchfahren !");
        }


        private void TelemetryFerry(object sender, EventArgs e)
        {
            job.Ferry = true;
            th.Sende_Faehre(user.authcode, job.Tollgate_Payment, 1);
        }

        private void TelemetryTrain(object sender, EventArgs e)
        {
            Logs.WriteLOG("<TOUR> Train Used");
            job.Train = true;
        }


        private void TelemetryRefuel(object sender, EventArgs e)
        {
            Logs.WriteLOG("<TOUR> Truck Refueld !" + job.Refuel_Amount.ToString());
        }

        private void frachtmarktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frachtmarkt fm = new Frachtmarkt();
            //fm.Show();
        }

        private bool NumLock()
        {
            if (GetKeyState((int)System.Windows.Forms.Keys.NumLock) != 0)
            {
                return true;
            }

            return false;
        }

        private void dockTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupStatistiken.Visible = false;
            groupVerkehr.Visible = false;
            Dashboard_1.Visible = false;
            utils.Reg_Schreiben("Dashboard", "0", "TruckersMP_Autorun");
            GroupBox_Individ_Texte.Visible = false;
            //speed_lb.Visible = false;
            //truck_lb.Visible = false;
            cargo_lb.Visible = false;
            depature_lb.Visible = false;

            frachtmarktToolStripMenuItem.Visible = false;
            toolStripMenuItem1.Visible = false;
            lbl_Overlay.Visible = false;
            GUI_SIZE_BUTTON.Visible = false;
            eventsToolStripMenuItem.Visible = false;
            topmenuwebsite.Visible = false;
            topMenuAccount.Visible = false;
            dateiToolStripMenuItem.Visible = false;
            darkToolStripMenuItem.Visible = false;

            BackgroundImage = null;
            if (Screen.AllScreens.Length > 1)
            {

                Rectangle area = Screen.AllScreens[0].WorkingArea;
                Rectangle area2 = Screen.AllScreens[1].WorkingArea;
                Width = area2.Width;
                Height = 150;
                Location = new Point(area2.Width - area2.Width - area.Width, 0);
                TopMost = true;
                BackColor = Color.Chocolate;

                lbl_Revision.Dock = DockStyle.Right;
                lbl_Revision.Dock = DockStyle.Top;

                speed_lb.Location = new Point(300, 20);
                speed_lb.Font = new Font("Arial", 14, FontStyle.Bold);

                truck_lb.Location = new Point(20, 20);

            }
            else
            {
                Rectangle area = Screen.AllScreens[0].WorkingArea;
                Width = area.Width;
                Height = 150;
                Location = new Point(0, 0);
                TopMost = true;
            }





        }

    }



}

