using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitColor : MonoBehaviour
{
   private                 Color currentColor;
   private static readonly int   colorKey = Shader.PropertyToID("_Color");

   public void Initialize(ColorSettings colorSettings, Renderer viewRenderer)
   {
      currentColor = colorSettings.unitColors[Random.Range(0, colorSettings.unitColors.Count)];
      
      var block = new MaterialPropertyBlock();
      viewRenderer.GetPropertyBlock(block);
      block.SetColor(colorKey, currentColor);
      viewRenderer.SetPropertyBlock(block);

   }

   public bool Compare(Color other)
   {
      return other == currentColor;
   }
}
