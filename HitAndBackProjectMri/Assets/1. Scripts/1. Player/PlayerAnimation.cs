using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;
    PlayerMove playerMove; // ����ǲ�ý������ε� �ؾ��� 
    private readonly int hashXmove = Animator.StringToHash("hSpd");
    private readonly int hashVmove = Animator.StringToHash("vSpd");
    private readonly int hashRun = Animator.StringToHash("isRun");
    private readonly int hashCharge = Animator.StringToHash("isCharge");

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


    public void SetRun(bool isRun)
    {

        anim.SetBool(hashRun, isRun);
    }
    public void SetCharge(bool isCharge)
    {
        anim.SetBool( hashCharge , isCharge);

    }



}
