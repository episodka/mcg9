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

namespace YouViewer
{

    public enum MyCachePriority
    {
        Default,
        NotRemovable
    }
    public partial class YouViewerMainWindow : Window
    {
        private static string APP_PATH = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string APP_NAME = "iTube";
        private static string DEV_KEY = "AI39si5aMxqRQl5eSGRz7rfIhgk5BxXxSZ99xusMFiBD3ONuPMAytuj3uSS6R5N8tTUzYBGI4L3yipWF454lUA6MO7obxXhbdQ";
        private static string LOGIN_TOKEN;

        private static string USERNAME;
        private static string PASSWORD;

        private ImageSource defaultAvatar;

       
        private List<VideoBase> datasource;
        private List<List<VideoBase>> playlists;

        private K54csYoutubeProvider.YoutubeProvider yProvider;

        private static Boolean isLoggedIn = false;


        private static ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;

        #region Data
        private Random rand = new Random(50);
        #endregion

        #region Ctor
        public static string app_path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public YouViewerMainWindow()
        {
            InitializeComponent();
            datasource = new List<VideoBase>();
            this.ytResult.ItemsSource = datasource;
            this.playlists = new List<List<VideoBase>>();
        }
        #endregion

        #region Private Methods

        
        /// <summary>
        /// Do a key word search
        /// </summary>
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                 List<VideoBase> listVideos = K54csYoutubeProvider.YoutubeProvider.searchByKeyWord(txtKeyWord.Text);
                 datasource = listVideos;
                 this.ytResult.ItemsSource = listVideos;
            }
        }


        #endregion

     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.toogleLayers(false);

            this.defaultAvatar = this.imgAvatar.Source;

            webPlayer.Source = new Uri(@"http://www.youtube.com/embed/aojwGQOEgCs");

            String strUserName = this.GetMyCachedItem("USER_NAME") as String;
            String strPassword = this.GetMyCachedItem("PASS_WORD") as String;
            this.txtUsername.Text = strUserName;
            this.txtPassword.Password = strPassword;

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

        
        private void ytResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int number = ytResult.SelectedIndex;
            if (number >= 0) webPlayer.Source = new Uri(datasource[number].LINK);
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
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
                this.lblUsername.Content = USERNAME;
                this.lblAge.Content = profile.AGE;
                this.lblHobbies.Content = profile.HOBBIES;
                this.lblHometown.Content = profile.HOMETOWN;
                this.lblID.Content = profile.ID;
                this.imgAvatar.Source = new BitmapImage(new Uri(profile.AVATAR));
                if (this.chbxdRememberMe.IsChecked == true)
                {
                    this.AddToMyCache("USER_NAME", USERNAME, MyCachePriority.Default);
                    this.AddToMyCache("PASS_WORD", USERNAME, MyCachePriority.Default);
                }
                else
                {
                    this.RemoveMyCachedItem("USER_NAME");
                    this.RemoveMyCachedItem("PASS_WORD");
                }
            }
        }

        private void btnWatchLater_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWatchHistory_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn) return;
            datasource.Clear();
            List<VideoBase> list = new List<VideoBase>();
            list = yProvider.GetMyHistory();
            datasource = list;
            this.ytResult.ItemsSource = list;
        }

        private void btnSubscription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn) return;
            yProvider.GetMyPlaylist();
            this.dropDownDatasouce.Visibility = System.Windows.Visibility.Visible;
            playlists = yProvider.MyPlaylistVideo;
            this.dropDownDatasouce.Items.Add("All Playlists");
            List<string> tempList = yProvider.MyPlaylistsName;
            for (int i = 0; i < tempList.Count; i++)
            {
                this.dropDownDatasouce.Items.Add(tempList[i]);
            }
            this.dropDownDatasouce.SelectedIndex = 0;
            datasource.Clear();
            List<VideoBase> list = new List<VideoBase>();
            for (int i = 0; i < playlists.Count; i++)
            {
                datasource.AddRange(playlists[i]);
                list.AddRange(playlists[i]);
            }
            datasource = list;
            this.ytResult.ItemsSource = list;
        }
        private void toogleLayers(bool show)
        {
            if (!show)
            {
                this.txtPassword.Visibility = System.Windows.Visibility.Visible;
                this.txtUsername.Visibility = System.Windows.Visibility.Visible;
                this.btnWatchHistory.Visibility = System.Windows.Visibility.Hidden;
                this.btnWatchLater.Visibility = System.Windows.Visibility.Hidden;
                this.btnSubscription.Visibility = System.Windows.Visibility.Hidden;
                this.btnPlaylist.Visibility = System.Windows.Visibility.Hidden;
                this.btnLogin.Content = "Login";
                this.chbxdRememberMe.Visibility = System.Windows.Visibility.Visible;
                this.expProfile.IsExpanded = false;
                this.expProfile.Visibility = System.Windows.Visibility.Hidden;
                this.dropDownDatasouce.Visibility = System.Windows.Visibility.Hidden;
                this.imgAvatar.Source = new BitmapImage(new Uri("\\..\\Images\\avatar_default.jpg",UriKind.Relative));
            }
            else
            {
                this.txtPassword.Visibility = System.Windows.Visibility.Hidden;
                this.txtUsername.Visibility = System.Windows.Visibility.Hidden;
                this.btnWatchHistory.Visibility = System.Windows.Visibility.Visible;
                this.btnWatchLater.Visibility = System.Windows.Visibility.Visible;
                this.btnSubscription.Visibility = System.Windows.Visibility.Visible;
                this.btnPlaylist.Visibility = System.Windows.Visibility.Visible;
                this.btnLogin.Content = "Logout";
                this.chbxdRememberMe.Visibility = System.Windows.Visibility.Hidden;
                this.expProfile.IsExpanded = false;
                this.expProfile.Visibility = System.Windows.Visibility.Visible;
            }
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
            this.btnSubscription.Visibility = System.Windows.Visibility.Hidden;
            this.btnPlaylist.Visibility = System.Windows.Visibility.Hidden;
        }

        private void expProfile_Collapsed(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                this.btnWatchHistory.Visibility = System.Windows.Visibility.Visible;
                this.btnWatchLater.Visibility = System.Windows.Visibility.Visible;
                this.btnSubscription.Visibility = System.Windows.Visibility.Visible;
                this.btnPlaylist.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void chbxdRememberMe_Click(object sender, RoutedEventArgs e)
        {
           
        }

        public void AddToMyCache(String CacheKeyName, Object CacheItem, 
	       MyCachePriority MyCacheItemPriority) 
        { 
            // 
            callback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback); 
            policy = new CacheItemPolicy(); 
            policy.Priority = (MyCacheItemPriority == MyCachePriority.Default) ? 
		            CacheItemPriority.Default : CacheItemPriority.NotRemovable; 
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00); 
            policy.RemovedCallback = callback; 

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy); 
        }

        public Object GetMyCachedItem(String CacheKeyName) 
        { 
        // 
            return cache[CacheKeyName] as Object; 
        }

        public void RemoveMyCachedItem(String CacheKeyName) 
        { 
        // 
            if (cache.Contains(CacheKeyName)) 
            { 
                cache.Remove(CacheKeyName); 
            } 
        }

        private void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments) 
        { 
            // Log these values from arguments list 
            String strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), " | Key-Name: ", arguments.CacheItem.Key, " | Value-Object: ", 
            arguments.CacheItem.Value.ToString()); 
        }

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

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
                datasource.AddRange(playlists[index-1]);
                list.AddRange(playlists[index-1]);
            }
            datasource = list;
            this.ytResult.ItemsSource = list;
        }

    }
}
