using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.YouTube;
using Google.GData.Extensions.MediaRss;
using System.Windows.Forms;

namespace K54csYoutubeProvider
{
    public enum StandardFeed
    {
        TOP_FAVORITES = 0,
        TOP_RATED = 1,
        MOST_SHARED = 2,
        MOST_POPULAR = 3,
        MOST_RECENT = 4,
        MOST_DISCUSSED = 5,
        ON_THE_WEB = 6,
        RECENTLY_FEATURED = 7,
        MOST_RESPONDED = 8
    }
    public class YoutubeProvider
    {
        private static string APP_NAME = "iTube";
        private static string DEV_KEY = "AI39si5aMxqRQl5eSGRz7rfIhgk5BxXxSZ99xusMFiBD3ONuPMAytuj3uSS6R5N8tTUzYBGI4L3yipWF454lUA6MO7obxXhbdQ";
        private static string login_token;
        private UserProfile userProfile;

        private YouTubeService ytService;
        private YouTubeRequest ytRequest;
        public string LOGIN_TOKEN {
            get
            {
                return login_token;
            }
            set
            {
                login_token = value;
            }
        }
        private bool isLoggedIn;

        private static string USERNAME;
        private static string PASSWORD;

        private List<PlaylistId> myPlaylistIDs;
        private List<VideoBase> myWatchLater;
        private List<Playlist> myPlaylist;
        private List<List<VideoBase>> myPlaylistVideo;
        private List<Playlist> myFavorite;
        private List<string> myPlaylistsName;
        private List<VideoBase> myHistory;
        private ObservableCollection<VideoBase> detail_list = new ObservableCollection<VideoBase>();

        public YoutubeProvider(string app_name, string dev_key, string username, string password)
        {
            APP_NAME = app_name;
            DEV_KEY = dev_key;
            USERNAME = username;
            PASSWORD = password;
            myPlaylist = new List<Playlist>();
            myPlaylistsName = new List<string>();
            myHistory = new List<VideoBase>();
            myPlaylistVideo = new List<List<VideoBase>>();
            myFavorite = new List<Playlist>();
            myPlaylistIDs = new List<PlaylistId>();
            userProfile = new UserProfile();
            myWatchLater = new List<VideoBase>();
            Login();
        }

        public List<Playlist> MyPlaylist 
        {
            get
            {
                return myPlaylist;
            }
            set 
            {
                myPlaylist.Clear();
                myPlaylist = value;
            }
        }

        public List<VideoBase> MyHistory
        {
            get
            {
                return myHistory;
            }
            set
            {
                myHistory.Clear();
                myHistory = value;
            }
        }
        public List<VideoBase> MYWatchLater
        {
            get
            {
                return myWatchLater;
            }
            set
            {
                myWatchLater.Clear();
                myWatchLater = value;
            }
        }
        public List<Playlist> MyFavorite
        {
            get
            {
                return myFavorite;
            }
            set
            {
                myFavorite.Clear();
                myFavorite = value;
            }
        }
        public List<List<VideoBase>> MyPlaylistVideo
        {
            get
            {
                return myPlaylistVideo;
            }
            set
            {
                myPlaylistVideo.Clear();
                myPlaylistVideo = value;
            }
        }
        public List<PlaylistId> MyPlaylistID
        {
            get { return myPlaylistIDs; }
            set
            {
                myPlaylistIDs.Clear();
                myPlaylistIDs = value;
            }
        }

        public List<string> MyPlaylistsName
        {
            get { return myPlaylistsName; }
            set
            {
                myPlaylistsName.Clear();
                myPlaylistsName = value;
            }
        }

        private void Login()
        {
            ytService = new YouTubeService(APP_NAME,  DEV_KEY);
            ytService.setUserCredentials(USERNAME, PASSWORD);
            ytRequest = new YouTubeRequest(new YouTubeRequestSettings(APP_NAME,DEV_KEY));
            try
            {
                LOGIN_TOKEN = ytService.QueryClientLoginToken();
                isLoggedIn = LOGIN_TOKEN.Length > 0;
                if (isLoggedIn)
                {
                    ProfileEntry profile = (ProfileEntry)ytService.Get(@"https://gdata.youtube.com/feeds/api/users/default");
                    if (profile == null) return;
                    userProfile.NICKNAME = profile.Firstname + " " + profile.Lastname;
                    userProfile.AGE = "Age: " + profile.Age;
                    userProfile.HOBBIES = "Hobbies: " + profile.Hobbies;
                    userProfile.ID = "ID:" + profile.Id.ToString();
                    userProfile.HOMETOWN = "Hometown: " + profile.Hometown;
                    userProfile.COMPANY = profile.Company;
                    userProfile.LOCATION = profile.Location;
                    userProfile.RELATIONSHIP = profile.Relationship;
                    userProfile.SCHOOL = profile.School;
                    Statistics statistics = profile.Statistics;
                    if (statistics != null)
                    {

                        userProfile.SUBSCRIBE_COUNT = statistics.SubscriberCount;
                        userProfile.WATCH_COUNT = statistics.WatchCount;
                        userProfile.VIEW_COUNT = statistics.ViewCount;
                        userProfile.FAVORITE_COUNT = statistics.FavoriteCount;
                    }
                    var thumbnail = (from e in profile.ExtensionElements where e.XmlName == "thumbnail" select (XmlExtension)e).SingleOrDefault();
                    string thumbnailUrl = "";
                    if (thumbnail != null)
                    {
                        thumbnailUrl = thumbnail.Node.Attributes["url"].Value;
                    }
                    userProfile.AVATAR = thumbnailUrl;
                }
            }
            catch
            {
                isLoggedIn = false;
            }
        }

        public Boolean Login(string username, string password)
        {
            if (username.Equals("") || password.Equals("")) return false;
            USERNAME = username;
            PASSWORD = password;
            Login();
            return isLoggedIn;
        }

        public void GetMyPlaylist()
        {
            if (isLoggedIn == false) return;
            YouTubeRequestSettings settings = new YouTubeRequestSettings(APP_NAME, DEV_KEY, USERNAME, PASSWORD);
            YouTubeRequest reqeuest = new YouTubeRequest(settings);
            Feed<Playlist> pl_feeds = reqeuest.Get<Playlist>(new Uri("https://gdata.youtube.com/feeds/api/users/default/playlists?v=2"));
            List<List<VideoBase>> list = new List<List<VideoBase>>();
            List<string> playlistName = new List<string>();
            foreach (Playlist pl in pl_feeds.Entries)
            {
                playlistName.Add(pl.Title + " " + pl.CountHint + " video(s)");
                myPlaylistIDs.Add(new PlaylistId(pl.Id));
                Feed<PlayListMember> video_list = reqeuest.GetPlaylist(pl);
                Playlist certain_pl = new Playlist();
                certain_pl = pl;
                List<VideoBase> subVideo = new List<VideoBase>();
                foreach (Video video in video_list.Entries)
                {
                    subVideo.Add(ConvertToVideoBase(video));
                }
                list.Add(subVideo);
            }
            MyPlaylistsName = playlistName;
            MyPlaylistVideo = list;
            MyPlaylist = pl_feeds.Entries.ToList();
        }

        public static VideoBase ConvertToVideoBase(Video video)
        {
            VideoBase vid = new VideoBase();
            vid.TITLE = video.Title;
            if (video.WatchPage != null)
            {
                vid.LINK = video.WatchPage.ToString();
            }
            else if (video.VideoId != null)
            {
                vid.LINK = video.VideoId;
            }
            vid.VIEW_COUNT = video.ViewCount;
            vid.COMMENT_COUNT = video.CommmentCount;
            double totalSecond = double.Parse(video.YouTubeEntry.Duration.Seconds);
            TimeSpan t = TimeSpan.FromSeconds(totalSecond);
            if (totalSecond < 59 * 60) vid.DURATION = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            else vid.DURATION = string.Format("{0:D2}:{1:D2}:{2:D2}",  t.Hours,  t.Minutes, t.Seconds);
            vid.DESCRIPTION = video.Description;

            vid.VID = video.VideoId;
            MediaThumbnail[] thumbs = video.Thumbnails.ToArray();
            List<string> tempThumbs = new List<string>();
            for (int i = 0; i < thumbs.Length; i++)
            {
                tempThumbs.Add(thumbs[i].Url);
            }
            vid.THUMBNAILS = tempThumbs;

            AtomCategory[] cates = video.Categories.ToArray();
            List<string> tempCates = new List<string>();
            for (int i = 0; i < cates.Length; i++)
            {
                tempCates.Add(cates[i].Label);
            }
            vid.CATEGORIES = tempCates;
            if (video.YouTubeEntry.YtRating != null && video.YouTubeEntry.YtRating.NumLikes != null) vid.LIKE = video.YouTubeEntry.YtRating.NumLikes;
            else vid.LIKE = "";
            if (video.YouTubeEntry.YtRating != null && video.YouTubeEntry.YtRating.NumDislikes != null) vid.DISLIKE = video.YouTubeEntry.YtRating.NumDislikes;
            else vid.DISLIKE = "";
            if (video.YouTubeEntry.Rating != null)
            {
                Dictionary<string, double> rating = new Dictionary<string, double>();
                rating.Add("NumRaters", video.YouTubeEntry.Rating.NumRaters);
                rating.Add("Min", video.YouTubeEntry.Rating.Min);
                rating.Add("Max", video.YouTubeEntry.Rating.Max);
                rating.Add("Average", video.YouTubeEntry.Rating.Average);
                vid.RATING = rating;
            }

            vid.AUTHOR = video.YouTubeEntry.Authors.ToString();
            return vid;
        }

        public List<VideoBase> TransformToVideoList(Playlist list)
        {
            List<VideoBase> result = new List<VideoBase>();
                       
            return result;
        }

        public List<Playlist> GetMyFavorite()
        {
            if (isLoggedIn == false) return null;
            YouTubeRequestSettings settings = new YouTubeRequestSettings(APP_NAME, DEV_KEY, USERNAME, PASSWORD);
            YouTubeRequest reqeuest = new YouTubeRequest(settings);
            Feed<Playlist> pl_feeds = reqeuest.Get<Playlist>(new Uri("https://gdata.youtube.com/feeds/api/users/default/favorites?v=2"));
            List<Playlist> list = pl_feeds.Entries.ToList();
            MyFavorite = list;
            return list;
        }

        public List<VideoBase> GetMyHistory()
        {
            if (isLoggedIn == false) return null;
            YouTubeRequestSettings settings = new YouTubeRequestSettings(APP_NAME, DEV_KEY, USERNAME, PASSWORD);
            YouTubeRequest reqeuest = new YouTubeRequest(settings);
            Feed<Video> pl_feeds = reqeuest.Get<Video>(new Uri("https://gdata.youtube.com/feeds/api/users/default/watch_history?v=2"));
            List<VideoBase> list = new List<VideoBase>();
            foreach (Video video in pl_feeds.Entries)
            {
                list.Add(ConvertToVideoBase(video));
            }
            MyHistory = list;
            return list;
        }
        public List<VideoBase> GetMyWatchLater()
        {
            if (isLoggedIn == false) return null;
            YouTubeRequestSettings settings = new YouTubeRequestSettings(APP_NAME, DEV_KEY, USERNAME, PASSWORD);
            YouTubeRequest reqeuest = new YouTubeRequest(settings);
            Feed<Video> pl_feeds = reqeuest.Get<Video>(new Uri("https://gdata.youtube.com/feeds/api/users/default/watch_later?v=2"));
            List<VideoBase> list = new List<VideoBase>();
            foreach (Video video in pl_feeds.Entries)
            {
                list.Add(ConvertToVideoBase(video));
            }
            MYWatchLater = list;
            return list;
        }

        public UserProfile UserProfile()
        {
            return userProfile;
        }

        public static List<VideoBase> searchByKeyWord(string keyword)
        {
            List<VideoBase> list = new List<VideoBase>();
            YouTubeRequest ytRequest = new YouTubeRequest(new YouTubeRequestSettings(APP_NAME, DEV_KEY));
            Feed<Video> vid_feed = ytRequest.Get<Video>(new Uri("https://gdata.youtube.com/feeds/api/videos?q=" + keyword + "&max-results=20&v=2"));
            foreach (Video video in vid_feed.Entries)
            {
                VideoBase baseVideo = ConvertToVideoBase(video);
                list.Add(baseVideo);
            }
            return list;
        }
        public static List<VideoBase> GetRelatedVideos(VideoBase vid)
        {
            List<VideoBase> list = new List<VideoBase>();
            YouTubeRequest ytRequest = new YouTubeRequest(new YouTubeRequestSettings(APP_NAME, DEV_KEY));
            Feed<Video> vid_feed = ytRequest.Get<Video>(new Uri(@"https://gdata.youtube.com/feeds/api/videos/"+vid.VID+"/related?v=2"));
            foreach (Video video in vid_feed.Entries)
            {
                VideoBase baseVideo = ConvertToVideoBase(video);
                list.Add(baseVideo);
            }
            return list;
        }
        public static List<VideoBase> GetStandardFeed(StandardFeed feed)
        {
            string feedQuery = "";
            switch (feed)
            {
                case StandardFeed.MOST_DISCUSSED:
                    feedQuery = "most_discussed";
                    break;
                case StandardFeed.MOST_POPULAR:
                    feedQuery = "most_popular";
                    break;
                case StandardFeed.MOST_RECENT:
                    feedQuery = "most_recent";
                    break;
                case StandardFeed.MOST_RESPONDED:
                    feedQuery = "most_responded";
                    break;
                case StandardFeed.MOST_SHARED:
                    feedQuery = "most_shared";
                    break;
                case StandardFeed.ON_THE_WEB:
                    feedQuery = "on_the_web";
                    break;
                case StandardFeed.RECENTLY_FEATURED:
                    feedQuery = "recently_featured";
                    break;
                case StandardFeed.TOP_FAVORITES:
                    feedQuery = "top_favorites";
                    break;
                case StandardFeed.TOP_RATED:
                    feedQuery = "top_rated";
                    break;
            }
            List<VideoBase> list = new List<VideoBase>();
            YouTubeRequest ytRequest = new YouTubeRequest(new YouTubeRequestSettings(APP_NAME, DEV_KEY));
            Feed<Video> vid_feed = ytRequest.Get<Video>(new Uri("https://gdata.youtube.com/feeds/api/standardfeeds/" + feedQuery));
            foreach (Video video in vid_feed.Entries)
            {
                VideoBase baseVideo = ConvertToVideoBase(video);
                list.Add(baseVideo);
            }
            return list;
        }
    }
}
