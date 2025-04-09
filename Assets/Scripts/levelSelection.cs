using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSelection : MonoBehaviour
{
    public GameObject[] levelBtn;
    public static int levelNo;
    public Text  coinText, coinText2;
    public GameObject loadingPanel;
    public AudioSource GameSound, BtnSound;
    public mainmenu instance;
    private SceneLoader sceneLoader;
    void Start()
    {
        GameSound.volume = mainmenu.musicVolume;
        BtnSound.volume = mainmenu.soundVolume;
        coinText.text = PlayerPrefs.GetInt("TotalCash").ToString();
        coinText2.text = PlayerPrefs.GetInt("TotalCash").ToString();
        int a;
        a = PlayerPrefs.GetInt("Level");
        unlock(a);
        sceneLoader = GetComponent<SceneLoader>();
        Debug.Log(a);
    }
    public void selectLevel(int level)
    {
       
        loadingPanel.SetActive(true);
        levelNo = level;
        StartCoroutine(GoToGamePlay());
    }
    public IEnumerator GoToGamePlay()
    {
        //AdsManager.instance.showAdmobInterstitial();
        yield return new WaitForSeconds(1f);
        if (sceneLoader == null)
        {
            GameObject loaderObject = new GameObject("SceneLoaderObject");
            sceneLoader = loaderObject.AddComponent<SceneLoader>();
        }
        sceneLoader.LoadSceneAsync("gameplay");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
    void unlock(int a)
    {
        for (int i = 1; i <= a; i++)
        {
            levelBtn[i].GetComponent<Button>().enabled = true;
            levelBtn[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}