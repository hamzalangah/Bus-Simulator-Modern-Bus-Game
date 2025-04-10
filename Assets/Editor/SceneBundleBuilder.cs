using UnityEditor;
using UnityEngine;
using System.IO;

public class SceneBundleBuilder
{
    [MenuItem("Tools/Build Scene Bundle")]
    static void BuildSceneBundle()
    {
        string outputPath = Path.Combine(Application.dataPath, "../BuiltBundles");
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        string[] scenes = { "Assets/Scenes/gameplay.unity" };

        BuildPipeline.BuildPlayer(
            scenes,
            Path.Combine(outputPath, "gameplay"),
            BuildTarget.Android, // or StandaloneWindows64, etc.
            BuildOptions.BuildAdditionalStreamedScenes | BuildOptions.CompressWithLz4
        );

        Debug.Log("Scene bundle built successfully.");
    }
}
