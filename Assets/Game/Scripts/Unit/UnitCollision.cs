using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollision : MonoBehaviour
{
   public Action<Color> OnCollide;


   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out TrackPointElement element))
      {
         OnCollide?.Invoke(element.Collide());
      }
   }
}
