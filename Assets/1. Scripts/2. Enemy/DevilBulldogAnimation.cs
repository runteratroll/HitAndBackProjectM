using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogAnimation : MonoBehaviour
{
    Animator anim;

    private readonly int hashAttack = Animator.StringToHash("Attack");

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void SetAttack()
    {
        anim.SetTrigger(hashAttack);
    }
}
