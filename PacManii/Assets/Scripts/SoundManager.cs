using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager:MonoBehaviour
{
    public AudioClip click;
    public AudioClip death;
    public AudioClip powerPellet;
    public AudioClip win;
    public enum clip
    {
        Click,
        Death,
        PowerPellet,
        Win
    }
    public static SoundManager Instance {get;private set;}
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    public static void PlaySound(AudioSource source, clip name)
    {
        source.clip = Instance.getClip(name);
        source.Play();
        // SoundManager.Instance.PlaySound();
        
    }
    private AudioClip getClip(clip name)
    {
        switch (name)
        {
            case clip.Click:
                return click;
                
            case clip.Death:
                return death;
            case clip.PowerPellet:
                return powerPellet;
            case clip.Win:
                return win; 
            default: 
                return click;
        }
    }
}
