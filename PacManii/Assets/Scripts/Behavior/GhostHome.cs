
using UnityEngine;
using System.Collections.Generic;

public class GhostHome :  GhostBehaviour
{
    // WAYPOINT SYSTEM FROM MAZEGRID
    
    private Vector2Int HomeEntryTarget = new Vector2Int(0, 3);
    private Vector2Int targetPosition= Vector2Int.zero;
    public bool _drawGizmos;
    private void OnDisable()
    {
        this.ghost.transform.Translate(Vector3.up*2,Space.World);
        
        ghost.scatter.Enable();
        // Debug.Log("CHASE STARTED");
        this.ghost.movement.ghostMoveDone = true;
        
    }
    
    private void OnEnable() {
        targetPosition = HomeEntryTarget;
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
                // SCRIPT 
                this.ghost.transform.Translate(Vector3.down*2,Space.World);
                ghost.home.Disable();
            }
        
        

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
