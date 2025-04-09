using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour
{
    public GameObject splashPanel1, splashPanel2, dataPrivacyPanel, activeFlagImage, activeProfileImage, loadingPanel, PlayerProfilePanel, SelectPlayerPanel, SelectCountryPanel;
    public InputField inputfield;
    public Button[] flagButtons, PlayerImages, AllBtns;
    public Button NameBtn, saveProfileBtn;
    public static int PlayerIndex = 0, CountryIndex = 0;
    public GameObject[] PlayerActiveImage, ActiveCountryImage;
    public AudioSource BtnSound;
    void Start()
    {
        StartCoroutine(SplashScreens());
        //AdsManager.instance.showAdMobBannerTopLeft();
        //AdsManager.instance.showAdMobBannerTopRight();
        string username = PlayerPrefs.GetString("PlayerName");
        int userimage = PlayerPrefs.GetInt("PlayerImage");
        int countryimage = PlayerPrefs.GetInt("CountryImage");
        if (!string.IsNullOrEmpty(username) && userimage != -1 && countryimage != -1)
        {
            loadingPanel.SetActive(true);
            StartCoroutine(LoadMainMenu());
        }
        inputfield.onValueChanged.AddListener(delegate { ActiveProfileBtn(); });
    }
    public IEnumerator SplashScreens()
    {
        yield return new WaitForSeconds(5f);
        splashPanel2.SetActive(true);
        splashPanel1.SetActive(false);
        yield return new WaitForSeconds(5f);
        dataPrivacyPanel.SetActive(true);
        splashPanel2.SetActive(false);
    }
    public IEnumerator LoadMainMenu()
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        //AdsManager.instance.showAdmobInterstitial();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("mainmenu");
    }
    public void GoToMainMenu()
    {
        StartCoroutine(ToMainMenu());
        StartCoroutine(LoadMainMenu());
    }
    public IEnumerator ToMainMenu()
    {
        PlayerPrefs.SetString("PlayerName", inputfield.text);
        PlayerPrefs.Save();
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        //AdsManager.instance.showAdmobInterstitial();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("mainmenu");
    }
    public void activeButton()
    {
        NameBtn.interactable = !string.IsNullOrEmpty(inputfield.text);
    }
    public void flagActive(int index)
    {
        CountryIndex = index;
        PlayerPrefs.SetInt("CountryName", index);
        activeFlagImage.transform.SetParent(flagButtons[index].transform);
        activeFlagImage.transform.localPosition = Vector3.zero;
        activeFlagImage.transform.localRotation = Quaternion.identity;
        activeFlagImage.transform.localScale = Vector3.one;
    }
    public void PlayerActive(int index)
    {
        PlayerIndex = index;
        PlayerPrefs.SetInt("PlayerImage", index);
        activeProfileImage.transform.SetParent(PlayerImages[index].transform);
        activeProfileImage.transform.localPosition = Vector3.zero;
        activeProfileImage.transform.localRotation = Quaternion.identity;
        activeProfileImage.transform.localScale = Vector3.one;
    }
    public void SaveProfileImage()
    {
        foreach (GameObject playerProfile in PlayerActiveImage)
        {
            playerProfile.SetActive(false);
        }
        PlayerActiveImage[PlayerIndex].SetActive(true);
        PlayerPrefs.SetInt("PlayerImage", PlayerIndex);
        PlayerPrefs.Save();
        ActiveProfileBtn();
        SelectPlayerPanel.SetActive(false);
        PlayerProfilePanel.SetActive(true);
    }
    public void SaveCountryImage()
    {
        foreach (GameObject countryProfile in ActiveCountryImage)
        {
            countryProfile.SetActive(false);
        }
        ActiveCountryImage[CountryIndex].SetActive(true);
        PlayerPrefs.SetInt("CountryImage", CountryIndex);
        PlayerPrefs.Save();
        ActiveProfileBtn();
        SelectCountryPanel.SetActive(false);
        PlayerProfilePanel.SetActive(true);
    }
    public void ActiveProfileBtn()
    {
        if (PlayerActiveImage[PlayerIndex].activeSelf && ActiveCountryImage[CountryIndex].activeSelf)
        {
            NameBtn.interactable = !string.IsNullOrEmpty(inputfield.text);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://gladiatorxstudios.blogspot.com/2025/02/at-gladiatorx-studios-accessible-from.html");
    }
}