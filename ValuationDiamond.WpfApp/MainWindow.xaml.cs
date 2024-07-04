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
using ValuationDiamond.Data.Models;
using ValuationDiamond.WpfApp.UI;

namespace ValuationDiamond.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Open_wOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            var p = new wOrderDetail();
            p.Owner = this;
            p.Show();
        }
    }
}
