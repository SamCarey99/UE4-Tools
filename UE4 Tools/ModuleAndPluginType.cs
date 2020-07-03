using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UE4_Tools
{
    /// <summary>
    /// Defines a list if all types which can be used while creating a Module or Plugin
    /// </summary>
    public enum ModuleAndPluginTypes
    {
        Runtime,
        RuntimeNoCommandlet,
        Developer,
        Editor,
        EditorNoCommandlet,
        Program
    }
    /// <summary>
    /// Defines a list if all LoadingPhases which can be used while creating a Module or Plugin
    /// </summary>
    public enum LoadingPhases 
    {
        EarliestPossible,
        PostConfigInit,
        PreEarlyLoadingScreen,
        PreLoadingScreen,
        PreDefault,
        Default,
        PostDefault,
        PostEngineInit,
        None,
        Max
    }

    /// <summary>
    /// List of default presets which can be used when creating a module
    /// </summary>
    public enum ModulePrest
    {
        Custom,
        SimpleRuntime,
        EditorExtension
    }

    /// <summary>
    /// List of categories that a new asset can be placed in
    /// </summary>
    public enum AssetTypeCategories
    {
        None,
        Basic,
        Animation,
        MaterialsAndTextures,
        Sounds,
        Physics,
        UI,
        Misc,
        Gameplay,
        Blueprint,
        Media,
    }

    /// <summary>
    /// Custom asset colours supported in the UE4 editor
    /// </summary>
    public enum UE4SupportedColors
    {
        Black,
        Blue,
        Cyan,
        Emerald,
        Green,
        Magenta,
        Orange,
        Purple,
        Red,
        Silver,
        Turquoise,
        White,
        Yellow
    }
}
