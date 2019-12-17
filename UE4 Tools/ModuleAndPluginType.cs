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

    public enum ModulePrest
    {
        Custom,
        SimpleRuntime,
        EditorExtension
    }
}
