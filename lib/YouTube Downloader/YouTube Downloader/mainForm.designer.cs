namespace YouTube_Downloader
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.exit_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grop_Panel2 = new System.Windows.Forms.Panel();
            this.quality_ComboBox = new System.Windows.Forms.ComboBox();
            this.quality_Label = new System.Windows.Forms.Label();
            this.name_Label1 = new System.Windows.Forms.Label();
            this.video_PictureBox = new System.Windows.Forms.PictureBox();
            this.down_Button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logo_PictureBox = new System.Windows.Forms.PictureBox();
            this.url_TextBox = new System.Windows.Forms.TextBox();
            this.get_Button = new System.Windows.Forms.Button();
            this.url_Label = new System.Windows.Forms.Label();
            this.paste_Button = new System.Windows.Forms.Button();
            this.grop_Panel1 = new System.Windows.Forms.Panel();
            this.grop_Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.video_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Save the download file";
            // 
            // exit_Button
            // 
            this.exit_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.exit_Button.Location = new System.Drawing.Point(396, 135);
            this.exit_Button.Name = "exit_Button";
            this.exit_Button.Size = new System.Drawing.Size(80, 23);
            this.exit_Button.TabIndex = 2;
            this.exit_Button.Text = "Cancel";
            this.exit_Button.UseVisualStyleBackColor = true;
            this.exit_Button.Click += new System.EventHandler(this.exit_Button_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grop_Panel2
            // 
            this.grop_Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grop_Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grop_Panel2.Controls.Add(this.exit_Button);
            this.grop_Panel2.Controls.Add(this.quality_ComboBox);
            this.grop_Panel2.Controls.Add(this.quality_Label);
            this.grop_Panel2.Controls.Add(this.name_Label1);
            this.grop_Panel2.Controls.Add(this.video_PictureBox);
            this.grop_Panel2.Controls.Add(this.down_Button);
            this.grop_Panel2.Location = new System.Drawing.Point(0, 7);
            this.grop_Panel2.Name = "grop_Panel2";
            this.grop_Panel2.Padding = new System.Windows.Forms.Padding(6);
            this.grop_Panel2.Size = new System.Drawing.Size(514, 167);
            this.grop_Panel2.TabIndex = 1;
            this.grop_Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.grop_Panel2_Paint);
            // 
            // quality_ComboBox
            // 
            this.quality_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.quality_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quality_ComboBox.Enabled = false;
            this.quality_ComboBox.FormattingEnabled = true;
            this.quality_ComboBox.Location = new System.Drawing.Point(188, 49);
            this.quality_ComboBox.Name = "quality_ComboBox";
            this.quality_ComboBox.Size = new System.Drawing.Size(314, 21);
            this.quality_ComboBox.TabIndex = 3;
            this.quality_ComboBox.EnabledChanged += new System.EventHandler(this.quality_ComboBox_EnabledChanged);
            // 
            // quality_Label
            // 
            this.quality_Label.AutoEllipsis = true;
            this.quality_Label.AutoSize = true;
            this.quality_Label.ForeColor = System.Drawing.Color.DimGray;
            this.quality_Label.Location = new System.Drawing.Point(140, 52);
            this.quality_Label.Name = "quality_Label";
            this.quality_Label.Size = new System.Drawing.Size(42, 13);
            this.quality_Label.TabIndex = 2;
            this.quality_Label.Text = "Quality:";
            // 
            // name_Label1
            // 
            this.name_Label1.Location = new System.Drawing.Point(0, 0);
            this.name_Label1.Name = "name_Label1";
            this.name_Label1.Size = new System.Drawing.Size(100, 23);
            this.name_Label1.TabIndex = 4;
            // 
            // video_PictureBox
            // 
            this.video_PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.video_PictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.video_PictureBox.ErrorImage = global::YouTube_Downloader.Properties.Resources.Image_Error;
            this.video_PictureBox.InitialImage = global::YouTube_Downloader.Properties.Resources.Image_Wait;
            this.video_PictureBox.Location = new System.Drawing.Point(9, 9);
            this.video_PictureBox.Name = "video_PictureBox";
            this.video_PictureBox.Size = new System.Drawing.Size(125, 149);
            this.video_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.video_PictureBox.TabIndex = 11;
            this.video_PictureBox.TabStop = false;
            // 
            // down_Button
            // 
            this.down_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.down_Button.Enabled = false;
            this.down_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.down_Button.Location = new System.Drawing.Point(282, 135);
            this.down_Button.Name = "down_Button";
            this.down_Button.Size = new System.Drawing.Size(108, 23);
            this.down_Button.TabIndex = 4;
            this.down_Button.Text = "Loading...";
            this.down_Button.UseVisualStyleBackColor = true;
            this.down_Button.Click += new System.EventHandler(this.down_Button_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 4);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // logo_PictureBox
            // 
            this.logo_PictureBox.Location = new System.Drawing.Point(0, 0);
            this.logo_PictureBox.Name = "logo_PictureBox";
            this.logo_PictureBox.Size = new System.Drawing.Size(100, 50);
            this.logo_PictureBox.TabIndex = 5;
            this.logo_PictureBox.TabStop = false;
            // 
            // url_TextBox
            // 
            this.url_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.url_TextBox.Location = new System.Drawing.Point(9, 32);
            this.url_TextBox.Name = "url_TextBox";
            this.url_TextBox.Size = new System.Drawing.Size(309, 20);
            this.url_TextBox.TabIndex = 1;
            this.url_TextBox.TextChanged += new System.EventHandler(this.url_TextBox_TextChanged);
            this.url_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrl_KeyPress);
            // 
            // get_Button
            // 
            this.get_Button.Location = new System.Drawing.Point(0, 0);
            this.get_Button.Name = "get_Button";
            this.get_Button.Size = new System.Drawing.Size(75, 23);
            this.get_Button.TabIndex = 0;
            // 
            // url_Label
            // 
            this.url_Label.Location = new System.Drawing.Point(0, 0);
            this.url_Label.Name = "url_Label";
            this.url_Label.Size = new System.Drawing.Size(100, 23);
            this.url_Label.TabIndex = 0;
            // 
            // paste_Button
            // 
            this.paste_Button.Location = new System.Drawing.Point(0, 0);
            this.paste_Button.Name = "paste_Button";
            this.paste_Button.Size = new System.Drawing.Size(75, 23);
            this.paste_Button.TabIndex = 0;
            // 
            // grop_Panel1
            // 
            this.grop_Panel1.Location = new System.Drawing.Point(0, 0);
            this.grop_Panel1.Name = "grop_Panel1";
            this.grop_Panel1.Size = new System.Drawing.Size(200, 100);
            this.grop_Panel1.TabIndex = 6;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(514, 172);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.grop_Panel2);
            this.Controls.Add(this.logo_PictureBox);
            this.Controls.Add(this.grop_Panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube Downloader";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.grop_Panel2.ResumeLayout(false);
            this.grop_Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.video_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo_PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button exit_Button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel grop_Panel2;
        private System.Windows.Forms.ComboBox quality_ComboBox;
        private System.Windows.Forms.Label quality_Label;
        private System.Windows.Forms.Label name_Label1;
        private System.Windows.Forms.PictureBox video_PictureBox;
        private System.Windows.Forms.Button down_Button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox logo_PictureBox;
        private System.Windows.Forms.TextBox url_TextBox;
        private System.Windows.Forms.Button get_Button;
        private System.Windows.Forms.Label url_Label;
        private System.Windows.Forms.Button paste_Button;
        private System.Windows.Forms.Panel grop_Panel1;
    }
}