using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Windows.Forms;


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
        Uri updateURL;
        bool isEndVersionDownload;



        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "MLQzJXkF14h6Z9iE5QcXUVauik4rQRHf3uHby4eO",
            BasePath = "https://wickedlauncher.firebaseio.com/"
        };

        IFirebaseClient firebaseClient;

        public MainWindow()
        {
            //Startup
            InitializeComponent();
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
            int firebaseMajor = Utils.StringToInt(launcherData.major);
            int firebaseMinor = Utils.StringToInt(launcherData.minor);
            int firebasePatch = Utils.StringToInt(launcherData.patch);
            Uri.TryCreate(launcherData.url, UriKind.Absolute, out updateURL);         
            Version firebaseVersione = new Version(firebaseMajor, firebaseMinor, firebasePatch,0);
            firebaseVersion = firebaseVersione;
            Console.WriteLine(firebaseVersion);
            isEndVersionDownload = true;
            Console.WriteLine(isEndVersionDownload);
        }
        //Apka
        void CheckUpdate()
        {

            string currentVersion = Directory.GetCurrentDirectory() + "/meta.dbg";
            //current
            string currentRead = File.ReadAllText(currentVersion);
            Meta deserializedCurrentMeta = JsonConvert.DeserializeObject<Meta>(currentRead);
            string localVersion = deserializedCurrentMeta.version.ToString();
            versionlabel.Text = "Aktualna wersja: " + localVersion;
            //server
            serverversionlbl.Text = "Wersja na serwerze: " + firebaseVersion;

            if (firebaseVersion.Equals(deserializedCurrentMeta.version))
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

            INodeInfo node = client.GetNodeFromLink(updateURL);

            IProgress<double> progressHandler = new Progress<double>(x => lbl1.Text = "Pobieram: " + x + "%");
            await client.DownloadFileAsync(updateURL, node.Name, progressHandler);

            client.Logout();
        }
        void installUpdate()
        {

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
            string currentVersion = Directory.GetCurrentDirectory() + "/meta.dbg";
            Meta meta = new Meta();
            meta.version = new Version(0, 1, 0, 0);
            string write = JsonConvert.SerializeObject(meta);
            StreamWriter file = File.CreateText(currentVersion);
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
