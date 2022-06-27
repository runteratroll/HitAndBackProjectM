using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public float attackDealy;

    protected float lastAttackTime = 0;
    public bool isAttack = false; //��������Ʈ���� ���뵥������ ���� ������
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
            if (lastAttackTime + attackDealy <= Time.time) //�����̰� ���� �ٽ� ���ݰ����ϴٸ�
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

        //�浹�� �༮���Լ� IDamageable�� �ִ��� üũ�ؼ�
        IDamageble hp = collision.gameObject.GetComponent<IDamageble>();

        //�׳༮�� null�� �ƴϸ� 
        if (hp != null)
        {
            ContactPoint cp2 = collision.contacts[0];
            Vector2 normal = cp2.normal;
            if (Mathf.Abs(normal.x) < 0.1f)
                normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1; //�������� �¾Ҵ��� ��𿡼� �¾Ҵ��� Ȯ��?

            hp.HealthDown(damage, cp2.point, normal, 20);
        }
        // �浹�� �༮�� contacts[0]�� �����ͼ� point��, normal�� �̾Ƴ���
        //�װɷ� �������� ���Ѵ�.
    }
}
