using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace YouViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            // Pause to show the splash screen for 3 seconds
            System.Threading.Thread.Sleep(3000);
        }
    }
}
