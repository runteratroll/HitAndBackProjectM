using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Mad : MonoBehaviour, MadState
{

    public NavMeshAgent Agent;
    public EnenmyState EnemyScriptableObject;
    public EnenmyState EnemyMadStateScriptAble;
    public ParticleSystem particle;
    private void Start()
    {
        FirstState();
    }

    //그러면 원래값과 분노할떄 값을 스크립트에이블로 만들자

    //원래의 자기값을 복사
    public abstract void FirstState();

    //AttackDelay로 가져오려면?
    //다른스크립트의 변수를 쉽게 가져오려면ㄴ


    public abstract void MadM();
   
}
