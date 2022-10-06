using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float     moveSpeed;
    [SerializeField] private float     moveSmooth;
    [SerializeField] private float     controlLimit;
    [SerializeField] private float     controlSensitive;
    private                  Vector3   initialPoint;
    private                  Vector3   finalPoint;
    private                  UniInput  input;
    private                  Coroutine moveRoutine;
    private                  float     currentControlOffset;
    private                  bool      moving;
    private                  float     pos;
    private                  int       trackLength;

    public event Action<Vector3> WhileMove;
    public event Action<bool>    OnMoveStop;

    public void Initialize(Track track, UniInput inputComponent)
    {
        var position = transform.position;
        initialPoint         = track.GetFirst(position.y);
        finalPoint           = track.GetLast(position.y);
        trackLength          = track.Size;
        input                = inputComponent;
        currentControlOffset = position.x;
    }

    public void MoveStart()
    {
        moving = true;
    }

    public void MoveStop(bool completed)
    {
        moving = false;
        OnMoveStop?.Invoke(completed);
    }

    private void FixedUpdate()
    {
        if (!moving) return;
        var remaining      = finalPoint.z - transform.position.z;
        var targetOffset   = Mathf.Clamp(currentControlOffset + input.Axis.x, -controlLimit, controlLimit);
        var targetPosition = Vector3.Lerp(initialPoint, finalPoint, pos);

        currentControlOffset = Mathf.Lerp(currentControlOffset, targetOffset, controlSensitive);
        targetPosition.x     = currentControlOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmooth * Time.fixedDeltaTime);

        pos += moveSpeed * Time.fixedDeltaTime / trackLength;
        
        WhileMove?.Invoke(transform.position);

        if (remaining <= 0.1f)
        {
            MoveStop(true);
        }
    }
}