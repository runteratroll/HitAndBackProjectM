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

    //�׷��� �������� �г��ҋ� ���� ��ũ��Ʈ���̺�� ������

    //������ �ڱⰪ�� ����
    public abstract void FirstState();

    //AttackDelay�� ����������?
    //�ٸ���ũ��Ʈ�� ������ ���� ���������餤


    public abstract void MadM();
   
}
