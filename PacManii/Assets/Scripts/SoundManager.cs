using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public static AudioClip click;
    public static AudioClip death;
    public static AudioClip powerPellet;
    public static AudioClip win;
    public enum clip
    {
        Click,
        Death,
        PowerPellet,
        Win
    }
    
    public static void PlaySound(AudioSource source, clip name)
    {
        source.clip = getClip(name);
        source.Play();
        
    }
    private static AudioClip getClip(clip name)
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
