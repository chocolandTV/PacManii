
using UnityEngine;
using System.Collections.Generic;
public class GhostFrightened : GhostBehaviour
{
    public bool eaten { get; private set; }
    public GameObject body;
    public GameObject eyes;
    public GameObject fright;
    private Vector2Int lastDir;
    private Vector2Int HomePosition = new Vector2Int(0, 1);
    private void FixedUpdate()
    {

        if (eaten)
        {
            MoveToHome();
        }
        else
        {

        }

    }
    private void FrightendRandom()
    {
        List<Vector2Int> validDirections = this.ghost.movement.AvailableDirections();

        if (validDirections.Contains(-lastDir))
        {
            validDirections.Remove(-lastDir);
        }
        //IF PACMAN IS NEARBY TURN 
        
        this.lastDir =validDirections[Random.Range(0,validDirections.Count)];
        this.ghost.movement.nextDirection = this.lastDir;
        this.ghost.movement.ghostMoveDone = true; 
        
    
        

    }
private void MoveToHome()
{
    List<Vector2Int> validDirections = this.ghost.movement.AvailableDirections();
    if (validDirections.Count > 2)
    {
        Vector2Int ghostpos = this.ghost.movement.GridPosition();
        float minDistance = float.MaxValue;
        foreach (Vector2Int x in validDirections)
        {
            float dist = Vector2Int.Distance(x + ghostpos, HomePosition);
            if (dist < minDistance)
                minDistance = dist;
        }
        // #3
        Vector2Int direction = Vector2Int.zero;
        foreach (Vector2Int x in validDirections)
        {
            if (Vector2Int.Distance(x + ghostpos, HomePosition) == minDistance)
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

    body.SetActive(false);
    eyes.SetActive(false);
    fright.SetActive(false);

    Invoke(nameof(Flash), duration / 2f);
}

public override void Disable()
{
    base.Disable();

    body.SetActive(true);
    eyes.SetActive(true);
    fright.SetActive(true);

}

private void Eaten()
{
    eaten = true;

    ///target scripted ghost home without collider

    ghost.home.Enable(duration);

    body.SetActive(false);
    eyes.SetActive(true);
    fright.SetActive(false);

}

private void Flash()
{
    if (!eaten)
    {
        fright.SetActive(false);
        // ANIMATE
    }
}

private void OnEnable()
{
    // ANIMATE
    ghost.movement.speedMultiplier = 0.5f;
    eaten = false;
}

private void OnDisable()
{
    ghost.movement.speedMultiplier = 1f;
    eaten = false;
}

}
