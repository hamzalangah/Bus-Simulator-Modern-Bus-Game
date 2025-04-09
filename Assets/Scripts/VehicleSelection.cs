using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VehicleSelection : MonoBehaviour
{
    public GameObject[] Panel, Vehicle, specifications;
    public int[] VehiclePrice;
    public GameObject buyButton, selectButton, LockImage, LeftBtn, RightBtn, LoadingPanel, NoInternet; //adBtn;
    public Text totalCoins, price;
    public static int _index;
    void Start()
    {
        Time.timeScale = 1.0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PlayerPrefs.SetInt("Vehicle0", 1);
        totalCoins.text = PlayerPrefs.GetInt("TotalCash").ToString();
        if (PlayerPrefs.GetInt("UnlockAllCars") == 1)
        {
            for (int i = 0; i < Vehicle.Length; i++)
            {
                PlayerPrefs.SetInt("Vehicle" + i, 1);
            }
        }
        SetVehicle();
    }
    public void Left()
    {
        if (_index > 0)
        {
            _index--;
        }
        if (_index <= 0)
        {
            print("No more Buses");
        }
        SetVehicle();
        HideLeftBtn();
        HideRightBtn();
    }
    public void Right()
    {
        if (_index < Vehicle.Length - 1)
        {
            _index++;
        }
        if (_index >= 2)
        {
            print("No more Buses");
        }
        SetVehicle();
        HideLeftBtn();
        HideRightBtn();
    }
    public void HideLeftBtn()
    {
        if (_index <= 0)
        {
            LeftBtn.SetActive(false);
        }
        else if (_index <= Vehicle.Length - 1)
        {
            LeftBtn.SetActive(true);
        }
        else
        {
            LeftBtn.SetActive(true);
        }
    }
    public void HideRightBtn()
    {
        if (_index <= 0)
        {
            RightBtn.SetActive(true);
        }
        else if (_index >= Vehicle.Length - 1)
        {
            RightBtn.SetActive(false);
        }
        else
        {
            RightBtn.SetActive(true);
        }
    }
    public void Buy()
    {
        if (PlayerPrefs.GetInt("Vehicle" + _index) != 1)
        {
            if (PlayerPrefs.GetInt("TotalCash") >= VehiclePrice[_index])
            {
                PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") - VehiclePrice[_index]);
                PlayerPrefs.SetInt("Vehicle" + _index, 1);
                Panel[1].SetActive(true);
            }
            else
            {
                Panel[0].SetActive(true);
            }
        }
        SetVehicle();
    }
    public void Ok()
    {
        Panel[0].SetActive(false);
        Panel[1].SetActive(false);
        Panel[2].SetActive(false);
    }
    public void SelectVehicle()
    {

        StartCoroutine(LevelsPlay());
    }
    public IEnumerator LevelsPlay()
    {
        LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        //AdsManager.instance.showAdmobInterstitial();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("levelsplay");
    }
    void SetVehicle()
    {
        if (PlayerPrefs.GetInt("Vehicle" + _index) != 1)
        {
            price.text = VehiclePrice[_index].ToString();
            buyButton.SetActive(true);
            selectButton.SetActive(false);
           // adBtn.SetActive(true);
            LockImage.SetActive(true);
        }
        else
        {
            price.text = "Owned";
            buyButton.SetActive(false);
           // adBtn.SetActive(false);
            selectButton.SetActive(true);
            LockImage.SetActive(false);
        }
        totalCoins.text = PlayerPrefs.GetInt("TotalCash").ToString();
        DisableAllVehicles();
        Vehicle[_index].SetActive(true);
        specifications[_index].SetActive(true);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void DisableAllVehicles()
    {
        for (int i = 0; i < Vehicle.Length; i++)
        {
            Vehicle[i].SetActive(false);
            specifications[i].SetActive(false);
        }
    }
    //public void WatchRewardeAd()
    //{
    //    NetworkReachability reachability = Application.internetReachability;

    //    if (reachability != NetworkReachability.NotReachable)
    //    {
    //        if (AdsManager.instance.checkIfAdmobRewardedInterstitialIsLoaded())
    //        {

    //            AdsManager.instance.ShowAdmobRewardedInterstitial();
    //            if (PlayerPrefs.GetInt("Vehicle" + _index) != 1)
    //            {
    //                PlayerPrefs.SetInt("Vehicle" + _index, 1);
    //                Panel[1].SetActive(true);
    //            }
    //            else
    //            {
    //                Panel[0].SetActive(true);
    //            }
    //            SetVehicle();
    //        }
    //        else
    //        {
    //            Panel[2].SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        Panel[2].SetActive(true);
    //    }
    //}

}