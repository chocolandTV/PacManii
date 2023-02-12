using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingAnim : MonoBehaviour
{
   [SerializeField]private Animator top;
   [SerializeField]private Animator bot;
   public void OnDeathAnimate()
   {
      Debug.Log("CAST DYING ANIMATION");
      top.SetTrigger("dying");
      bot.SetTrigger("dying");
   }
   public void OnStartAnimate()
   {
      top.ResetTrigger("dying");
      bot.ResetTrigger("dying");
   }
}
