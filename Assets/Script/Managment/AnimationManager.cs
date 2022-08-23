using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    public void SetTrigger(Animator animator, string triggerParameter)
    {
        animator.SetTrigger(triggerParameter);
    }
    public void SetBool(Animator animator, string boolParameter, bool value)
    {
        animator.SetBool(boolParameter, value);
    }

    public void SetFloat(Animator animator, string floatParameter, float value)
    {
        animator.SetFloat(floatParameter, value);
    }
    public void SetInt(Animator animator, string intParameter, int value)
    {
        animator.SetInteger(intParameter, value);
    }
}
