using System;
using System.IO;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using WickedHamsters;

using FireSharp.Config;
using FireSharp.Interfaces;
using Ionic.Zip;
using FireSharp.Response;


namespace TestApp
{
    public partial class GameLauncher : Form
    {
        //Zmienne globalne
        string metaFile = Directory.GetCurrentDirectory() + "\\meta.dbg";
        string launcherArchivePath = Directory.GetCurrentDirectory() + "\\launcher.zip";
        AppMeta globalAppMeta = new AppMeta();

        //debugZmienne
        Version appVersion = new Version(0,0,0,0);
        bool isUpdated = false;

        //Flagi
        bool isInstallingUpdate = false;

        IFirebaseConfig config = new FirebaseConfig { 
            AuthSecret= "MLQzJXkF14h6Z9iE5QcXUVauik4rQRHf3uHby4eO",
            BasePath= "https://wickedlauncher.firebaseio.com/"
        };

        IFirebaseClient client;

        public GameLauncher()
        {
            InitializeComponent();
            FirebaseConnectionSetup();
            setupApp();
            
            //setupFirebase();
            
        }
        void setupApp() {

            if (File.Exists(metaFile))
            {
                Console.WriteLine("Czytam z pliku");
                globalAppMeta = readMeta();
                if (globalAppMeta.afterUpdate == true) {
                    cleanAppUpdate();
                    
                }
                lblVersion.Text = globalAppMeta.version.Major.ToString() + "."+globalAppMeta.version.Minor.ToString()+ globalAppMeta.version.Build.ToString() + globalAppMeta.version.Revision.ToString();
            }
            else {
                globalAppMeta.afterUpdate = false;
                globalAppMeta.url = "";
                globalAppMeta.version = new Version(0,0,0,1);
                globalAppMeta.isBeta = false;
                globalAppMeta.isDebug = false;
                updateMeta(globalAppMeta);
            }
            if (globalAppMeta.isDebug)
            {
                serverversionlbl.Visible = true;
                firestatus.Visible = true;
                versionlabel.Visible = true;
            }
            else
            {
                serverversionlbl.Visible = false;
                firestatus.Visible = false;
                versionlabel.Visible = false;
            }
            if (globalAppMeta.isBeta)
            {
                betaLabel.Visible = true;
            }
            else
            {
                betaLabel.Visible = false;
            }
        }
        void FirebaseConnectionSetup() {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                firestatus.Text = "Firebase status: " + "Połączony";
            }
            else {
                firestatus.Text = "Firebase status: " + "Rozłączony";
            }
        }
        async void CheckAppUpdate()
        {
            
            lbl1.Text = "Sprawdzam dostępność aktualizacji...";           
            
            //current
            versionlabel.Text = "Aktualna wersja: " + globalAppMeta.version;
            //server
            if (globalAppMeta.isBeta)
            {
                FirebaseResponse response = await client.GetAsync("appMetaDevelopment");
                AppMeta appMeta = response.ResultAs<AppMeta>();
                Console.WriteLine("Otrzymane dane: " + appMeta.url + " " + appMeta.version);
                serverversionlbl.Text = "Wersja na serwerze: " + appMeta.version;

                if (appMeta.version.Equals(globalAppMeta.version))
                {
                    lbl1.Text = "Posiadasz aktualna wersję!";
                }
                else
                {
                    lbl1.Text = "Znaleziono aktualizację!";
                    appVersion = appMeta.version;
                    DownloadAppUpdate(appMeta);
                }
            }
            else {
                FirebaseResponse response = await client.GetAsync("appMetaProduction");
                AppMeta appMeta = response.ResultAs<AppMeta>();
                Console.WriteLine("Otrzymane dane: " + appMeta.url + " " + appMeta.version);
                serverversionlbl.Text = "Wersja na serwerze: " + appMeta.version;

                if (appMeta.version.Equals(globalAppMeta.version))
                {
                    lbl1.Text = "Posiadasz aktualna wersję!";
                }
                else
                {
                    lbl1.Text = "Znaleziono aktualizację!";
                    appVersion = appMeta.version;
                    
                    DownloadAppUpdate(appMeta);

                        
                                        
                    
                }
            }         



        }
        void installAppUpdate() {
            Console.WriteLine("==========DEBUG: AppUpdate list==========");

            string[] plikiLocal = Directory.GetFiles(Directory.GetCurrentDirectory());
           
            for (int i = 0; i < plikiLocal.Length; i++)
            {
                Console.WriteLine("==========DEBUG: AppUpdate list==========");
                Console.WriteLine("App update old: " + plikiLocal[i]);
                if (plikiLocal[i] == metaFile || plikiLocal[i] == launcherArchivePath)
                {
                    continue;
                }
                else {
                    Console.WriteLine("App update new: " + plikiLocal[i] + ".old");
                    File.Move(plikiLocal[i], plikiLocal[i]+".old");
                }
            }
            using (ZipFile zip = new ZipFile(launcherArchivePath))
            {
                zip.ExtractAll(Directory.GetCurrentDirectory());
                zip.Dispose();
            }
            isUpdated = true;
            Console.WriteLine("==========DEBUG: AppUpdate meta local==========");
            Console.WriteLine(appVersion);
            Console.WriteLine(isUpdated);
            globalAppMeta.version = appVersion;
            globalAppMeta.afterUpdate = isUpdated;
            Console.WriteLine("==========DEBUG: AppUpdate meta global==========");
            Console.WriteLine(globalAppMeta.afterUpdate);
            Console.WriteLine(globalAppMeta.version);
            updateMeta(globalAppMeta);
            Application.Restart();
        }
        void cleanAppUpdate() {
            globalAppMeta.afterUpdate = false;
            string[] plikiLocal = Directory.GetFiles(Directory.GetCurrentDirectory(),"*.old");
            for (int i = 0; i < plikiLocal.Length; i++)
            {
                File.Delete(plikiLocal[i]);
            }
            updateMeta(globalAppMeta);
        }
        void DownloadAppUpdate(AppMeta appMeta) {
            WebClient webClient = new WebClient();
            lbl1.Text = "Pobieram aktualizacje...";
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadAppUpdateCallback);
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            webClient.DownloadFileAsync(new Uri(appMeta.url),"launcher.zip");
            
            
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            lbl1.Text = "Instauluje aktualizację...";
            if (!isInstallingUpdate)
            {
                isInstallingUpdate = true;
                installAppUpdate();
            }
        }

        void DownloadAppUpdateCallback(object sender, DownloadProgressChangedEventArgs e) {
            progressBar.Value = e.ProgressPercentage;
            lbl1.Text = "Pobrano " + e.BytesReceived / 1024 + " KB z " + e.TotalBytesToReceive / 1024 + " KB";

        }        
        void updateMeta(AppMeta appMeta)
        {
            Console.WriteLine("==========DEBUG: UPDATE META==========");
            Console.WriteLine("App meta check: " + appMeta.version + " after update: " + appMeta.afterUpdate);
            StreamWriter file = File.CreateText(metaFile);
            string write = JsonConvert.SerializeObject(appMeta);            
            file.Write(write);
            file.Close();
        }
        AppMeta readMeta() {
            string reader = File.ReadAllText(metaFile);
            AppMeta readerMeta = JsonConvert.DeserializeObject<AppMeta>(reader);
            return readerMeta;
        }
        async void setupFirebase() {
            AppMeta appMeta = new AppMeta
            {
                version = new Version(0, 4, 0, 0),
                url = "wstaw pan url kurła"
            };
            PushResponse response = await client.PushAsync("appMetaProduction",appMeta);
            Console.WriteLine("Dodano dane do firebase: " + response.Result.name);

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            CheckAppUpdate();
        }
        private void GameLauncher_Load(object sender, EventArgs e) { 
        }
    }
    public class AppMeta{
        public Version version;
        public string url;
        public bool afterUpdate = false;
        public bool isBeta = false;
        public bool isDebug = false;
    }
}
