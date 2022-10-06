using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPointElement : MonoBehaviour
{
   [SerializeField] private Renderer   mesh;
   private                  TrackPoint line;

   public void Initialize(TrackPoint parentLine, out Renderer meshRenderer)
   {
      line         = parentLine;
      meshRenderer = mesh;
   }
   
   public Color Collide()
   {
      if (!line.isActive) return default;
      
      gameObject.SetActive(false);
      line.isActive = false;

      return line.GetColor(this);
   }
}
