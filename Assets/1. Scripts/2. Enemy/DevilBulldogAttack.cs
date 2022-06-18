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
        attackCol = GetComponentInChildren<AttackCollider>(); //gameObject�� ���̶� �׷�������

    }


    public override void Attack() //�̰����� ������ ���ؾ� �ϴµ�
    {
        Debug.Log("attack");
        anim.SetAttack();
        attackCol.SetAttack(true);

    }

    public void SetAttackDisable()
    {
        attackCol.SetAttack(false);
    }
    //��� ���� �����ġ�Ⱑ ���ݾ�? �׷��� �ڽĸ��� ���ִ°� �ƴ϶� �θ𿡼� OnCollision�� ó���ϸ� �Ǵ°� �ƴұ�?
    //�÷��̾ ����� �����ġ�� �� ��� . �¾ƾ���
    

    
    
}
