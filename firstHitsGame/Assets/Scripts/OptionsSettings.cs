using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsSettings : MonoBehaviour
{
    public TMP_Dropdown res;
    public TMP_Dropdown quality;

    Resolution[] resolutions;

    void Start()
    {
        res.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currResIndex = i;
            }
        }

        res.AddOptions(options);
        res.RefreshShownValue();
        LoadSettings(currResIndex);
    }


    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", quality.value);
        PlayerPrefs.SetInt("ResolutionPreference", res.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings(int currentResIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            quality.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            quality.value = 4;

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            res.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            res.value = currentResIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
    }
}
