using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargedTime : MonoBehaviour
{
    public float timer;
    bool timerOn;

    PlayerMove playerMove;
    PlayerAnimation playerAnimation;
    public Player player;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {

        if (player.CanUseChargedAttack() == true)
        {
            Timer();
        }
       
    }

    private void OnGUI()
    {
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.normal.textColor = Color.black;
        gUIStyle.fontSize = 50;
        GUI.Label(new Rect(20, 20, 800, 500), timer.ToString(), gUIStyle);
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            timerOn = true; //������ �ִ���  


        }
        
        
        if (context.canceled) //�����°� �ƴ϶� �ð��������� �ڵ����� ������
        {
            if (timer < 2)
            {
                //�׳� ����
            }
            else
            {
                //�̋� �������ý���

                gameObject.SendMessage("ChargeAttack");

            }
            timerOn = false;
            playerAnimation.SetCharge(timerOn);
            gameObject.SendMessage("SetStopMove", false);

        
        }
    }

    void Timer()
    {
        if(timerOn == true)
        {
            Debug.Log("�ð��� ������ ����");
            timer += Time.deltaTime;

        }
        else
        {
            timer = 0;
        }

        if (timer > 0.5f)
        {
            Debug.Log("�����ڼ� �ϼ���");
            playerAnimation.SetCharge(timerOn);
            gameObject.SendMessage("SetStopMove", true);
        }
    }
}
