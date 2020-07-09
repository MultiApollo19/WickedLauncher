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
using WickedHamsters;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace TestApp
{
    public partial class TestApp : Form
    {
        //public
        Version firebaseVersion;
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "MLQzJXkF14h6Z9iE5QcXUVauik4rQRHf3uHby4eO",
            BasePath = "https://wickedlauncher.firebaseio.com/"
        };

        IFirebaseClient firebaseClient;

        public TestApp()
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
            Version firebaseVersione = new Version(firebaseMajor, firebaseMinor, firebasePatch,0);
            while (firebaseVersione == null)
            {
                Console.WriteLine("Czekano!");
                await Task.Delay(25);
            }
            firebaseVersion = firebaseVersione;
            while (firebaseVersion == null)
            {
                Console.WriteLine("Czekano!");
                await Task.Delay(25);
            }

        }
        //Apka
        void CheckUpdate()
        {
            
            lbl1.Text = "Sprawdzam dostępność aktualizacji...";
            firebaseGetVersion();

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
                return;
            }
            else
            {
                lbl1.Text = "Znaleziono aktualizację!";
            }




        }
        void MakeNewJson()
        {
            string currentVersion = Directory.GetCurrentDirectory() + "/meta.dbg";
            Meta meta = new Meta();
            meta.version = new Version(0, 1, 0,0);
            string write = JsonConvert.SerializeObject(meta);
            StreamWriter file = File.CreateText(currentVersion);
            file.Write(write);
            file.Close();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            CheckUpdate();
        }

        private void TestApp_Load(object sender, EventArgs e)
        {

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
