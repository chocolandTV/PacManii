
using UnityEngine;
using System.Collections.Generic;

public class GhostScatter : GhostBehaviour
{
    // WAYPOINT SYSTEM FROM MAZEGRID
    private Vector2[] blinkyPattern = new Vector2[4] { new Vector2(15, 19), new Vector2(18, 19), new Vector2(18, 17), new Vector2(15, 17) };
    private Vector2[] InkyPattern = new Vector2[8] { new Vector2(13, 5), new Vector2(15, 5), new Vector2(15, 3), new Vector2(18, 3), new Vector2(18, 1), new Vector2(11, 1), new Vector2(11, 3), new Vector2(13, 3) };
    private Vector2[] PinkyPattern = new Vector2[4] { new Vector2(5, 19), new Vector2(2, 19), new Vector2(2, 17), new Vector2(5, 17) };
    private Vector2[] ClydePattern = new Vector2[8]{new Vector2(7,5), new Vector2(5,5), new Vector2(5,3), new Vector2(2,3),new Vector2(2,1), new Vector2(9,1), new Vector2(9,3), new Vector2(7,3) };
    // LATER TARGET SURROUNDING IMPLEMENT !
    private Vector2Int clydeTarget = new Vector2Int(6,2);
    private Vector2Int InkyTarget = new Vector2Int(14,2);
    private Vector2Int PinkyTarget = new Vector2Int(4,18);
    private Vector2Int BlinkyTarget = new Vector2Int(16,18);
    private Vector2Int target;
    private Vector2Int lastDir;
    private void FixedUpdate() {
        if(this.ghost.movement.ghostMoveDone)
        {
            this.ghost.movement.ghostMoveDone=false;
            updateTarget();
            HandleNextDirection();
        }
    }
    private void updateTarget()
    {
        target = ghostTargetSwitch(this.ghost.ghostName);
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
                this.ghost.movement.nextDirection = lastDir;
            else// CHECK CORNERS
            {   // GO NEXT INTERSECTION 
                validDirections.Remove(-lastDir);
                this.ghost.movement.nextDirection = validDirections[0];
                
            }
        }
        

    }
    private void OnDisable()
    {
        this.ghost.chase.Enable(); // SCATTER NORMALIZE
    }
    private void OnEnable() {
        this.ghost.scatter.Enable();
    }
    private Vector2Int ghostTargetSwitch(Ghost.Name ghost)
    {
        Vector2Int result;
        switch (ghost)
        {
            case Ghost.Name.Blinky: 
                result = BlinkyTarget;
                break;
            case Ghost.Name.Clyde:
                result = clydeTarget;
                break;
            case Ghost.Name.Inky:
                result  = InkyTarget;
                break;
            case Ghost.Name.Pinky:
                result  = PinkyTarget;
                break;
            default: 
                result = BlinkyTarget;
                break;
        }
        return result;
    }
}
