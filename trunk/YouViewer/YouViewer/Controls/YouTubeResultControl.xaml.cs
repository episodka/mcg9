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
        private SelectedMode currentMode;
        private bool dragMode = true;
        private YouTubeInfo info = null;

        public event SelectedEventHandler SelectedEvent;
        #endregion

        #region Ctor
        public YouTubeResultControl()
        {

            InitializeComponent();
            //Loaded
            this.Loaded += delegate
            {
                CurrentMode = SelectedMode.DRAG;
                imageMain.SetValue(DragCanvas.CanBeDraggedProperty, true);
            };
            //MouseEnter
            this.MouseEnter += delegate
            {
                if (CurrentMode == SelectedMode.PLAY)
                {
                    Storyboard sb = this.TryFindResource("OnMouseEnter") as Storyboard;
                    if (sb != null)
                        sb.Begin(this);
                }
            };
            //MouseLeave
            this.MouseLeave += delegate
            {
                if (CurrentMode == SelectedMode.PLAY)
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

        public SelectedMode CurrentMode
        {
            get {return currentMode;}
            set
            {
                currentMode = value;
                imageMain.SetValue(DragCanvas.CanBeDraggedProperty, dragMode);
                string title = "";
                switch (currentMode)
                {
                    case SelectedMode.DRAG:
                        title = "Drag Mode";
                        break;
                    case SelectedMode.PLAY:
                        title = "Play Mode";
                        break;
                    case SelectedMode.PLAYLIST:
                        title = "Playlist Mode";
                        break;
                    case SelectedMode.IDLE:
                        title = "";
                        break;
                    default:
                        title = "Bookmark Mode";
                        break;
                }
                lblDragMode.Content = title;
            }
        }
        
        #endregion

        #region Private Methods

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMode == SelectedMode.PLAY)
            {
                OnSelectedEvent(new YouTubeResultEventArgs(Info));
                string filename = YouViewerMainWindow.history_path + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month;
                DirectoryInfo di = new DirectoryInfo(filename);
                if (!di.Exists) di.Create();
                File.AppendAllText(filename + "\\" + DateTime.Now.Day + ".htr", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
            }
            else if (CurrentMode == SelectedMode.BOOKMARK)
            {
                string filename = YouViewerMainWindow.bookmark_path + "\\general.bmk";
                File.AppendAllText(filename,Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
            }
        }
        #endregion

        private void menuAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuPlayState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuAddPlaylist_Click(object sender, RoutedEventArgs e)
        {

        }


        private void menuSubFilmBookmark_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\film.bmk", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubMusicBookmark_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\music.bmk", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubEducationBookmark_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\education.bmk", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubTutorialBookmark_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\tutorial.bmk", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }

        private void menuSubOtherBookmark_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText(YouViewerMainWindow.bookmark_path + "\\other.bmk", DateTime.Now.TimeOfDay.TotalSeconds + Environment.NewLine + Info.LinkUrl + Environment.NewLine + Info.EmbedUrl + Environment.NewLine + Info.ThumbNailUrl + Environment.NewLine);
        }
       
    }
}
