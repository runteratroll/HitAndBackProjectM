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
    //이펙트시스템도 만들어야 하는데

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
        if (lastDamageTime + damageDelay > Time.time) return; //연속 데미지는 막아주고

        lastDamageTime = Time.time;
        base.HealthDown(damage , hitPoint, normal , power); //자식한다고 부모것도 호출해주진않는구나
        //상태마다 다른것을
        //여기에서 이제 날라가는 함수를

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

        //여기에 이제 플레이어죽음애니메이션, 효과음, 이펙트 ,
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
