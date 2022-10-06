using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private                 Animator animator;
    private static readonly int      runKey     = Animator.StringToHash("run");
    private static readonly int      victoryKey = Animator.StringToHash("victory");

    public void Initialize(Animator animatorComponent)
    {
        animator = animatorComponent;
    }
    
    public void ToggleRunAnimation(bool value)
    {
        animator.SetBool(runKey, value);
    }

    public void PlayVictoryAnimation()
    {
        animator.SetTrigger(victoryKey);
    }
    
}
