using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Auth;
using WickedHamsters;
namespace Uploader
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                Uploader();
            }).GetAwaiter().GetResult();


            Console.ReadLine();
        }
        static async void Uploader() {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            { 
                AuthSecret = "MLQzJXkF14h6Z9iE5QcXUVauik4rQRHf3uHby4eO",
                BasePath = "https://wickedlauncher.firebaseio.com/"
            };

            IFirebaseClient _client;

            _client = new FireSharp.FirebaseClient(config);

            AppMeta appMeta = new AppMeta();
            int typ, branch, gameID, major, minor, build, revision;
            string typS = null, branchS = null,database = "appMetaDevelopment";
            Console.WriteLine("Uploader 0.1 Launcher: launcher.zip Gra: game.zip");
            Console.WriteLine("Wybierz co chcesz wrzucić: 1. launcher");
            typ = WickedHamsters.Utils.StringToInt(Console.ReadLine());
            Console.WriteLine("Wybierz linie: 1. prod 2. dev");
            branch = WickedHamsters.Utils.StringToInt(Console.ReadLine());

            if (typ == 1)
            {
                var stream = File.Open(Directory.GetCurrentDirectory() + "/launcher.zip", FileMode.Open);

                typS = "launcher";
                if (branch == 1)
                {
                    Console.WriteLine("Łącze z firebase: Launcher Produkcyjny");
                    branchS = "production";
                    database = "appMetaProduction";
                }
                else if (branch == 2)
                {
                    Console.WriteLine("Łącze z firebase: Launcher Developerski");
                    branchS = "dev";
                    database = "appMetaDevelopment";
                }
                else
                {
                    branchS = "dev";
                }

                /*FirebaseResponse responseDownload = await _client.GetAsync("appMetaDevelopment");
                AppMeta appMetaDownload = responseDownload.ResultAs<AppMeta>();*/

                Console.WriteLine("Teraz linia po lini wpisuj następujące wartości: major,minor,build,revision" + " aktualna wersja na serwerze to: ");//+ appMetaDownload.version);
                major = WickedHamsters.Utils.StringToInt(Console.ReadLine());
                minor = WickedHamsters.Utils.StringToInt(Console.ReadLine());
                build = WickedHamsters.Utils.StringToInt(Console.ReadLine());
                revision = WickedHamsters.Utils.StringToInt(Console.ReadLine());
                Version appversion = new Version(major, minor, build, revision);
                appMeta.version = appversion;

                var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig("AIzaSyDRVJ4yVaJckPBDtENZz2pWtRW8t2ax4uk"));
                var a = await auth.SignInWithEmailAndPasswordAsync("uploader@upload.net", "123456");

                var task = new FirebaseStorage(
                    "wickedlauncher.appspot.com",
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true,
                    })
                .Child("wickedlauncher")
                .Child(typS)
                .Child(branchS)
                .Child("launcher.zip")
                .PutAsync(stream);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Postęp uploadu: {e.Percentage} %");
                var downloadUrl = await task;
                appMeta.url = downloadUrl;
                Console.WriteLine("Plik dostępny pod adresem: " + downloadUrl + " rozpoczynam aktualizację bazy.");

                FirebaseResponse response = await _client.UpdateAsync(database, appMeta);
                Console.WriteLine("Zaktualizowano dane w bazie");
            }
        }
    }
    class AppMeta{
        public string url;
        public Version version;
    }
}
