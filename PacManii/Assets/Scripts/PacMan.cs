using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PacMan : MonoBehaviour
{
    private Movement mov;
    private GameManager _gameManager;
    private Vector2 tempMov;
    private void Start() {
        mov = GetComponent<Movement>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            mov.direction = context.ReadValue<Vector2>();
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
    private void Animate()
    {
        if(mov.direction.x < 0 && tempMov != mov.direction)
        {
            this.gameObject.transform.Rotate(Vector3.right*90, Space.Self);
        }
         if(mov.direction.y > 0 && tempMov != mov.direction){
            this.gameObject.transform.Rotate(Vector3.left*90, Space.Self);
        }
        if(mov.direction.y < 0 && tempMov != mov.direction)
        {
            this.gameObject.transform.Rotate(Vector3.up*90, Space.Self);
        }
         if(mov.direction.y > 0 && tempMov != mov.direction){
            this.gameObject.transform.Rotate(Vector3.down*90, Space.Self);
        }
        tempMov = mov.direction;
    }
    private void FixedUpdate() {
        Animate();
    }
    // INPUT 
    // CHECK INPUT (position in mazegrid )
    // A 
    // private Vector2Int position;
    // if(MazeGrid.CheckMoveNext(position, MazeGrid.Direction.Left)
    // {
        
    // }
    
}
