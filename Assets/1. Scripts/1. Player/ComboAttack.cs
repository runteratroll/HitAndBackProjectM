using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Animator playerANim;
    bool comboPossible;
    public int comboStep;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("���� ���ʸ��콺Ŭ��");
            Attack();
        }
    }

    public void AttackSound()
    {
        if (comboStep == 1)
        {
            //SoundManagerM.PlaySound();
            SoundManagerM.PlaySound(SoundManagerM.Sound.PlayerAttackA);
        }
        else if (comboStep == 2)
        {
            SoundManagerM.PlaySound(SoundManagerM.Sound.PlayerAttackB);
        }
        else if(comboStep == 3)
        {
            SoundManagerM.PlaySound(SoundManagerM.Sound.PlayerAttackC);
        }
    }

    public void Attack()
    {
        if(comboStep == 0)
        {
           
            playerANim.Play("AttackA");//�۽�Ʈ ����
            
            comboStep = 1;
            return;
        }

        if(comboStep != 0)
        {
            if(comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }

    }

    public void ComboPossible()
    {
        Debug.Log("�޺��Ľú�");
        comboPossible = true;

    }

    public void Combo()
    {
        Debug.Log("�޺�");
        if(comboStep == 2)
        {
            //SoundManager.instance.PlaySE("HammerM");
            playerANim.Play("AttackB");
          

        }
        if (comboStep == 3)
        {
            //SoundManager.instance.PlaySE("HammerR");
            playerANim.Play("AttackC");
        }
         
     
    }

    public void Comboreset()
    {
        Debug.Log("�޺�����");
        comboPossible = false;
        comboStep = 0;
    }
}
