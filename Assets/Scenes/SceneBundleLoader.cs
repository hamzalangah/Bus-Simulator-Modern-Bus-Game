using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneBundleLoader : MonoBehaviour
{
    // URL to the AssetBundle on GitHub
    string bundleURL = "https://github.com/hamzalangah/Bus-Simulator-Modern-Bus-Game/raw/refs/heads/main/BuiltBundles/gameplay";

    // Add a reference to your UI Text or Slider for progress indication
    public UnityEngine.UI.Text progressText;  // or a Slider, etc.

    void Start()
    {
        StartCoroutine(DownloadAndLoadScene());
    }

    IEnumerator DownloadAndLoadScene()
    {
        Debug.Log("Downloading scene bundle...");
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        request.SendWebRequest();

        // Loop to show download progress
        while (!request.isDone)
        {
            float progress = request.downloadProgress;
            Debug.Log($"Download Progress: {progress * 100f}%");

            // Update UI Text or ProgressBar with the current progress
            if (progressText != null)
            {
                progressText.text = $"Downloading: {progress * 100f:0.0}%";
            }

            yield return null;
        }

        // Check if the download was successful
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Download failed: " + request.error);
            yield break;
        }

        // Get AssetBundle content
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        string[] scenes = bundle.GetAllScenePaths();
        Debug.Log("Scenes found in bundle: " + string.Join(", ", scenes));

        if (scenes.Length > 0)
        {
            string scenePath = scenes[0];
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            Debug.Log("Loading scene: " + sceneName);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            yield return asyncOperation;

            // Fix pink shaders AFTER scene loads
            FixPinkShaders();
        }
        else
        {
            Debug.LogError("No scenes found in the bundle!");
        }
    }

    void FixPinkShaders()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        Material defaultMat = new Material(Shader.Find("Standard")); // Or "Mobile/Diffuse"

        foreach (Renderer r in renderers)
        {
            if (r.sharedMaterial != null && (r.sharedMaterial.shader == null || r.sharedMaterial.color == Color.magenta))
            {
                r.material = defaultMat;
            }
        }

        Debug.Log("Pink shaders (if any) replaced with default material.");
    }
}