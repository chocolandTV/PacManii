using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PacMan : MonoBehaviour
{
    private Movement mov;
    private GameManager _gameManager;
    
    private Rigidbody rb;
    // COINS
    // public int collectedPallets { get; private set; }
    public GameObject palletAfterEffectPrefab;
    private GameObject palletAfterEffectObject;

    private void Start()
    {
        mov = GetComponent<Movement>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        palletAfterEffectObject = Instantiate(palletAfterEffectPrefab);
        // palletAfterEffectObject.SetActive(true);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mov.nextDirection = context.ReadValue<Vector2>();
            // Animate(mov.nextDirection);
            if (mov.nextDirection.y != 0)
            {
                this.transform.rotation = Quaternion.LookRotation(mov.nextDirection, Vector3.back);
            }
            else
            if (mov.nextDirection.x != 0)
            {
                this.transform.rotation = Quaternion.LookRotation(mov.nextDirection, Vector3.up);
            }
            //this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (mov.nextDirection), Time.deltaTime * 40f);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            _gameManager.LoseLive();
        }

        if (other.CompareTag("Frightend"))
        {
            other.GetComponent<Movement>().MoveFrightend();
            // ADD SCORE
        }
        if (other.CompareTag("Teleport1"))
        {
            rb.transform.position = GameObject.FindGameObjectWithTag("Teleport2").transform.position + (Vector3.left * 1);
        }
        if (other.CompareTag("Teleport2"))
        {
            rb.transform.position = GameObject.FindGameObjectWithTag("Teleport1").transform.position + (Vector3.right * 1);
        }
        if (other.CompareTag("pallets"))
        {
            _gameManager.CollectPellet();
            palletAfterEffectObject.transform.position = other.transform.position;
            palletAfterEffectObject.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject, 0.0f);

        }
        if (other.CompareTag("SuperPallet"))
        {
            _gameManager.CollectPellet();
            palletAfterEffectObject.transform.position = other.gameObject.transform.position;
            palletAfterEffectObject.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject, 0.0f);
        }
    }
    // private void Animate(Vector2 dir)
    // {
        // this.transform.rotation = Quaternion.LookRotation (dir);
        // // Vector3 target = new Vector3 (0f,0f,0f);
        // Quaternion target = Quaternion.Euler(0.0f,180.0f,0.0f);
        // if (dir.x == -1.0f)//LEFT
        // {
        //     target.y += 90f;
        // }else 
        // if (dir.x == 1.0f )//RIGHT
        // {
        //     target.y -= 90f;
        // }else 
        // if (dir.y == -1.0f )// DOWN
        // {
        //     target.x = 90f;
        //     target.y = 270f;
        //     target.z = -270f;
        // }else 
        // if (dir.y ==1.0f)// UP
        // {
        //     target.x = -90f;
        //     target.z = -180f;
        // }
        // this.gameObject.transform.rotation=target;
        //this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    // }
    // private void FixedUpdate()
    // {
    //     Animate();
    // }
    // private void SetCollectedPallets(int value)
    // {
    //     collectedPallets = value;
    // }
    // INPUT 
    // CHECK INPUT (position in mazegrid )
    // A 
    // private Vector2Int position;
    // if(MazeGrid.CheckMoveNext(position, MazeGrid.Direction.Left)
    // {

    // }

}
