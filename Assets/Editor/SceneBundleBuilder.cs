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

        // Ensure all necessary shaders are included
        IncludeShadersInBundle(scenePaths);

        string bundleOutput = Path.Combine(bundlePath, "gameplay.bundle");

        BuildPipeline.BuildPlayer(
            scenePaths,
            bundleOutput,
            BuildTarget.Android,
            BuildOptions.BuildAdditionalStreamedScenes | BuildOptions.CompressWithLz4
        );

        Debug.Log("Scene bundle built at: " + bundleOutput);
    }

    static void IncludeShadersInBundle(string[] scenePaths)
    {
        // Go through each scene and find all the materials and shaders
        foreach (string scenePath in scenePaths)
        {
            UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(scenePath);
            if (!scene.isLoaded)
                continue;

            var rootGameObjects = scene.GetRootGameObjects();
            foreach (var go in rootGameObjects)
            {
                Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    Material[] materials = renderer.sharedMaterials;
                    foreach (var material in materials)
                    {
                        Shader shader = material.shader;
                        string shaderPath = AssetDatabase.GetAssetPath(shader);
                        // Make sure shaders are included
                        if (!string.IsNullOrEmpty(shaderPath))
                        {
                            AssetDatabase.ImportAsset(shaderPath);
                        }
                    }
                }
            }
        }
    }
}
