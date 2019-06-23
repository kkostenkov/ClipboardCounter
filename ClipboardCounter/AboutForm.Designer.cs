namespace ClipboardCounter
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.developerLabel = new System.Windows.Forms.Label();
            this.developerLink = new System.Windows.Forms.LinkLabel();
            this.srcLabel = new System.Windows.Forms.Label();
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(108, 87);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // developerLabel
            // 
            this.developerLabel.AutoSize = true;
            this.developerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.developerLabel.Location = new System.Drawing.Point(51, 27);
            this.developerLabel.Name = "developerLabel";
            this.developerLabel.Size = new System.Drawing.Size(81, 20);
            this.developerLabel.TabIndex = 1;
            this.developerLabel.Text = "Developer";
            // 
            // developerLink
            // 
            this.developerLink.AutoSize = true;
            this.developerLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.developerLink.LinkArea = new System.Windows.Forms.LinkArea(0, 10);
            this.developerLink.Location = new System.Drawing.Point(164, 27);
            this.developerLink.Name = "developerLink";
            this.developerLink.Size = new System.Drawing.Size(88, 20);
            this.developerLink.TabIndex = 2;
            this.developerLink.TabStop = true;
            this.developerLink.Text = "@kiparis87";
            this.developerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeveloperLink_LinkClicked);
            // 
            // srcLabel
            // 
            this.srcLabel.AutoSize = true;
            this.srcLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.srcLabel.Location = new System.Drawing.Point(51, 53);
            this.srcLabel.Name = "srcLabel";
            this.srcLabel.Size = new System.Drawing.Size(99, 20);
            this.srcLabel.TabIndex = 3;
            this.srcLabel.Text = "Source code";
            // 
            // githubLink
            // 
            this.githubLink.AutoSize = true;
            this.githubLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.githubLink.Location = new System.Drawing.Point(166, 53);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(57, 20);
            this.githubLink.TabIndex = 4;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "Github";
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLink_LinkClicked);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 121);
            this.Controls.Add(this.githubLink);
            this.Controls.Add(this.srcLabel);
            this.Controls.Add(this.developerLink);
            this.Controls.Add(this.developerLabel);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label developerLabel;
        private System.Windows.Forms.LinkLabel developerLink;
        private System.Windows.Forms.Label srcLabel;
        private System.Windows.Forms.LinkLabel githubLink;
    }
}