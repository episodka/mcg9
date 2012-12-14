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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using WPF.JoshSmith.Controls;

namespace YouViewer
{

    public enum SelectedMode { DRAG, PLAY, PLAYLIST, BOOKMARK, IDLE };
    /// <summary>
    /// Event delegate
    /// </summary>
    public delegate void SelectedEventHandler(object sender, YouTubeResultEventArgs e);


    /// <summary>
    /// Shows a single image, and when Mouse is over and the mode is
    /// not in drag mode, then show a play button, which when
    /// clicked will notify the YouViewerMainWindow to show a
    /// Viewer control
    /// </summary>
    public partial class YouTubeResultControl : UserControl
    {
        #region Data
        private bool dragMode = true;
        private YouTubeInfo info = null;

        public event SelectedEventHandler SelectedEvent;
        #endregion

        #region Ctor
        public YouTubeResultControl()
        {

            InitializeComponent();
            this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "/back_ipad.png")));
            //Loaded
            this.Loaded += delegate
            {
                DragMode = true;
                imageMain.SetValue(DragCanvas.CanBeDraggedProperty, true);
            };
            //MouseEnter
            this.MouseEnter += delegate
            {
                if (!DragMode)
                {
                    Storyboard sb = this.TryFindResource("OnMouseEnter") as Storyboard;
                    if (sb != null)
                        sb.Begin(this);
                }
            };
            //MouseLeave
            this.MouseLeave += delegate
            {
                if (!DragMode)
                {
                    Storyboard sb = this.TryFindResource("OnMouseLeave") as Storyboard;
                    if (sb != null)
                        sb.Begin(this);
                }
            };
        }
        #endregion

        #region Events
        /// <summary>
        /// Raised when this control btnPlay is clicked
        /// </summary>
        protected virtual void OnSelectedEvent(YouTubeResultEventArgs e) 
        {
            if (SelectedEvent != null) 
            {
                //Invokes the delegates.
                SelectedEvent(this, e); 
            }
        }
        #endregion

        #region Properties

        private string ImageUrl
        {
            set
            {
             //   String photolocation = YouViewerMainWindow.image_path + "\\" + YouViewerMainWindow.CalculateMD5Hash(Info.LinkUrl) + ".bmp";  //file name 
             //   FileInfo file = new FileInfo(photolocation);
              //  if (!file.Exists)
             //   {
                    BitmapImage bmp = new BitmapImage(new Uri(value, UriKind.RelativeOrAbsolute));
                //    FileStream stream = new FileStream(photolocation, FileMode.Create);
               //     TiffBitmapEncoder encoder = new TiffBitmapEncoder();
               //     encoder.Frames.Add(BitmapFrame.Create(bmp));
               //     encoder.Save(stream);
                    imageMain.Source = bmp;
              /*  }
                else
                {
                    ImageSource imageSource = new BitmapImage(new Uri(photolocation));
                    imageMain.Source = imageSource;
                }*/
            }
        }


        public YouTubeInfo Info
        {
            get { return info; }
            set
            {
                info = value;
                ImageUrl = info.ThumbNailUrl;
            }
        }
        public bool DragMode
        {
            get { return dragMode; }
            set
            {
                dragMode = value;
                imageMain.SetValue(DragCanvas.CanBeDraggedProperty, dragMode);
            }
        }

        public string Description
        {
            set 
            {
                lblDragMode.Content = value;
            }
        }

        #endregion

        #region Private Methods

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (!DragMode)
            {
                OnSelectedEvent(new YouTubeResultEventArgs(Info));
                string filename = YouViewerMainWindow.history_path + "\\" + DateTime.Now.Year + "-" + DateTime.Now.Month;
                DirectoryInfo di = new DirectoryInfo(filename);
                if (!di.Exists) di.Create();
                double time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                File.AppendAllText(filename + "\\" + DateTime.Now.Day + ".htr", time + Environment.NewLine + Info.Title + Environment.NewLine + Info.Description + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
            }
        }
        #endregion


        private void menuPlayState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuAddPlaylist_Click(object sender, RoutedEventArgs e)
        {

        }


        private void menuSubFilmBookmark_Click(object sender, RoutedEventArgs e)
        {
            double time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\Film.bmk", time + Environment.NewLine + Info.Title + Environment.NewLine + Info.Description + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubMusicBookmark_Click(object sender, RoutedEventArgs e)
        {
            double time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\Music.bmk", time + Environment.NewLine + Info.Title + Environment.NewLine + Info.Description + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubEducationBookmark_Click(object sender, RoutedEventArgs e)
        {
            double time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\Education.bmk", time + Environment.NewLine + Info.Title + Environment.NewLine + Info.Description + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubTutorialBookmark_Click(object sender, RoutedEventArgs e)
        {
            double time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\Tutorial.bmk", time + Environment.NewLine + Info.Title + Environment.NewLine + Info.Description + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubOtherBookmark_Click(object sender, RoutedEventArgs e)
        {
            double time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\Other.bmk", time + Environment.NewLine + Info.Title + Environment.NewLine + Info.Description + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubNewBookmark_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new YouViewer.CustomView.InputDialog();
            dialog.ShowDialog();
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\"+dialog.valueText+".bmk", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }
       
    }
}
