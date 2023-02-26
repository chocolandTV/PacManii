
using UnityEngine;
using System.Collections.Generic;

public class GhostScatter : GhostBehaviour
{
    // WAYPOINT SYSTEM FROM MAZEGRID
    private Vector2Int[] blinkyPattern = new Vector2Int[4] { new Vector2Int(15, 19), new Vector2Int(18, 19), new Vector2Int(18, 17), new Vector2Int(15, 17) };
    private Vector2Int[] InkyPattern = new Vector2Int[8] { new Vector2Int(13, 5), new Vector2Int(15, 5), new Vector2Int(15, 3), new Vector2Int(18, 3), new Vector2Int(18, 1), new Vector2Int(11, 1), new Vector2Int(11, 3), new Vector2Int(13, 3) };
    private Vector2Int[] PinkyPattern = new Vector2Int[4] { new Vector2Int(5, 19), new Vector2Int(2, 19), new Vector2Int(2, 17), new Vector2Int(5, 17) };
    private Vector2Int[] ClydePattern = new Vector2Int[8] { new Vector2Int(7, 5), new Vector2Int(5, 5), new Vector2Int(5, 3), new Vector2Int(2, 3), new Vector2Int(2, 1), new Vector2Int(9, 1), new Vector2Int(9, 3), new Vector2Int(7, 3) };
    // LATER TARGET SURROUNDING IMPLEMENT !
    private Vector2Int clydeTarget = new Vector2Int(6, 2);
    private Vector2Int InkyTarget = new Vector2Int(14, 2);
    private Vector2Int PinkyTarget = new Vector2Int(4, 18);
    private Vector2Int BlinkyTarget = new Vector2Int(16, 18);
    private Vector2Int targetPosition= Vector2Int.zero;
    // private Vector2Int lastDir;
    private int currentWayPoint = 0;
    // private List<Vector2Int> tempSteps = new List<Vector2Int>();
    public bool _drawGizmos;
    private void OnDisable()
    {
        ghost.chase.Enable();
        Debug.Log("CHASE STARTED");
        this.ghost.movement.ghostMoveDone = true;
        this.ghost.movement.speedMultiplier += 1.01f;
    }
    
    
    private void FixedUpdate()
    {

        updateTarget();

        HandleNextDirection();
    
    }
    private void updateTarget()
    {
        if (targetPosition == this.ghost.movement.GridPosition())
            {
                if (currentWayPoint == ghostTargetSwitch(this.ghost.ghostName).Length-1)// when all targets done renew
                {
                    currentWayPoint = 0;
                }else{
                    currentWayPoint++;
                }
                // Debug.Log("Reached Waypoint.");
                

            }
        targetPosition = ghostTargetSwitch(this.ghost.ghostName)[currentWayPoint];
        

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
                float dist = Vector2Int.Distance(x + ghostpos, targetPosition);
                if (dist < minDistance)
                    minDistance = dist;
            }
            // #3
            Vector2Int direction = Vector2Int.zero;
            foreach (Vector2Int x in validDirections)
            {
                if (Vector2Int.Distance(x + ghostpos, targetPosition) == minDistance)
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

    private Vector2Int[] ghostTargetSwitch(Ghost.Name ghost)
    {
        Vector2Int[] result;
        switch (ghost)
        {
            case Ghost.Name.Blinky:
                result = blinkyPattern;
                break;
            case Ghost.Name.Clyde:
                result = ClydePattern;
                break;
            case Ghost.Name.Inky:
                result = InkyPattern;
                break;
            case Ghost.Name.Pinky:
                result = PinkyPattern;
                break;
            default:
                result = blinkyPattern;
                break;
        }
        return result;
    }
    private void OnDrawGizmos()
    {
        if (_drawGizmos && Application.isPlaying)
        {
            if (targetPosition != null )
            {
                Vector3 cutPosition = this.ghost.movement.RealPosition(targetPosition);
                
                Gizmos.color = Color.blue;
                
                Gizmos.DrawCube(cutPosition, Vector3.one * 0.5f);
            }
        }
    }
}
