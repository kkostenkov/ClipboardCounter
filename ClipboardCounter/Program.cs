using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardCounter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadSettings();
            ParseMode(Environment.GetCommandLineArgs());
            
            Application.Run(new Form1());
        }

        public static Settings Settings; 

        private static void LoadSettings()
        {
            Settings = Settings.LoadSettings();
        }

        private static void ParseMode(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (string.Equals("--help", arg))
                {
                    Console.WriteLine("use --translate key to run in translate mode");
                }
                else if (string.Equals("--translate", arg))
                {
                    Settings.Mode = Mode.Translator;
                }
            }
        }
    }
}
