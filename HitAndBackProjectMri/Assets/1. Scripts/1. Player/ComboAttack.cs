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
            Attack();
        }
    }
    public void Attack()
    {
        if(comboStep == 0)
        {
            playerANim.Play("AttackA");//퍼스트 어택
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
        comboPossible = true;

    }

    public void Combo()
    {
        if(comboStep == 2)
        {
            playerANim.Play("AttackB");

           
        }
        if (comboStep == 3)
            playerANim.Play("AttackC");
    }

    public void Comboreset()
    {
        comboPossible = false;
        comboStep = 0;
    }
}
