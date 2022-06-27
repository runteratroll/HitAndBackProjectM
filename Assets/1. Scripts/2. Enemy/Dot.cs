using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public Transform enemy;
    public Transform player;
    //public LineRenderer forwardLine;

    //public Text isForward;

    private void Update()
    {
       // forwardLine.SetPosition(0, enemy.position);
        //forwardLine.SetPosition(1, enemy.forward * 5);

        IsForward();
    }

    public bool isForward; 

    void IsForward()
    {
        float dot = Vector3.Dot(enemy.forward, player.position);
        //적의 위치에 대한 벡터와 플레이어의 진행방향 벡터를 내적.
        //내적의 결과가 양수일 때의 각의 범위와 음수일 때의 각의 범위로 나누어 생각.

      
        if(dot >= 0)
        {
            isForward = true;
        }
        else if(dot  < 0)
        {
            isForward = false;
            //isForwar가 펄스일떄 떄리면 성공
            //플레이어가 떄렸다는 신호를 전달하는 이벤트함수를 만들어서 적재적소에 사용할 수 있지 않을까?
        }
    }
}