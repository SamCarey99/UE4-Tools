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
            RenameProject subWindow = new RenameProject();
            subWindow.Show();
            Close();
        }

        private void RenameClass_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In Development");
        }

        private void CreateModule_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In Development");
        }

        private void CreatePlugin_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In Development");
        }
    }
}
