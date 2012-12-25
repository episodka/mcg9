//Copy rights are reserved for Akram kamal qassas
//Email me, Akramnet4u@hotmail.com
using System;
using System.Windows.Forms;

namespace YouTube_Downloader
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string url = "http://www.youtube.com/watch?v=dk9Yt1PqQiw";
            //Application.Run(new frmYouTubeDownloader(url));
            Application.Run(new mainForm(url));
        }
        public static void download(string url)
        {

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm(url));
        }
    }
}
