using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class WindowSkillTree : MonoBehaviour
{

    //플레이어 상태를 많이 표현하니까

    //플레이어를 GameManage에 표현을 해야곘다.
    public PlayerMove playerMove;


    private void Start()
    {
        transform.Find("DashBtn").GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static("Left Shift");
        transform.Find("DashBtn").GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

        transform.Find("ChargedAttackBtn").GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static("Press and 1.5f hold mouse AfterAttacking");
        transform.Find("ChargedAttackBtn").GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

        transform.Find("MoveSpeed1Btn").GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static("Speed 1.2");
        transform.Find("MoveSpeed1Btn").GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

        transform.Find("MoveSpeed2Btn").GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static("Speed 1.7");
        transform.Find("MoveSpeed2Btn").GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
    }
}
