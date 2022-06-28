using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private float lastDamageTime;
    public float damageDelay;
    public HealthBar healthBar;
    PlayerMove playerMove;
    PlayerAnimation playerAnimation;
    SkinnedMeshRenderer[] skinnedMeshRenderer;
    //����Ʈ�ý��۵� ������ �ϴµ�

    public float delay;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        lastDamageTime = Time.time;
        skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    public override void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f)
    {
        if (lastDamageTime + damageDelay > Time.time) return; //���� �������� �����ְ�

        //playerAnimation.SetDead(false);
        lastDamageTime = Time.time;
        
        //���¸��� �ٸ�����
        //���⿡�� ���� ���󰡴� �Լ���

        playerMove.SetHit(normal, power, delay);
        playerSetColor();
        base.HealthDown(damage, hitPoint, normal, power); //�ڽ��Ѵٰ� �θ�͵� ȣ���������ʴ±���
        healthBar.SetHealth((float)currentHp / (float)maxHp);


    }


    IEnumerator playerSetColor()
    {

        for (int i = 0; i < 6; i++)
        {
            skinnedMeshRenderer[i].material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            skinnedMeshRenderer[i].material.color = Color.white;
        }

    }

  

    protected override void OnDie()
    {
        Debug.Log("����");
        playerAnimation.SetDead(true);
        playerMove.SetStopMove(true);
        
   

        gameObject.tag = "Enemy";

        //���⿡ ���� �÷��̾������ִϸ��̼�, ȿ����, ����Ʈ ,
    }


    public void HealthMax()
    {

        Loader.Load(Loader.Scene.Dead);
        healthBar.SetHealth(maxHp);
        playerAnimation.SetDead(false);
        playerMove.SetStopMove(true);
        currentHp = maxHp;
        gameObject.tag = "Player";
    }


 
}
