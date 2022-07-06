using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Quest
{

    //이걸이제 스크립터블 오브젝트로 하는거지?
    //스크립터블이 끝나면은 이제 Editor로 관리하는거고
    //로직은 수학을 공부하면서해결하고
    //그래픽은 개념을 이제 배우고
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal goal;
    //아보여주는 창 
    //로직창 
    //데이터창 흐흐
    //이걸이제 보여주는 창과 
    //로직창을 나눠가지고 
    //title을 넣고
    //로직은 이걸 가지고 이걸 한다면은 올라가고 다하면은 불을 반환하는 방식으로 한다.
}
