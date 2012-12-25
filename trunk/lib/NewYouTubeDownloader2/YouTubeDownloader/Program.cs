//Copy rights are reserved for Akram kamal qassas
//Email me, Akramnet4u@hotmail.com
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace YouTubeDownloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string url = "http://www.youtube.com/watch?v=5XEN4vtH4Ic";
            Application.Run(new frmYouTubeDownloader(url));

        }
    }
}
