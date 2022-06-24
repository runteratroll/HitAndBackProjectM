using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    //�̱���

    public static PlayerMove PlayerMove
    {
        get
        {
            if(instance != null)
            {
                return instance.playerMove;
            }
            else
            {
                return null;
            }

        }

        
    }

    //�÷��̾�� ��Ŀ�ø� �ϱ����ؼ� static���� �����Ѱǰ�, �÷��̾�� �ϴ� �Ѹ��̴ϱ�
    //��Ƽ��� ����
    public static Transform Player //�÷��̾���ġ�� ���ֱ��ϴ� ��찡 �����ϱ�
    {
        get
        {
            if(instance != null)
            {
                return instance.player;
            }
            else
            {
                return null;
            }
        }
    }

    //�̱�����ü�� ��Ŀ�ø��ϱ����ؼ� �ʿ��ߴ��ž�
    public Transform player;
    public PlayerMove playerMove;
    public GameObject bloodParticlePrefab;
    public GameObject attackParticlPrefab;


    private void Awake()
    {
        

        SoundManagerM.Initialize();

        if(instance != null)
        {

        }

        instance = this;
    }


    private void OnEnable()
    {
        if (PoolManager.pool.Count == 0)
        {
            Debug.Log("Ǯ�Ŵ���");
            PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, transform, 10);
            PoolManager.CreatePool<AttackParticle>(attackParticlPrefab, transform, 10);
        }
    }



}
