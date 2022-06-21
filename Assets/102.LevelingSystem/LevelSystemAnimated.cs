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
using CodeMonkey.Utils;

public class LevelSystemAnimated {

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private LevelSystem levelSystem;
    private bool isAnimating;
    private float updateTimer;
    private float updateTimerMax;

    private int level;
    private int experience;

    public LevelSystemAnimated(LevelSystem levelSystem) {
        SetLevelSystem(levelSystem);
        updateTimerMax = .010f;

        FunctionUpdater.Create(() => Update());
    }

    public void SetLevelSystem(LevelSystem levelSystem) {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience();

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged; //경험치가 바뀔때 실행시키는 함수를 넣을떄
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged; // 레벨이 바뀔떄 실행시키는 함수를 넣을때
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e) {
        isAnimating = true;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e) {
        isAnimating = true;
    }

    private void Update() {
        if (isAnimating) { //특정함수가 발동했을때만 업데이트 시간이 실행되게
            // Check if its time to update
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax) {
                // Time to update
                updateTimer -= updateTimerMax;
                UpdateAddExperience();
            }
        }
    }

    private void UpdateAddExperience() {
        if (level < levelSystem.GetLevelNumber()) {
            // Local level under target level
            AddExperience(); //여기에서 실행시킬때 
        } else {
            //애니메이팅 끝나고 레벨이 같을때인가
            // Local level equals the target level
            if (experience < levelSystem.GetExperience()) {
                AddExperience();
            } else {
                isAnimating = false;
            }
        }
    }

    private void AddExperience() {
        experience++;
        if (experience >= levelSystem.GetExperienceToNextLevel(level)) {
            level++;
            experience = 0;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty); //함수실행시키는 건가?
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty); //건너뛰면 또 애니메이팅함수가 실행되니까 OnLevel_System기능을 넣어듯이
        //이걸 실행시키면 isAnimating이 트루됨
    }

    public int GetLevelNumber() {
        return level;
    }
    
    public float GetExperienceNormalized() {
        if (levelSystem.IsMaxLevel(level)) {
            return 1f;
        } else {
            return (float)experience / levelSystem.GetExperienceToNextLevel(level);
        }
    }

}
