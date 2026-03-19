using UnityEngine;
//using UnityEngine.UI; // Use this for default UI Dropdown
using TMPro; // Use this instead if using TextMeshPro
using System.Collections.Generic;
using System.Linq; // Required for Distinct() method

public class ResolutionSettings : MonoBehaviour
{
    // Reference to the Dropdown UI element in the Editor
    public TMP_Dropdown resolutionDropdown; 
    // If using TextMeshPro, use: public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        // Filter resolutions to only include those matching the current monitor's refresh rate (to avoid duplicates)
        currentRefreshRate = Screen.currentResolution.refreshRate;

        foreach (var res in resolutions)
        {
            // Note: In newer Unity versions, refreshRate is an int. In older versions, it might be refreshRateRatio
            if (Mathf.Approximately(res.refreshRate, currentRefreshRate)) 
            {
                filteredResolutions.Add(res);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(option);

            // Find the index of the current active resolution
            if (filteredResolutions[i].width == Screen.currentResolution.width &&
                filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Clear existing options and add the new ones
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        // Add a listener for when the dropdown value changes
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    // Public method to change the resolution, called by the Dropdown's On Value Changed event
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
