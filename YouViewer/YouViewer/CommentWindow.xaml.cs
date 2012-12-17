using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Google.GData.YouTube;
using Google.YouTube;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;


using K54csYoutubeProvider;

namespace YouViewer
{
    /// <summary>
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        private String videoId;
        private CommentsSource commentSource;

        public CommentWindow(VideoBase currentVideo)
        {
            InitializeComponent();

            commentSource = new CommentsSource();
            videoId = currentVideo.VID;
            this.Title = currentVideo.TITLE;
            updateComment();
        }

        private void updateComment()
        {
            YouTubeRequest req = new YouTubeRequest(new YouTubeRequestSettings("YoutubeUploader", MainWindow.DEV_KEY));
            Uri videoEntryUrl = new Uri(string.Format("{0}/{1}", Google.GData.YouTube.YouTubeQuery.DefaultVideoUri, videoId));
            Google.YouTube.Video newVideo = req.Retrieve<Google.YouTube.Video>(videoEntryUrl);

            Feed<Comment> comments = req.GetComments(newVideo);
            commentSource.ListComment.Clear();
            for (int i = 0; i < comments.Entries.Count<Comment>(); i++)
            {
                Comment comment = comments.Entries.ElementAt(i);
                CustomComment customComment = new CustomComment(comment.Content, comment.Author, comment.Updated.ToString());
                commentSource.ListComment.Add(customComment);
            }
            this.commentsListBox.DataContext = commentSource;
        }

        public class CustomComment
        {
            public string Content { get; set; }
            public string Author { get; set; }
            public string PubDate { get; set; }

            public CustomComment(string content, string author, string pub)
            {
                Content = content;
                Author = author;
                PubDate = pub;
            }
        }

        public class CommentsSource
        {
            ObservableCollection<CustomComment> _ListComment = new ObservableCollection<CustomComment>();
            public ObservableCollection<CustomComment> ListComment
            {
                get
                {
                    return this._ListComment;
                }
            }
        }

        private void postComment(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.isLoggedIn)
            {
                MessageBox.Show("You have to login first");
                return;
            }

            YouTubeRequest req = new YouTubeRequest(new YouTubeRequestSettings(MainWindow.APP_NAME, MainWindow.DEV_KEY, MainWindow.USERNAME, MainWindow.PASSWORD));

            Uri videoEntryUrl = new Uri(string.Format("{0}/{1}", Google.GData.YouTube.YouTubeQuery.DefaultVideoUri, videoId));
            Google.YouTube.Video newVideo = req.Retrieve<Google.YouTube.Video>(videoEntryUrl);

            Comment c = new Comment();
            c.Content = commentTxtBox.Text.ToString();
            try
            {
                req.AddComment(newVideo, c);
            }
            catch (Google.GData.Client.GDataRequestException ex)
            {
                MessageBox.Show("You have post too many comments! Try again later!");
                return;
            }

            updateComment();
        }
    }
}
