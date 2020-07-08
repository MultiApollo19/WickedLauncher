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


namespace TestApp
{
    public partial class TestApp : Form
    {
        public TestApp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        void CheckUpdate()
        {
            
            lbl1.Text = "Sprawdzam dostępność aktualizacji...";
            string serverVersion = "https://doc-0c-5c-docs.googleusercontent.com/docs/securesc/nke845s47lsc0f9ldr2m9gapal47qm72/f8em3jk6bqf5cc310r3uvhfii1ppmcrv/1580497200000/11875819559820862250/11875819559820862250/1mUaw4m5ViMymKZn0HPlw9CxFRhCH31hP?e=download&gd=true&access_token=ya29.Il-8BzgEcMwRg-sqYFAYfuwvEpxD8i9WrESqAi0MIRs0NPHtXHIzxY3YDv610oB4dw2_jJ1etb8rdK5dYwUrpfCwxuCDFNcXGq3O8W8Z4OvIuBef-Cx4yY4nTNlDqkdLJg";
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
