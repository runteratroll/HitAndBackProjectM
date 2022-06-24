using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogAttack : EnemyAttack
{

    DevilBulldogAnimation anim;
    public AttackCollider attackCol;


    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<DevilBulldogAnimation>();
        //attackCol = GetComponentInChildren<AttackCollider>(); //gameObject�� ���̶� �׷�������

    }


    public override void Attack() //�̰����� ������ ���ؾ� �ϴµ�
    {
        Debug.Log("attack");
        anim.SetAttack();
        attackCol.SetAttack(true);

       
    }


    public void AttackParticle()
    {
        AttackParticle hitParticle = PoolManager.GetItem<AttackParticle>(); //�̸��� ������ ������
        hitParticle.SetRotation(transform.rotation.eulerAngles);
        hitParticle.Play(transform.position + transform.forward * 2);
    }
    public void SetAttackDisable()
    {
        attackCol.SetAttack(false);
    }
    //��� ���� �����ġ�Ⱑ ���ݾ�? �׷��� �ڽĸ��� ���ִ°� �ƴ϶� �θ𿡼� OnCollision�� ó���ϸ� �Ǵ°� �ƴұ�?
    //�÷��̾ ����� �����ġ�� �� ��� . �¾ƾ���
    

    
    
}
