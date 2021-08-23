using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using WickedHamsters;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace TestApp
{
    public partial class GameLauncher : Form
    {
        //Flagi
        bool isDebug = false;
        bool isBeta = false;

        IFirebaseConfig config = new FirebaseConfig { 
            AuthSecret= "MLQzJXkF14h6Z9iE5QcXUVauik4rQRHf3uHby4eO",
            BasePath= "https://wickedlauncher.firebaseio.com/"
        };

        IFirebaseClient client;

        public GameLauncher()
        {
            InitializeComponent();
            setupApp();
            FirebaseConnectionSetup();
            //setupFirebase();
            
        }
        void setupApp() {
            if (isDebug)
            {
                serverversionlbl.Visible = true;
                firestatus.Visible = true;
                versionlabel.Visible = true;
            }
            else {
                serverversionlbl.Visible = false;
                firestatus.Visible = false;
                versionlabel.Visible = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
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
        async void CheckUpdate()
        {
            
            lbl1.Text = "Sprawdzam dostępność aktualizacji...";           
            string currentVersion = Directory.GetCurrentDirectory() + "/meta.dbg";
            //current
            string currentRead = File.ReadAllText(currentVersion);
            AppMeta deserializedCurrentMeta = JsonConvert.DeserializeObject<AppMeta>(currentRead);
            string localVersion = deserializedCurrentMeta.version.ToString();
            versionlabel.Text = "Aktualna wersja: " + localVersion;
            //server
            if (isBeta)
            {
                FirebaseResponse response = await client.GetAsync("appMeta");
                AppMeta appMeta = response.ResultAs<AppMeta>();
                Console.WriteLine("Otrzymane dane: " + appMeta.url + " " + appMeta.version);
                serverversionlbl.Text = "Wersja na serwerze: " + appMeta.version;

                if (appMeta.version.Equals(deserializedCurrentMeta.version))
                {
                    lbl1.Text = "Posiadasz aktualna wersję!";
                }
                else
                {
                    lbl1.Text = "Znaleziono aktualizację!";
                }
            }
            else {
                FirebaseResponse response = await client.GetAsync("appMeta");
                AppMeta appMeta = response.ResultAs<AppMeta>();
                Console.WriteLine("Otrzymane dane: " + appMeta.url + " " + appMeta.version);
                serverversionlbl.Text = "Wersja na serwerze: " + appMeta.version;

                if (appMeta.version.Equals(deserializedCurrentMeta.version))
                {
                    lbl1.Text = "Posiadasz aktualna wersję!";
                }
                else
                {
                    lbl1.Text = "Znaleziono aktualizację!";
                }
            }         



        }
        void MakeNewJson()
        {
            string currentVersion = Directory.GetCurrentDirectory() + "/meta.dbg";
            AppMeta meta = new AppMeta();
            meta.version = new Version("0.1.0.0");
            meta.url = "https://doc-14-5c-docs.googleusercontent.com/docs/securesc/nke845s47lsc0f9ldr2m9gapal47qm72/65rvtprl8nsrdqo6dfsvj16fhgtcitc2/1580496300000/11875819559820862250/11875819559820862250/1SWwA0cFmE5q97ky2q2EUroe8dzlG3ukE?e=download&gd=true&access_token=ya29.Il-8BzgEcMwRg-sqYFAYfuwvEpxD8i9WrESqAi0MIRs0NPHtXHIzxY3YDv610oB4dw2_jJ1etb8rdK5dYwUrpfCwxuCDFNcXGq3O8W8Z4OvIuBef-Cx4yY4nTNlDqkdLJg";
            string write = JsonConvert.SerializeObject(meta);
            StreamWriter file = File.CreateText(currentVersion);
            file.Write(write);
            file.Close();
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
            CheckUpdate();
        }

    }
    public class AppMeta{
        public Version version;
        public string url;
    }
}
