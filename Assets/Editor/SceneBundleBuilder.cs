using UnityEditor;
using UnityEngine;
using System.IO;

public class SceneBundleBuilder
{
    [MenuItem("Tools/Build Scene Bundle")]
    static void BuildSceneBundle()
    {
        // Save bundle outside of Assets folder
        string bundlePath = Path.Combine(Application.dataPath, "../BuiltScenes");
        if (!Directory.Exists(bundlePath))
            Directory.CreateDirectory(bundlePath);

        string[] scenePaths = { "Assets/Scenes/gameplay.unity" }; // Change to your actual scene path

        string bundleOutput = Path.Combine(bundlePath, "gameplay.bundle");

        BuildPipeline.BuildPlayer(
            scenePaths,
            bundleOutput,
            BuildTarget.Android,
            BuildOptions.BuildAdditionalStreamedScenes | BuildOptions.CompressWithLz4
        );

        Debug.Log("Scene bundle built at: " + bundleOutput);
    }
}
