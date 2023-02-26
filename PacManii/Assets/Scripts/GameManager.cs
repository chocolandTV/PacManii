
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public GameObject pacman;
    public Transform pellets;
    [SerializeField] private GameObject hudObject;
    [SerializeField] private GameObject gameoverObject;
    [SerializeField] private GameObject menuObject;
   
    private HUD hudScript;
    // STATS AND COLLECTABLES
    public int score { get; private set; }
    public int lives { get; private set; }
    public int cherries { get; private set; }
    public int secrets { get; private set; }
    public int level { get; private set; }
    public int pelletsRemaining { get; private set; }
    public int paciiStatus { get; private set; }
    public float MenuTurningSpeed { get; set; } = 0.5f;
    public int ghostMultiplier = 1;
    // SETTINGS VARIABLES
    public float soundFXVolume { get; set; } = 40.0f;
    public float musicVolume { get; set; } = 40.0f;
    public GameState currentState;
    // END STATS AND COLLECTABLE 
    public enum GameState
    {
        Menu,
        Paused,
        Running,
        GameOver
    }

    private void Start()
    {

        if (currentState == GameState.Running)
        {
            hudScript = hudObject.GetComponent<HUD>();
            Time.timeScale = 1;
            NewGame();
        }
    }

    private void GetAllPallets()
    {
        this.pelletsRemaining = GameObject.FindGameObjectsWithTag("pallets").Length +
         GameObject.FindGameObjectsWithTag("SuperPallet").Length;
        Debug.Log("Found :" + this.pelletsRemaining + " PELLETS");
        SetPellets(this.pelletsRemaining);
    }
    private void NewGame()
    {
        currentState = GameState.Running;
        menuObject.SetActive(false);
        hudObject.SetActive(true);
        SetScore(0);
        SetLives(3);
        SetCherries(9);
        SetLevel(1);
        GetAllPallets();
        SetSecret(0);
        SetPaciiStatus(1);
        NewRound();

    }
    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
            // RESPAWN PALLET OBJECT
        }
        ResetState();
    }
    public void Resume()
    {
        if(currentState == GameState.Paused)
        {
            Time.timeScale =paciiStatus;
            currentState = GameState.Running;
        }
    }
    public void OnGameStatePaused()
    {
        if(currentState == GameState.Running)
        {
            Time.timeScale =  0;
            currentState = GameState.Paused;
            this.gameObject.GetComponent<Button>().OnButtonEnterPaused();
        }
    }
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            //this.ghosts[i].ResetState();
        }
        this.pacman.gameObject.SetActive(true);
        this.pacman.GetComponent<DyingAnim>().OnStartAnimate();
    }
    private void GameOver()
    {
        if (GameState.Running == currentState)
        {

            currentState = GameState.GameOver;
            for (int i = 0; i < this.ghosts.Length; i++)
            {
                this.ghosts[i].gameObject.SetActive(false);
            }
            this.pacman.gameObject.SetActive(false);
            // GAMESPEED 0
            // GAMEOVER MENU ON RETRY BUTTON
            Time.timeScale = 0;
            gameoverObject.SetActive(true);
        }
    }
    private void ResetGhostMultiplier()
    {

    }
    ///////// STATS SET ///////////////////////
    #region SETSTATS

    private void SetScore(int score)
    {
        this.score = score;
        hudScript.OnValueChanged(score, HUD.TextType.score);
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
        hudScript.OnValueChanged(lives, HUD.TextType.live);
    }
    private void SetPellets(int pallets)
    {
        this.pelletsRemaining = pallets;
        hudScript.OnValueChanged(pallets, HUD.TextType.pellet);
    }
    private void SetLevel(int _level)
    {
        this.level = _level;
        hudScript.OnValueChanged(_level, HUD.TextType.level);
    }
    private void SetCherries(int _cherries)
    {
        this.cherries = _cherries;
        hudScript.OnValueChanged(_cherries, HUD.TextType.cherry);
    }
    private void SetSecret(int _secret)
    {
        this.secrets = _secret;
        hudScript.OnValueChanged(_secret, HUD.TextType.secret);
    }
    private void SetPaciiStatus(int _status)
    {
        this.paciiStatus = _status;
        Time.timeScale = _status;
        hudScript.OnValueChanged(_status, HUD.TextType.paciiStatus);
    }
    #endregion SETSTATS
    ////////////////// STATS SET END /////////////////

    /////////////// PUBLIC CALLS //////////////////
    public void GameStart()
    {
        // UI ON AND MENU OFF
        NewGame();
    }
    public void GameRestart()
    {
        if (currentState == GameState.GameOver)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void GameAbortToMenu()
    {
        currentState = GameState.Menu;
        hudObject.SetActive(false);
        menuObject.SetActive(true);
    }
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + ghost.points);
        this.ghostMultiplier ++;
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        this.pacman.gameObject.transform.position = this.pacman.gameObject.GetComponent<Movement>().startingPosition;
        SetLives(this.lives - 1);

        // START ANIMATION DYING!
        this.pacman.GetComponent<DyingAnim>().OnDeathAnimate();
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 2.0f);
        }
        else
        {
            GameOver();
        }
    }
    public void LoseLive()
    {
        this.pacman.gameObject.SetActive(false);
        this.pacman.gameObject.transform.position = this.pacman.gameObject.GetComponent<Movement>().startingPosition;
        SetLives(this.lives - 1);

        // START ANIMATION DYING!
        this.pacman.GetComponent<DyingAnim>().OnDeathAnimate();
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 2.0f);
        }
        else
        {
            GameOver();
        }
    }
    public void CollectPellet()
    {
        SetPellets(this.pelletsRemaining - 1);
        //ADD SCORE FOR PELLET
    }
    public void CollectFruits()
    {
        SetCherries(this.cherries +1);
    }
    public void SuperPelletEffect()
    {
        foreach (Ghost x in ghosts)
        {
            x.frightened.Enable(8);
        }
    }
}
