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
        public RenameProject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Rename all required elements of project
        /// </summary>
        private void RenameProjectFiles ()
        {
            string ProjectPath = ProjectSelector.Directory;
            string OldProjectName = ProjectSelector.FileName;
            string NewProjectName = NameSelectorRef.InputText;

            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Saved");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Intermediate");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/.vs");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Binaries");

            #region Change name internal  
            //List of all files which need some form of modification
            var Files = GlobalFunction.GetFilesWithExtension(ProjectPath, new string[] { ".cs", ".h", ".cpp", ".uproject", ".sln", ".ini" }, SearchOption.AllDirectories);

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

        private void RenameProject_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectSelector.Validate() && NameSelectorRef.Validate())
            {
                if(!ProjectSelector.FileName.Equals(NameSelectorRef.InputText))
                {
                    if (GlobalFunction.ProjectBackupPrompt(ProjectSelector.Directory, ProjectSelector.FileName))
                    {
                        RenameProjectFiles();
                        MessageBox.Show("Your project has been renamed\nFollow steps on next page to open your project for first time");
                        Tutorial Temp = new Tutorial();
                        Temp.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("You must select a new name to continue");
                }
            }
        }
    }
}