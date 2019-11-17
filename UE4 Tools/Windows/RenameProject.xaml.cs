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
using System.Diagnostics;

namespace UE4_Tools
{
    /// <summary>
    /// Interaction logic for RenameProject.xaml
    /// </summary>
    public partial class RenameProject : Window
    {
        private string ProjectPath = "";        //Root Directory of UE4 project
        private string OldProjectName = "";     //Name of old project
        private string NewProjectName = "";     //Name of new project
        private bool ValidName = true;          //true if NewProjectName is valid

        public RenameProject()
        {
            InitializeComponent();
            NewProjectName = NewProjectName_txt.Text;
            ResizeMode = ResizeMode.NoResize;
        }

        private void SelectProject_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Unreal Engine Project File (.uproject)|*.uproject";
            if (openFileDialog.ShowDialog() == true)
            {
                OldProjectName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                ProjectPath = Path.GetDirectoryName(openFileDialog.FileName);
                ProjectName_L.Content = OldProjectName;
            }
        }

        private void RenameObject_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectName_L.Content.Equals("NO PROJECT SELECTED"))
            {
                MessageBox.Show("You must select a project to continue", "Select project");
            }
            else if (!ValidName)
            {
                MessageBox.Show("Invalid Name\nProject name must NOT: \n-Be empty\n-Start with a digit\n-Contain spaces", "Invalid Name");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("WARNING. It is highly recommended that you create a backup of your project continuing. If you have not created a backup click cancel and create one before you continue", "WARNING. Read carefully", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    RenameProjectFiles();
                    MessageBox.Show("Your Project has been renamed. Follow the steps on the next page to load your renamed project for the first time");

                    Tutorial TutorialWindow = new Tutorial();
                    TutorialWindow.Show();
                    Close();
                }
            }
        }

        private void NewProjectName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string NewNameTemp = NewProjectName_txt.Text;

            if (NewNameTemp.Length == 0 || char.IsDigit(NewNameTemp[0]) || NewNameTemp.IndexOf(" ") != -1)
            {
                ValidName = false;
                NewProjectName_txt.Foreground = Brushes.Red;
            }
            else
            {
                ValidName = true;
                NewProjectName_txt.Foreground = Brushes.Black;
                NewProjectName = NewProjectName_txt.Text;
            }
        }

        /// <summary>
        /// Removes Directory if it can find it
        /// </summary>
        /// <param name="Path">Path of Directory to remove</param>
        private void RemoveDirectoryIfValid (string Path)
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
        }

        /// <summary>
        /// Rename all required elements of project using ProjectPath, OldProjectName and NewProjectName
        /// </summary>
        private void RenameProjectFiles ()
        {
            RemoveDirectoryIfValid(ProjectPath + "/Saved");
            RemoveDirectoryIfValid(ProjectPath + "/Intermediate");
            RemoveDirectoryIfValid(ProjectPath + "/.vs");
            RemoveDirectoryIfValid(ProjectPath + "/Binaries");

            #region Change name internal  
            //List of all files which need some form of modification
            var Files = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories)
                .Where(
                        s => s.EndsWith(".cs") ||
                        s.EndsWith(".h") ||
                        s.EndsWith(".cpp") ||
                        s.EndsWith(".uproject") ||
                        s.EndsWith(".sln") ||
                        s.EndsWith(".ini")
                   );

            foreach (string i in Files)
            {
                File.WriteAllText(i, File.ReadAllText(i).Replace(OldProjectName, NewProjectName));

                string OldAPI = OldProjectName.ToUpper() + "_API";
                string NewAPI = NewProjectName.ToUpper() + "_API";
                File.WriteAllText(i, File.ReadAllText(i).Replace(OldAPI, NewAPI));
            }
            #endregion

            #region File Rename
            var FileName = from string i in Files
                           where Path.GetFileNameWithoutExtension(i).IndexOf(OldProjectName) != -1
                           select i;

            foreach (string i in FileName)
            {
                string newS = Path.GetDirectoryName(i) + "/" + Path.GetFileName(i).Replace(OldProjectName, NewProjectName);
                File.Move(i, newS);
            }
            Directory.Move(ProjectPath + "\\Source\\" + OldProjectName, ProjectPath + "\\Source\\" + NewProjectName);
            #endregion

            #region Project Icon
            string ProjectIconPath = ProjectPath + "\\" + OldProjectName + ".png";
            if (File.Exists(ProjectIconPath))
            {
                File.Move(ProjectIconPath, ProjectPath  + "\\" + NewProjectName + ".png");
            }
            #endregion
        }

        private void ReturnHome_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu Menu = new MainMenu();
            Menu.Show();
            Close();
        }
    }
}