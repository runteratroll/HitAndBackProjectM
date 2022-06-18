using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{

    private PlayerSkills playerSkills;
    private LevelSystemAnimated levelSystemAnimated;

    private void Awake()
    {
        playerSkills = new PlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        
    }

    private void Start()
    {
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        //Level Up
        
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case PlayerSkills.SkillType.None:
                break;
            case PlayerSkills.SkillType.Dash:
                break;
            case PlayerSkills.SkillType.ChargedAttack:
                break;
            case PlayerSkills.SkillType.Parring:
                break;
            case PlayerSkills.SkillType.MoveSpeed_1:
                SetMovementSpeed(50);
                break;
            case PlayerSkills.SkillType.MoveSpeed_2:
                SetMovementSpeed(120);
                break;
            case PlayerSkills.SkillType.AttackSpeed_1:
                SetAttackSpeed(30);
                break;
            case PlayerSkills.SkillType.AttackSpeed_2:
                SetAttackSpeed(120);
                break;
            default:
                break;
        }
    }


    
    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }
    public bool CanUseDash()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Dash);
    }

    public bool CanUseChargedAttack()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.ChargedAttack);
    }

    public bool CanUseParring()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Parring);
    }

    public void SetMovementSpeed(float setSpeed)
    {
        //플레이어무브를 담아와서 스피드에다가 곱하는 함수
        //playerSword.SetMoventSpeed(setSpeed)를 할거임
        //그럼 플레이어무브에는 speed말고 speed에 곱해주는 함수를 하나 만들자 
    }

    public void SetAttackSpeed(float setAttackSpeed)
    {

    }
}
