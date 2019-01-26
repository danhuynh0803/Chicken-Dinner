using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSettings : MonoBehaviour {

    /*
    #region Singleton
    public static AudioSettings instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    float musicVolume = 0.5f;
    float sfxVolume = 0.5f;
    float masterVolume = 1.0f;

    private void Start()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SFXVolumeTest");
    }

    private void Update()
    {
        Music.setVolume(musicVolume);
        SFX.setVolume(sfxVolume);
        Master.setVolume(masterVolume);
    }

    public void SetMasterVolumeLevel(float newVolume)
    {
        masterVolume = newVolume;
    }

    public void SetSFXVolumeLevel(float newVolume)
    {
        sfxVolume = newVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Debug.Log("play SFX sound test");
            SFXVolumeTestEvent.start();
        }
    }

    public void SetMusicVolumeLevel(float newVolume)
    {
        musicVolume = newVolume;
    }

    public void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
    }

    public void PlaySound(string path, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, position);
    }
    */
}
