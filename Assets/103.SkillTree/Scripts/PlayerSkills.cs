/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills {

    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs {
        public SkillType skillType;
    }

    public enum SkillType {
        None,
        Dash,
        ChargedAttack,
        Parring,
        MoveSpeed_1,
        MoveSpeed_2,    
        AttackSpeed_1,
        AttackSpeed_2,
    }

    private List<SkillType> unlockedSkillTypeList;
    private int skillPoints;

    public PlayerSkills() {
        unlockedSkillTypeList = new List<SkillType>();
    }

    public void AddSkillPoint() {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSkillPoints() {
        return skillPoints;
    }

    private void UnlockSkill(SkillType skillType) {
        if (!IsSkillUnlocked(skillType)) {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(SkillType skillType) {
        return unlockedSkillTypeList.Contains(skillType);
    }

    public bool CanUnlock(SkillType skillType) {
        SkillType skillRequirement = GetSkillRequirement(skillType);
        //스킬타이피에 MoveSPeed2를 누를시 1이 전해지곤
        if (skillRequirement != SkillType.None) {
            if (IsSkillUnlocked(skillRequirement)) {
                //MoveSpeed1가 언락이 됬다면 트루를 전함
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    public SkillType GetSkillRequirement(SkillType skillType) {
        switch (skillType) {
        case SkillType.MoveSpeed_2:     return SkillType.MoveSpeed_1;
        case SkillType.AttackSpeed_2:     return SkillType.AttackSpeed_1;
        }
        return SkillType.None;



    }

    public bool TryUnlockSkill(SkillType skillType) {
        if (CanUnlock(skillType)) { //무브스피드2를 언락할수 있어!
            if (skillPoints > 0) { //물론 스킬포인트가 있을때만
                skillPoints--;
                OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
                UnlockSkill(skillType);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

}
