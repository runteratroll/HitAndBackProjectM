using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleT<GameManager> 
{
    private static GameManager instance = null;
    //싱글톤

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

    //플레이어와 디커플링 하기위해서 static으로 선언한건가, 플레이어는 일단 한명이니까
    //멀티라면 몰라도
    public static Transform Player //플레이어위치를 자주구하는 경우가 있으니까
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

    //싱글톤자체도 디커플링하기위해서 필요했던거야
    public Transform player;
    public PlayerMove playerMove;
    public GameObject bloodParticlePrefab;
    public GameObject attackParticlPrefab;


    private void Awake()
    {
        



        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            Debug.Log("풀매니저");
            PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, transform, 10);
            PoolManager.CreatePool<AttackParticle>(attackParticlPrefab, transform, 10);
            SoundManagerM.Initialize();


        }
        else if(this != instance)
        {
            Destroy(this.gameObject);
        }

    }




}
