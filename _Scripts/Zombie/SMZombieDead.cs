using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMZombieDead : StateMachineBehaviour
{
    public int index;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
       index =  Random.Range(0, 3);
        animator.GetComponent<ZombieController>().ActivateDeadFx(index);
    }
}
