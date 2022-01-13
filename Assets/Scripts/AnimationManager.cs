using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimationManager : MonoBehaviour
{
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        defaultAnimation();
    }

    public void defaultAnimation()
    {
        animator.Play(0);
    }
}
