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

public class LevelSystem {

    public event EventHandler OnExperienceChanged;
    //Evnet는 커플링을 줄여주는구나 외부와의 커플링을 줄이기 위한 도구중 ㅎㄴ
    public event EventHandler OnLevelChanged;
    //Evnet의 상위호환 UniRx를 연습하자 커플링 줄이기 위함

    private static readonly int[] experiencePerLevel = new[] { 100, 120, 140, 160, 180, 200, 220, 250, 300, 400 };

    private int level;
    private int experience;

    public LevelSystem() {
        level = 0;
        experience = 0;
    }
    // MonoBeHavior을 상속받지 않고 초기화 하는 함수 넣으면은 레퍼런스를 받지않아도 함수의 
    //내용을 실행할 수 있네 테스트용도 있고
    public void AddExperience(int amount) {
        if (!IsMaxLevel()) {
            experience += amount;
            while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level)) {
                // Enough experience to level up
                //확실히 비용이 넘을떄 빼면은 되는구나 레벨만 올리면 되지 나머지는 로직이
                //똑같아야 하니까 비용이 넘을땐 뺴기를 습득
                experience -= GetExperienceToNextLevel(level);
                //다음으로가는 레벨만큼 experience를 빼준다.
                //기능하는 내용이 똑같으면 역할만 달라질뿐 로직은 같네, 이걸 이용해야겠다.
                level++;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty); 
                //함수가 들어있으면 뺴주기 위해서? 그런거겠지? 왜?
                //아하 레벨갑과 경험치를 바꾸고 업데이트하기위해서 쓰네
                //저위는 레벨바꾸는 함수네
                //재귀함수 같은거ㅇ닐까
                //프로그래밍은 시스템, 서비스를 개발하기위해 필요한 모듈 필요한 함수들 
                //주고받으면서 문제를 해결해가면서 서비스를 
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber() {
        return level;
    }

    public float GetExperienceNormalized() {
        if (IsMaxLevel()) {
            return 1f;
        } else {
            return (float)experience / GetExperienceToNextLevel(level);
        }
    }
    

    public int GetExperience() {
        return experience;
    }

    public int GetExperienceToNextLevel(int level) {
        if (level < experiencePerLevel.Length) {
            return experiencePerLevel[level];
        } else {
            // Level Invalid
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
    }

    public bool IsMaxLevel() {
        return IsMaxLevel(level);
    }

    public bool IsMaxLevel(int level) {
        return level == experiencePerLevel.Length - 1;
    }

}
