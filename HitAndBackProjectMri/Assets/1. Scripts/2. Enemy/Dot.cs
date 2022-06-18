using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public Transform enemy;
    public Transform player;
    public LineRenderer forwardLine;

    //public Text isForward;

    private void Update()
    {
        forwardLine.SetPosition(0, enemy.position);
        forwardLine.SetPosition(1, enemy.forward * 5);

        IsForward();
    }

    public bool isForward; 

    void IsForward()
    {
        float dot = Vector3.Dot(enemy.forward, player.position);
        //���� ��ġ�� ���� ���Ϳ� �÷��̾��� ������� ���͸� ����.
        //������ ����� ����� ���� ���� ������ ������ ���� ���� ������ ������ ����.

      
        if(dot >= 0)
        {
            isForward = true;
        }
        else if(dot  < 0)
        {
            isForward = false;
        }
    }
}