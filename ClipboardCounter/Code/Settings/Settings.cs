using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ClipboardCounter
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Settings
    {
        [JsonProperty]
        public Mode Mode;
        [JsonProperty]
        public string SourceLanguageCode = "de";
        [JsonProperty]
        public string TargetLanguageCode = "en";
        [JsonProperty]
        public string YandexKey;
        [JsonProperty]
        public string YandexCloudApiKey;



        private const string CONFIG_NAME = "config.json";
        private const string LOG_NAME = "errors.log";

        private static string AppDirectory
        {
            get
            {
                var execPath = Application.ExecutablePath;
                var filename = Path.GetFileName(execPath);
                var end = execPath.Length - filename.Length;
                var dirPath = execPath.Substring(0, end);
                return dirPath;
            }
        }

        private static string ConfigPath => Path.Combine(AppDirectory, CONFIG_NAME);
        private static string LogPath => Path.Combine(AppDirectory, LOG_NAME);

        public static Settings LoadSettings()
        {
            var path = ConfigPath;
            string text = null;
            try
            {
                text = File.ReadAllText(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Config file not found");
            }

            if (text != null)
            {
                try
                {
                    var settings = JsonConvert.DeserializeObject<Settings>(text);
                    return settings;
                }
                catch (ArgumentException exc)
                {
                    WriteLog("Bad json");
                }
            }
            
            return new Settings();
        }

        public void DumpToFile()
        {
            var dump = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(ConfigPath, dump);
        }

        public static void WriteLog(string entry)
        {
            Console.WriteLine(entry);
            using (var writer = File.AppendText(LogPath))
            {
                writer.WriteLine(entry);
                writer.Flush();
            }
        }
    }
}