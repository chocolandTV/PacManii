using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // STATS LEVEL 1  SCATTER 7" CHASE 20" SCATTER 7" CHASE 20" SCATTER 5" CHASE 20" SCATTER 5" 
    // LEVEL 2-4  - 7 - 20 - 7 - 20 - 5 - 17 - 0
    // LEVEL 5+   - 5 - 20 - 5 - 20 - 5 - 17 - 0
    // RED GHOST BLINKY can allways chase when less dots remaining level 1  - 20, 30,40,50
    /* #############################################
    // MOVEMENT MECHANIC TARGET 
    ################################################
    //1. Top has highest priority, then Left, down , right when same Linear Dist²
    // LINEAR CALCULATION DIST² =  x² + y² ( 8² + 2²)  = 68

    2. IN SCATTER MODE  PINK GOES TOP LEFT CORNER AND SUROUNDING, BLINKY RED GOES TO RIGHT and SUROUNDING
    .YELLOW GHOST LEFT BOTTOM CORNER an big surround and BLUE GHOST RIGHT CORNER
    3. DIFFERENT TARGETING . ROSE GHOST Targeting HIT 2 Steps Right but when PacMan facing up ( Target +4 +2 Left)

    ################################################ POSITION ################################################
    PACMAN  [40, 38] REDGHOST [48,38], ROSEGHOST [44,42] TURKY[38,52] ORANGEGHOST [50,46]
    */
    public int points = 200; // change on state
    public GameObject GhostEnemy {get;private set;}
    public Transform pacManPos;
    //private Movement movement;
    // NEW FIELDS
    public Movement movement {get; private set;}
    public GhostHome home {get ;private set;}
    public GhostScatter scatter{get;private set;}
    public GhostChase chase{get;private set;}
    public GhostFrightened frightened {get;private set;}
    public GhostBehaviour initialBehavior;
    // public Transform target;
    public Vector2 ChaseOffset;


    public enum Name
    {
        Inky,// Cyan
        Blinky, //Red
        Pinky,//Pink
        Clyde // Orange FFB751
    }
    private enum GhostState
    {
        SCATTER,
        CHASE,
        FRIGHTENED
    }
    public Name ghostName;
    private GhostState ghostState;
    // Start is called before the first frame update
    void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome>();
        this.scatter= GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
        // GHOST INIT + STATE SET
        // SET MATERIAL COLOR
        // START BEHAVIOUR
    }
    private void Start() {
       ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
 // DEBUG EDIT
        // this.frightened.Disable();
        // this.chase.Disable();
        this.scatter.Enable();
        //this.scatter.Enable();

        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }else{
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Teleport1"))
        {
            this.movement.rigidbody.transform.position = GameObject.FindGameObjectWithTag("Teleport2").transform.position + (Vector3.left * 2);
        }
        if (other.CompareTag("Teleport2"))
        {
            this.movement.rigidbody.transform.position = GameObject.FindGameObjectWithTag("Teleport1").transform.position + (Vector3.right * 2);
        }
    }
}
