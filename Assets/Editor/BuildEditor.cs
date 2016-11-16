using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;

public class BuildEditor
{
    static string[] sceneNames = FindEnabledEditorScenes();
    [MenuItem("Build/Windows(x86)")]
    static void PerformWindowsx86Build()
    {
        string targetDir = "Build/Windows(x86)/" + PlayerSettings.productName + ".exe";
        GenericBuild(targetDir, BuildTarget.StandaloneWindows, BuildOptions.None);
;    }
    static void GenericBuild(string targetDir, BuildTarget buildTarget, BuildOptions buildOptions)
    {
        //switch the platform to target platform
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
        //Perform the build
        string result = BuildPipeline.BuildPlayer(sceneNames, targetDir, buildTarget, buildOptions);
        if (result.Length > 0)
        {
            string error = "Build Failure: " + result;
            //print error to unity
            Debug.LogError(error);
            //print error to console
            System.Console.WriteLine(error);
        }

    }

    static string[] FindEnabledEditorScenes()
    {
        List<string> editorScenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            //check if the current scene is enabled
            if (scene.enabled)
            {
                //Add it to the list
                editorScenes.Add(scene.path);
            }
        }
        return editorScenes.ToArray();
    }

}
#endif