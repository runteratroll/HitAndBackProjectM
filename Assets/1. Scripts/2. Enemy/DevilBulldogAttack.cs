using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogAttack : EnemyAttack
{

    DevilBulldogAnimation anim;
    protected AttackCollider attackCol; 
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<DevilBulldogAnimation>();
        attackCol = GetComponentInChildren<AttackCollider>(); //gameObject가 널이라서 그런가보다

    }


    public override void Attack() //이걸이제 조리를 잘해야 하는데
    {
        Debug.Log("attack");
        anim.SetAttack();
        attackCol.SetAttack(true);

    }

    public void SetAttackDisable()
    {
        attackCol.SetAttack(false);
    }
    //모든 적은 몸통박치기가 있잖아? 그러면 자식마다 해주는게 아니라 부모에서 OnCollision을 처리하면 되는게 아닐까?
    //플레이어가 고블린과 몸통박치기 할 경우 . 맞아야지
    

    
    
}
