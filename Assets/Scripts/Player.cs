using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWS;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    public Transform[] PickPos, DropPos, MidPos, levels;
    public Transform CheckPointPos;
    public GameObject[] pParking, Dparking, SmallScene, SmallSceneCamera, pickWaypoints, BusCam, FirstCutScenes, LastCutScenes, Arrows, ArrowsL5, finalCutScenes, StationGates;
    public GameObject[] SmallCutScenes, CheckPoints;
    public GameObject[] SmallCutScenesCameras;
    public GameObject[] CrossWalkPassenges;
    public GameObject[] PickingChars, PickingChars2, PickingChars3, PickingChars4, PickingChars5, PickingChars6, PickingChars7, PickingChars8, PickingChars9, PickingChars10;
    public GameObject[] SittingChar, SittingChar2, SittingChar3, SittingChar4, SittingChar5, SittingChar6, SittingChar7, SittingChar8, SittingChar9, SittingChar10;
    public GameObject[] DropChar, DropChar2, DropChar3, DropChar4, DropChar5, DropChar6, DropChar7, DropChar8, DropChar9, DropChar10;
    public GameObject Controls, Rccam, CompletePanel, skipBtn, FailedScreen, RightArrow, LeftArrow, StraightArrow, StationGate, FastBtn, BlackPanel, SceneBlackPanel;
    public GameObject Bus4;
    public Animator[] BusDoor, DriverDoor;
    //  public Animator barrier, DriverDoor;
    public AudioSource DoorSound, TurnRight, TurnLeft, GoStraight, CheckPointSound;
    public AudioSource[] Songs;
    public gameplay PlayCurrentSong;
    
    public float speed, moveSpeed;
    public float[] FirstDuration, LastDuration, FSDuration, SSDuration;
    public int ArrowsIndex = 0, dropIndex = 0, ArrowsIndexL5 = 0, CheckPointsIndex = 0;
    //  public float speed = 10, moveSpeed = 10;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TLCheckPoint")
        {
            StartCoroutine(TLCheckPoint());
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.tag== "SLAccident")
        {
            StartCoroutine(SLAccident());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "CheckPoint")
        {
            CheckPointSound.Play();
            other.gameObject.SetActive(false);
            CheckPointsIndex++;
            CheckPoints[CheckPointsIndex].SetActive(true);
        }
        if (other.gameObject.tag == "firstsmallcutscene")
        {
            StartCoroutine(FSCutSceen());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "peoplecrossing")
        {
            StartCoroutine(PeopleCrossing());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Dparking10")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking10());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Dparking11")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking11());
        }
        if (other.gameObject.tag == "Dparking12")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking12());
        }
        if (other.gameObject.tag == "Dparking13")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking13());
        }
        if (other.gameObject.tag == "Dparking14")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking14());
        }
        if (other.gameObject.tag == "Dparking15")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking15());
        }
        if (other.gameObject.tag == "Dparking16")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking16());
        }
        if (other.gameObject.tag == "Dparking17")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DParking17());
        }
        if (other.gameObject.tag == "FirstCutScene")
        {
            StartCoroutine(FirstScenes());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "TurnRight")
        {
            StartCoroutine(RightDirection());
            if (levelSelection.levelNo == 4)
            {
                ArrowsShowL5();
            }
            else
            {
                ArrowsShow();
            }
            other.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.tag == "TurnLeft")
        {
            StartCoroutine(LeftDirection());
            if (levelSelection.levelNo == 4)
            {
                ArrowsShowL5();
            }
            else
            {
                ArrowsShow();
            }
            other.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.tag == "GoStraight")
        {
            StartCoroutine(StraightDirection());
            if (levelSelection.levelNo == 4)
            {
                ArrowsShowL5();
            }
            else
            {
                ArrowsShow();
            }
            other.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.tag == "gateopen")
        {
            StationGate.GetComponent<Animator>().enabled = true;
        }
        if (other.gameObject.tag == "gateopen2")
        {
            StationGates[1].GetComponent<Animator>().enabled = true;
        }
        if (other.gameObject.tag == "gateopen3")
        {
            StationGates[2].GetComponent<Animator>().enabled = true;
        }
        if (other.gameObject.tag == "marketscene")
        {
            StartCoroutine(SSCutSceen());
        }

        if (other.gameObject.tag == "accidentScene")
        {
            StartCoroutine(TSCutSceen());
        }
        if (other.gameObject.tag == "Water")
        {
            FailedScreen.SetActive(true);
        }
        //      if(other.gameObject.tag == "Barrier")
        //      {
        //          barrier.SetBool("barrier", true);
        //      }
        if (other.gameObject.tag == "Pick")
        {

            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(PickPessangers());
        }
        if (other.gameObject.tag == "Drop")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 10f;
            StartCoroutine(DropPassengers());

        }
        //      if (other.gameObject.tag == "ElephantScene")
        //      {
        //          StartCoroutine(ElephantScene());
        //      }
        //      if(other.gameObject.tag == "checkpoint")
        //      {
        //          StartCoroutine(CheckpointScene());
        //      }
        //      if(other.gameObject.tag == "CowScene")
        //      {
        //          StartCoroutine(CowScene());
        //      }
        //      if(other.gameObject.tag == "FishingScene")
        //      {
        //          StartCoroutine(FishingScene());
        //      }
        //      if(other.gameObject.tag == "CoupleScene")
        //      {
        //          StartCoroutine(CoupleScene());
        //      }
        //      if(other.gameObject.tag == "PickMover" || other.gameObject.tag == "DropMover")
        //      {
        //          StartCoroutine(Mover());
        //      }
        //      if(other.gameObject.tag == "ShipSound")
        //      {
        //          siphorn.Play();
        //      } 
        //      if(other.gameObject.tag == "LionScene")
        //      {
        //          StartCoroutine(LionScene());
        //          
        //      }
        //      if(other.gameObject.tag == "StoneScene")
        //      {
        //          SmallScene[6].GetComponent<PlayableDirector>().enabled = true;
        //      }
        //  }
    }
    public IEnumerator TLCheckPoint()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        while (Vector3.Distance(transform.position, CheckPointPos.position) > 0.1f ||
             Quaternion.Angle(transform.rotation, CheckPointPos.rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, CheckPointPos.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, CheckPointPos.rotation, speed * Time.deltaTime);
            yield return null;
        }
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        SmallCutScenes[4].SetActive(true);
        yield return new WaitForSeconds(3f);
        DriverDoor[VehicleSelection._index].SetBool("DDOpen", true);
        yield return new WaitForSeconds(SSDuration[4]);
        yield return new WaitForSeconds(3f);
        DriverDoor[VehicleSelection._index].SetBool("DDOpen", false);
        SmallCutScenes[4].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
    }
    public IEnumerator SLAccident()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        SmallCutScenes[3].SetActive(true);
        yield return new WaitForSeconds(SSDuration[3]);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
    }
    public IEnumerator FSCutSceen()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        SmallCutScenes[0].GetComponent<PlayableDirector>().enabled = true;
        SmallCutScenesCameras[0].SetActive(true);
        yield return new WaitForSeconds(SSDuration[0]);
        SmallCutScenes[0].GetComponent<PlayableDirector>().enabled = false;
        SmallCutScenesCameras[0].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
    }

    public IEnumerator SSCutSceen()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        SmallCutScenes[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        yield return new WaitForSeconds(SSDuration[1]);
        SmallCutScenes[1].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
    }

    public IEnumerator TSCutSceen()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        SmallCutScenes[2].GetComponent<PlayableDirector>().enabled = true;
        SmallCutScenesCameras[1].SetActive(true);
        yield return new WaitForSeconds(SSDuration[2]);
        SmallCutScenes[2].GetComponent<PlayableDirector>().enabled = false;
        SmallCutScenesCameras[1].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
    }
    public IEnumerator PeopleCrossing()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        foreach (GameObject padestrain in CrossWalkPassenges)
        {
            padestrain.GetComponent<Animator>().SetBool("pwalk", true);
            padestrain.GetComponent<splineMove>().enabled = true;
        }
        yield return new WaitForSeconds(10f);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        Bus4.GetComponent<splineMove>().enabled = true;
        StationGates[1].GetComponent<Animator>().enabled = true;
    }
    public void ArrowsShow()
    {
        if (ArrowsIndex == 0)
        {
            Debug.Log("No Arrows");
        }
        else
        {
            Arrows[ArrowsIndex - 1].SetActive(false);
        }
        ArrowsIndex++;
        Arrows[ArrowsIndex].SetActive(true);
    }
    public void ArrowsShowL5()
    {
        if (ArrowsIndexL5 == 0)
        {
            Debug.Log("No Arrows");
        }
        else
        {
            ArrowsL5[ArrowsIndexL5 - 1].SetActive(false);
        }
        ArrowsIndexL5++;
        ArrowsL5[ArrowsIndexL5].SetActive(true);
    }
    public IEnumerator FirstScenes()
    {
        stopSongs();
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(2f);
        Rccam.SetActive(false);
        FirstCutScenes[levelSelection.levelNo].SetActive(true);
        yield return new WaitForSeconds(FirstDuration[levelSelection.levelNo]);
        FirstCutScenes[levelSelection.levelNo].SetActive(false);
        Rccam.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        PlayCurrentSong.PlaySong();
    }
    public IEnumerator RightDirection()
    {
        TurnRight.Play();
        RightArrow.SetActive(true);
        yield return new WaitForSeconds(3f);
        RightArrow.SetActive(false);
    }
    public IEnumerator LeftDirection()
    {
        TurnLeft.Play();
        LeftArrow.SetActive(true);
        yield return new WaitForSeconds(3f);
        LeftArrow.SetActive(false);
    }
    public IEnumerator StraightDirection()
    {
        GoStraight.Play();
        StraightArrow.SetActive(true);
        yield return new WaitForSeconds(3f);
        StraightArrow.SetActive(false);
    }
    public void stopSongs()
    {
        foreach (AudioSource song in Songs)
        {
            if (song.isPlaying)
            {
                song.Pause();
            }
        }
    }
    public IEnumerator PickPessangers()
    {
        FastBtn.SetActive(true);
        Controls.SetActive(false);
        pParking[levelSelection.levelNo].SetActive(false);
        while (Vector3.Distance(transform.position, PickPos[levelSelection.levelNo].position) > 0.1f ||
           Quaternion.Angle(transform.rotation, PickPos[levelSelection.levelNo].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PickPos[levelSelection.levelNo].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, PickPos[levelSelection.levelNo].rotation, speed * Time.deltaTime);
            yield return null;
        }
        stopSongs();
        yield return new WaitForSeconds(1);
        Rccam.SetActive(false);
        BusCam[VehicleSelection._index].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        BusDoor[VehicleSelection._index].SetBool("open", true);
        DoorSound.Play();
        yield return new WaitForSeconds(3);
        if (levelSelection.levelNo == 0)
        {
            for (int i = 0; i < PickingChars.Length; i++)
            {
                PickingChars[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars[i].GetComponent<splineMove>().enabled = true;
                PickingChars[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars[i].SetActive(false);
                SittingChar[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            Dparking[0].SetActive(true);
            PlayCurrentSong.PlaySong();
        }
        if (levelSelection.levelNo == 1)
        {
            for (int i = 0; i < PickingChars2.Length; i++)
            {
                PickingChars2[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars2[i].GetComponent<splineMove>().enabled = true;
                PickingChars2[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars2.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars2[i].SetActive(false);
                SittingChar2[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
            Dparking[1].SetActive(true);
        }
        if (levelSelection.levelNo == 2)
        {
            for (int i = 0; i < PickingChars3.Length; i++)
            {
                PickingChars3[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars3[i].GetComponent<splineMove>().enabled = true;
                PickingChars3[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars3.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars3[i].SetActive(false);
                SittingChar3[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
            
            Dparking[2].SetActive(true);
        }
        if (levelSelection.levelNo == 3)
        {
            for (int i = 0; i < PickingChars4.Length; i++)
            {
                PickingChars4[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars4[i].GetComponent<splineMove>().enabled = true;
                PickingChars4[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars4.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars4[i].SetActive(false);
                SittingChar4[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
           
            Dparking[10].SetActive(true);
        }
        if (levelSelection.levelNo == 4)
        {
            for (int i = 0; i < PickingChars5.Length; i++)
            {
                PickingChars5[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars5[i].GetComponent<splineMove>().enabled = true;
                PickingChars5[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars5.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars5[i].SetActive(false);
                SittingChar5[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
            
            Dparking[14].SetActive(true);
        }
        if (levelSelection.levelNo == 5)
        {
            for (int i = 0; i < PickingChars6.Length; i++)
            {
                PickingChars6[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars6[i].GetComponent<splineMove>().enabled = true;
                PickingChars6[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars6.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars6[i].SetActive(false);
                SittingChar6[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
           
        }
        if (levelSelection.levelNo == 6)
        {
            for (int i = 0; i < PickingChars7.Length; i++)
            {
                PickingChars7[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars7[i].GetComponent<splineMove>().enabled = true;
                PickingChars7[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars7.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars7[i].SetActive(false);
                SittingChar7[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
           
        }
        if (levelSelection.levelNo == 7)
        {
            for (int i = 0; i < PickingChars8.Length; i++)
            {
                PickingChars8[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars8[i].GetComponent<splineMove>().enabled = true;
                PickingChars8[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars8.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars8[i].SetActive(false);
                SittingChar8[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
            
        }
        if (levelSelection.levelNo == 8)
        {
            for (int i = 0; i < PickingChars9.Length; i++)
            {
                PickingChars9[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars9[i].GetComponent<splineMove>().enabled = true;
                PickingChars9[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars9.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars9[i].SetActive(false);
                SittingChar9[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
           
        }
        if (levelSelection.levelNo == 9)
        {
            for (int i = 0; i < PickingChars10.Length; i++)
            {
                PickingChars10[i].gameObject.GetComponent<splineMove>().pathContainer = pickWaypoints[VehicleSelection._index].GetComponent<PathManager>();
                PickingChars10[i].GetComponent<splineMove>().enabled = true;
                PickingChars10[i].GetComponent<Animator>().SetBool("walk", true);
                yield return new WaitForSeconds(1);
            }
            for (int i = 0; i < PickingChars10.Length; i++)
            {
                yield return new WaitForSeconds(2);
                PickingChars10[i].SetActive(false);
                SittingChar10[i].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            BusDoor[VehicleSelection._index].SetBool("open", false);
            DoorSound.Play();
            PlayCurrentSong.PlaySong();
           
        }
        yield return new WaitForSeconds(4f);
        FastBtn.SetActive(false);
        Rccam.SetActive(true);
        BusCam[VehicleSelection._index].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        Time.timeScale = 1f;
    }
    public void FastGame()
    {
        Time.timeScale = 4f;
    }
    public IEnumerator DropPassengers()
    {
        FastBtn.SetActive(true);
        Controls.SetActive(false);
        Dparking[levelSelection.levelNo].SetActive(false);

        while (Vector3.Distance(transform.position, DropPos[levelSelection.levelNo].position) > 0.1f ||
     Quaternion.Angle(transform.rotation, DropPos[levelSelection.levelNo].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[levelSelection.levelNo].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[levelSelection.levelNo].rotation, speed * Time.deltaTime);
            yield return null;
        }

        stopSongs();
        yield return new WaitForSeconds(1);
        Rccam.SetActive(false);
        BusCam[VehicleSelection._index].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        BusDoor[VehicleSelection._index].SetBool("open", true);
        DoorSound.Play();
        yield return new WaitForSeconds(3);
        if (levelSelection.levelNo == 0)
        {
            for (int i = 0; i < DropChar.Length; i++)
            {
                DropChar[i].SetActive(true);
                DropChar[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 1)
        {
            for (int i = 0; i < DropChar2.Length; i++)
            {
                DropChar2[i].SetActive(true);
                DropChar2[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar2[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 2)
        {

            yield return new WaitForSeconds(3);
            for (int i = 0; i < DropChar3.Length; i++)
            {
                DropChar3[i].SetActive(true);
                DropChar3[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar3[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }

        if (levelSelection.levelNo == 4)
        {
            yield return new WaitForSeconds(3);
            for (int i = 0; i < DropChar5.Length; i++)
            {
                DropChar5[i].SetActive(true);
                DropChar5[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar5[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 5)
        {
            for (int i = 0; i < DropChar6.Length; i++)
            {
                DropChar6[i].SetActive(true);
                DropChar6[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar6[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 6)
        {
            for (int i = 0; i < DropChar7.Length; i++)
            {
                DropChar7[i].SetActive(true);
                DropChar7[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar7[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 7)
        {
            for (int i = 0; i < DropChar8.Length; i++)
            {
                DropChar8[i].SetActive(true);
                DropChar8[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar8[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 8)
        {
            for (int i = 0; i < DropChar9.Length; i++)
            {
                DropChar9[i].SetActive(true);
                DropChar9[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar9[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        if (levelSelection.levelNo == 9)
        {
            for (int i = 0; i < DropChar10.Length; i++)
            {
                DropChar10[i].SetActive(true);
                DropChar10[i].GetComponent<Animator>().SetBool("walk", true);
                SittingChar10[i].SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }
        yield return new WaitForSeconds(4f);
        BusDoor[VehicleSelection._index].SetBool("open", false);
        DoorSound.Play();
        FastBtn.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(LevelComplete());
    }
    public IEnumerator DParking10()
    {
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        while (Vector3.Distance(transform.position, DropPos[10].position) > 0.1f ||
             Quaternion.Angle(transform.rotation, DropPos[10].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[10].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[10].rotation, speed * Time.deltaTime);
            yield return null;
        }
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        SmallCutScenes[5].SetActive(true);
        yield return new WaitForSeconds(SSDuration[5]);
        SmallCutScenes[5].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        Dparking[11].SetActive(true);
    }
    public IEnumerator DParking11()
    {
        FastBtn.SetActive(true);
        Controls.SetActive(false);
        Dparking[11].SetActive(false);

        while (Vector3.Distance(transform.position, DropPos[11].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[11].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[11].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[11].rotation, speed * Time.deltaTime);
            yield return null;
        }

        stopSongs();
        yield return new WaitForSeconds(1);
        Rccam.SetActive(false);
        BusCam[VehicleSelection._index].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        BusDoor[VehicleSelection._index].SetBool("open", true);
        DoorSound.Play();
        yield return new WaitForSeconds(3);

        DropChar4[2].SetActive(true);
        DropChar4[2].GetComponent<Animator>().SetBool("walk", true);
        SittingChar4[2].SetActive(false);

        yield return new WaitForSeconds(4f);
        BusDoor[VehicleSelection._index].SetBool("open", false);
        DoorSound.Play();
        FastBtn.SetActive(false);
        Time.timeScale = 1f;

        yield return new WaitForSeconds(4f);
        DropChar4[2].SetActive(false);
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BusCam[VehicleSelection._index].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Dparking[12].SetActive(true);
    }
    public IEnumerator DParking12()
    {
        FastBtn.SetActive(true);
        Controls.SetActive(false);
        Dparking[12].SetActive(false);

        while (Vector3.Distance(transform.position, DropPos[12].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[12].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[12].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[12].rotation, speed * Time.deltaTime);
            yield return null;
        }

        stopSongs();
        yield return new WaitForSeconds(1);
        Rccam.SetActive(false);
        BusCam[VehicleSelection._index].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        BusDoor[VehicleSelection._index].SetBool("open", true);
        DoorSound.Play();
        yield return new WaitForSeconds(3);

        DropChar4[3].SetActive(true);
        DropChar4[3].GetComponent<Animator>().SetBool("walk", true);
        SittingChar4[3].SetActive(false);

        yield return new WaitForSeconds(4f);
        BusDoor[VehicleSelection._index].SetBool("open", false);
        DoorSound.Play();
        FastBtn.SetActive(false);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(4f);
        DropChar4[3].SetActive(false);
        Rccam.SetActive(true);
        Controls.SetActive(true);
        BusCam[VehicleSelection._index].SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Dparking[13].SetActive(true);
    }
    public IEnumerator DParking13()
    {
        FastBtn.SetActive(true);
        Controls.SetActive(false);
        Dparking[13].SetActive(false);

        while (Vector3.Distance(transform.position, DropPos[13].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[13].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[13].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[13].rotation, speed * Time.deltaTime);
            yield return null;
        }

        stopSongs();
        yield return new WaitForSeconds(1);
        Rccam.SetActive(false);
        BusCam[VehicleSelection._index].SetActive(true);
        yield return new WaitForSeconds(3.5f);
        BusDoor[VehicleSelection._index].SetBool("open", true);
        DoorSound.Play();
        yield return new WaitForSeconds(3);

        DropChar4[4].SetActive(true);
        DropChar4[4].GetComponent<Animator>().SetBool("walk", true);
        SittingChar4[4].SetActive(false);
        yield return new WaitForSeconds(1);
        DropChar4[5].SetActive(true);
        DropChar4[5].GetComponent<Animator>().SetBool("walk", true);
        SittingChar4[5].SetActive(false);

        yield return new WaitForSeconds(4f);
        BusDoor[VehicleSelection._index].SetBool("open", false);
        DoorSound.Play();
        yield return new WaitForSeconds(4f);
        DropChar4[4].SetActive(false);
        DropChar4[5].SetActive(false);
        StartCoroutine(LevelComplete());
        FastBtn.SetActive(false);
        Time.timeScale = 1f;
    }
    public IEnumerator DParking14()
    {
        Dparking[14].SetActive(false);
        while (Vector3.Distance(transform.position, DropPos[14].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[14].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[14].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[14].rotation, speed * Time.deltaTime);
            yield return null;
        }
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        finalCutScenes[0].SetActive(true);
        yield return new WaitForSeconds(FSDuration[0]);
        finalCutScenes[0].SetActive(false);
        Rccam.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        Dparking[15].SetActive(true);
    }
    public IEnumerator DParking15()
    {
        Dparking[15].SetActive(false);
        while (Vector3.Distance(transform.position, DropPos[15].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[15].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[15].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[15].rotation, speed * Time.deltaTime);
            yield return null;
        }
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        finalCutScenes[1].SetActive(true);
        yield return new WaitForSeconds(FSDuration[0]);
        finalCutScenes[1].SetActive(false);
        Rccam.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        Dparking[16].SetActive(true);
    }
    public IEnumerator DParking16()
    {
        Dparking[16].SetActive(false);
        while (Vector3.Distance(transform.position, DropPos[16].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[16].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[16].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[16].rotation, speed * Time.deltaTime);
            yield return null;
        }
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        finalCutScenes[2].SetActive(true);
        yield return new WaitForSeconds(FSDuration[0]);
        finalCutScenes[2].SetActive(false);
        Rccam.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        Dparking[17].SetActive(true);

    }
    public IEnumerator DParking17()
    {
        Dparking[17].SetActive(false);
        while (Vector3.Distance(transform.position, DropPos[17].position) > 0.1f ||
   Quaternion.Angle(transform.rotation, DropPos[17].rotation) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DropPos[17].position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, DropPos[17].rotation, speed * Time.deltaTime);
            yield return null;
        }
        this.gameObject.GetComponent<Rigidbody>().drag = 4f;
        Controls.SetActive(false);
        yield return new WaitForSeconds(1f);
        Rccam.SetActive(false);
        finalCutScenes[3].SetActive(true);
        yield return new WaitForSeconds(FSDuration[0]);
        finalCutScenes[3].SetActive(false);
        Rccam.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().drag = 0.01f;
        Controls.SetActive(true);
        BlackPanel.SetActive(false);
        SceneBlackPanel.SetActive(false);
        Dparking[4].SetActive(true);
    }

    public IEnumerator LevelComplete()
    {
        LastCutScenes[levelSelection.levelNo].SetActive(true);
        yield return new WaitForSeconds(LastDuration[levelSelection.levelNo]);
        LastCutScenes[levelSelection.levelNo].SetActive(false);
        BlackPanel.SetActive(false);
        CompletePanel.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + 1000);
        PlayerPrefs.SetInt("Level", levelSelection.levelNo + 1);
        Debug.Log(PlayerPrefs.GetInt("Level"));    
    }
}