using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//////using SWS;
////using UnityEngine.EventSystems;
//////using DG.Tweening;

public class gameplay : MonoBehaviour
{
    public GameObject[] playerPostions, players, levels, road, cutScenes, arrows, ActiveSongImage, ActiveControls, AfterSceneAssets, HeadLights, Wippers, AC, TV, instructions, RainParticle;
    public GameObject playerCamera, fullControls, bridge, seatBeltPanel, blackPanel, environment, PlaySongBtn, PauseSongBtn, StartBtn, SkipBtn, HLOnBtn, HLOffBtn, WipperOnBtn, WipperOffBtn, instructionsBtn, SnowParticles;
    public Material nightSkybox, rainSkybox, daySkybox, fogSkybox, nightModeRoadMaterial, firstLevelBridgeMaterial, normalRoadMaterial, normalBridgeMaterial;
    public Material snowRoad, rainRoad, snowTexture;
    public Material[] SnowMaterial, NormalMaterial;
    public float[] Duration;
    public AudioSource song, startEngineSound, SeatBeltSound, BtnSound;
    public AudioSource[] Songs;
    public int CurrentSongIndex = 0;
    public Player Instance;
    public Text LevelText;
    public Slider soundSlider;
    public mainmenu instance;
    void Start()
    {
        soundSlider.value = mainmenu.soundVolume;
        Time.timeScale = 1f;
        Application.targetFrameRate = 0;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        players[VehicleSelection._index].SetActive(true);
        players[VehicleSelection._index].transform.position = playerPostions[levelSelection.levelNo].transform.position;
        players[VehicleSelection._index].transform.rotation = playerPostions[levelSelection.levelNo].transform.rotation;
        levels[levelSelection.levelNo].SetActive(true);
        StartCoroutine(ShowInstructions());
        LevelText.text = (levelSelection.levelNo + 1).ToString();
        ControlsFunction();
    }
    public IEnumerator ShowInstructions()
    {
        instructions[levelSelection.levelNo].SetActive(true);
        yield return new WaitForSeconds(5f);
        instructionsBtn.SetActive(true);
    }
    public void hideInstruction()
    {
        instructions[levelSelection.levelNo].SetActive(false);
        instructionsBtn.SetActive(false);
        fullControls.SetActive(true);
    }

    public void ControlsFunction()
    {
        int PlayerControlsIndex = PlayerPrefs.GetInt("PlayerControl");
        Debug.Log("Controls Index " + PlayerControlsIndex);
        ActiveControls[PlayerControlsIndex].SetActive(true);
        if (PlayerControlsIndex == 0)
        {
            RCC.SetMobileController(RCC_Settings.MobileController.SteeringWheel);
            ActiveControls[0].SetActive(true);
            ActiveControls[1].SetActive(false);
            ActiveControls[2].SetActive(false);
        }
        else if (PlayerControlsIndex == 1)
        {
            RCC.SetMobileController(RCC_Settings.MobileController.TouchScreen);
            ActiveControls[0].SetActive(false);
            ActiveControls[1].SetActive(true);
            ActiveControls[2].SetActive(false);
        }
        else
        {
            RCC.SetMobileController(RCC_Settings.MobileController.Gyro);
            ActiveControls[0].SetActive(false);
            ActiveControls[1].SetActive(false);
            ActiveControls[2].SetActive(true);
        }
    }
    public void StartEngine()
    {
        CameraShake.instance.StartShake(0.5f, 0.1f);
        startEngineSound.Play();
        busEngineSoundsOn();
        PlaySong();
        SeatBeltSound.Play();
        hideInstruction();
    }
    public void busEngineSoundsOff()
    {
        players[VehicleSelection._index].GetComponent<RCC_CarControllerV3>().idleEngineSoundVolume = 0f;
        players[VehicleSelection._index].GetComponent<RCC_CarControllerV3>().maxEngineSoundVolume = 0f;
        players[VehicleSelection._index].GetComponent<RCC_CarControllerV3>().minEngineSoundVolume = 0f;
    }
    public void busEngineSoundsOn()
    {
        players[VehicleSelection._index].GetComponent<RCC_CarControllerV3>().idleEngineSoundVolume = 1f;
        players[VehicleSelection._index].GetComponent<RCC_CarControllerV3>().maxEngineSoundVolume = 1f;
        players[VehicleSelection._index].GetComponent<RCC_CarControllerV3>().minEngineSoundVolume = 1f;
    }
    public void restartLevel()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void nextLevel()
    {
        if (levelSelection.levelNo == 4)
        {
            SceneManager.LoadScene("mainmenu");
        }
        else if (levelSelection.levelNo < levels.Length - 1)
        {
            levelSelection.levelNo++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AudioListener.volume = 1;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void changeSkyBoxToNight()
    {
        //int materialIndex = 3;
        RenderSettings.skybox = nightSkybox;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 10f;
        RenderSettings.fogEndDistance = 100f;
        RenderSettings.fogColor = new Color(0f, 0f, 0f, 0f);
        //bridge.GetComponent<Renderer>().materials[materialIndex] = firstLevelBridgeMaterial;
        //foreach (GameObject snowroads in road)
        //{
        //    snowroads.GetComponent<Renderer>().material = nightModeRoadMaterial;
        //}
        //environment.GetComponent<Renderer>().materials = NormalMaterial;
        HeadLightsOn();
        RainParticle[VehicleSelection._index].SetActive(false);
        SnowParticles.SetActive(false);


    }
    public void changeSkyBoxToRain()
    {
        RenderSettings.skybox = rainSkybox;
      //foreach (GameObject snowroads in road)
      //{
      //    snowroads.GetComponent<Renderer>().material = rainRoad;
      //}
        RenderSettings.fog = false;
        RainParticle[VehicleSelection._index].SetActive(true);
        //environment.GetComponent<Renderer>().materials = NormalMaterial;
        SnowParticles.SetActive(false);

    }
    public void changeSkyBoxToDay()
    {
       // int materialIndex = 3;
        RenderSettings.skybox = daySkybox;
       // foreach (GameObject snowroads in road)
       // {
       //     snowroads.GetComponent<Renderer>().material = normalRoadMaterial;
       // }
       // bridge.GetComponent<Renderer>().materials[materialIndex] = normalBridgeMaterial;
       // environment.GetComponent<Renderer>().materials = NormalMaterial;
        RenderSettings.fog = false;
        HeadLightsOff();
        RainParticle[VehicleSelection._index].SetActive(false);
        SnowParticles.SetActive(false);

    }
    public void changeSkyBoxToSnow()
    {
        RenderSettings.skybox = fogSkybox;
      // foreach (GameObject snowroads in road)
      // {
      //     snowroads.GetComponent<Renderer>().material = snowRoad;
      // }
      // environment.GetComponent<Renderer>().materials = SnowMaterial;
        RenderSettings.fog = false;
        RainParticle[VehicleSelection._index].SetActive(false);
        SnowParticles.SetActive(true);
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
        AudioListener.volume = 1;
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        busEngineSoundsOff();
        StopSong();
        SeatBeltSound.Pause();
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        busEngineSoundsOn();
        PlaySong();
        SeatBeltSound.Play();
    }
    public void controlsChange()
    {
        switch (RCC_Settings.Instance.mobileController)
        {
            case RCC_Settings.MobileController.SteeringWheel:
                RCC.SetMobileController(RCC_Settings.MobileController.TouchScreen);
                PlayerPrefs.SetInt("PlayerControl", 1);
                ActiveControls[0].SetActive(false);
                ActiveControls[1].SetActive(true);
                ActiveControls[2].SetActive(false);
                break;

            case RCC_Settings.MobileController.TouchScreen:
                RCC.SetMobileController(RCC_Settings.MobileController.Gyro);
                PlayerPrefs.SetInt("PlayerControl", 2);
                ActiveControls[0].SetActive(false);
                ActiveControls[1].SetActive(false);
                ActiveControls[2].SetActive(true);

                break;

            case RCC_Settings.MobileController.Gyro:
                RCC.SetMobileController(RCC_Settings.MobileController.SteeringWheel);
                PlayerPrefs.SetInt("PlayerControl", 0);
                ActiveControls[0].SetActive(true);
                ActiveControls[1].SetActive(false);
                ActiveControls[2].SetActive(false);

                break;
        }
    }
    public void SetControlsToSteering()
    {
        PlayerPrefs.SetInt("PlayerControl", 0);
        RCC.SetMobileController(RCC_Settings.MobileController.SteeringWheel);
        PlayerPrefs.Save();
        ActiveControls[0].SetActive(true);
        ActiveControls[1].SetActive(false);
        ActiveControls[2].SetActive(false);
    }
    public void SetControlsToButtons()
    {
        PlayerPrefs.SetInt("PlayerControl", 1);
        RCC.SetMobileController(RCC_Settings.MobileController.TouchScreen);
        PlayerPrefs.Save();
        ActiveControls[0].SetActive(false);
        ActiveControls[1].SetActive(true);
        ActiveControls[2].SetActive(false);
    }
    public void SetControlsToGyro()
    {
        PlayerPrefs.SetInt("PlayerControl", 2);
        RCC.SetMobileController(RCC_Settings.MobileController.Gyro);
        PlayerPrefs.Save();
        ActiveControls[0].SetActive(false);
        ActiveControls[1].SetActive(false);
        ActiveControls[2].SetActive(true);
    }
    public void seatBeltAnimation()
    {
        StartCoroutine(SeatBelt());
    }
    public IEnumerator SeatBelt()
    {
        seatBeltPanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        seatBeltPanel.SetActive(false);
        SeatBeltSound.Stop();
    }
    public void SongsSelection(int index)
    {
        PlaySongBtn.SetActive(false);
        PauseSongBtn.SetActive(true);
        Songs[CurrentSongIndex].Pause();
        ActiveSongImage[CurrentSongIndex].SetActive(false);
        CurrentSongIndex = index;
        Songs[CurrentSongIndex].Play();
        ActiveSongImage[CurrentSongIndex].SetActive(true);
    }
    public void NextSong()
    {
        PlaySongBtn.SetActive(false);
        PauseSongBtn.SetActive(true);
        Songs[CurrentSongIndex].Pause();
        ActiveSongImage[CurrentSongIndex].SetActive(false);
        CurrentSongIndex++;
        if (CurrentSongIndex >= Songs.Length)
        {
            CurrentSongIndex = 0;
        }
        Songs[CurrentSongIndex].Play();
        ActiveSongImage[CurrentSongIndex].SetActive(true);
    }
    public void PreviousSong()
    {
        PlaySongBtn.SetActive(false);
        PauseSongBtn.SetActive(true);
        Songs[CurrentSongIndex].Pause();
        ActiveSongImage[CurrentSongIndex].SetActive(false);
        CurrentSongIndex--;
        if (CurrentSongIndex < 0)
        {
            CurrentSongIndex = Songs.Length - 1;
        }
        Songs[CurrentSongIndex].Play();
        ActiveSongImage[CurrentSongIndex].SetActive(true);
    }
    public void StopSong()
    {
        Songs[CurrentSongIndex].Pause();
        PauseSongBtn.SetActive(false);
        PlaySongBtn.SetActive(true);
    }
    public void PlaySong()
    {
        Songs[CurrentSongIndex].Play();
        PauseSongBtn.SetActive(true);
        PlaySongBtn.SetActive(false);
    }
    public void HeadLightsOn()
    {
        HeadLights[VehicleSelection._index].SetActive(true);
        HLOnBtn.SetActive(false);
        HLOffBtn.SetActive(true);
    }
    public void HeadLightsOff()
    {
        HeadLights[VehicleSelection._index].SetActive(false);
        HLOnBtn.SetActive(true);
        HLOffBtn.SetActive(false);
    }
    public void WipperOn()
    {
        Wippers[VehicleSelection._index].GetComponent<Animator>().enabled = false;
        WipperOnBtn.SetActive(false);
        WipperOffBtn.SetActive(true);
    }
    public void WipperOff()
    {
        Wippers[VehicleSelection._index].GetComponent<Animator>().enabled = false;
        WipperOnBtn.SetActive(true);
        WipperOffBtn.SetActive(false);
    }
}