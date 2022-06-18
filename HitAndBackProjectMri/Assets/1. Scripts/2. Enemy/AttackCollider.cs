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
        Debug.Log("부딪힘");
        if (!isAttack) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("부딪힘");
            IDamageble hp = collision.gameObject.GetComponent<IDamageble>();

            if (hp != null)
            {
                ContactPoint cp = collision.contacts[0]; //충돌위치
                Vector3 normal = cp.normal; //충돌노말
                if(Mathf.Abs(normal.x) < 0.1f) //맞은곳의 법선의 x위치가 작다면 키워주는거 아닐까
                {
                    normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;

                }
                //if (Mathf.Abs(normal.z) < 0.1f) //맞은곳의 법선의 x위치가 작다면 키워주는거 아닐까
                //{
                //    normal.z = collision.gameObject.transform.position.z < transform.position.z ? 1 : -1;

                //}
                //그럼 z축도 해야되는 거 아니야
                hp.HealthDown(damage, cp.point, normal, power);
            }
        }
    }
}
