using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pujcovnaHudebnichNastroju
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void prihlaseniBtn_Click(object sender, RoutedEventArgs e)
        {
            login l = new login();
            l.Top = Application.Current.MainWindow.Top + 80;
            l.Left = Application.Current.MainWindow.Left + 80;
            l.Owner = this;
            l.ShowDialog();
        }
    }
}
