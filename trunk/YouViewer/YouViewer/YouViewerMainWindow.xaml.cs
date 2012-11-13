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
using System.Runtime.Caching;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace YouViewer
{
    /// <summary>
    /// Hosts a WPF.JoshSmith.Controls.DragCanvas which
    /// in turn holds n-many YouTubeResultControls
    /// </summary>
    public partial class YouViewerMainWindow : Window
    {
        #region Data
        private Random rand = new Random(50);
        #endregion

        #region Ctor
        public static string app_path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string bookmark_path = app_path + "\\bookmark";
        public static string history_path = app_path + "\\history";
        public static string image_path = app_path + "\\images";
        public YouViewerMainWindow()
        {
            InitializeComponent();
            viewer.ClosedEvent += viewer_ClosedEvent;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Raised when Viewer is closed
        /// </summary>
        private void viewer_ClosedEvent(object sender, EventArgs e)
        {
            dragCanvas.Opacity = 1.0;
        }

        /// <summary>
        /// Create a new YouTubeResultControl for each YouTubeInfo in input list
        /// </summary>
        private void PopulateCanvas(List<YouTubeInfo> infos, bool ordered, bool AtoZ = true)
        {
            // Tung's result scrollviewer
            for (int i = 0; i < infos.Count; i++)
            {
                YouTubeResultControl control = new YouTubeResultControl { Info = infos[i] };
                lbResult.Items.Add(control);
            }
            //EndofTung's scrollviewer
            //-----------------------------------------
            //dragCanvas.Children.Clear();
            //int _index = AtoZ ? -1 : 1;
            //int _limit = AtoZ ? 0 : infos.Count - 1;
            //for (int i = 0; i < infos.Count; i++)
            //{
            //    YouTubeResultControl control = new YouTubeResultControl { Info = infos[_limit - i * _index] };
            //    // Xoay goc Canvas
            //    //int angleMutiplier = i % 2 == 0 ? 1 : -1;
            //    //control.RenderTransform = new RotateTransform { Angle = GetRandom(30, angleMutiplier) };

            //    control.SetValue(Canvas.LeftProperty, 0.0);
            //    control.SetValue(Canvas.TopProperty, GetRandomDist(dragCanvas.ActualHeight - 150.0));
            //    control.lblDragMode.Content = infos[_limit - i * _index].Title;
            //    control.lblDescription.Content = infos[_limit - i * _index].Description;
            //    control.SelectedEvent += control_SelectedEvent;
            //    dragCanvas.Children.Add(control);
            //}
        }

        /// <summary>
        /// Raised when a new YouTubeResultControl is selected
        /// </summary>
        private void control_SelectedEvent(object sender, YouTubeResultEventArgs e)
        {
            dragCanvas.Opacity = 0.6;
            viewer.VideoUrl = e.Info.EmbedUrl;
        }

        /// <summary>
        /// Get a new random number between 0.0 and 1.0 which is mutltiplied by
        /// limit and angleMutiplier
        /// </summary>
        private int GetRandom(double limit, int angleMutiplier)
        {
            return (int)((rand.NextDouble() * limit) * angleMutiplier);
        }

        /// <summary>
        /// Get a new random number between 0.0 and 1.0 which is mutltiplied by
        /// limit
        /// </summary>
        private double GetRandomDist(double limit)
        {
            return rand.NextDouble() * limit;
        }

        /// <summary>
        /// Do a key word search
        /// </summary>
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtKeyWord.Text != string.Empty)
                {
                    List<YouTubeInfo> infos = YouTubeProvider.LoadVideosKey(txtKeyWord.Text);
                    PopulateCanvas(infos, false);
                    ObjectCache cache = MemoryCache.Default;
                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(10.0);
                    cache.Add("lastSearchResults", infos, policy, null);
                }
                else
                {
                    MessageBox.Show("you need to enter a search word", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #region Drag Methods
        /// <summary>
        /// Toggles the WPF.JoshSmith.Controls.DragCanvas.SetCanBeDragged
        /// property for all the YouTubeResultControl controls within the
        /// WPF.JoshSmith.Controls.DragCanvas
        /// </summary>
        #endregion


        #endregion

        private void menuLoadCache_Click(object sender, RoutedEventArgs e)
        {
            ObjectCache cache = MemoryCache.Default;
            List<YouTubeInfo> cached = cache["lastSearchResults"] as List<YouTubeInfo>;
            string msg = "Loading Cache: ";
            if (cached == null) msg += " NULL";
            else if (cached.Count > 0) msg += cached.Count + " elements";
            else msg += " 0 element";
            MessageBox.Show(msg);
            if (cached != null) PopulateCanvas(cached, false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DirectoryInfo di = new DirectoryInfo(bookmark_path);
            if (!di.Exists) di.Create();
            DirectoryInfo dii = new DirectoryInfo(history_path);
            if (!dii.Exists) dii.Create();
            DirectoryInfo diii = new DirectoryInfo(image_path);
            if (!diii.Exists) diii.Create();



            /*
            ObjectCache cache = MemoryCache.Default;
            List<YouTubeInfo> cached = cache["lastSearchResults"] as List<YouTubeInfo>;
            string msg = "Loading Cache: ";
            if (cached == null) msg += " NULL";
            else if (cached.Count > 0) msg += cached.Count + " elements";
            else msg += " 0 element";
            //  MessageBox.Show(msg);
            if (cached != null) PopulateCanvas(cached, false);*/
        }


        private void btnHistoryView_Click(object sender, RoutedEventArgs e)
        {

            string filename = YouViewerMainWindow.history_path + "\\" + DateTime.Now.Year + "-" + DateTime.Now.Month;
            DirectoryInfo di = new DirectoryInfo(filename);
            if (!di.Exists) di.Create();
            string[] A = Directory.GetFiles(filename, "*.htr", SearchOption.AllDirectories);
            List<YouTubeInfo> temp = new List<YouTubeInfo>();
            for (int i = 0; i < A.Length; i++)
            {
                temp.AddRange(readBookMarkFile(A[i]));
            }
            PopulateCanvas(temp, true, false);
        }

        public static List<YouTubeInfo> readBookMarkFile(string filename)
        {
            List<YouTubeInfo> infoHistory = new List<YouTubeInfo>();
            infoHistory.Add(new YouTubeInfo());
            infoHistory.Clear();
            FileInfo file = new FileInfo(filename);
            if (!file.Exists) return infoHistory;
            StreamReader reader = file.OpenText();

            
            string time = reader.ReadLine();
            string title = reader.ReadLine();
            string des = reader.ReadLine();
            string link = reader.ReadLine();
            string embed = reader.ReadLine();
            string thumb = reader.ReadLine();
            while (link != null && embed != null && thumb != null)
            {
                YouTubeInfo yInfo = new YouTubeInfo();
                DateTime addedtime = (new DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(double.Parse(time));
                yInfo.CachedTime = addedtime.ToShortDateString() + " "+ addedtime.ToShortTimeString();
                yInfo.Title = title;
                yInfo.Description = des;
                yInfo.LinkUrl = link;
                yInfo.EmbedUrl = embed;
                yInfo.ThumbNailUrl = thumb;
                infoHistory.Add(yInfo);
                time = reader.ReadLine();
                title = reader.ReadLine();
                des = reader.ReadLine();
                link = reader.ReadLine();
                embed = reader.ReadLine();
                thumb = reader.ReadLine();
            }
            return infoHistory;
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            List<YouTubeInfo> list = new List<YouTubeInfo>();
            PopulateCanvas(list, false);
        }

        private void btnMode_Click(object sender, RoutedEventArgs e)
        {
            if (dragCanvas.Children.Count == 0) return;
            bool canBeDragged = WPF.JoshSmith.Controls.DragCanvas.GetCanBeDragged(dragCanvas.Children[0]);
            canBeDragged = !canBeDragged;
            this.btnMode.Content = canBeDragged ? "Play Mode" : "Drag Mode";
            foreach (YouTubeResultControl child in dragCanvas.Children)
            {
                WPF.JoshSmith.Controls.DragCanvas.SetCanBeDragged(child, canBeDragged);
                child.DragMode = canBeDragged;
            }
        }

        private void cbmBookmark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<YouTubeInfo> list = new List<YouTubeInfo>();
            switch (this.cbmBookmark.SelectedIndex)
            {
                case -1: return;
                case 0:
                    list = readBookMarkFile(bookmark_path + "\\film.bmk");
                    break;
                case 1:
                    list = readBookMarkFile(bookmark_path + "\\music.bmk");
                    break;
                case 2:
                    list = readBookMarkFile(bookmark_path + "\\tutorial.bmk");
                    break;
                case 3:
                    list = readBookMarkFile(bookmark_path + "\\education.bmk");
                    break;
                default:
                    list = readBookMarkFile(bookmark_path + "\\other.bmk");
                    break;
            }
            PopulateCanvas(list, true);
        }

        private void btnClrAll_Click(object sender, RoutedEventArgs e)
        {
            CustomView.AlertBox alert = new CustomView.AlertBox();
            alert.Message = "Are you sure you want to delete all saved data:" + Environment.NewLine + "Bookmark, History?";
            alert.ShowDialog();
            if (!alert.ResultButton) return;
            DirectoryInfo dirBookMark = new DirectoryInfo(bookmark_path);
            if (dirBookMark.Exists)
            {
                dirBookMark.Delete(true);
            }
            DirectoryInfo dirHistory = new DirectoryInfo(history_path);
            if (dirHistory.Exists)
            {
                dirHistory.Delete(true);
            }
            DirectoryInfo dirImage = new DirectoryInfo(image_path);
            if (dirImage.Exists)
            {
                dirImage.Delete(true);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
