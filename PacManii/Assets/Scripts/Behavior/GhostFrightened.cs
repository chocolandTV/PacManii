
using UnityEngine;
using System.Collections.Generic;
public class GhostFrightened : GhostBehaviour
{
    public bool eaten { get; private set; }
    public GameObject BaseGhost;
    public GameObject BaseFrightened;
    public GameObject BaseEyes;
    // private Vector2Int lastDir;
    private Vector2Int HomePosition = new Vector2Int(0, 1);
    private Vector2Int currentTarget;
    private void FixedUpdate()
    {

        updateTarget();
        
        
        

    }
    private void updateTarget()
    {
        if (!eaten)
        {
            currentTarget = this.ghost.movement.GridPosition(this.ghost.pacManPos.position);
        }else{
            currentTarget  = HomePosition;
            HandleTarget();
        }
        
    
        

    }
private void HandleTarget()
{
    List<Vector2Int> validDirections = this.ghost.movement.AvailableDirections();
    if (validDirections.Count > 2)
    {
        Vector2Int ghostpos = this.ghost.movement.GridPosition();
        float minDistance = float.MaxValue;
        foreach (Vector2Int x in validDirections)
        {
            float dist = Vector2Int.Distance(x + ghostpos, currentTarget);
            if (dist < minDistance)
                minDistance = dist;
        }
        // #3
        Vector2Int direction = Vector2Int.zero;
        foreach (Vector2Int x in validDirections)
        {
            if (Vector2Int.Distance(x + ghostpos, currentTarget) == minDistance)
            {
                direction = x;
                break;
            }
        }
        Debug.Log("NEW DIRECTION");
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
private void HandleFrightenedGhost()
{
    List<Vector2Int> validDirections = this.ghost.movement.AvailableDirections();
    if (validDirections.Count > 2)
    {
        Vector2Int ghostpos = this.ghost.movement.GridPosition();
        float maxDistance = float.MinValue;
        foreach (Vector2Int x in validDirections)
        {
            float dist = Vector2Int.Distance(x + ghostpos, currentTarget);
            if (dist > maxDistance)
                maxDistance = dist;
        }
        // #3
        Vector2Int direction = Vector2Int.zero;
        foreach (Vector2Int x in validDirections)
        {
            if (Vector2Int.Distance(x + ghostpos, currentTarget) == maxDistance)
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
public override void Enable(float duration)
{
    base.Enable(duration);

    BaseGhost.SetActive(false);
    BaseFrightened.SetActive(true);
    BaseEyes.SetActive(false);
    this.ghost.ghostState =Ghost.GhostState.FRIGHTENED;
    Invoke(nameof(Flash), duration / 2f);
}

public override void Disable()
{
    base.Disable();

    BaseGhost.SetActive(true);
    BaseFrightened.SetActive(false);
    BaseEyes.SetActive(false);
    this.ghost.ghostState =Ghost.GhostState.CHASE;

}

private void Eaten()
{
    eaten = true;

    ///target scripted ghost home without collider

    ghost.home.Enable(duration);

    BaseGhost.SetActive(false);
    BaseFrightened.SetActive(false);
    BaseEyes.SetActive(true);

}

private void Flash()
{
    if (!eaten)
    {
        BaseEyes.SetActive(false);
        // ANIMATE
    }
}

private void OnEnable()
{
    // ANIMATE
    // ghost.movement.speedMultiplier *= 0.5f;
    eaten = false;
}

private void OnDisable()
{
    // ghost.movement.speedMultiplier *= 1.5f;
    eaten = false;
}

}
