//Copy rights are reserved for Akram kamal qassas
//Email me, Akramnet4u@hotmail.com
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YouTubeDownloader
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        public static frmYouTubeDownloader yDownloader;
        public static void Main(string url)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
        public static void download(string url)
        {

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            yDownloader = new frmYouTubeDownloader(url);
            Application.Run(yDownloader);
        }
        public static void exit()
        {
            yDownloader.buttonCancel_Click1();
        }
    }
}
