using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private float lastDamageTime;
    public float damageDelay;
    //static으로 해주고
    //스태틱 헬스를 해주고 

    public HealthBar healthBar; //헬스바다운을 이제 public이 아닌! 
    PlayerMove playerMove;
    PlayerAnimation playerAnimation;
    SkinnedMeshRenderer[] skinnedMeshRenderer;
    //이펙트시스템도 만들어야 하는데

    public float delay;
    protected override void Awake()
    {
        base.Awake();
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        lastDamageTime = Time.time;
        skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        healthBar.SetHealth((float)currentHp / (float)maxHp);
    }
    public override void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f)
    {
        if (lastDamageTime + damageDelay > Time.time) return; //연속 데미지는 막아주고

        //playerAnimation.SetDead(false);
        lastDamageTime = Time.time;
        
        //상태마다 다른것을
        //여기에서 이제 날라가는 함수를

        playerMove.SetHit(normal, power, delay);
        playerSetColor();
        base.HealthDown(damage, hitPoint, normal, power); //자식한다고 부모것도 호출해주진않는구나
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
        Debug.Log("죽음");
        playerAnimation.SetDead(true);
        playerMove.SetStopMove(true);
        
   

        gameObject.tag = "Enemy";

        //여기에 이제 플레이어죽음애니메이션, 효과음, 이펙트 ,
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
