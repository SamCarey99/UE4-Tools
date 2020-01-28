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
using System.IO;
using System.Text.RegularExpressions;
using Path = System.IO.Path;

namespace UE4_Tools.Windows
{
    /// <summary>
    /// Interaction logic for CreateModule.xaml
    /// </summary>
    public partial class CreateModule : Window
    {
        public CreateModule()
        {
            InitializeComponent();
        }

        private void ReturnHome_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu temp = new MainMenu();
            temp.Show();
            Close();
        }

        private void Teplate_SB_MyChangeBoxEventHandler(string Value)
        {
            //Load preset module settings
            if(IsLoaded)
            {
                LoadingPhases_Sb.IsEnabled = Value.Equals("Custom");
                ModuleType_SB.IsEnabled = Value.Equals("Custom");

                if (Value.Equals("SimpleRuntime"))
                {
                    ModuleType_SB.Select_CB.SelectedValue = "Runtime";
                    LoadingPhases_Sb.Select_CB.SelectedValue = "Default";
                }

                if (Value.Equals("EditorExtension"))
                {
                    ModuleType_SB.Select_CB.SelectedValue = "Editor";
                    LoadingPhases_Sb.Select_CB.SelectedValue = "PostEngineInit";
                }
            }
        }

        private void CreateModule_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectSelector.Validate() && NewModuleName.Validate())
            {
                if (GlobalFunction.ProjectBackupPrompt(ProjectSelector.Directory, ProjectSelector.FileName))
                {
                    CreateNewModule();
                    MessageBox.Show("Your new module has been created\nFollow steps on next pages to open your project for the first time");
                    Tutorial Temp = new Tutorial();
                    Temp.Show();
                    Close();
                }
            }
        }

        //Ad
        private void CreateNewModule()
        {
            string ProjectPath = ProjectSelector.Directory;
            string ModuleName = NewModuleName.InputText;
            bool IsEditor = ModuleType_SB.Select_CB.SelectedValue.ToString().Equals("Editor");

            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Saved");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Intermediate");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/.vs");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Binaries");

            #region Uproject
                string FullUProject = File.ReadAllText(ProjectSelector.FullPathAndFile);
                string NewUProject = "," + Environment.NewLine + File.ReadAllText(@"../../BoilerplateCode/Module/BoilerplateUproject.txt");

                NewUProject = NewUProject.Replace("NameOfModule", ModuleName);
                NewUProject = NewUProject.Replace("ModuleType",   ModuleType_SB.Select_CB.SelectedValue.ToString());
                NewUProject = NewUProject.Replace("LoadingPhaseType", LoadingPhases_Sb.Select_CB.SelectedValue.ToString());

                MatchCollection Results = Regex.Matches(FullUProject, @"}", RegexOptions.Singleline);
                FullUProject = FullUProject.Insert(Results[Results.Count - 2].Index+1, NewUProject);
                File.WriteAllText(ProjectSelector.FullPathAndFile, FullUProject);
            #endregion

            #region File Structure / Startup Shutdown class
                string NewDirectory = ProjectPath + "/Source/" + ModuleName;
                Directory.CreateDirectory(NewDirectory);
                Directory.CreateDirectory(NewDirectory + "/Public");
                Directory.CreateDirectory(NewDirectory + "/Private");

                string BuildFile    = File.ReadAllText(@"../../BoilerplateCode/Module/BoilerplateModule.Build.txt").Replace("BoilerplateModule", ModuleName);
                BuildFile = BuildFile.Replace("BoilerplateNameOfProject", ProjectSelector.FileName);

                //Only include UnrealEd if creating a editor class
                if (!IsEditor)
                {
                    BuildFile = BuildFile.Replace(", \"UnrealEd\"", "");
                }

                string PublicFile   = File.ReadAllText(@"../../BoilerplateCode/Module/BoilerplateStartUpH.txt").Replace("BoilerplateModule", ModuleName);
                string PrivateFile  = File.ReadAllText(@"../../BoilerplateCode/Module/BoilerplateStartUpCPP.txt").Replace("BoilerplateModule", ModuleName);

                File.WriteAllText(NewDirectory + "/" + ModuleName + ".build.cs", BuildFile);
                File.WriteAllText(NewDirectory + "/Public/" + ModuleName + ".h", PublicFile);
                File.WriteAllText(NewDirectory + "/Private/" + ModuleName + ".cpp", PrivateFile);
            #endregion

            #region Game Target Files
                string TargetGame = File.ReadAllText(ProjectPath + "/Source/" + ProjectSelector.FileName + ".Target.cs");
                string TargetGameEditor = File.ReadAllText(ProjectPath + "/Source/" + ProjectSelector.FileName + "Editor.Target.cs");

                string NewTarget = File.ReadAllText(@"../../BoilerplateCode/Module/BoilerplateTargetFile.txt");
                //NewTarget = NewTarget.Replace("BoilerplateNameOfProject", ProjectSelector.FileName);
                NewTarget = NewTarget.Replace("BoilerplateModule", ModuleName);

                Results = Regex.Matches(TargetGame, @"}", RegexOptions.Singleline);
                string NewTargetGame = TargetGame.Insert(Results[Results.Count - 2].Index, NewTarget + Environment.NewLine + "\t");

                Results = Regex.Matches(TargetGameEditor, @"}", RegexOptions.Singleline);
                string NewTargetGameEditor = TargetGameEditor.Insert(Results[Results.Count - 2].Index, NewTarget + Environment.NewLine + "\t");

                File.WriteAllText(ProjectPath + "/Source/" + ProjectSelector.FileName + "Editor.Target.cs", NewTargetGameEditor);
                if (!IsEditor)
                {
                    File.WriteAllText(ProjectPath + "/Source/" + ProjectSelector.FileName + ".Target.cs", NewTargetGame);
                }
            #endregion
        }
    }
}