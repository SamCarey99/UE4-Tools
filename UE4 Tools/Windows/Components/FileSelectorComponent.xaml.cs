using Microsoft.Win32;
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
using Path = System.IO.Path;

namespace UE4_Tools.Windows.Components
{
    /// <summary>
    /// Interaction logic for FileSelectorComponent.xaml
    /// </summary>
    public partial class FileSelectorComponent : UserControl
    {
        public bool ShowFullPath { get; set; } = true;              //Show both the path and file name after selecting a file
        public bool ShowFileExtension { get; set; } = true;         //Display the file extension
        public string FileType { get; set; } = "";                  //The type of file which should be selected
        public string FileTypeName { get; set; } = "";              //Description of the file type. Displayed to user
        public string NotSelectedMessage { get; set; } = "";        //Error message to display if no file is selected
        public string DefaultValue { get; set; } = "Select File";   //Default text to display

        public string Directory;        //The directory of the selected file
        public string FileName;         //The selected file name without directory
        public string FullPathAndFile;  //The selected file name with directory included

        public FileSelectorComponent()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            PathBox_L.Content = DefaultValue;
            base.EndInit();
        }

        private void Select_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = string.Format("{0}|*{1}", FileTypeName, FileType);
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                Directory = Path.GetDirectoryName(openFileDialog.FileName);
                FullPathAndFile = Path.GetFullPath(openFileDialog.FileName);

                if (ShowFullPath)
                {
                    PathBox_L.Content = FullPathAndFile;
                }
                else
                {
                    
                    PathBox_L.Content = ShowFileExtension ? Path.GetFileName(FullPathAndFile) : FileName;
                }
                Select_btn.Content = "Change";
            }
        }

        /// <summary>
        /// Test if a valid file has been selected
        /// </summary>
        /// <returns></returns>
        public bool Validate ()
        {
            if (PathBox_L.Content.Equals(DefaultValue))
            {
                MessageBox.Show(NotSelectedMessage, "Error");
                return false;
            }
            return true;
        }
    }
}
