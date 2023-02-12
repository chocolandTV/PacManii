
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[]ghosts;
    public GameObject pacman;
    public Transform pellets;
    [SerializeField] private GameObject hudObject;
    private HUD hudScript;
    // STATS AND COLLECTABLES
    public int score {get; private set;}
    public int lives {get; private set;}
    public int cherries {get; private set;}
    public int secrets { get; private set;}
    public int level {get; private set;}
    public int pelletsRemaining {get; private set;}
    public int paciiStatus {get;private set;}

    // END STATS AND COLLECTABLE 
    public bool GameState {get;private set;}
   
    private void Start() {
        hudScript = hudObject.GetComponent<HUD>();
        
        NewGame();
    }
    private void Update() {
        // SPEED UP 

    }
    private void GetAllPallets()
    {
        this.pelletsRemaining = GameObject.FindGameObjectsWithTag("pallets").Length +
         GameObject.FindGameObjectsWithTag("SuperPallet").Length;
    }
    private void NewGame()
    {
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
        }
       ResetState();
    }
    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
        this.pacman.GetComponent<DyingAnim>().OnStartAnimate();
    }
    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
        // HUD OFF 
        // GAMEOVER MENU ON RETRY BUTTON
    }
    ///////// STATS SET ///////////////////////
    #region SETSTATS
    
    private void SetScore(int score)
    {
        this.score =score;
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
    public void GhostEaten (Ghost ghost)
    {
        SetScore(this.score + ghost.points);
    }
    public void LoseLive()
    {
        this.pacman.gameObject.SetActive(false);
        this.pacman.gameObject.transform.position = this.pacman.gameObject.GetComponent<Movement>().startingPosition;
        SetLives(this.lives -1);

        // START ANIMATION DYING!
        this.pacman.GetComponent<DyingAnim>().OnDeathAnimate();
        if(this.lives > 0 )
        {
            Invoke(nameof(ResetState), 2.0f);
        }else{
            GameOver();
        }
    }
    public void CollectPellet()
    {
        SetPellets(this.pelletsRemaining -1);
    }
}
