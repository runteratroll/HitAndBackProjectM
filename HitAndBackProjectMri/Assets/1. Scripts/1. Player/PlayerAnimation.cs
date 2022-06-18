using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;
    PlayerMove playerMove; // 뉴인풋시스템으로도 해야지 
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
        anim.SetFloat(hashXmove, playerMove.horizontal); //내적이니 반대로 쓰는데 
        anim.SetFloat(hashVmove, playerMove.vertical); //내적이니까 90도이면 하나는 -로 해야 0이되니까?

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
