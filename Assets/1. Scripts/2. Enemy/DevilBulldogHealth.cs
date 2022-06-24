using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogHealth : Health
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    public Player player;
    public HealthBar healthBar;
    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInParent<SkinnedMeshRenderer>();
       // healthBar.SetMaxHealth(maxHp);

    }
    protected override void OnDie()
    {
        Debug.Log("����");

        SoundManagerM.PlaySound(SoundManagerM.Sound.EnemyDie);
    }

    public override void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power) //�� protected�� �ȵɱ�
    {
        Debug.Log("������ ����");

        SoundManagerM.PlaySound(SoundManagerM.Sound.EnemyHit);
        ColorSet();
        player.PlayerSkillsAddUp();
        base.HealthDown(damage, hitPoint, normal, power);
        healthBar.SetHealth((float)currentHp / (float)maxHp);


    }

    IEnumerator ColorSet()
    {
        Debug.Log("����Ǵ�?");
        for (int i = 0; i < 4; i++)
        {
            skinnedMeshRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            skinnedMeshRenderer.material.color = Color.red;
        }
       
    }

}
