using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrow : MonoBehaviour
{
    [SerializeField] private float     growOffset;
    [SerializeField] private float     growDuration;
    [SerializeField] private int       growIterationLimit;
    private                  int       currentGrowIteration;
    private                  Vector3   startViewSize;
    private                  Coroutine currentRoutine;
    private                  Transform unitView;
    private                  Action    onInterrupt;

    public void Initialize(Transform view)
    {
        unitView      = view;
        startViewSize = view.localScale;
    }


    public void Grow()
    {
        HandleSizeChange(false);
    }


    public void Reduce(Action onCollapse = null)
    {
        HandleSizeChange(true, onCollapse);
    }


    private void HandleSizeChange(bool negative, Action onCollapse = null)
    {
        currentGrowIteration = Mathf.Clamp(currentGrowIteration + (negative ? -1 : 1), -1, growIterationLimit);
        
        if (currentGrowIteration < 0)
        {
            onCollapse?.Invoke();
            return;
        }

        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
            currentRoutine = null;
        }

        currentRoutine = StartCoroutine(GrowRoutine());
    }


    private IEnumerator GrowRoutine()
    {
        var start  = unitView.localScale;
        var target = startViewSize + Vector3.one * (growOffset * currentGrowIteration);
        var time   = 0f;

        while (time <= growDuration)
        {
            unitView.localScale = Vector3.Lerp(start, target, time / growDuration);

            time += Time.deltaTime;

            yield return null;
        }

        currentRoutine = null;
    }
}