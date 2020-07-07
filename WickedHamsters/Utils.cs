using System;
using System.IO;
using System.Net;

namespace WickedHamsters
{
    public static class Utils
    {
        public static string GetTextFile(string url)
        {
            try
            {
                url = url.Trim();
                if (!url.ToLower().StartsWith("http")) url = "http://" + url;
                WebClient web_client = new WebClient();
                MemoryStream image_stream =
                    new MemoryStream(web_client.DownloadData(url));
                StreamReader reader = new StreamReader(image_stream);
                string result = reader.ReadToEnd();
                reader.Close();
                return result;
            }
            catch
            {
                return "";
            }
            
        }
        public static int StringToInt(string input)
        {
            int result = 0;
            if (Int32.TryParse(input, out int j))
            {
                result = j;
            }
            else
            {
                return 0;
            }
            
            return result;
        }
    }
}
