using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer bgmMixer;
    [SerializeField] AudioMixer vfxMixer;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider vfxSlider;
    //public Dropdown resolutionDropdown;
    public TMP_Dropdown resolutionDropdown;
    public static GameObject instance;
    Resolution[] resolutions;
    float value;
    float menuvalue;

    // Use this for initialization

    void Awake()
    {

    }
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void OpenOptionsMenu()
    {
        GameObject.FindGameObjectWithTag("OptionsMenu").SetActive(true);
    }
    public void SetBGMVolume(float volume)
    {
        //the bgm volume is an exposed parameter in the audio mixer
        bgmMixer.SetFloat("BGMVolume", volume);
    }
    public void SetVFXVolume(float volume)
    {
        //the vfx volume is an exposed parameter in the audio mixer
        vfxMixer.SetFloat("VFXVolume", volume);
    }




}
