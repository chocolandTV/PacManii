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
    public int points = 200;
    public GameObject GhostEnemy {get;private set;}
    
    public enum Name
    {
        Inky,// Cyan
        Blinky, //Red
        Pinky,//Pink
        Clyde // Orange FFB751
    }
    // Start is called before the first frame update
    void Start()
    {
        // GHOST INIT + STATE SET
        // SET MATERIAL COLOR
        // START BEHAVIOUR
    }

    // Update is called once per frame
    void Update()
    {
        // CALC DIST² x² + y²
        // CHANGE DIR WITH LESS DIST²
        
    }
    private void EnemyStateChase()
    {
        // MAIN STATE
    }
    private void EnemyStateScatter()
    {
        //TIMEEVENT AFTER CHASE
    }
    private void EnemyStateEaten()
    {
        //IF PACMAN HIT WHILE FRIGHTENED STATE
        // new Target GhostHouse, when reached turn back to ChaseMode
    }
    private void EnemyStateFrightened()
    {
        // IF PACMAN EATS POWER PELLET
        // TURN 180° 
        // DIRECTION RANDOM
    }
}
