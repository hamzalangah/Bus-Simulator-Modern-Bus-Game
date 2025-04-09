using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteSceneTester : MonoBehaviour
{
    public string bundleURL = "https://yourserver.com/bundles/gameplay.bundle";

    void Start()
    {
        StartCoroutine(DownloadAndLogScenePaths());
    }

    IEnumerator DownloadAndLogScenePaths()
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Bundle download failed: " + request.error);
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        string[] scenes = bundle.GetAllScenePaths();

        Debug.Log("Scenes in bundle:");
        foreach (string scene in scenes)
        {
            Debug.Log(scene);
        }

        // Optional: Load the scene
        // string sceneName = Path.GetFileNameWithoutExtension(scenes[0]);
        // SceneManager.LoadSceneAsync(sceneName);
    }
}
