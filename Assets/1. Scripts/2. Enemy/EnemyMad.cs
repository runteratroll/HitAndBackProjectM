using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMad : Mad
{

    //이걸 이제 이벤트프로그래밍을 한다면은?
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
        Debug.Log("매드");
        animation.anim.speed = 1.5f;

    }

    
}
