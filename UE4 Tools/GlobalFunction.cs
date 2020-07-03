using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UE4_Tools.Windows.Components;

/// <summary>
/// A set of global functions which can provide reusable code to multiple other windows within the project
/// </summary>
namespace UE4_Tools
{
    class GlobalFunction
    {
        /// <summary>
        /// Removes a directory if it can be found
        /// </summary>
        /// <param name="DictionaryPath">Directory to remove</param>
        public static void RemoveDirectoryIfValid(string DirectoryPath)
        {
            if (Directory.Exists(DirectoryPath))
            {
                Directory.Delete(DirectoryPath, true);
            }
        }

        /// <summary>
        /// Get a list of all files in directory of a certain file format
        /// </summary>
        /// <param name="SearchPath">Path of directory to search</param>
        /// <param name="Types">List of all file which will be searched for</param>
        /// <param name="SearchOption">Search depth options</param>
        /// <returns>Returns a list of all files which match the filters</returns>
        public static string[] GetFilesWithExtension(string SearchPath, string[] Types, SearchOption SearchOption)
        {
            var Files = Directory.GetFiles(SearchPath, "*.*", SearchOption).Where(i => Types.Contains(Path.GetExtension(i)));
            return Files.ToArray();
        }

        /// <summary>
        /// Prompt the user to backup their project. Start backup if you selects
        /// </summary>
        /// <param name="ProjectPath">Current path of project</param>
        /// <param name="ProjectName">Current name of project</param>
        /// <returns>Return true if the action can continue</returns>
        public static bool ProjectBackupPrompt (string ProjectPath, string ProjectName)
        {
            MessageBoxResult result = MessageBox.Show("WARNING. It is highly recommended that you create a backup of your project before continuing.\nDo you want to create a backup?\n\nYes=\tCreate backup and continue\nNo=\tDon't create backup and continue(Not recommended)\nCancel=\tCancel operation and return to previous menu", "WARNING. Read carefully", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                CreateProjectBackup(ProjectPath, ProjectName);
                return true;
            }
            if (result == MessageBoxResult.No)
            {
                MessageBoxResult result2 = MessageBox.Show("WARNING. You are continuing without a backup of your project files. This action can NOT be undone\n\nAre you sure you want to continue?", "Are you sure you want to continue?", MessageBoxButton.YesNo);
                return result2 == MessageBoxResult.Yes;
            }
            return false;  
        }

        /// <summary>
        /// Create an ActiveRedirect in DefaultEngine.ini
        /// </summary>
        /// <param name="ProjectPath">Path of project folder</param>
        /// <param name="StartName">Old name of class or project</param>
        /// <param name="EndName">New name of class or project</param>
        /// <param name="Class">True=Class Redirect False=Project Redirect</param>
        public static void ActiveRedirect(string ProjectPath, string StartName, string EndName, bool Class)
        {
            string NewPath  = ProjectPath + "\\Config\\DefaultEngine.ini";
            string Redirect = Class ? string.Format("+ActiveClassRedirects=(OldClassName=\"{0}\",NewClassName=\"{1}\")", StartName, EndName) :
                                      string.Format("+ActiveGameNameRedirects=(OldGameName=\"/Script/{0}\",NewGameName=\"/Script/{1}\"", StartName, EndName);

            using (StreamWriter ConfigFile = File.AppendText(NewPath))
            {
                ConfigFile.WriteLine(Environment.NewLine +"[/Script/Engine.Engine]");
                ConfigFile.WriteLine(Redirect);
            }
        }

        /// <summary>
        /// Creates an exact backup of a project within the same parent directory
        /// </summary>
        /// <param name="ProjectPath">Current path of project</param>
        /// <param name="ProjectName">Current name of project</param>
        private static void CreateProjectBackup (string ProjectPath, string ProjectName)
        {
            string BackupPath = Directory.GetParent(ProjectPath) + "\\" + ProjectName + "_Backup";
            Directory.CreateDirectory(BackupPath);
            string[] DirectoriesList = Directory.GetDirectories(ProjectPath, "*", SearchOption.AllDirectories);
            string[] FileList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories);

            foreach (string i in DirectoriesList)
            {
                Directory.CreateDirectory(i.Replace(ProjectPath, BackupPath));
            }
            foreach (string i in FileList)
            { 
                File.Copy(i, i.Replace(ProjectPath, BackupPath), true);
            }
        }

        public static string FindResourceTextFile (string FileName)
        {
            var AssemblyRef = Assembly.GetExecutingAssembly();
            string ResourceName = AssemblyRef.GetManifestResourceNames().Single(str => str.EndsWith(FileName + ".txt"));
            return new StreamReader(AssemblyRef.GetManifestResourceStream(ResourceName)).ReadToEnd();
        }
    }
}
