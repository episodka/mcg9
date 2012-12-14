using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K54csYoutubeProvider
{
    public class VideoBase
    {
        private string link;
        private List<string> thumbnails;
        private List<string> categories;
        private Dictionary<string,double> rating;

        public string TITLE { get; set; }
        public string PUB_DATE { get; set; }
        public string UPDATE { get; set; }
        public List<string> CATEGORIES { get { return this.categories; } set { this.categories = value; } }
        public string LINK
        {
            get
            {
                return link;
            }
            set
            {
                if (!value.Contains("watch?v=")){
                    link = @"http://www.youtube.com/embed/" + value + "?autoplay=1";
                }else{
                    link = value.Substring(0, value.IndexOf("&")).Replace("watch?v=", "embed/") + "?autoplay=1";
                }
            }
        }
        public string AUTHOR { get; set; }
        public Dictionary<string, double> RATING { get { return this.rating; } set { this.rating = value; } }
        public int FAVORITE_COUNT { get; set; }
        public int VIEW_COUNT { get; set; }
        public int COMMENT_COUNT { get; set; }
        public string LIKE { get; set; }
        public string DISLIKE { get; set; }
        public string COMMENTS { get; set; }
        public string DESCRIPTION { get; set; }
        public List<string> THUMBNAILS { get { return this.thumbnails; } set { this.thumbnails = value; } }
        public string DURATION { get; set; }
        public string UPLOADER_ID { get; set; }
        public string VID { get; set; }
        public override string ToString()
        {
            return TITLE + Environment.NewLine + "-- " + VIEW_COUNT + " views, "/* + "Like: " + LIKE + ", "*/ + COMMENT_COUNT + " comments, " + Environment.NewLine + " [... " + DESCRIPTION + " ...]";
        }
 
    }
}
