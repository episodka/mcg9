using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
using K54csYoutubeProvider;
using System.Diagnostics;
using System.Collections.ObjectModel;
using YouTubeDownloader;

namespace YouViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string searchQuery = string.Empty;
        private static string APP_PATH = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string APP_NAME = "iTube";
        private static string DEV_KEY = "AI39si5aMxqRQl5eSGRz7rfIhgk5BxXxSZ99xusMFiBD3ONuPMAytuj3uSS6R5N8tTUzYBGI4L3yipWF454lUA6MO7obxXhbdQ";
        private static string LOGIN_TOKEN;

        private static string USERNAME;
        private static string PASSWORD;

        private VideoBase currentVideo;
        private ImageSource defaultAvatar;


        private List<VideoBase> datasource;
        private List<VideoBase> relatedList;
        private List<List<VideoBase>> playlists;
        public  frmYouTubeDownloader yyDownloader;

        private K54csYoutubeProvider.YoutubeProvider yProvider;
        string videoUrl = "";
        private static Boolean isLoggedIn = false;


        public MainWindow()
        {
            InitializeComponent();
            relatedList = new List<VideoBase>();
            USERNAME = "";
            PASSWORD = "";
         
        }
        public MainWindow(string username, string password)
        {
            InitializeComponent();
            relatedList = new List<VideoBase>();
            USERNAME = username;
            PASSWORD = password;
            this.lbResult.ItemsSource = datasource;
            this.horizontalListBox.ItemsSource = relatedList;

        }
        public MainWindow(string query)
        {
            InitializeComponent();
            relatedList = new List<VideoBase>();       
            this.searchQuery = query;
            txtKeyWord.Text = query;
            List<VideoBase> listVideos = K54csYoutubeProvider.YoutubeProvider.searchByKeyWord(this.searchQuery);
            datasource = listVideos;
            if (datasource.Count > 0)
            {
                this.lbResult.ItemsSource = datasource;
                this.lbResult.SelectedIndex = 0;
                currentVideo = datasource[0];
                relatedList = K54csYoutubeProvider.YoutubeProvider.GetRelatedVideos(currentVideo);
                this.horizontalListBox.ItemsSource = relatedList;
            }
        }

        #region normal click event (close, mimimize, move)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
        /// <summary>
        /// Do a key word search
        /// </summary>
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtKeyWord.Text != string.Empty)
                    {
                        List<VideoBase> listVideos = K54csYoutubeProvider.YoutubeProvider.searchByKeyWord(txtKeyWord.Text);
                        if (listVideos.Count > 0)
                        {
                            datasource.Clear();
                            datasource = listVideos;
                            this.lbResult.ItemsSource = datasource;
                            this.lbResult.SelectedIndex = 0;
                            currentVideo = datasource[0];
                            relatedList.Clear();
                            relatedList = K54csYoutubeProvider.YoutubeProvider.GetRelatedVideos(currentVideo);
                            this.horizontalListBox.ItemsSource = relatedList;
                        }
                    }
                    else
                    {
                        MessageBox.Show("you need to enter a search word", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch { }
        }

        #region other button click events

        private void btnWatchLater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoggedIn) return;
                List<VideoBase> list = new List<VideoBase>();
                list = yProvider.GetMyWatchLater();
                if (list == null || list.Count == 0) return;
                datasource.Clear();
                datasource = list;
                this.lbResult.ItemsSource = datasource;
            }
            catch
            {
            }
        }

        private void btnWatchHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoggedIn) return;
                List<VideoBase> list = new List<VideoBase>();
                list = yProvider.GetMyHistory();
                if (list == null || list.Count == 0) return;
                datasource.Clear();
                datasource = list;
                this.lbResult.ItemsSource = datasource;
            }
            catch
            {
            }
        }


        private void btnPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoggedIn) return;
                yProvider.GetMyPlaylist();
                this.dropDownDatasouce.Visibility = System.Windows.Visibility.Visible;
                playlists = yProvider.MyPlaylistVideo;
                if (playlists == null || playlists.Count == 0) return;
                this.dropDownDatasouce.Items.Add("All Playlists");
                List<string> tempList = yProvider.MyPlaylistsName;
                for (int i = 0; i < tempList.Count; i++)
                {
                    this.dropDownDatasouce.Items.Add(tempList[i]);
                }
                this.dropDownDatasouce.SelectedIndex = 0;
                List<VideoBase> list = new List<VideoBase>();

                datasource.Clear();
                for (int i = 0; i < playlists.Count; i++)
                {
                    datasource.AddRange(playlists[i]);
                    list.AddRange(playlists[i]);
                }
                datasource = list;
                this.lbResult.ItemsSource = datasource;
            }
            catch
            {
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isLoggedIn)
                {
                    isLoggedIn = false;
                    LOGIN_TOKEN = "";
                    this.toogleLayers(false);
                    this.expProfile.Header = "Hello, Guest";
                    return;
                }
                USERNAME = this.txtUsername.Text;
                PASSWORD = this.txtPassword.Password;
                if (USERNAME.Equals("") || PASSWORD.Equals(""))
                {
                    return;
                }
                yProvider = new YoutubeProvider(APP_NAME, DEV_KEY, USERNAME, PASSWORD);
                isLoggedIn = yProvider.Login(USERNAME, PASSWORD);
                if (!isLoggedIn)
                {
                    LOGIN_TOKEN = "";
                    this.toogleLayers(false);
                    this.expProfile.Header = "Hello, Guest";
                    this.txtUsername.Background = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    LOGIN_TOKEN = yProvider.LOGIN_TOKEN;
                    this.txtUsername.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF525252"));
                    this.toogleLayers(true);
                    K54csYoutubeProvider.UserProfile profile = yProvider.UserProfile();
                    this.expProfile.Header = profile.NICKNAME;
                    this.lblSmallFav.Content = "Subscribe Count: " + profile.SUBSCRIBE_COUNT;
                    this.lblSmallView.Content = "View Count     : " + profile.VIEW_COUNT;
                    this.lblSmallWatch.Content = "Watch Count    : " + profile.WATCH_COUNT;
                    this.imgAvatar.Source = new BitmapImage(new Uri(profile.AVATAR));
                    if (this.chbxdRememberMe.IsChecked == true)
                    {
                    }
                    else
                    {
                    }
                }
            }
            catch { }
        }

        private void btnRecommendation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isLoggedIn) return;
                List<VideoBase> list = new List<VideoBase>();
                list = yProvider.GetRecommendedVideos();
                if (list == null || list.Count == 0) return;
                datasource.Clear();
                datasource = list;
                this.lbResult.ItemsSource = datasource;
            }
            catch
            {
            }
        }
        private void chbxdRememberMe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void download_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                yyDownloader.buttonCancel_Click1();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
             * */

            YouTubeDownloader.Program.download(currentVideo.LINK.Replace("embed/", "watch?v="));
            //Application
            /*
            yyDownloader = YouTubeDownloader.Program.yDownloader;
            yyDownloader.buttonCancel_Click1();
             */
        }
        #endregion


        private void toogleLayers(bool show)
        {
            try
            {
                if (!show)
                {
                    this.txtPassword.Visibility = System.Windows.Visibility.Visible;
                    this.txtUsername.Visibility = System.Windows.Visibility.Visible;
                    this.btnWatchHistory.Visibility = System.Windows.Visibility.Hidden;
                    this.btnWatchLater.Visibility = System.Windows.Visibility.Hidden;
                    this.btnRecommendation.Visibility = System.Windows.Visibility.Hidden;
                    this.btnPlaylist.Visibility = System.Windows.Visibility.Hidden;
                    this.btnLogin.Content = "Login";
                    this.chbxdRememberMe.Visibility = System.Windows.Visibility.Visible;
                    this.expProfile.IsExpanded = false;
                    this.expProfile.Visibility = System.Windows.Visibility.Hidden;
                    this.dropDownDatasouce.Visibility = System.Windows.Visibility.Hidden;
                    this.imgAvatar.Source = new BitmapImage(new Uri("\\..\\Images\\avatar_default.jpg", UriKind.Relative));
                }
                else
                {
                    this.txtPassword.Visibility = System.Windows.Visibility.Hidden;
                    this.txtUsername.Visibility = System.Windows.Visibility.Hidden;
                    this.btnWatchHistory.Visibility = System.Windows.Visibility.Visible;
                    this.btnWatchLater.Visibility = System.Windows.Visibility.Visible;
                    this.btnRecommendation.Visibility = System.Windows.Visibility.Visible;
                    this.btnPlaylist.Visibility = System.Windows.Visibility.Visible;
                    this.btnLogin.Content = "Logout";
                    this.chbxdRememberMe.Visibility = System.Windows.Visibility.Hidden;
                    this.expProfile.IsExpanded = false;
                    this.expProfile.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch { }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            Color colo = (Color)ColorConverter.ConvertFromString("#FF525252");
            if (!colo.Equals(this.txtUsername.Background)) this.txtUsername.Background = new SolidColorBrush(colo);
        }


        private void expProfile_Expanded(object sender, RoutedEventArgs e)
        {
            this.btnWatchHistory.Visibility = System.Windows.Visibility.Hidden;
            this.btnWatchLater.Visibility = System.Windows.Visibility.Hidden;
            this.btnRecommendation.Visibility = System.Windows.Visibility.Hidden;
            this.btnPlaylist.Visibility = System.Windows.Visibility.Hidden;
        }

        private void expProfile_Collapsed(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                this.btnWatchHistory.Visibility = System.Windows.Visibility.Visible;
                this.btnWatchLater.Visibility = System.Windows.Visibility.Visible;
                this.btnRecommendation.Visibility = System.Windows.Visibility.Visible;
                this.btnPlaylist.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #region selection changed
        private void lbResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int number = this.lbResult.SelectedIndex;
                if (number < 0 || datasource.Count <= number) return;
                currentVideo = datasource[number];
                //wbPlayer.Source = new Uri(datasource[number].LINK);
                string clip = datasource[number].LINK;
                int startIndex = clip.LastIndexOf('/');
                string clipID = clip.Substring(startIndex + 1,11);
                wbPlayer.ShowYouTubeVideo(clipID);
                //temp.Text = clipID;
                relatedList.Clear();
                relatedList = YoutubeProvider.GetRelatedVideos(currentVideo);
                this.horizontalListBox.ItemsSource = relatedList;
            }
            catch { }
        }

        private void cbmBookmark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StandardFeed feedQuery = (StandardFeed)this.cbmBookmark.SelectedIndex;
            this.loadVideoByStandardFeed(feedQuery);
        }

        private void dropDownDatasouce_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.dropDownDatasouce.SelectedIndex;
            datasource.Clear();
            List<VideoBase> list = new List<VideoBase>();
            if (index == 0)
            {
                for (int i = 0; i < playlists.Count; i++)
                {
                    datasource.AddRange(playlists[i]);
                    list.AddRange(playlists[i]);
                }
            }
            else
            {
                datasource.AddRange(playlists[index - 1]);
                list.AddRange(playlists[index - 1]);
            }
            datasource = list;
            this.lbResult.ItemsSource = datasource;
        }

        private void horizontalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int number = this.horizontalListBox.SelectedIndex;
            if (relatedList == null) relatedList = new List<VideoBase>();
            if (number < 0 || relatedList.Count <= number) return;
            currentVideo = relatedList[number];
            relatedList.Clear();
            relatedList = YoutubeProvider.GetRelatedVideos(currentVideo);
            //wbPlayer.Source = new Uri(relatedList[number].LINK);
            string clip = relatedList[number].LINK;
            int startIndex = clip.LastIndexOf('/');
            string clipID = clip.Substring(startIndex + 1, 11);
            wbPlayer.ShowYouTubeVideo(clipID);

            this.horizontalListBox.ItemsSource = relatedList;
        }

        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.toogleLayers(false);

                this.defaultAvatar = this.imgAvatar.Source;
                this.txtUsername.Text = "ohtehands@gmail.com";
                this.txtPassword.Password = "";
                this.loadVideoByStandardFeed(StandardFeed.MOST_POPULAR);
                if (datasource != null && datasource.Count > 0)
                {
                    currentVideo = datasource[0];
                    lbResult.SelectedIndex = 1;
                    relatedList.Clear();
                    relatedList = YoutubeProvider.GetRelatedVideos(currentVideo);
                    this.horizontalListBox.ItemsSource = relatedList;
                }
            }
            catch { }

        }

        private void loadVideoByStandardFeed(StandardFeed feedQuery)
        {
            try
            {
                if (datasource != null) datasource.Clear();
                else datasource = new List<VideoBase>();
                if (this.lbResult == null) this.lbResult = new ListBox();
                if (this.horizontalListBox == null) this.horizontalListBox = new ListBox();
                List<VideoBase> list = new List<VideoBase>();
                list = YoutubeProvider.GetStandardFeed(feedQuery);
                if (list == null || list.Count == 0) return;
                datasource.Clear();
                datasource = list;
                this.lbResult.ItemsSource = datasource;
            }
            catch { }
        }


    }
    #region webbrowser extension
    public static class WebBrowserExtensions
    {
        private static string GetYouTubeVideoPlayerHTML(string videoCode)
        {
            var sb = new StringBuilder();

            const string YOUTUBE_URL = @"http://www.youtube.com/v/";

            sb.Append("<html>");
            sb.Append("    <head>");
            sb.Append("        <meta name=\"viewport\" content=\"width=device-width; height=device-height;\">");
            sb.Append("    </head>");
            sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
            sb.Append("        <object width=\"100%\" height=\"100%\">");
            sb.Append("            <param name=\"movie\" value=\"" + YOUTUBE_URL + videoCode + "?autoplay=1&amp;rel=0\" />");
            sb.Append("            <param name=\"allowFullScreen\" value=\"true\" />");
            sb.Append("            <param name=\"allowscriptaccess\" value=\"always\" />");
            sb.Append("            <embed src=\"" + YOUTUBE_URL + videoCode + "?autoplay=1&amp;rel=0\" type=\"application/x-shockwave-flash\"");
            sb.Append("                   width=\"100%\" height=\"100%\" allowscriptaccess=\"always\" allowfullscreen=\"true\" />");
            sb.Append("        </object>");
            sb.Append("    </body>");
            sb.Append("</html>");
            Console.WriteLine(sb);
            return sb.ToString();
        }

        public static void ShowYouTubeVideo(this WebBrowser webBrowser, string videoCode)
        {
            if (webBrowser == null) throw new ArgumentNullException("webBrowser");

            webBrowser.NavigateToString(GetYouTubeVideoPlayerHTML(videoCode));
        }
    #endregion
    }
}
