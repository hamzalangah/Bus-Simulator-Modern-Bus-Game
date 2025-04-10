using UnityEditor;
using UnityEngine;
using System.IO;

public class SceneBundleBuilder
{
    [MenuItem("Tools/Build Scene AssetBundle")]
    static void BuildSceneAssetBundle()
    {
        string bundleFolder = Path.Combine(Application.dataPath, "../BuiltScenes");
        if (!Directory.Exists(bundleFolder))
            Directory.CreateDirectory(bundleFolder);

        string scenePath = "Assets/Scenes/gameplay.unity";

        AssetBundleBuild bundleBuild = new AssetBundleBuild
        {
            assetBundleName = "gameplaybundle",
            assetNames = new[] { scenePath }
        };

        BuildPipeline.BuildAssetBundles(
            bundleFolder,
            new AssetBundleBuild[] { bundleBuild },
            BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.Android
        );

        Debug.Log("Scene AssetBundle built at: " + bundleFolder);
    }
}
