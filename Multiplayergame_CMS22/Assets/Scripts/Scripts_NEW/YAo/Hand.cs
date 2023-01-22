using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AnimationClip;

[RequireComponent(typeof(Animator))]

public class Hand : MonoBehaviour
{
    Animator animator;
    private float speed;
    private float triggerTarget;
    private float smashCurrent;
    private string animatorSmashParam = "smash"; 
    private string animatorTriggerParam = "trigger";
    private float smashTarget;
    private float triggerCurrent;
    private float v;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    internal void SetTrigger()
    {

        triggerTarget = v;
    }

    internal void SetSmash()
    {

        smashTarget = v;
    }


    void AnimateHand()
    {
        if(smashCurrent != smashTarget)
        {
            smashCurrent = Mathf.MoveTowards(smashCurrent, smashTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorSmashParam, smashCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(smashCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        //animator.SetTrigger("HandAnimation");
    }

}
