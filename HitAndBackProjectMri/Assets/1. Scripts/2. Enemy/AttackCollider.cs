using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public int damage;
    private bool isAttack = false;
    public float power;

    public void SetAttack(bool value)
    {
        isAttack = value;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�ε���");
        if (!isAttack) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�ε���");
            IDamageble hp = collision.gameObject.GetComponent<IDamageble>();

            if (hp != null)
            {
                ContactPoint cp = collision.contacts[0]; //�浹��ġ
                Vector3 normal = cp.normal; //�浹�븻
                if(Mathf.Abs(normal.x) < 0.1f) //�������� ������ x��ġ�� �۴ٸ� Ű���ִ°� �ƴұ�
                {
                    normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;

                }
                //if (Mathf.Abs(normal.z) < 0.1f) //�������� ������ x��ġ�� �۴ٸ� Ű���ִ°� �ƴұ�
                //{
                //    normal.z = collision.gameObject.transform.position.z < transform.position.z ? 1 : -1;

                //}
                //�׷� z�൵ �ؾߵǴ� �� �ƴϾ�
                hp.HealthDown(damage, cp.point, normal, power);
            }
        }
    }
}
