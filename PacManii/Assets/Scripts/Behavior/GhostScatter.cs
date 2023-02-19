
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    // WAYPOINT SYSTEM FROM MAZEGRID
    private Vector2[] blinkyPattern = new Vector2[4] { new Vector2(15, 19), new Vector2(18, 19), new Vector2(18, 17), new Vector2(15, 17) };
    private Vector2[] InkyPattern = new Vector2[8] { new Vector2(13, 5), new Vector2(15, 5), new Vector2(15, 3), new Vector2(18, 3), new Vector2(18, 1), new Vector2(11, 1), new Vector2(11, 3), new Vector2(13, 3) };
    private Vector2[] PinkyPattern = new Vector2[4] { new Vector2(5, 19), new Vector2(2, 19), new Vector2(2, 17), new Vector2(5, 17) };
    private Vector2[] ClydePattern = new Vector2[8]{new Vector2(7,5), new Vector2(5,5), new Vector2(5,3), new Vector2(2,3),new Vector2(2,1), new Vector2(9,1), new Vector2(9,3), new Vector2(7,3) };
    // LATER TARGET SURROUNDING IMPLEMENT !
    private Vector2 clydeTarget = new Vector2(6,2);
    private Vector2 InkyTarget = new Vector2(14,2);
    private Vector2 PinkyTarget = new Vector2(4,18);
    private Vector2 BlinkyTarget = new Vector2(16,18);
    // private void FixedUpdate() {
    //     if(this.enabled && this.ghost.frightened.enabled && this.ghost.TargetDone)
    //     {
    //         this.ghost.movement.nextTarget(ghostTargetSwitch(this.ghost.ghostName));
    //     }
    // }
    // NEXT DIRECTION CALL
    private void OnDisable() {
        this.ghost.chase.Enable();
    }
    private Vector2 ghostTargetSwitch(Ghost.Name ghost)
    {
        Vector2 result;
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
