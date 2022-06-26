using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;
    PlayerMove playerMove; // ����ǲ�ý������ε� �ؾ��� 
    private readonly int hashXmove = Animator.StringToHash("xSpeed");
    private readonly int hashVmove = Animator.StringToHash("zSpeed");
    private readonly int hashRun = Animator.StringToHash("isRun");
    private readonly int hashWalk = Animator.StringToHash("isWalk");
    private readonly int hashCharge = Animator.StringToHash("isCharge");
    private readonly int hashDamage = Animator.StringToHash("isDamage");
    private readonly int hashDead = Animator.StringToHash("isDead");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMove>();

    }


    private void Update()
    {
        anim.SetFloat(hashXmove, playerMove.horizontal); //�����̴� �ݴ�� ���µ� 
        anim.SetFloat(hashVmove, playerMove.vertical); //�����̴ϱ� 90���̸� �ϳ��� -�� �ؾ� 0�̵Ǵϱ�?

    }


    public void SetWalkRun(bool isRun , bool isWalk)
    {
        anim.SetBool(hashWalk, isWalk);
        anim.SetBool(hashRun, isRun);
    }

    public void SetDamage(bool isDamage)
    {
        anim.SetBool(hashDamage, isDamage);
    }

    public void SetDead()
    {

        anim.SetBool(hashDead, true);
    }

    public void SetCharge(bool isCharge)
    {
        anim.SetBool( hashCharge , isCharge);

    }



}
