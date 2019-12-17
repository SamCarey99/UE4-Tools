using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for RenameClass.xaml
    /// </summary>
    public partial class RenameClass : Window
    {
        public RenameClass()
        {
            InitializeComponent();
        }
        private void ReturnHome_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu temp = new MainMenu();
            temp.Show();
            Close();
        }

        private void RenameClassAndReferences()
        {
            GlobalFunction.RemoveDirectoryIfValid(ProjectSelector.Directory + "/Intermediate");
            GlobalFunction.RemoveDirectoryIfValid(ProjectSelector.Directory + "/.vs");
            GlobalFunction.RemoveDirectoryIfValid(ProjectSelector.Directory + "/Binaries");

            //Find and rename references
            var Files = GlobalFunction.GetFilesWithExtension(ProjectSelector.Directory, new string[] { ".h", ".cpp" }, SearchOption.AllDirectories);
            foreach (string i in Files)
            {
                File.WriteAllText(i, File.ReadAllText(i).Replace(ClassSelector.FileName, NameSelector.InputText));
            }

            //Rename .h and .cpp files
            File.Move(ClassSelector.Directory + "\\" + ClassSelector.FileName + ".h",   ClassSelector.Directory + "\\" + NameSelector.InputText + ".h");
            File.Move(ClassSelector.Directory + "\\" + ClassSelector.FileName + ".cpp", ClassSelector.Directory + "\\" + NameSelector.InputText + ".cpp");

            GlobalFunction.ActiveRedirect(ProjectSelector.Directory, ClassSelector.FileName, NameSelector.InputText, true);
        }

        private void RenameClass_btn_Click(object sender, RoutedEventArgs e)
        {
            if(ProjectSelector.Validate() && ClassSelector.Validate() && NameSelector.Validate())
            {
                if(GlobalFunction.ProjectBackupPrompt(ProjectSelector.Directory, ProjectSelector.FileName))
                {
                    RenameClassAndReferences();
                    MessageBox.Show("Your class has been renamed\nFollow steps on next page to open your project for the first time");
                    Tutorial Temp = new Tutorial();
                    Temp.Show();
                    Close();
                }
            }
        }
    }
}
