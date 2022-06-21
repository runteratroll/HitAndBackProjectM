/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

    //[SerializeField] private ExperienceBar experienceBar;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;

    //private PlayerSword playerSword;
    private PlayerMove playerMove;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;
    private PlayerSkills playerSkills;
    private LevelWindow levelWindow;

    private void Awake() {
        // playerSword = GetComponent<PlayerSword>();
        playerMove = GetComponentInParent<PlayerMove>();
         levelSystem = new LevelSystem();
        levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        playerSkills = new PlayerSkills();
        levelWindow = new LevelWindow(); //이거면 이제 설정해주겠지?
        levelWindow.SetLevelSystem(levelSystem);
        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
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
                SetMovementSpeed(1.2f); //1.2배
                break;
            case PlayerSkills.SkillType.MoveSpeed_2:
                SetMovementSpeed(1.7f); //1.7배
                break;
            case PlayerSkills.SkillType.AttackSpeed_1:
                SetAttackSpeed(30); //공격애니메이션을 빠르게
                break;
            case PlayerSkills.SkillType.AttackSpeed_2:
                SetAttackSpeed(120);
                break;
            default:
                break;
        }
    }


    private void Start() {
        //playerSword.OnEnemyKilled += PlayerSword_OnEnemyKilled;
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
        levelText.SetText((levelSystemAnimated.GetLevelNumber() + 1).ToString());
    }

    public PlayerSkills GetPlayerSkills() {
        return playerSkills;
    }


    public void PlayerSkillsAddUp()
    {
        playerSkills.AddSkillPoint();
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e) {
        // Level Up
        levelText.SetText((levelSystemAnimated.GetLevelNumber() + 1).ToString());
        //levelWindow.SetLevelNumber((levelSystemAnimated.GetLevelNumber() + 1));
        //SetHealthAmountMax(8 + levelSystemAnimated.GetLevelNumber());
        //playerSkills.AddSkillPoint();
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e) {
        //experienceBar.SetSize(levelSystemAnimated.GetExperienceNormalized());
        //levelWindow.SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }

    private void PlayerSword_OnEnemyKilled(object sender, System.EventArgs e) {
        levelSystem.AddExperience(30);
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
        playerMove.SetMovementSpeed(setSpeed);
        //그럼 플레이어무브에는 speed말고 speed에 곱해주는 함수를 하나 만들자 
    }

    public void SetAttackSpeed(float setAttackSpeed)
    {

    }

}
