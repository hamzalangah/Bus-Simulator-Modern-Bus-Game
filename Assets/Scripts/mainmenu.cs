using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public Text PlayerName, coinText;
    public GameObject[] PlayerProfile, ActiveControls;
    public Slider soundSlider, musicSlider;
    public static float soundVolume = 1, musicVolume = 1;
    public AudioSource gameSound, ButtonSound;
    public static int PlayerControlsIndex = 1;
    public Material[] Skyboxes; 
    void Start()
    {
        gameSound.volume = musicVolume;
        ButtonSound.volume = soundVolume;
        PlayerName.text = PlayerPrefs.GetString("PlayerName");
        int PlayerProfileIndex = PlayerPrefs.GetInt("PlayerImage");
        PlayerProfile[PlayerProfileIndex].SetActive(true);
        Debug.Log(PlayerProfileIndex);
        coinText.text = PlayerPrefs.GetInt("TotalCash").ToString();
        if (PlayerPrefs.HasKey("PlayerControl"))
        {
            PlayerControlsIndex = PlayerPrefs.GetInt("PlayerControl");
        }
        else
        {
            PlayerControlsIndex = 1;
            PlayerPrefs.SetInt("PlayerControl", PlayerControlsIndex);
        }
        if (PlayerControlsIndex >= 0 && PlayerControlsIndex < ActiveControls.Length && ActiveControls[PlayerControlsIndex] != null)
        {
            ActiveControls[PlayerControlsIndex].SetActive(true);
            PlayerPrefs.SetInt("PlayerControl", PlayerControlsIndex);
        }
        else
        {
            ActiveControls[1].SetActive(true);
            PlayerPrefs.SetInt("PlayerControl", 1);
        }
        StartCoroutine(ChangeSkyboxes());
    }
   IEnumerator ChangeSkyboxes()
    {
        yield return new WaitForSeconds(5f);
        RenderSettings.skybox = Skyboxes[1];
        yield return new WaitForSeconds(5f);
        RenderSettings.skybox = Skyboxes[2];
        yield return new WaitForSeconds(5f);
        RenderSettings.skybox = Skyboxes[1];
        yield return new WaitForSeconds(5f);
        RenderSettings.skybox = Skyboxes[0];
        StartCoroutine( ChangeSkyboxes());
    }
    public void GameSound()
    {
        musicVolume = musicSlider.value;
    }
    public void BtnSound()
    {
        soundVolume = soundSlider.value;
    }
    public void goToLevelScene()
    {
        //AdsManager.instance.showAdmobInterstitial();
        SceneManager.LoadScene("levelsplay");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Setting()
    {
        //AdsManager.instance.showAdMobRectangleBannerBottomLeft();
    }
    public void Save()
    {
        //AdsManager.instance.hideAdmobBottomLeftBanner();
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://gladiatorxstudios.blogspot.com/2025/02/at-gladiatorx-studios-accessible-from.html");
    }
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=GladiatorX+Studios");
    }
    public void SetControlsToSteering()
    {
        PlayerPrefs.SetInt("PlayerControl", 0);
        PlayerPrefs.Save();
        ActiveControls[0].SetActive(true);
        ActiveControls[1].SetActive(false);
        ActiveControls[2].SetActive(false);
    }
    public void SetControlsToButtons()
    {
        PlayerPrefs.SetInt("PlayerControl", 1);
        PlayerPrefs.Save();
        ActiveControls[0].SetActive(false);
        ActiveControls[1].SetActive(true);
        ActiveControls[2].SetActive(false);
    }
    public void SetControlsToGyro()
    {
        PlayerPrefs.SetInt("PlayerControl", 2);
        PlayerPrefs.Save();
        ActiveControls[0].SetActive(false);
        ActiveControls[1].SetActive(false);
        ActiveControls[2].SetActive(true);
    }
}