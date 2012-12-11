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

        public YouViewerUploadWindow(YouTubeRequestSettings settings)
        {
            mYoutubeReqSettings = settings;
            InitializeComponent();
        }

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            btn_cancel.IsEnabled = true;

            uploadVideo = new Video();
            uploadVideo.Title = txt_title.Text.ToString();
            uploadVideo.Description = txt_description.Text.ToString();
            //uploadVideo.Tags.Add(new MediaCategory(cb_category.SelectedItem.ToString(), YouTubeNameTable.CategorySchema));
            uploadVideo.Keywords = txt_keywords.Text.ToString();
            if (cb_privacy.SelectedIndex == 1)
            {
                uploadVideo.Private = true;
            }
            else
            {
                uploadVideo.Private = false;
            }
            uploadVideo.YouTubeEntry.MediaSource = new MediaFileSource(txt_video_path.Text.ToString(), "video/quicktime");

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
                MessageBox.Show(re.Message);
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Upload completed!");
            btn_cancel.IsEnabled = false;
            btn_upload.IsEnabled = true;
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.InitialDirectory = "c:\\documents\\videos";
            ofd.RestoreDirectory = true;
            ofd.Filter = "video files (*.mp4)|*.mp4";
            if (ofd.ShowDialog() == true)
            {
                txt_video_path.Text = ofd.FileName;
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
