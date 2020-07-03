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
            ActionList_SP.Children.Add(new ContextActionComponent());
            ContextActionComponent Temp = ((ContextActionComponent)ActionList_SP.Children[ActionList_SP.Children.Count - 1]);
            Temp.Height = 60;
            Temp.Width = 450;
            NumberOfActions_TB.Text = "Number of Context Actions: " + ActionList_SP.Children.Count;
        }

        /// <summary>
        /// Clears all editor actions
        /// </summary>
        private void ClearContextActions_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to remove all context actions?", "Are you sure?", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ActionList_SP.Children.Clear();
            }
            NumberOfActions_TB.Text = "Number of Context Actions: 0";
        }

        private void CreateAsset_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectSelector.Validate() && EditorModuleSelector.Validate())
            {
                string ModuleDirectory = EditorModuleSelector.Directory;
                string InitFile = File.ReadAllText(ModuleDirectory + "/Private/" + EditorModuleSelector.FileName.Substring(0, EditorModuleSelector.FileName.IndexOf(".")) + ".cpp");
                string BuildFile = File.ReadAllText(EditorModuleSelector.FullPathAndFile);

                if (BuildFile.IndexOf("UnrealEd") != -1)
                {
                    if (GlobalFunction.ProjectBackupPrompt(ProjectSelector.Directory, ProjectSelector.FileName))
                    {
                        CreateNewCustomAsset();
                        if (UseAssetType_CB.IsChecked)
                        {
                            CreateAssetActions();
                        }

                        MessageBox.Show("Your new factory class and object have been created");
                        if (UseAssetType_CB.IsChecked)
                        {
                            MessageBox.Show("An FAssetTypeActions_Base class has been created. Remember to included the required dependencies and registered with the \"AssetTools\" module");
                        }

                        Tutorial Temp = new Tutorial();
                        Temp.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Your selected module must be of type editor and include UnrealEd");
                }
            }
        }

        private void CreateType_SB_MyCheckedChangeEventHandler(bool IsChecked)
        {
            if(IsLoaded)
            {
                NewAssetName.IsEnabled = IsChecked;
                SelectExistingClass_SB.IsEnabled = !IsChecked;
                string ModuleDirectory = EditorModuleSelector.FileName;
                ModuleDirectory = ModuleDirectory.Substring(0, ModuleDirectory.IndexOf("."));
                Console.WriteLine(ModuleDirectory);
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
            if(CreateType_CB.IsChecked)
            {
                string HeaderFile = GlobalFunction.FindResourceTextFile("BoilerPlateUObject Header");

                HeaderFile = HeaderFile.Replace("ProjectNameAPI", APIRef);
                HeaderFile = HeaderFile.Replace("CustomAssetName", NewAssetName.InputText);

                File.WriteAllText(SourceDirectory + NewAssetName.InputText + ".h", HeaderFile);
                File.WriteAllText(SourceDirectory + NewAssetName.InputText + ".cpp", "#include \"" + NewAssetName.InputText + ".h\"");
                ClassName = NewAssetName.InputText;
            }
            #endregion

            #region CreateFactory
            string FactoryClassName = ClassName + "Factory";
            string FactoryCPP = GlobalFunction.FindResourceTextFile("BoilerplateFactory CPP");
            FactoryCPP = FactoryCPP.Replace("SetAssetTypeCategories", AssetCategory_SB.Select_CB.SelectedValue.ToString());
            FactoryCPP = FactoryCPP.Replace("CustomAssetName", ClassName);
            FactoryCPP = FactoryCPP.Replace("CustomAssetFactoryName", FactoryClassName);

            string FactoryHeader = GlobalFunction.FindResourceTextFile("BoilerplateFactory Header");
            FactoryHeader = FactoryHeader.Replace("CustomAssetName", ClassName);
            FactoryHeader = FactoryHeader.Replace("CustomAssetFactoryName", FactoryClassName); 

            string ModuleDirectory = EditorModuleSelector.Directory;
            File.WriteAllText(ModuleDirectory + "/Public/"  + FactoryClassName + ".h", FactoryHeader);
            File.WriteAllText(ModuleDirectory + "/Private/" + FactoryClassName + ".cpp", FactoryCPP);
            #endregion
        }

        private void CreateAssetActions()
        {
            string APIRef = EditorModuleSelector.FileName.ToUpper();
            APIRef = APIRef.Substring(0, APIRef.IndexOf(".")) + "_API";
            Console.WriteLine(APIRef);

            string CustomAssetAssetName = CreateType_CB.IsChecked? NewAssetName.InputText: SelectExistingClass_SB.FileName;
            string CustomAssetActionName = CustomAssetAssetName + "Actions";
            string ModuleDirectory = EditorModuleSelector.Directory;

            #region Create Actions Header
            //string ActionHeader = File.ReadAllText(@"../../BoilerplateCode/AssetActions/.txt");
            string ActionHeader = GlobalFunction.FindResourceTextFile("BoilerplateAssetTypeActions Header");
            ActionHeader = ActionHeader.Replace("ProjectNameAPI", APIRef);
            ActionHeader = ActionHeader.Replace("CustomAssetActionName", CustomAssetActionName);
            File.WriteAllText(ModuleDirectory + "/Public/" + CustomAssetActionName + ".h", ActionHeader);
            #endregion

            #region Create C++ file
            //string ActionCPP = File.ReadAllText(@"../../BoilerplateCode/AssetActions/BoilerplateAssetTypeActions CPP.txt");
            string ActionCPP = GlobalFunction.FindResourceTextFile("BoilerplateAssetTypeActions CPP");
            ActionCPP = ActionCPP.Replace("CustomAssetName", CustomAssetAssetName);
            ActionCPP = ActionCPP.Replace("CustomAssetActionName", CustomAssetActionName);

            //Settings
            ActionCPP = ActionCPP.Replace("BoilerPlate_Category", (string)AssetCategory_SB.Select_CB.SelectedValue);
            ActionCPP = ActionCPP.Replace("BoilerPlate_HexColor", (string)Color_SB.Select_CB.SelectedValue);
            ActionCPP = ActionCPP.Replace("BoilerPlate_EditorName", EditorDisplayName.InputText);
            ActionCPP = ActionCPP.Replace("BoilerPlate_EditorDescription", EditorTooltip.InputText);

            //Actions
            bool HasActions = ActionList_SP.Children.Count > 0;
            ActionCPP = ActionCPP.Replace("BoilerPlate_HasActions", HasActions ? "true" : "false");

            string ActionList = "";
            //string BoilerplateAction = File.ReadAllText(@"../../BoilerplateCode/AssetActions/BoilerplateContextMenuAction.txt");
            string BoilerplateAction = GlobalFunction.FindResourceTextFile("BoilerplateContextMenuAction");
            IEnumerable<ContextActionComponent> Actions = ActionList_SP.Children.OfType<ContextActionComponent>();

            foreach (ContextActionComponent i in Actions)
            {
                string NewAction = string.Copy(BoilerplateAction);
                NewAction = NewAction.Replace("BoilerPlate_RightClickActionName", i.Name_TB.Text);
                NewAction = NewAction.Replace("BoilerPlate_RightClickActionDescription", i.Tooltip_TB.Text);
                ActionList += (NewAction + Environment.NewLine);
            }
            ActionCPP = ActionCPP.Replace("BoilerPlate_ActionsList", ActionList);
            File.WriteAllText(ModuleDirectory + "/Private/" + CustomAssetActionName + ".cpp", ActionCPP);
            #endregion

            //UnImplemented
            #region Register asset actions with module
            //string InitFile = File.ReadAllText(ModuleDirectory + "/Public/" + EditorModuleSelector.FileName + ".cpp");
            #endregion
        }

        private void UseEditorSettings_SB_MyCheckedChangeEventHandler(bool IsChecked)
        {
            ContentHolder_sp.IsEnabled = IsChecked;
        }

        private void Details_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/SamCarey99/UE4-Tools-Code-Samples/blob/master/Register%20a%20FAssetTypeActions_Base%20class.md");
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            ContextActionEditor.Visibility = Visibility.Hidden;
            NumberOfActions_TB.Text = "Number of Context Actions: " + ActionList_SP.Children.Count;
        }
        private void OpenEditor_Click(object sender, RoutedEventArgs e)
        {
            ContextActionEditor.Visibility = Visibility.Visible;
        }
    }
}