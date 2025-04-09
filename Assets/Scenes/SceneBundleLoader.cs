using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneBundleLoader : MonoBehaviour
{
    // URL to the AssetBundle on GitHub
    string bundleURL = "https://raw.githubusercontent.com/hamzalangah/Bus-Simulator-Modern-Bus-Game/b7f9a3be8994bdac85772bcf1313ea7036f0cfcf/BuiltScenes/gameplay.bundle";

    // Renderer reference (make sure to assign this in the Unity Inspector)
    public Renderer myRenderer;

    void Start()
    {
        // If you need to set a material to a renderer, make sure 'myRenderer' is assigned
        if (myRenderer != null)
        {
            Material mat = new Material(Shader.Find("Standard")); // Replace with your shader if needed
            myRenderer.material = mat;
        }

        // Start the coroutine to download and load the scene
        StartCoroutine(DownloadAndLoadScene());
    }

    IEnumerator DownloadAndLoadScene()
    {
        Debug.Log("Downloading scene bundle...");
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
        {
            Debug.LogError("Download failed: " + request.error);
            yield break;
        }

        // Get the AssetBundle content
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        // Get all scenes in the AssetBundle
        string[] scenes = bundle.GetAllScenePaths();
        Debug.Log("Scenes found in bundle: " + string.Join(", ", scenes));

        if (scenes.Length > 0)
        {
            // Get the first scene from the AssetBundle
            string scenePath = scenes[0];
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            Debug.Log("Loading scene: " + sceneName);

            // Load the scene asynchronously from the bundle
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogError("No scenes found in the bundle!");
        }
    }
}
