using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class systemOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer masterVolume;
    public AudioMixerGroup sfxVolume;
    Resolution[] resolutions;
    //KeyCode[] all;
    public Dropdown resolutionDropdown, graphicsDropdown;
    private Toggle fullScreen;
    public Slider masterVolumeControl, sfxVolumeControl, musicVolumeControl, voiceVolumeControl;
    public Slider mouseSensitivityControl;
    public Text masterVolumeText, sfxVolumeText, musicVolumeText, voiceVolumeText;

   // private SystemGUI_Manager systemGUI;

    void Start()
    {
     //   systemGUI = FindObjectOfType<SystemGUI_Manager>(
        masterVolume = Resources.Load("MasterAudio/Master") as AudioMixer;
        
        resolutionDropdown = this.gameObject.transform.Find("SYSTEM_option").transform.Find("graphicSettings").Find("resolution").transform.GetComponent<Dropdown>();
        graphicsDropdown = this.gameObject.transform.Find("SYSTEM_option").transform.Find("graphicSettings").Find("quality").transform.GetComponent<Dropdown>();
        fullScreen = this.gameObject.transform.Find("SYSTEM_option").transform.Find("graphicSettings").Find("fullScreen").transform.GetComponent<Toggle>();
        // default setips
        masterVolumeControl = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("masterSlider").transform.GetComponent<Slider>();
        sfxVolumeControl = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("sfxSlider").transform.GetComponent<Slider>();
        musicVolumeControl = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("musicSlider").transform.GetComponent<Slider>();
        voiceVolumeControl = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("voiceSlider").transform.GetComponent<Slider>();

        mouseSensitivityControl = this.gameObject.transform.Find("SYSTEM_option").transform.Find("mouseSettings").Find("mouseSlider").transform.GetComponent<Slider>();

        masterVolumeText = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("masterSensitivity").transform.GetComponent<Text>();
        sfxVolumeText = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("sfxSensitivity").transform.GetComponent<Text>();
        musicVolumeText = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("musicSensitivity").transform.GetComponent<Text>();
        voiceVolumeText = this.gameObject.transform.Find("SYSTEM_option").transform.Find("soundSettings").Find("voiceSensitivity").transform.GetComponent<Text>();

        masterVolumeControl.value = -20;
        sfxVolumeControl.value = -20;
        musicVolumeControl.value = -20;
        voiceVolumeControl.value = -20;

        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(setResolution);
        else resolutionDropdown.onValueChanged.RemoveListener(setResolution);
        if (graphicsDropdown != null) graphicsDropdown.onValueChanged.AddListener(setQuality);
        else graphicsDropdown.onValueChanged.RemoveListener(setQuality);
        if (fullScreen != null) fullScreen.onValueChanged.AddListener(setFullScreen);
        else fullScreen.onValueChanged.RemoveListener(setFullScreen);


        
        if (masterVolumeControl != null) masterVolumeControl.onValueChanged.AddListener(setVolume);
        else masterVolumeControl.onValueChanged.RemoveListener(setVolume);
        if (sfxVolumeControl != null) sfxVolumeControl.onValueChanged.AddListener(setSfxVolume);
        else sfxVolumeControl.onValueChanged.RemoveListener(setSfxVolume);
        if (musicVolumeControl != null) musicVolumeControl.onValueChanged.AddListener(setMusicVolume);
        else musicVolumeControl.onValueChanged.RemoveListener(setMusicVolume);
        if (voiceVolumeControl != null) voiceVolumeControl.onValueChanged.AddListener(setVoiceVolume);
        else voiceVolumeControl.onValueChanged.RemoveListener(setVoiceVolume);

        if (mouseSensitivityControl != null) mouseSensitivityControl.onValueChanged.AddListener(setMouseSensitivity);
        else mouseSensitivityControl.onValueChanged.RemoveListener(setMouseSensitivity);
        //  resolutionDropdown = systemGUI.transform.Find("Resolution").transform.GetComponent<Dropdown>();
        // getting default setting values.
        getAllResolutions();
  
    }

    // Update is called once per frame
   public void getAllResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setVolume(float volume)
    {
        masterVolume.SetFloat("Master", volume);
        masterVolumeText.text = volume.ToString();
    }

    public void setSfxVolume(float volume)
    {
        masterVolume.SetFloat("SFX", volume);
        sfxVolumeText.text = volume.ToString();
    }
    public void setMusicVolume(float volume)
    {
        masterVolume.SetFloat("Music", volume);
        musicVolumeText.text = volume.ToString();
    }
    public void setVoiceVolume(float volume)
    {
        masterVolume.SetFloat("Voice", volume);
        voiceVolumeText.text = volume.ToString();
    }
    public void setMouseSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
