using System;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Xml.Linq;
using System.Text;

using System.Runtime.Caching;
using System.Runtime.CompilerServices;

using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Diagnostics;

using System.ComponentModel;

using K54csYoutubeProvider;
using YouTubeDownloader;

using Google.GData.YouTube;
using Google.YouTube;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;


namespace YouViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string APP_PATH = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string APP_NAME = "iTube";
        public static string DEV_KEY = "AI39si5aMxqRQl5eSGRz7rfIhgk5BxXxSZ99xusMFiBD3ONuPMAytuj3uSS6R5N8tTUzYBGI4L3yipWF454lUA6MO7obxXhbdQ";
        private static string LOGIN_TOKEN;

        private ImageSource defaultAvatar;
        private string searchQuery = string.Empty;

        private List<VideoBase> datasource;
        private List<VideoBase> relatedList;
        private List<List<VideoBase>> playlists;
        public frmYouTubeDownloader yyDownloader;

        private K54csYoutubeProvider.YoutubeProvider yProvider;
        public static ProgramProperties mProgramProperties;

        public MainWindow()
        {
            InitializeComponent();
            mProgramProperties = new ProgramProperties();
            this.uploadBtn.DataContext = mProgramProperties;

            relatedList = new List<VideoBase>();

            this.toogleLayers(false);

            this.defaultAvatar = this.imgAvatar.Source;

            String lastUsername = Properties.Settings.Default.Username;
            String lastPassword = Properties.Settings.Default.Password;

            this.txtUsername.Text = lastUsername;
            this.txtPassword.Password = lastPassword;

            if (lastUsername.Equals("") || lastPassword.Equals(""))
            {
                searchVideo("charlie");
            }
            else
            {
                this.LoginYoutube();
                List<VideoBase> listVideos = K54csYoutubeProvider.YoutubeProvider.GetStandardFeed(StandardFeed.MOST_POPULAR);
                if (listVideos.Count > 0)
                {
                    datasource = listVideos;
                    this.lbResult.ItemsSource = datasource;
                    this.lbResult.SelectedIndex = 0;
                    mProgramProperties.CurrentVideo = datasource[0];
                    relatedList = K54csYoutubeProvider.YoutubeProvider.GetRelatedVideos(mProgramProperties.CurrentVideo);
                    this.horizontalListBox.ItemsSource = relatedList;
                }
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
                        searchVideo(txtKeyWord.Text);
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

        private void searchVideo(String query)
        {
            BackgroundWorker searchBw = new BackgroundWorker();
            searchBw = new BackgroundWorker();
            searchBw.DoWork += new DoWorkEventHandler(searchBw_DoWork);
            searchBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(searchBw_Completed);
            if (searchBw.IsBusy != true)
            {
                List<object> arguments = new List<object>();
                arguments.Add(query);
                searchBw.RunWorkerAsync(arguments);
            }
        }

        private void searchBw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            List<object> genericlist = e.Result as List<object>;
            List<VideoBase> listVideos = genericlist.ElementAt(0) as List<VideoBase>;
            if (listVideos.Count > 0)
            {
                datasource.Clear();
                datasource = listVideos;
                this.lbResult.ItemsSource = datasource;
                this.lbResult.SelectedIndex = 0;
                mProgramProperties.CurrentVideo = datasource[0];
                getRelatedVideo(mProgramProperties.CurrentVideo);
                this.horizontalListBox.ItemsSource = relatedList;
            }
            else
            {
                MessageBox.Show("you need to enter a search word", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void searchBw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> genericlist = e.Argument as List<object>;
            String query = (String)genericlist.ElementAt(0);
            if (query != String.Empty)
            {
                List<VideoBase> listVideos = K54csYoutubeProvider.YoutubeProvider.searchByKeyWord(query);
                List<object> arguments = new List<object>();
                arguments.Add(listVideos);
                e.Result = arguments;
            }
        }

        private void getRelatedVideo(VideoBase video)
        {
            BackgroundWorker getRelatedBw = new BackgroundWorker();
            getRelatedBw = new BackgroundWorker();
            getRelatedBw.DoWork += new DoWorkEventHandler(getRelatedBw_DoWork);
            getRelatedBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getRelatedBw_Completed);
            if (getRelatedBw.IsBusy != true)
            {
                List<object> arguments = new List<object>();
                arguments.Add(video);
                getRelatedBw.RunWorkerAsync(arguments);
            }
        }

        private void getRelatedBw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> genericlist = e.Argument as List<object>;
            VideoBase video = (VideoBase)genericlist.ElementAt(0);
            List<VideoBase> result = K54csYoutubeProvider.YoutubeProvider.GetRelatedVideos(video);
            List<object> arguments = new List<object>();
            arguments.Add(result);
            e.Result = arguments;
        }

        private void getRelatedBw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            List<object> genericlist = e.Result as List<object>;
            List<VideoBase> listVideos = genericlist.ElementAt(0) as List<VideoBase>;
            relatedList.Clear();
            relatedList = listVideos;
        }

        #region other button click events

        private void btnWatchLater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!mProgramProperties.IsLogedIn) return;
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
                if (!mProgramProperties.IsLogedIn) return;
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
                if (!mProgramProperties.IsLogedIn) return;
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
            this.LoginYoutube();
        }

        private void LoginYoutube()
        {
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Password;

            BackgroundWorker loginBw = new BackgroundWorker();
            loginBw = new BackgroundWorker();
            loginBw.WorkerSupportsCancellation = true;
            loginBw.DoWork += new DoWorkEventHandler(loginBw_DoWork);
            loginBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loginBw_Completed);
            if (loginBw.IsBusy != true)
            {
                List<object> arguments = new List<object>();
                arguments.Add(username);
                arguments.Add(password);
                loginBw.RunWorkerAsync(arguments);
            }
        }

        private void loginBw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            List<object> resultlist = e.Result as List<object>;
            int result = (int)resultlist.ElementAt(0);

            switch (result)
            {
                case 1: // Log out
                    mProgramProperties.IsLogedIn = false;
                    LOGIN_TOKEN = "";
                    this.toogleLayers(false);
                    this.expProfile.Header = "Hello, Guest";
                    this.txtPassword.Password = "";
                    this.txtUsername.Text = (String)Properties.Settings.Default.Username;

                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.Save();
                    break;
                case 2: // Login fail
                    LOGIN_TOKEN = "";
                    this.toogleLayers(false);
                    this.expProfile.Header = "Hello, Guest";
                    this.txtUsername.Background = new SolidColorBrush(Colors.Red);
                    break;
                case 3: // Login success
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
                        Properties.Settings.Default.Username = mProgramProperties.Username;
                        Properties.Settings.Default.Password = mProgramProperties.Password;
                        Properties.Settings.Default.Save();
                    }

                    break;
                default:
                    break;
            }
        }

        private void loginBw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> genericlist = e.Argument as List<object>;
            string username = (string)genericlist.ElementAt(0);
            string password = (string)genericlist.ElementAt(1);

            List<object> arguments = new List<object>();
            try
            {
                if (mProgramProperties.IsLogedIn)
                {
                    arguments.Add(1);
                    e.Result = arguments;
                    return;
                }

                if (username.Equals("") || password.Equals(""))
                {
                    arguments.Add(4);
                    e.Result = arguments;
                    return;
                }

                yProvider = new YoutubeProvider(APP_NAME, DEV_KEY, username, password);
                mProgramProperties.IsLogedIn = yProvider.Login(username, password);

                if (!mProgramProperties.IsLogedIn)
                {
                    arguments.Add(2);
                    e.Result = arguments;
                }
                else
                {
                    mProgramProperties.Username = username;
                    mProgramProperties.Password = password;

                    arguments.Add(3);
                    e.Result = arguments;
                }
            }
            catch { }
        }

        private void btnRecommendation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!mProgramProperties.IsLogedIn) return;
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

        private void download_Click(object sender, RoutedEventArgs e)
        {
            YouTubeDownloader.Program.download(mProgramProperties.CurrentVideo.LINK.Replace("embed/", "watch?v="));
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
                    this.btnUploaded.Visibility = Visibility.Hidden;
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
                    this.btnUploaded.Visibility = Visibility.Visible;
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
            this.btnUploaded.Visibility = Visibility.Hidden;
        }

        private void expProfile_Collapsed(object sender, RoutedEventArgs e)
        {
            if (mProgramProperties.IsLogedIn)
            {
                this.btnWatchHistory.Visibility = System.Windows.Visibility.Visible;
                this.btnWatchLater.Visibility = System.Windows.Visibility.Visible;
                this.btnRecommendation.Visibility = System.Windows.Visibility.Visible;
                this.btnPlaylist.Visibility = System.Windows.Visibility.Visible;
                this.btnUploaded.Visibility = Visibility.Visible;
            }
        }

        #region selection changed
        private void lbResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int number = this.lbResult.SelectedIndex;
                if (number < 0 || datasource.Count <= number) return;
                mProgramProperties.CurrentVideo = datasource[number];
                string clip = mProgramProperties.CurrentVideo.LINK;
                int startIndex = clip.LastIndexOf('/');
                string clipID = clip.Substring(startIndex + 1, 11);
                wbPlayer.ShowYouTubeVideo(clipID);
                relatedList.Clear();
                relatedList = YoutubeProvider.GetRelatedVideos(mProgramProperties.CurrentVideo);
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
            mProgramProperties.CurrentVideo = relatedList[number];
            relatedList.Clear();
            relatedList = YoutubeProvider.GetRelatedVideos(mProgramProperties.CurrentVideo);
            wbPlayer.Source = new Uri(relatedList[number].LINK);
            this.horizontalListBox.ItemsSource = relatedList;
        }

        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
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

        private void uploadBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (mProgramProperties.IsLogedIn)
            {
                YouViewerUploadWindow yuw = new YouViewerUploadWindow(mProgramProperties.Username, mProgramProperties.Password);
                yuw.Show();
            }
        }

        private void showCommentBtn_Click_1(object sender, RoutedEventArgs e)
        {
            CommentWindow cw = new CommentWindow();
            cw.initData(mProgramProperties.CurrentVideo);
            cw.Show();
        }

        private void btnUploaded_Click(object sender, RoutedEventArgs e)
        {
            getUploadedVideo(mProgramProperties.Username);
        }

        private void getUploadedVideo(String username)
        {
            BackgroundWorker getUploadedBw = new BackgroundWorker();
            getUploadedBw = new BackgroundWorker();
            getUploadedBw.DoWork += new DoWorkEventHandler(getUploadedBw_DoWork);
            getUploadedBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getUploadedBw_Completed);
            if (getUploadedBw.IsBusy != true)
            {
                List<object> arguments = new List<object>();
                arguments.Add(username);
                getUploadedBw.RunWorkerAsync(arguments);
            }
        }

        private void getUploadedBw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            List<object> genericlist = e.Result as List<object>;
            List<VideoBase> listVideos = genericlist.ElementAt(0) as List<VideoBase>;

            datasource.Clear();
            datasource = listVideos;
            this.lbResult.ItemsSource = datasource;
        }

        private void getUploadedBw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> genericlist = e.Argument as List<object>;
            String username = (String)genericlist.ElementAt(0);
            
            Uri uri = new Uri("http://gdata.youtube.com/feeds/api/users/"+username+"/uploads");
            YouTubeRequest req = new YouTubeRequest(new YouTubeRequestSettings("YouViewer", DEV_KEY));
            Feed<Video> videoFeeds = req.Get<Video>(uri);

            List<VideoBase> result = new List<VideoBase>();
            for (int i = 0; i < videoFeeds.Entries.Count(); i++)
            {
                Video video = videoFeeds.Entries.ElementAt(i);
                result.Add(YoutubeProvider.ConvertToVideoBase(video));
            }
            
            List<object> arg = new List<object>();
            arg.Add(result);
            e.Result = arg;
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

            return sb.ToString();
        }

        public static void ShowYouTubeVideo(this WebBrowser webBrowser, string videoCode)
        {
            if (webBrowser == null) throw new ArgumentNullException("webBrowser");

            webBrowser.NavigateToString(GetYouTubeVideoPlayerHTML(videoCode));
        }
    #endregion
    }

    public class ProgramProperties : INotifyPropertyChanged
    {
        private String username = String.Empty;
        private String password = String.Empty;
        private VideoBase currentVideo = null;
        private bool isLogedIn = false;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public String Username
        {
            get { return this.username; }
            set
            {
                this.username = value;
                NotifyPropertyChanged();
            }
        }

        public String Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                NotifyPropertyChanged();
            }
        }

        public VideoBase CurrentVideo
        {
            get { return this.currentVideo; }
            set
            {
                this.currentVideo = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsLogedIn
        {
            get { return this.isLogedIn; }
            set
            {
                this.isLogedIn = value;
                NotifyPropertyChanged();
            }
        }
    }
}
