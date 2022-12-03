using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class audiosavedata : MonoBehaviour 
{

    // Use this for initialization
    public Slider MasterSlider;
    public Slider VoicelineSlider;

    public float voiceLines = 0.4f;
    public float backgroundMusic = 0.4f;
    [SerializeField] AudioSource BGMPlayer;
    [SerializeField] AudioSource voicePlayer;
    Scene activeScene;
     
    void Awake()
    {
        voiceLines = PlayerPrefs.GetFloat("VoiceLinesVolume",voiceLines);
        backgroundMusic = PlayerPrefs.GetFloat("BGMVolume",backgroundMusic);
        SetAudioSettings();
        GameObject[] me =GameObject.FindGameObjectsWithTag(this.tag);
        if( me.Length > 1)
        {
             DestroyImmediate(gameObject);
        }
    }
    void Start ()
    {
        
		DontDestroyOnLoad(this.gameObject);
        activeScene = SceneManager.GetActiveScene();
        
	}

    public void SaveOptions()
    {
        voiceLines = VoicelineSlider.value;
        backgroundMusic = MasterSlider.value;
        SetAudioSettings();
        PlayerPrefs.SetFloat("VoiceLinesVolume", voiceLines);
        PlayerPrefs.SetFloat("BGMVolume", backgroundMusic);
        PlayerPrefs.Save();
    }

    private void SetAudioSettings()
    {
        voicePlayer = GameObject.FindGameObjectWithTag("VoiceSource").GetComponent<AudioSource>();
        BGMPlayer = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        voicePlayer.volume = voiceLines;
        BGMPlayer.volume = backgroundMusic;
    }

    // Update is called once per frame
    void Update ()
    {
    
    }

    private IEnumerator OpenAudioSettings()
    {
        // can be made with on value changed
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("MasterSlider") != null);
        MasterSlider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
        VoicelineSlider = GameObject.FindGameObjectWithTag("VCSlider").GetComponent<Slider>(); 
        MasterSlider.value = backgroundMusic;
        VoicelineSlider.value = voiceLines; 
        MasterSlider.onValueChanged.AddListener (delegate {SaveOptions ();});        
    }

    public void StartOpenAudio()
    {
        StartCoroutine(OpenAudioSettings());
    }
}
