using UnityEngine;

[RequireComponent(typeof(Ghost))]

public abstract class GhostBehaviour: MonoBehaviour
{
   public Ghost ghost {get; private set;}
    public float duration;
   private void Awake() {
        this.ghost = GetComponent<Ghost>();
        
   }
   public void Enable()
   {
        Enable(duration);
   }
   public virtual void Enable(float duration)
   {
        enabled =true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
   }
   public virtual void Disable()
   {
        enabled = false;
        CancelInvoke();
   }
}
