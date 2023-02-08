using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PacMan : MonoBehaviour
{
    private Movement mov;
    private GameManager _gameManager;
    private void Start() {
        mov = GetComponent<Movement>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            mov.direction  = context.ReadValue<Vector2>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ghost"))
        {
            _gameManager.GameOverState = true;
        }else
        if ( other.CompareTag("Frightend"))
        {
            other.GetComponent<Movement>().MoveFrightend();
            // ADD SCORE
        }
    }
    // INPUT 
    // CHECK INPUT (position in mazegrid )
    // A 
    // private Vector2Int position;
    // if(MazeGrid.CheckMoveNext(position, MazeGrid.Direction.Left)
    // {
        
    // }
    
}
