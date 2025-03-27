using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Service", menuName = "MindGG/Sound Service")]
public class SoundService : Service
{
    public List<SFX> sfxs = new List<SFX>();
    
    private AudioSource _sfxSource;

    public override void Init()
    {
        GameObject audioManager = new GameObject("AudioService");
        _sfxSource = audioManager.AddComponent<AudioSource>();
        _sfxSource.volume = 0.5f;
        GameObject.DontDestroyOnLoad(audioManager);
        Debug.Log("Audio Service initialized.");
    }
    
    public void PlaySFX(string audioName)
    {
        if (_sfxSource == null)
        {
            Init();
        }

        SFX sfx = sfxs.Find(s => s.name == audioName);
        if (sfx.clip == null)
        {
            Debug.LogWarning("Attempted to play a null clip.");
            return;
        }

        if (_sfxSource != null)
        {
            _sfxSource.PlayOneShot(sfx.clip);
        }
        else
        {
            Debug.LogError("AudioService not initialized correctly: AudioSource is null.");
        }
    }
}
[Serializable]
public class SFX
{
    public string name;
    public AudioClip clip;
}
