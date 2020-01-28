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
using UE4_Tools.Windows.Components;

namespace UE4_Tools.Windows
{
    /// <summary>
    /// Interaction logic for CreateCustomAsset.xaml
    /// </summary>
    public partial class CreateCustomAsset : Window
    {
        public CreateCustomAsset()
        {
            InitializeComponent();
            MessageBox.Show("Creating custom assets requires an Editor module. If you don't have one, an editor module can be created under 'Create C++ Module' in the main menu.");
        }

        private void ReturnHome_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu temp = new MainMenu();
            temp.Show();
            Close();
        }

        /// <summary>
        /// Add new editor action to list ()
        /// </summary>
        private void CreateContextAction_btn_Click(object sender, RoutedEventArgs e)
        {
            Actions.Children.Add(new ContextActionComponent());
            ContextActionComponent Temp = ((ContextActionComponent)Actions.Children[Actions.Children.Count - 1]);
        }

        /// <summary>
        /// Clears all editor actions
        /// </summary>
        private void ClearContextActions_btn_Click(object sender, RoutedEventArgs e)
        {
            Actions.Children.Clear();
        }

        private void CreateAsset_btn_Click(object sender, RoutedEventArgs e)
        {
            string BuildFile = File.ReadAllText(EditorModuleSelector.FullPathAndFile);
            if (BuildFile.IndexOf("UnrealEd") != -1)
            {
                if (ProjectSelector.Validate() && NewAssetName.Validate())
                {
                    if (GlobalFunction.ProjectBackupPrompt(ProjectSelector.Directory, ProjectSelector.FileName))
                    {
                        CreateNewCustomAsset();
                        MessageBox.Show("Your new factory class and object have been created\nFollow the steps on next pages to open your project for the first time");
                        MessageBox.Show("Your asset can be found under the miscellaneous tab in the new asset window");
                        Tutorial Temp = new Tutorial();
                        Temp.Show();
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Your selected module must be of type editor and include UnrealEd");
            }
        }

        /// <summary>
        /// Select Presets
        /// </summary>
        private void CreateType_SB_MyChangeBoxEventHandler(string Value)
        {
            if(IsLoaded)
            {
                NewAssetName.IsEnabled = !Value.Equals("ExistingUObject");
                SelectExistingClass_SB.IsEnabled = Value.Equals("ExistingUObject");
            }
        }

        /// <summary>
        /// Generate new code and save to file
        /// </summary>
        private void CreateNewCustomAsset ()
        {
            string ProjectPath = ProjectSelector.Directory;
            string APIRef = ProjectSelector.FileName.ToUpper() + "_API";
            string SourceDirectory = ProjectPath + "/Source/" + ProjectSelector.FileName + "/";

            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Saved");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Intermediate");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/.vs");
            GlobalFunction.RemoveDirectoryIfValid(ProjectPath + "/Binaries");

            string ClassName = SelectExistingClass_SB.FileName;

            #region Create UObjectClass
            if(CreateType_SB.Select_CB.SelectedValue.Equals("NewUObject"))
            {
                string HeaderFile = File.ReadAllText(@"../../BoilerplateCode/Factory/BoilerPlateUObject Header.txt");
                HeaderFile = HeaderFile.Replace("ProjectNameAPI", APIRef);
                HeaderFile = HeaderFile.Replace("CustomAssetName", NewAssetName.InputText);

                File.WriteAllText(SourceDirectory + NewAssetName.InputText + ".h", HeaderFile);
                File.WriteAllText(SourceDirectory + NewAssetName.InputText + ".cpp", "#include \"" + NewAssetName.InputText + ".h\"");
                ClassName = NewAssetName.InputText;
            }
            #endregion

            #region CreateFactory
            string FactoryClassName = ClassName + "Factory";
            string FactoryCPP = File.ReadAllText(@"../../BoilerplateCode/Factory/BoilerplateFactory CPP.txt");
            FactoryCPP = FactoryCPP.Replace("SetAssetTypeCategories", AssetCategory_SB.Select_CB.SelectedValue.ToString());
            FactoryCPP = FactoryCPP.Replace("CustomAssetName", ClassName);
            FactoryCPP = FactoryCPP.Replace("CustomAssetFactoryName", FactoryClassName);

            string FactoryHeader = File.ReadAllText(@"../../BoilerplateCode/Factory/BoilerplateFactory Header.txt");
            FactoryHeader = FactoryHeader.Replace("CustomAssetName", ClassName);
            FactoryHeader = FactoryHeader.Replace("CustomAssetFactoryName", FactoryClassName); 

            string ModuleDirectory = EditorModuleSelector.Directory;
            File.WriteAllText(ModuleDirectory + "/Public/"  + FactoryClassName + ".h", FactoryHeader);
            File.WriteAllText(ModuleDirectory + "/Private/" + FactoryClassName + ".cpp", FactoryCPP);
            #endregion
        }
    }
}
