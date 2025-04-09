using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneBundleLoader : MonoBehaviour
{
    // URL to the AssetBundle on GitHub
    string bundleURL = "https://raw.githubusercontent.com/hamzalangah/Bus-Simulator-Modern-Bus-Game/b7f9a3be8994bdac85772bcf1313ea7036f0cfcf/BuiltScenes/gameplay.bundle";

    void Start()
    {
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
