using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private float lastDamageTime;
    public float damageDelay;
    public HealthBar healthBar;
    PlayerMove playerMove;
    //����Ʈ�ý��۵� ������ �ϴµ�

    public float delay;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
 
        lastDamageTime = Time.time;
    }
    public override void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f)
    {
        if (lastDamageTime + damageDelay > Time.time) return; //���� �������� �����ְ�

        lastDamageTime = Time.time;
        base.HealthDown(damage , hitPoint, normal , power); //�ڽ��Ѵٰ� �θ�͵� ȣ���������ʴ±���
        //���¸��� �ٸ�����
        //���⿡�� ���� ���󰡴� �Լ���

        playerMove.SetHit(normal, power, delay);
        
        healthBar.SetHealth((float)currentHp / (float)maxHp);

    }


 

    protected override void OnDie()
    {
        //���⿡ ���� �÷��̾������ִϸ��̼�, ȿ����, ����Ʈ ,
    }
}
