using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private GameObject gameManagerObject;
    [SerializeField]private GameObject Hmenu;
    [SerializeField]private GameObject ControlUI;
    [SerializeField]private GameObject SettingsUI;
    [SerializeField]private GameObject HudMenu;
    private GameManager gameManager;
    private void Start() {
        gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }
    public void OnButtonEnterRestart()
    {
       SceneManager.LoadScene(1);
    }
    public void OnButtonEnterStartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OnButtonEnterResume()
    {
        HudMenu.SetActive(true);
        Hmenu.SetActive(false);
        ControlUI.SetActive(false);
        SettingsUI.SetActive(false);
        gameManager.Resume();
    }
    public void OnButtonEnterPaused()
    {
        HudMenu.SetActive(false);
        Hmenu.SetActive(true);
        ControlUI.SetActive(false);
        SettingsUI.SetActive(false);
        
    }
    public void OnButtonEnterControl()
    {
        // UI CONTROLS ON
        Hmenu.SetActive(false);
        ControlUI.SetActive(true);
        SettingsUI.SetActive(false);

    }
    public void OnButtonEnterSettings()
    {
        // SETTINGS ON 
        // SOUND SFX
        Hmenu.SetActive(false);
        ControlUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    
    public void OnButtonEnterBack()
    {
        // ALL OFF 
        // HMENU ON
        Hmenu.SetActive(true);
        ControlUI.SetActive(false);
        SettingsUI.SetActive(false);
    }
    public void OnButtonExit()
    {
        Application.Quit();
    }
    public void OnButtonEnterHMenuTurningSpeed()
    {
        Debug.Log("increased Turning Speed: " + gameManager.MenuTurningSpeed);
        gameManager.MenuTurningSpeed += 0.5f;
    }
}
