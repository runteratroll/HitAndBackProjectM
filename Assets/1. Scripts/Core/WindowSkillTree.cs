using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class WindowSkillTree : MonoBehaviour
{

    //�÷��̾� ���¸� ���� ǥ���ϴϱ�

    //�÷��̾ GameManage�� ǥ���� �ؾ߁ٴ�.
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
