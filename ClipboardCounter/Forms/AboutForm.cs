using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardCounter
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeveloperLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            developerLink.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.t.me/kiparis87");
        }

        private void GithubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            githubLink.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.github.com/kkostenkov/ClipboardCounter");
        }
    }
}
