using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public float attackDealy;

    protected float lastAttackTime = 0;
    public bool isAttack = false; //모든오브젝트마다 몸통데미지가 있지 않을까
    public int damage;

    protected virtual void Awake()
    {
        lastAttackTime = Time.time;
    }

    public abstract void Attack();

    protected virtual void Update()
    {
        if (isAttack)
        {
            if (lastAttackTime + attackDealy <= Time.time) //딜레이가 끝나 다시 공격가능하다면
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
    }

    public virtual void OnStartAttack()
    {
        //Do nothing;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //if ((1 << collision.gameObject.layer & knife.whatIsEnemy) > 0)
        //{
        //   \
        //}

        //충돌한 녀석에게서 IDamageable이 있는지 체크해서
        IDamageble hp = collision.gameObject.GetComponent<IDamageble>();

        //그녀석이 null이 아니면 
        if (hp != null)
        {
            ContactPoint cp2 = collision.contacts[0];
            Vector2 normal = cp2.normal;
            if (Mathf.Abs(normal.x) < 0.1f)
                normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1; //우측에서 맞았는지 어디에서 맞았는지 확인?

            hp.HealthDown(damage, cp2.point, normal, 20);
        }
        // 충돌한 녀석의 contacts[0]를 가져와서 point와, normal을 뽑아내고
        //그걸로 데미지를 가한다.
    }
}
