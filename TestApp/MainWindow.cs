using System;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;


using Newtonsoft.Json;
using System.IO.Compression;
using CG.Web.MegaApiClient;
using WickedHamsters;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace GameLauncher
{
    public partial class MainWindow : Form
    {
        //PUBLIC
        //firebase
        Version firebaseVersion;
        Uri launcherUpdateURI;
        bool isEndVersionDownload;
        //version
        int fireMajor;
        int fireMinor;
        int firePatch;

        //app
        public string metaURL = Directory.GetCurrentDirectory() + "\\meta.wgl";
        public string launcherupdateURL = Directory.GetCurrentDirectory() + "\\launcherUpdate.zip";
        public Meta deserializedLocal;


        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "MLQzJXkF14h6Z9iE5QcXUVauik4rQRHf3uHby4eO",
            BasePath = "https://wickedlauncher.firebaseio.com/"
        };

        IFirebaseClient firebaseClient;

        public MainWindow(bool afterUpdate)
        {
            //Startup
            InitializeComponent();
            metaSetup();
            if (afterUpdate)
            {
                cleanUpLauncher();            
            }
            
            //debug
            //MakeNewJson();

            //firebase
            firebaseClient = new FireSharp.FirebaseClient(firebaseConfig);
            firebaseCheck(fireBaseStatusLabel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //firebaseCheck(fireBaseStatusLabel);
        }
        //Firebase
        void firebaseCheck(Label firebaseStatus)
        {
            if (firebaseClient != null)
            {
                firebaseStatus.Text = "Firebase status: connected";
            }
            else
            {
                firebaseStatus.Text = "Firebase status: disconnected";
            }
        }
        async void firebaseGetVersion()
        {
            FirebaseResponse firebaseResponse = await firebaseClient.GetAsync("app");
            FirebaseLauncherData launcherData = firebaseResponse.ResultAs<FirebaseLauncherData>();
            fireMajor = Utils.StringToInt(launcherData.major);
            fireMinor = Utils.StringToInt(launcherData.minor);
            firePatch = Utils.StringToInt(launcherData.patch);
            Uri.TryCreate(launcherData.url, UriKind.Absolute, out launcherUpdateURI);         
            Version firebaseVersione = new Version(fireMajor, fireMinor, firePatch,0);
            firebaseVersion = firebaseVersione;
            Console.WriteLine(firebaseVersion);
            isEndVersionDownload = true;
            Console.WriteLine(isEndVersionDownload);
        }
        //APP
        //checkMetaExist
        void metaSetup()
        {
            if (File.Exists(metaURL))
            {
                return;
            }
            else
            {
                MakeNewJson();
            }
     
        }

        void CheckUpdate()
        {
            //current
            string currentRead = File.ReadAllText(metaURL);
            deserializedLocal = JsonConvert.DeserializeObject<Meta>(currentRead);
            string localVersion = deserializedLocal.version.ToString();
            versionlabel.Text = "Aktualna wersja: " + localVersion;
            //server
            serverversionlbl.Text = "Wersja na serwerze: " + firebaseVersion;

            if (firebaseVersion.Equals(deserializedLocal.version))
            {
                lbl1.Text = "Posiadasz najnowszą wersję!";
            }
            else
            {
                lbl1.Text = "Znaleziono aktualizację!";
                downloadUpdate();
            }
        }
        async void downloadUpdate()
        {
            var client = new MegaApiClient();
            client.LoginAnonymous();

            INodeInfo node = client.GetNodeFromLink(launcherUpdateURI);

            IProgress<double> progressHandler = new Progress<double>(x => lbl1.Text = "Pobieram: " + x + "%");
            await client.DownloadFileAsync(launcherUpdateURI, node.Name, progressHandler);

            client.Logout();
            installUpdate();
        }
        void installUpdate()
        {
            string[] plikiLocal = Directory.GetFiles(Directory.GetCurrentDirectory());
            
            for (int i = 0; i < plikiLocal.Length; i++)
            {
                if(plikiLocal[i] == metaURL || plikiLocal[i] == launcherupdateURL)
                {
                    continue;
                }
                else
                {
                    File.Move(plikiLocal[i], plikiLocal[i] + ".old");
                }
            }
            ZipFile.ExtractToDirectory(launcherupdateURL, Directory.GetCurrentDirectory());
            


            deserializedLocal.version = new Version(fireMajor,fireMinor,firePatch,0);
            string newMeta = JsonConvert.SerializeObject(deserializedLocal);
            Stream stream = File.Open(metaURL, FileMode.OpenOrCreate);
            StreamWriter file = new StreamWriter(stream);
            file.Write(newMeta);
            file.Close();

            //cmdReboot
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("start GameLauncher.exe -true");
            cmd.StandardInput.Flush();
            
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Application.Exit();

        }
        void cleanUpLauncher()
        {
            string[] plikiOld = Directory.GetFiles(Directory.GetCurrentDirectory() + ".old");
            for (int i = 0; i < plikiOld.Length; i++)
            {
                File.Delete(plikiOld[i]);
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            lbl1.Text = "Sprawdzam dostępność aktualizacji...";
            Thread t1 = new Thread(new ThreadStart(firebaseGetVersion));
            t1.Start();
            Thread.Sleep(2000);

            CheckUpdate();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        //Debug
        void MakeNewJson()
        {
            Meta meta = new Meta();
            meta.version = new Version(0, 1, 0, 0);
            string write = JsonConvert.SerializeObject(meta);
            StreamWriter file = File.CreateText(metaURL);
            file.Write(write);
            file.Close();
        }
    }
    public class Meta{
        public Version version;
    }
    public class FirebaseLauncherData
    {
        public string major;
        public string minor;
        public string patch;
        public string url;
    }
}
