// // NEW TARGETDIRECTION ON NEXTMOVE
// if(this.enabled && this.ghost.frightened.enabled && this.ghost.TargetDone)
// MOVEMENT PATTERN
// {https://dev.to/code2bits/pac-man-patterns--ghost-movement-strategy-pattern-1k1a
// ALGORYTHM https://gameinternals.com/understanding-pac-man-ghost-behavior

//     this.ghost.movement.nextTarget(ghostOffset(this.ghost.ghostName));
// }
// PROBLEM:  GHOST IS WAITING IF PACMAN IS RIGHT OR TOP OF HIM AND STILL MOVING TO TOP RIGHT CORNER
/*
    ALGORYTHM: CHASE  = 
    geg: pacman                 ges.: nextDirection ( x² + y² )
        ghostposition
        grid
        availableMovements
        points of interest available directions 3+

        Problems:  Sackgassen
                   Intersections
    #1 welche Richtungen sind legal
    #2 wenn mögliche richtungen >3 
             jede richtung und distance zu ziel
             minimum der distancen speichern
    #3    für jede legale Richtung    
            wenn distance = minimun 
                return legale Richtung


    Foreach Available Pos
            if < 2 change dir
            else


*/

using UnityEngine;
using System.Collections.Generic;

public class GhostChase : GhostBehaviour
{
    [SerializeField] private Vector2 Offset = new Vector2(0, 0);  // DISTANCE 

    public Vector2Int target;
    // private Vector2Int lastDir;

    private void OnDisable()
    {
        ghost.scatter.Enable();
        Debug.Log("SCATTER STARTED");
        this.ghost.movement.ghostMoveDone = true;
        
        
    }
    private void FixedUpdate()
    {

        if (this.ghost.movement.ghostMoveDone)
        {
            this.ghost.movement.ghostMoveDone = false;
            updateTarget();
        }
        HandleNextDirection();
        

    }
    private void updateTarget()
    {
        switch (this.ghost.ghostName)
        {
            case Ghost.Name.Blinky:
                target = this.ghost.movement.GridPosition(this.ghost.pacManPos.position);
                break;
            case Ghost.Name.Inky: // Pacman 2 Rechts + Direction von Blinky to target.Length *2
                target = this.ghost.movement.GridPosition(this.ghost.pacManPos.position);
                break;
            case Ghost.Name.Pinky:
                target = this.ghost.movement.GridPosition(this.ghost.pacManPos.position);
                break;
            case Ghost.Name.Clyde:
                target = this.ghost.movement.GridPosition(this.ghost.pacManPos.position);
                break;
            default:
                target = Vector2Int.zero;
                break;
        }
    }
    private Vector2Int InkyTargeting(Vector2Int target)
    {
        // BlinkyPos distance.Length to pacypos * 2
        // Pacman + 2
        Vector2Int pacypos = this.ghost.movement.GridPosition(this.ghost.pacManPos.position);
        Quaternion rotation = this.ghost.pacManPos.rotation;
        if (rotation.z == 90) // RIGHT
        {
            pacypos.x += 2;
        }
        else
        {
            pacypos.x -= 2;
        }
        return pacypos;
    }
    private void HandleNextDirection()
    {
        List<Vector2Int> validDirections = this.ghost.movement.AvailableDirections();
        if (validDirections.Count > 2)
        {
            Vector2Int ghostpos = this.ghost.movement.GridPosition();
            float minDistance = float.MaxValue;
            foreach (Vector2Int x in validDirections)
            {
                float dist = Vector2Int.Distance(x + ghostpos, target);
                if (dist < minDistance)
                    minDistance = dist;
            }
            // #3
            Vector2Int direction = Vector2Int.zero;
            foreach (Vector2Int x in validDirections)
            {
                if (Vector2Int.Distance(x + ghostpos, target) == minDistance)
                {
                    direction = x;
                    break;
                }
            }
            this.ghost.movement.nextDirection = direction;
            this.lastDir = direction;
            this.ghost.movement.ghostMoveDone = true;
        }
        else
        {// ONLY 2 DIRECTIONS !
            // CHECK TUNNLES
            if (validDirections.Contains(lastDir))
            {
                this.ghost.movement.nextDirection = lastDir;
                
            }
            else// CHECK CORNERS
            {   // GO NEXT INTERSECTION 
                validDirections.Remove(-lastDir);
                this.ghost.movement.nextDirection = validDirections[0];
                this.lastDir = validDirections[0];
                // Debug.Log(StringDirection(validDirections[0]));
            }
        }


    }

    private string StringDirection(Vector2 pos)
    {
        if (pos == Vector2.up)
            return "validDir(up)";
        if (pos == Vector2.down)
            return "validDir(down)";
        if (pos == Vector2.left)
            return "validDir(left)";

        if (pos == Vector2.right)
            return "validDir(right)";
        return "validDir(Null)";
    }

}
