using System;
using System.Windows.Forms;

namespace ClipboardCounter
{
    public partial class Form1 : Form
    {
        private ClipboardListener clipboardListener = new ClipboardListener();
        private TextProcessor textProcessor = new TextProcessor();
        private TextTranslator translator = new TextTranslator();
        private YaCloudTranslator cloudTranslator = new YaCloudTranslator();

        private bool fireNotifications = true;
        private bool alwaysOnTop = true;
        private Settings settings;

        public Form1()
        {
            InitializeComponent();
            // Add our form to the chain of clipboard viewers.
            clipboardListener.Subscribe(this.Handle);
            //textProcessor.SetNotificationTool(FireBalloonTip);
            fireNotificationsToolStripMenuItem.Checked = fireNotifications;
            fireNotificationsToolStripMenuItem1.Checked = fireNotifications;
            alwaysOnTopToolStripMenuItem.Checked = alwaysOnTop;
            alwaysOnTopToolStripMenuItem1.Checked = alwaysOnTop;
            this.TopMost = alwaysOnTop;
            settings = Program.Settings;
            SetMode(settings.Mode);
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            clipboardListener.Unsubscribe(this.Handle);
        }

        internal void FireBalloonTip(string text)
        {
            notifyIcon1.ShowBalloonTip(1000, "I say:", text, ToolTipIcon.None);
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);    // Process the message
            if (settings == null)
            {
                return;
            }

            if (m.Msg == ClipboardListener.WM_DRAWCLIPBOARD)
            {
                IDataObject iData = Clipboard.GetDataObject();      // Clipboard's data
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    string text = (string)iData.GetData(DataFormats.Text);      // Clipboard text
                    ProcessText(text);
                    return;
                }
            }
        }

        private void ProcessText(string text)
        {
            clipboardMirror.Text = text;
            string output;
            switch ( settings.Mode)
            {
                case Mode.Translator:
                case Mode.CloudTranslator:
                {
                    output = Translate(text);
                    break;
                }
                default:
                {
                    output = CountChars(text);
                    break;
                }       
            }
            
            if (fireNotifications)
            {
                FireBalloonTip(output);
            }
        }

        private string Translate(string text)
        {
            ITranslator translationTool = (settings.Mode == Mode.Translator) 
                ? (ITranslator)translator 
                : cloudTranslator;
            var translation = translationTool.Fetch(text);
            charsCountLabel.Text = translation;
            return translation;
        }

        private string CountChars(string text)
        {
            var charCount = textProcessor.CountChars(text);
            var stringCharCount = charCount.ToString();
            charsCountLabel.Text = $"Chars count: {stringCharCount}";
            return stringCharCount;
        }
        

        private void FireNotificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFireNotifications();
        }

        private void FireNotificationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToggleFireNotifications();
        }

        private void ToggleFireNotifications()
        {
            fireNotifications = !fireNotifications;
            fireNotificationsToolStripMenuItem.Checked = fireNotifications;
            fireNotificationsToolStripMenuItem1.Checked = fireNotifications;
        }

        private void AlwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleAlwaysOnTop();
        }

        private void AlwaysOnTopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToggleAlwaysOnTop();
        }

        private void ToggleAlwaysOnTop()
        {
            alwaysOnTop = !alwaysOnTop;
            this.TopMost = alwaysOnTop;
            alwaysOnTopToolStripMenuItem.Checked = alwaysOnTop;
            alwaysOnTopToolStripMenuItem1.Checked = alwaysOnTop;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void countToggle_Click(object sender, EventArgs e)
        {
            SetMode(Mode.Default);
        }

        private void translateToggle_Click(object sender, EventArgs e)
        {
            SetMode(Mode.Translator);
        }
        
        private void cloudTranslateToggle_Click(object sender, EventArgs e)
        {
            SetMode(Mode.CloudTranslator);
        }

        private void SetMode(Mode newMode)
        {
            ResetToggles();
            switch (newMode)
            {
                case Mode.Translator:
                {
                    translateModeToggle.Checked = true;
                    break;
                }
                case Mode.CloudTranslator:
                    cloudTranslateModeToggle.Checked = true;
                    break;
                case Mode.Default:
                default:
                {
                    countModeToggle.Checked = true;
                    break;
                }       
            }
            settings.Mode = newMode;
        }
        
        private void ResetToggles()
        {
            countModeToggle.Checked = false;
            translateModeToggle.Checked = false;
            cloudTranslateModeToggle.Checked = false;
        }

        private void WriteConfig_Click(object sender, EventArgs e)
        {
            settings.DumpToFile();
        }
    }
}
