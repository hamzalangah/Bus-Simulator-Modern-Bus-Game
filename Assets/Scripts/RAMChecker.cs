using UnityEngine;

public class RAMChecker : MonoBehaviour
{
    public Camera Camera;
    void Start()
    {
        int totalRAM = SystemInfo.systemMemorySize; // Get total RAM in MB
        Debug.Log("Total RAM: " + totalRAM + " MB");
        
        // Now, you can compare the totalRAM variable to determine the amount of RAM.
        if (totalRAM >= 8192) // 8 GB RAM
        {
            Camera.farClipPlane =350;
            QualitySettings.SetQualityLevel(4, true);
        }
        else if (totalRAM >= 6144) // 6 GB RAM
        {
            Camera.farClipPlane =280;
            QualitySettings.SetQualityLevel(3, true);
        }
        else if (totalRAM >= 4096) // 4 GB RAM
        {
            Camera.farClipPlane = 220;
            QualitySettings.SetQualityLevel(2, true);
        }
        else if (totalRAM >= 2048) // 2 GB RAM
        {
            Camera.farClipPlane =150;
            QualitySettings.SetQualityLevel(1, true);
        }
        else
        {
            Camera.farClipPlane = 130;
            QualitySettings.SetQualityLevel(0, true);
        }
    }
}