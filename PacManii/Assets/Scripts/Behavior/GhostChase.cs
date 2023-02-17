
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private Vector2 clydeOffset = new Vector2(4,4);  // DISTANCE 
    private Vector2 InkyOffset = new Vector2(3,-3);
    private Vector2 PinkyOffset = new Vector2(3,0);
    private Vector2 BlinkyOffset = new Vector2(0,0);
    

   private void FixedUpdate() { // CALL IS TO MUCH !° ON FINISHTARGET !
        if(this.enabled && this.ghost.frightened.enabled && this.ghost.TargetDone)
        {
            this.ghost.movement.nextTarget(ghostTargetSwitch(this.ghost.ghostName));
        }
        if(this.ghost.chase == null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero; // DIRECTION TO NULL
            float minDistance = this.ghost.; // Minimun distance
            // MINDISTANCE
            // WAIT FOR NEXT TARGET
            foreach (Vector2 availableDirection in this.ghost.movement.AvailableDirections())
            {
                Vector3 newPosition = this.ghost.transform.position + new Vector3(availableDirection.x,availableDirection.y,-1);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if(distance < minDistance)
                
            }

        }
    }
    // NEXT DIRECTION CALL
    private void OnDisable() {
        this.ghost.scatter.Enable();
    }
    private Vector2 ghostTargetSwitch(Ghost.Name ghost)
    {   
        int randomClydeAroundTarget;
        Vector2 result;
        switch (ghost)
        {
            case Ghost.Name.Blinky: 
                result =  this.ghost.movement.GetPacManPosition() + BlinkyOffset;
                break;
            case Ghost.Name.Clyde:
                randomClydeAroundTarget= Random.Range(0,1);
                if(randomClydeAroundTarget >0)  
                    clydeOffset -= clydeOffset*2;
                    
                result = this.ghost.movement.GetPacManPosition() + clydeOffset;
                break;
            case Ghost.Name.Inky:
                result  = this.ghost.movement.GetPacManPosition() + InkyOffset;
                break;
            case Ghost.Name.Pinky:
                result  = this.ghost.movement.GetPacManPosition() + PinkyOffset;
                break;
            default: 
                result = this.ghost.movement.GetPacManPosition() + BlinkyOffset;
                break;
        }
        return result;
    }
}
