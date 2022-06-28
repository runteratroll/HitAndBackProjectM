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

        lastDamageTime = Time.time;
        base.HealthDown(damage , hitPoint, normal , power); //�ڽ��Ѵٰ� �θ�͵� ȣ���������ʴ±���
        //���¸��� �ٸ�����
        //���⿡�� ���� ���󰡴� �Լ���

        playerMove.SetHit(normal, power, delay);
        playerSetColor();
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
        playerMove.SetStopMove(true);
        
   
        playerAnimation.SetDead(true);
        gameObject.tag = "Enemy";

        //���⿡ ���� �÷��̾������ִϸ��̼�, ȿ����, ����Ʈ ,
    }


    public void HealthMax()
    {

        SceneManager.LoadScene("SampleScene");
        healthBar.SetHealth(maxHp);
        playerAnimation.SetDead(false);
        playerMove.SetStopMove(true);
        currentHp = maxHp;
        gameObject.tag = "Player";
    }


 
}
