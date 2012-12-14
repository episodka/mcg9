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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YouViewer
{
    /// <summary>
    /// Interaction logic for startpage.xaml
    /// </summary>
    public partial class startpage : Window
    {
        private bool isShown = false;
        public startpage()
        {
            isShown = false;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Do a key word search
        /// </summary>
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtKeyWord.Text != string.Empty)
                {

                    var newWindow = new MainWindow(this.txtKeyWord.Text);
                    newWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("you need to enter a search word", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            label2.Content = "History is cliked";
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            label2.Content = "Most liked is clicked";
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            label2.Content = "Most viewed is click";
        }

        private void Image_SignIn(object sender, MouseButtonEventArgs e)
        {
            /*
            if (isShown)
            {
                if (this.txtUsernameHome.Text.Equals("") || this.txtPasswordHome.Password.Equals("")) return;
                var newWindow = new MainWindow(this.txtUsernameHome.Text, this.txtPasswordHome.Password);
                newWindow.Show();
                this.Close();
            }
            else
            {
                isShown = true;
                txtUsernameHome.Visibility = Visibility;
                txtPasswordHome.Visibility = Visibility;
            }*/
        }
    }
}
