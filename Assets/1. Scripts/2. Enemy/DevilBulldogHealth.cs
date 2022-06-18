using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogHealth : Health
{
    SkinnedMeshRenderer skinnedMeshRenderer;

    public HealthBar healthBar;
    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInParent<SkinnedMeshRenderer>();
       // healthBar.SetMaxHealth(maxHp);

    }
    protected override void OnDie()
    {
        Debug.Log("죽음");
    }

    public override void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power) //왜 protected는 안될까
    {
        Debug.Log("데미지 받음");
        ColorSet();
        base.HealthDown(damage, hitPoint, normal, power);
        healthBar.SetHealth((float)currentHp / (float)maxHp);


    }

    IEnumerator ColorSet()
    {
        Debug.Log("실행되니?");
        for (int i = 0; i < 4; i++)
        {
            skinnedMeshRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            skinnedMeshRenderer.material.color = Color.red;
        }
       
    }

}
