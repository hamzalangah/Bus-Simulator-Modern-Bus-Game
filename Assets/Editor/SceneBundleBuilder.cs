using UnityEditor;
using System.IO;

public class FullBundleBuilder
{
    [MenuItem("Tools/Build All Asset Bundles")]
    static void BuildAllBundles()
    {
        string bundleDir = "Assets/../BuiltBundles";
        if (!Directory.Exists(bundleDir))
            Directory.CreateDirectory(bundleDir);

        BuildPipeline.BuildAssetBundles(bundleDir, BuildAssetBundleOptions.None, BuildTarget.Android);
        UnityEngine.Debug.Log("All bundles built at: " + bundleDir);
    }
}
