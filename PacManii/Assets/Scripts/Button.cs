using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]private GameObject gameManagerObject;
    private GameManager gameManager;
    private void Start() {
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }
    public void OnButtonEnterRestart()
    {
        gameManager.GameRestart();
    }
    public void OnButtonEnterStartGame()
    {
        gameManager.GameStart();
    }
    public void OnButtonEnterHMenuTurningSpeed()
    {
        Debug.Log("increased Turning Speed: " + gameManager.MenuTurningSpeed);
        gameManager.MenuTurningSpeed += 0.5f;
    }
}
