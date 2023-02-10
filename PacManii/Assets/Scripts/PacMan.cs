using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PacMan : MonoBehaviour
{
    private Movement mov;
    private GameManager _gameManager;
    
    private float smooth = 5.0f;
    private void Start()
    {
        mov = GetComponent<Movement>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mov.nextDirection = context.ReadValue<Vector2>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            _gameManager.GameOverState = true;
        }
        else
        if (other.CompareTag("Frightend"))
        {
            other.GetComponent<Movement>().MoveFrightend();
            // ADD SCORE
        }
    }
    private void Animate()
    {
        Quaternion target = Quaternion.Euler(0f, 0f, 0f);
        if (mov.direction.x < 0)//LEFT
        {
            target.y = -90f;
        }
        if (mov.direction.y > 0 )
        {//RIGHT
            target.y = 90f;
        }
        if (mov.direction.y < 0 )// DOWN
        {
            target.x = 90f;
            target.y = 90f;
        }
        if (mov.direction.y > 0)
        {// UP
            target.x = -90f;
            target.y = 90f;
        }
        
        //this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
    private void FixedUpdate()
    {
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
