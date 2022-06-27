using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargedTime : MonoBehaviour
{
    public float timer;
    bool timerOn;

    PlayerAnimation playerAnimation;
    public Player player;

    private void Awake()
    {

        playerAnimation = GetComponent<PlayerAnimation>();
    }
    bool isChargeAttack;

    private void Update()
    {

        if (player.CanUseChargedAttack() == true)
        {
            Timer();
        }

        if(isChargeAttack == true)
        {
            gameObject.SendMessage("ChargeAttack");
            isChargeAttack = false;

        }
       
    }

    private void OnGUI()
    {
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.normal.textColor = Color.black;
        gUIStyle.fontSize = 50;
        GUI.Label(new Rect(20, 20, 800, 500), "ChargedTime " + timer.ToString(), gUIStyle);
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Debug.Log("Attack버튼");
            timerOn = true; //누르고 있는중  


        }
        
        
        if (context.canceled) //누르는게 아니라 시간이지나면 자동으로 때지게
        {
            if (timer < 1.5f)
            {
                //그냥 공격
            }
            else
            {
                //이떄 차지어택실행
                isChargeAttack = true;

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
            Debug.Log("시간이 오르고 있음");
            timer += Time.deltaTime;

        }
        else
        {
            timer = 0;
        }

        if (timer > 0.5f)
        {
            Debug.Log("차지자세 하세요");
            playerAnimation.SetCharge(timerOn);
            gameObject.SendMessage("SetStopMove", true);
        }
    }

    public LayerMask isTarget;
    public int ChargedDamage;
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & isTarget) > 0)
        {
            Debug.Log("Target이랑 부딪힘");
            IDamageble hp = other.gameObject.GetComponent<IDamageble>();
            MadState mad = other.gameObject.GetComponentInParent<MadState>();

            if (mad != null)
            {
                mad.FirstState();
            }
     

            if(hp != null)
            {
                
                hp.HealthDown(ChargedDamage, other.transform.position, other.transform.forward, 0);
            }
        }
    }
}
