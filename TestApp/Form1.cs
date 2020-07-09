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
            MakeNewJson();

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
            FirebaseResponse firebaseResponse = await firebaseClient.GetAsync("app/version");
            string firebaseVersionString = firebaseResponse.ResultAs<string>();
            Console.WriteLine(firebaseVersionString);
            int firebaseVersionInt = WickedHamsters.Utils.StringToInt(firebaseVersionString);
            Console.WriteLine("Wersja int: " + firebaseVersionInt);
            double firebaseVersionDouble = Convert.ToDouble(firebaseVersionString);
            Console.WriteLine("Wersja double: " + firebaseVersionDouble);
        }
        //Apka
        void CheckUpdate()
        {
            firebaseGetVersion();
            lbl1.Text = "Sprawdzam dostępność aktualizacji...";
            string serverVersion = "http://wickedlauncher.5v.pl/launcher/meta.dbg";
            string currentVersion = Directory.GetCurrentDirectory() + "/meta.dbg";
            //current
            string currentRead = File.ReadAllText(currentVersion);
            Meta deserializedCurrentMeta = JsonConvert.DeserializeObject<Meta>(currentRead);
            string localVersion = deserializedCurrentMeta.version.ToString();
            versionlabel.Text = "Aktualna wersja: " + localVersion;
            //server

            string serverRead = WickedHamsters.Utils.GetTextFile(serverVersion);
            Meta deserializedServerMeta = JsonConvert.DeserializeObject<Meta>(serverRead);
            string serverVersionn = deserializedServerMeta.version.ToString();
            serverversionlbl.Text = "Wersja na serwerze: " + serverVersionn;

            if (deserializedServerMeta.version.Equals(deserializedCurrentMeta.version))
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
            meta.version = 0100;
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
        public int version;
    }
}
