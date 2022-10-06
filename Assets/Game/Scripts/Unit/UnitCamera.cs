using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCamera : MonoBehaviour
{
   [SerializeField] private Transform cameraRoot;
   [SerializeField] private float     followSpeed;
   [SerializeField] private float     threshold;
   private                  Vector3   targetPosition;
   
   public void UpdatePosition(Vector3 unitPosition)
   {
      targetPosition   = unitPosition;
      targetPosition.x = cameraRoot.position.x;
   }

   private void FixedUpdate()
   {
      var dist = (cameraRoot.position - targetPosition).magnitude;
      
      if(dist <= threshold) return;
      
      cameraRoot.position = Vector3.Lerp(cameraRoot.position, targetPosition, followSpeed * Time.fixedDeltaTime);
   }
}
