using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardCounter
{
    public partial class Form1 : Form
    {
        private ClipboardListener clipboardListener = new ClipboardListener();
        private TextProcessor textProcessor = new TextProcessor();

        private bool fireNotifications = true;

        public Form1()
        {
            InitializeComponent();
            // Add our form to the chain of clipboard viewers.
            clipboardListener.Subscribe(this.Handle);
            //textProcessor.SetNotificationTool(FireBalloonTip);
            fireNotificationsToolStripMenuItem.Checked = fireNotifications;
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FireNotificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fireNotifications = !fireNotifications;
            fireNotificationsToolStripMenuItem.Checked = fireNotifications;
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
            var charCount = textProcessor.CountChars(text);
            var stringCharCount = charCount.ToString();
            charsCountLabel.Text = $"Chars count: {stringCharCount}";
            if (fireNotifications)
            {
                FireBalloonTip(stringCharCount);
            }
        }

    }
}
