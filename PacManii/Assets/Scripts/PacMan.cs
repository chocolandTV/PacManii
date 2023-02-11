using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PacMan : MonoBehaviour
{
    private Movement mov;
    private GameManager _gameManager;
  
    private float smooth = 5.0f;
    private Rigidbody rb;
    // COINS
    public int collectedPallets{get; private set;}
    public GameObject palletAfterEffect;
    private void Start()
    {
        mov = GetComponent<Movement>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb  = GetComponent<Rigidbody>();
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
        
        if (other.CompareTag("Frightend"))
        {
            other.GetComponent<Movement>().MoveFrightend();
            // ADD SCORE
        }
        if(other.CompareTag("Teleport1"))
        {
            rb.transform.position = GameObject.FindGameObjectWithTag("Teleport2").transform.position+(Vector3.left*2);
        }
        if(other.CompareTag("Teleport2"))
        {
            rb.transform.position = GameObject.FindGameObjectWithTag("Teleport1").transform.position+(Vector3.right*2);
        }
        if(other.CompareTag("pallets"))
        {
            this.SetCollectedPallets(collectedPallets+1);
            Destroy(other.gameObject,0.0f);
        }
        if(other.CompareTag("SuperPallet"))
        {
            this.SetCollectedPallets(collectedPallets+10);
            Destroy(other.gameObject,0.0f);
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
    private void SetCollectedPallets(int value)
    {
        collectedPallets = value;
    }
    // INPUT 
    // CHECK INPUT (position in mazegrid )
    // A 
    // private Vector2Int position;
    // if(MazeGrid.CheckMoveNext(position, MazeGrid.Direction.Left)
    // {

    // }

}
