using UnityEditor;
using UnityEngine;
using System.IO;

public class AssetBundleBuilder
{
    [MenuItem("Tools/Build CutScenes AssetBundle")]
    static void BuildAssetBundles()
    {
        // Specify the output directory for the AssetBundle
        string outputPath = Path.Combine(Application.dataPath, "../BuiltBundles");
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        // Build AssetBundles
        BuildPipeline.BuildAssetBundles(
            outputPath, // Where to save the bundles
            BuildAssetBundleOptions.ChunkBasedCompression, // Use compression
            BuildTarget.Android // Or the platform you want to build for
        );

        Debug.Log("CutScenes AssetBundle built successfully.");
    }
}
