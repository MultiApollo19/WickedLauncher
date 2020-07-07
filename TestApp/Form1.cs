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
            meta.url = "http://wickedlauncher.5v.pl/launcher/meta.dbg";
            string write = JsonConvert.SerializeObject(meta);
            StreamWriter file = File.CreateText(currentVersion);
            file.Write(write);
            file.Close();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            CheckUpdate();
        }

    }
    public class Meta{
        public Version version;
        public string url;
    }
}
