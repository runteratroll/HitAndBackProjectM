using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBulldogHealth : Health
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    public Player player;
    public HealthBar healthBar;
    public GameObject Devil;
    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInParent<SkinnedMeshRenderer>();
        // healthBar.SetMaxHealth(maxHp);

    }
    protected override void OnDie()
    {
        Debug.Log("죽음");

        SoundManagerM.PlaySound(SoundManagerM.Sound.EnemyDie);
        Devil.SetActive(false);
    }

    bool isMad;
    public override void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power) //왜 protected는 안될까
    {
        Debug.Log("데미지 받음");


        if (currentHp <= maxHp / 2)
        {

            if (isMad == false)
            {

                MadState madState = gameObject.GetComponentInParent<MadState>();

                if (madState != null)
                {
                    isMad = true;
                    Debug.Log("적이 화났습니다.");
                    madState.MadM();
                }
            }
        }

            SoundManagerM.PlaySound(SoundManagerM.Sound.EnemyHit);
            ColorSet();
            player.PlayerSkillsAddUp();

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
