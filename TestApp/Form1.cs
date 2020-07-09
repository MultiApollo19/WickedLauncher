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
{//HEHEHEHEHEHEHEHEHEHHEHEHEHE
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
            InitializeComponent();
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
            string firebaseVersion = firebaseResponse.ResultAs<string>();
            Console.WriteLine(firebaseVersion);
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
            meta.version = new Version("0.1.0.0");
            meta.url = "https://doc-14-5c-docs.googleusercontent.com/docs/securesc/nke845s47lsc0f9ldr2m9gapal47qm72/65rvtprl8nsrdqo6dfsvj16fhgtcitc2/1580496300000/11875819559820862250/11875819559820862250/1SWwA0cFmE5q97ky2q2EUroe8dzlG3ukE?e=download&gd=true&access_token=ya29.Il-8BzgEcMwRg-sqYFAYfuwvEpxD8i9WrESqAi0MIRs0NPHtXHIzxY3YDv610oB4dw2_jJ1etb8rdK5dYwUrpfCwxuCDFNcXGq3O8W8Z4OvIuBef-Cx4yY4nTNlDqkdLJg";
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
        public string url;
    }
}
