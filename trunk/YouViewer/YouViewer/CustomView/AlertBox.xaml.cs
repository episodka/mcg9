using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YouViewer.CustomView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AlertBox : Window
    {
        private bool resultButton;
        public AlertBox()
        {
            InitializeComponent();
            
        }

        public string Message
        {
            get { return this.message.Content.ToString(); }
            set { this.message.Content = value; }
        }

        public bool ResultButton
        {
            get { return resultButton; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            resultButton = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            resultButton = false;
            this.Close();
        }
    }
}
