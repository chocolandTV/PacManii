
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private Vector2 clydeOffset = new Vector2(4,4);  // DISTANCE 
    private Vector2 InkyOffset = new Vector2(3,-3);
    private Vector2 PinkyOffset = new Vector2(3,0);
    private Vector2 BlinkyOffset = new Vector2(0,0);
    
private void Start() {
    Debug.Log("CHASE STARTED");
}
   private void FixedUpdate() 
   {
        // // NEW TARGETDIRECTION ON NEXTMOVE
        // if(this.enabled && this.ghost.frightened.enabled && this.ghost.TargetDone)
        // {
        //     this.ghost.movement.nextTarget(ghostOffset(this.ghost.ghostName));
        // }
        if(this.ghost.chase != null && this.enabled && !this.ghost.frightened.enabled)
        {
           int distance=100;
           Vector2 tempVector = Vector2.zero;
            foreach (Vector2 availableDirection in this.ghost.movement.AvailableDirections())
            {
              
              if(availableDirection != Vector2.zero)
              {
                if(this.ghost.movement.DistanceCheck(availableDirection + ghostOffset(this.ghost.ghostName)) < distance)
                {
                    tempVector = availableDirection;
                    
                }else
                {
                    distance = this.ghost.movement.DistanceCheck(availableDirection +ghostOffset(this.ghost.ghostName));
                }
              } 
                
                
            }
            this.ghost.movement.nextDirection = tempVector;
            Debug.Log(this.ghost.movement.nextDirection);
        }
    }
    // NEXT DIRECTION CALL
    private void OnDisable() {
        this.ghost.chase.Enable();
    }
    private Vector2 ghostOffset(Ghost.Name ghost)
    {   
        int randomClydeAroundTarget;
        Vector2 result;
        switch (ghost)
        {
            case Ghost.Name.Blinky: 
                result =   BlinkyOffset;
                break;
            case Ghost.Name.Clyde:
                randomClydeAroundTarget= Random.Range(0,1);
                if(randomClydeAroundTarget >0)  
                    clydeOffset -= clydeOffset*2;
                    
                result =  clydeOffset;
                break;
            case Ghost.Name.Inky:
                result  =  InkyOffset;
                break;
            case Ghost.Name.Pinky:
                result  = PinkyOffset;
                break;
            default: 
                result = BlinkyOffset;
                break;
        }
        return result;
    }
}
