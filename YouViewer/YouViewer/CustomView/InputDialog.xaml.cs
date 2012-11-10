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
    public partial class InputDialog : Window
    {
        public string valueText;
        public InputDialog()
        {
            InitializeComponent();
        }
        public string InputText
        {
            get { return inputText.Text; }
            set { inputText.Text = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            valueText = InputText;
            this.Close();
        }
    }
}
