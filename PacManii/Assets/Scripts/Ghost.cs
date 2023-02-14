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
    [SerializeField] private Transform pacManPos;
    private Movement movement;
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
    void Start()
    {
        movement = GetComponent<Movement>();
        // GHOST INIT + STATE SET
        // SET MATERIAL COLOR
        // START BEHAVIOUR
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // CALC DIST² x² + y²
        // CHANGE DIR WITH LESS DIST²
        SwitchState();
        ChangeDir();
        
    }
    private void SwitchState()
    {

    }
    private void ChangeDir()
    {
        Debug.Log(movement.GridPosition());
        // CHECK EVERY DIRECTION WITH PACMAN POS 
        // NEXT DIRECTION = CALC DIRECTION
    }
    private Vector2 Target()
    {
        // STATE 
        // NAME DIFFERENT
        return Vector2.zero;
    }
    //////////////// TARGETING AND POSITION CALCULATION //////////////////
    private Vector2 ShortestDistance()
    {
        // CHECK ALL POSSIBLE DIRECTIONS  mov.nextDirection
        // CHECK DISTANSTE² FOREACH DIRECTION
        // RETURN SHORTEST
        return Vector2.zero;
    }
    public int Distance(Vector3 pacManPos, Vector2 direction)
    {
        //LINEAR CALCULATION DIST² =  x² + y² ( 8² + 2²)  = 68
        return 0;
    }

//////////////// TARGETING AND POSITION CALCULATION //////////////////
    private void GhostChase()
    {
        // MAIN STATE
    }
    private void GhostScatter()
    {
        //TIMEEVENT AFTER CHASE
    }
    private void GhostEaten()
    {
        //IF PACMAN HIT WHILE FRIGHTENED STATE
        // new Target GhostHouse, when reached turn back to ChaseMode
    }
    private void GhostFrightened()
    {
        // IF PACMAN EATS POWER PELLET
        // TURN 180° 
        // DIRECTION RANDOM
    }
}
