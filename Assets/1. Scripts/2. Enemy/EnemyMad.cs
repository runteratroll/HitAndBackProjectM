using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMad : Mad
{
    public DevilBulldogAnimation animation;

    public override void FirstState()
    { 
        animation.anim.speed = 1f;
        Agent.speed = EnemyScriptableObject.Speed;
        //transform.localScale = new Vector3(1f, 1f, 1f);
        animation.SetMad(false);
        particle.Stop();
    }
    public override void MadM()
    {

        particle.Play();
        animation.SetMad(true);
        Agent.speed = EnemyMadStateScriptAble.Speed;
        Debug.Log("¸Åµå");
        animation.anim.speed = 1.5f;
        //transform.localScale = new Vector3(5f, 5f, 5f);

    }

    
}
