using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu_Music : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip StartClip;
    [SerializeField] AudioClip LoopClip;
    [SerializeField] AudioMixerGroup bgmMixer;
    [SerializeField] AudioSource menuMusicPlayer;


    void Start()
    {
        menuMusicPlayer = this.GetComponent<AudioSource>();
        StartCoroutine(playSound());
    }



    IEnumerator playSound()
    {
        menuMusicPlayer.clip = StartClip;
        menuMusicPlayer.outputAudioMixerGroup = bgmMixer;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(StartClip.length);
        menuMusicPlayer.clip = LoopClip;
        menuMusicPlayer.outputAudioMixerGroup = bgmMixer;
        menuMusicPlayer.Play();
        menuMusicPlayer.loop = true;
    }
}
