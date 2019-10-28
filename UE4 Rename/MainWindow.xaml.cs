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
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;

namespace UE4_Rename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string OldProjectPath = "";
        private string OldProjectName = "";

        private string NewProjectName = "TestProject";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select .uproject File";
            openFileDialog.Filter = ".uproject|*.uproject";

            if (openFileDialog.ShowDialog() == true)
            {
                ProjectPathText.Content = openFileDialog.FileName;

                OldProjectPath = Path.GetDirectoryName(openFileDialog.FileName);
                OldProjectName = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void UpdateName (string Path)
        {
            System.IO.File.Move(OldProjectPath + OldProjectName, OldProjectPath + NewProjectName + ".uproject");    //Rename .uproject File
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
