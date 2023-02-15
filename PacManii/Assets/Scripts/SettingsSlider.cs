using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsSlider : MonoBehaviour
{
    public Slider mainSlider;
    private GameManager gameManager;
    private GameObject gameManagerOb;
    public AudioSource MusicSound;
    public int type = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerOb = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerOb.GetComponent<GameManager>();
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    public void ValueChangeCheck()
    {
        if (type == 0)
        {
            // SOUND PARAMETER CHANGED
            gameManager.soundFXVolume = mainSlider.value;
            Debug.Log("SoundVolume: " + gameManager.soundFXVolume);
            
        }
        else if (type == 1)
        {
            gameManager.musicVolume = mainSlider.value;
            Debug.Log("MusicVolume: " + gameManager.musicVolume);
            MusicSound.volume = gameManager.musicVolume;
        }
    }
}
