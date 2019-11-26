using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace UE4_Tools.Windows.Components
{
    /// <summary>
    /// Interaction logic for ProjectBackupProgressBar.xaml
    /// </summary>
    public partial class ProjectBackupProgressBar : UserControl
    {
        public ProjectBackupProgressBar()
        {
            InitializeComponent();
        }
        /*
        public void CreateProjectBackup(string ProjectPath, string ProjectName)
        {
            int Counter = 0;

            string BackupPath = Directory.GetParent(ProjectPath) + "\\" + ProjectName + "_Backup";
            Directory.CreateDirectory(BackupPath);

            string[] DirectoriesList = Directory.GetDirectories(ProjectPath, "*", SearchOption.AllDirectories);
            string[] FileList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories);
            ProgressBarRef.Maximum = DirectoriesList.Length + FileList.Length;
            
            foreach (string i in DirectoriesList)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    CurrentFile_L.Content = "Copying Directory " + System.IO.Path.GetFileName(i);
                }), DispatcherPriority.Normal);

                Directory.CreateDirectory(i.Replace(ProjectPath, BackupPath));
                Counter++;
                ProgressBarRef.Value = Counter;
            }
            foreach (string i in FileList)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    CurrentFile_L.Content = "Copying File " + System.IO.Path.GetFileName(i);
                }), DispatcherPriority.Normal);

                File.Copy(i, i.Replace(ProjectPath, BackupPath), true);
                Counter++;
                ProgressBarRef.Value = Counter;
            }
        }
        */
    }
}
