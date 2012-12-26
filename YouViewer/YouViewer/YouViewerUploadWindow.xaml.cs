using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

using Google.GData.YouTube;
using Google.YouTube;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;

namespace YouViewer
{
    /// <summary>
    /// Interaction logic for YouViewerUploadWindow.xaml
    /// </summary>
    public partial class YouViewerUploadWindow : Window
    {
        private YouTubeRequestSettings mYoutubeReqSettings;
        BackgroundWorker bw;
        Video uploadVideo;

        public YouViewerUploadWindow(String username, String password)
        {
            mYoutubeReqSettings = new YouTubeRequestSettings("YouViewer", MainWindow.DEV_KEY, username, password);
            InitializeComponent();
        }

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            btn_upload.IsEnabled = false;
            btn_upload.Content = "Uploading";
            btn_cancel.IsEnabled = true;

            uploadVideo = new Video();
            uploadVideo.Title = txt_title.Text.ToString();
            uploadVideo.Description = txt_description.Text.ToString();
            uploadVideo.Tags.Add(new MediaCategory("Autos", YouTubeNameTable.CategorySchema));
            uploadVideo.Keywords = txt_keywords.Text.ToString();
            if (cb_privacy.SelectedIndex == 1)
            {
                uploadVideo.Private = true;
            }
            else
            {
                uploadVideo.Private = false;
            }
            uploadVideo.YouTubeEntry.MediaSource = new MediaFileSource(txt_video_path.Text.ToString(), getMimeType(txt_video_path.Text.ToString()));

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            YouTubeRequest req = new YouTubeRequest(mYoutubeReqSettings);
            try
            {
                req.Upload(uploadVideo);
            }
            catch (Google.GData.Client.GDataRequestException re)
            {
                MessageBox.Show("Youtube server is not avaiable!");
                e.Result = false;
            }
            e.Result = true;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
            {
                MessageBox.Show("Upload completed!");
            }
            btn_upload.Content = "Upload";
            btn_cancel.IsEnabled = false;
            btn_upload.IsEnabled = true;
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.InitialDirectory = "c:\\documents\\videos";
            ofd.RestoreDirectory = true;
            ofd.Filter = "(*.mp4)|*.mp4|(*.flv)|*.flv|(*.avi)|*.avi|(*.3gp)|*.3gp|(*.mov)|*.mov";
            if (ofd.ShowDialog() == true)
            {
                txt_video_path.Text = ofd.FileName;
            }
        }

        private String getMimeType(String videoPath)
        {
            string[] title = videoPath.Split('\\');
            int i = title.GetUpperBound(0);
            string temp = title[i];
            string[] title1 = temp.Split('.');
            String filetype = title1[1];
            switch (filetype)
            {
                case "flv": return "video/x-flv";
                case "avi": return "video/avi";
                case "3gp": return "video/3gpp";
                case "mov": return"video/quicktime";
                default: return "video/quicktime";
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
        }
    }
}
