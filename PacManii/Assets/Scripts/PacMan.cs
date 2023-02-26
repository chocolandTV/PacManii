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
    public Transform ghosty;
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
    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _gameManager.OnGameStatePaused();
        }
    }
    public void OnStopMoving(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            mov.nextDirection = Vector2.zero;
            Debug.Log(mov.GridPosition());
            // Debug.Log(mov.DistanceCheck(ghosty.position));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            //  _gameManager.LoseLive();
            if(other.gameObject.GetComponent<Ghost>().ghostState == Ghost.GhostState.FRIGHTENED)
            {
                FindObjectOfType<GameManager>().GhostEaten(other.gameObject.GetComponent<Ghost>());
            }
            else{
                _gameManager.LoseLive();
            }
        }

        // if (other.CompareTag("Frightend"))
        // {
        //     // other.GetComponent<Movement>().MoveFrightend();
        //     // ADD SCORE
        // }
        if (other.CompareTag("Teleport1"))
        {
            rb.transform.position = GameObject.FindGameObjectWithTag("Teleport2").transform.position + (Vector3.left * 2);
        }
        if (other.CompareTag("Teleport2"))
        {
            rb.transform.position = GameObject.FindGameObjectWithTag("Teleport1").transform.position + (Vector3.right * 2);
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
            _gameManager.SuperPelletEffect();
            Destroy(other.gameObject, 0.0f);
        }

    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.mov.ResetState();
    }
    

}
