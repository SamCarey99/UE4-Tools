using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace UE4_Tools
{
    /// <summary>
    /// Interaction logic for ChangeLog.xaml
    /// </summary>
    public partial class ChangeLog : Window
    {
        public ChangeLog()
        {
            InitializeComponent();
        }

        private void ReturnHome_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu temp = new MainMenu();
            temp.Show();
            Close();
        }

        private void RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
