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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void RenameProject_btn_Click(object sender, RoutedEventArgs e)
        {
            RenameProject RenameProject = new RenameProject();
            RenameProject.Show();
            Close();
        }

        private void RenameClass_btn_Click(object sender, RoutedEventArgs e)
        {
            RenameClass RenameClass = new RenameClass();
            RenameClass.Show();
            Close();
        }

        private void CreateModule_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In Development");
        }

        private void CreatePlugin_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In Development");
        }

        private void Changelog_btn_Click(object sender, RoutedEventArgs e)
        {
            ChangeLog Changelog = new ChangeLog();
            Changelog.Show();
            Close();
        }

        private void RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
