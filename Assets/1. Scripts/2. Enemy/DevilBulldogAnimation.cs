using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogAnimation : MonoBehaviour
{
    public Animator anim;

    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashScale = Animator.StringToHash("isMad");

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void SetMad(bool isMad)
    {
        anim.SetBool(hashScale, isMad);
    }

    public void SetAttack()
    {
        anim.SetTrigger(hashAttack);
    }
}
